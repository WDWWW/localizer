using System;
using System.Linq;
using System.Text;
using AutoMapper;
using Localizer.Api.Infrastructure;
using Localizer.Api.Infrastructure.HealthChecks;
using Localizer.Api.Repositories;
using Localizer.Api.Services;
using Localizer.Common;
using Localizer.Domain;
using Localizer.Domain.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Localizer.Api
{
	public class Startup
	{
		private readonly IWebHostEnvironment _environment;

		private readonly LocalizerSettings _settings;

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			_environment = environment;
			Configuration = configuration;
			_settings = configuration.GetSection(nameof(LocalizerSettings)).Get<LocalizerSettings>();
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddDistributedMemoryCache();
			services.AddSingleton<IDateTimeOffsetProvider, UtcDateTimeOffsetProvider>();
			services.AddOptions<LocalizerSettings>()
				.Bind(Configuration.GetSection("LocalizerSettings"))
				.ValidateDataAnnotations()
				.Validate((LocalizerSettings _, HealthCheckService healthCheck) => healthCheck.CheckHealthAsync().Result.Status == HealthStatus.Healthy);

			services.AddScoped(provider => provider.GetRequiredService<IOptionsSnapshot<LocalizerSettings>>().Value);
			services.AddScoped<EmailService>();
			services.AddScoped<AccountRepository>();
			services.AddScoped<AuthenticationService>();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new()
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = _settings.Authentication.ServiceName,
						ValidAudience = _settings.Authentication.ServiceName,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Authentication.TokenSigningKey)),
					};
				});
			
			services.AddDbContext<LocalizerDb>();
			services.AddScoped(provider =>
			{
				var settings = provider.GetRequiredService<LocalizerSettings>();
				return new DbContextOptionsBuilder<LocalizerDb>()
					.UseLazyLoadingProxies()
					.UseNpgsql(settings.DatabaseConnection)
					.AddInterceptors(new WriteTimeInterceptor(provider.GetRequiredService<IDateTimeOffsetProvider>()))
					.Options;
			});

			services.AddAutoMapper(config =>
			{

			}, Enumerable.Empty<Type>(), ServiceLifetime.Singleton);

			services.AddScoped<DbContextHealthCheck>();
			services.AddScoped<LocalizerMailServerHealthCheck>();
			services.AddScoped<DistributedCacheCheck>();
			services.AddHealthChecks()
				.AddCheck<DbContextHealthCheck>("database")
				.AddCheck<LocalizerMailServerHealthCheck>("smtp")
				.AddCheck<DistributedCacheCheck>("cache");
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseHttpsRedirection();

			app.UseRouting();


			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}