using System.Text;
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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Localizer.Api
{
	public class Startup
	{
		private readonly IWebHostEnvironment _environment;

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			_environment = environment;
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddSingleton<IDateTimeOffsetProvider, UtcDateTimeOffsetProvider>();
			services.AddOptions<LocalizerSettings>()
				.Bind(Configuration.GetSection("LocalizerSettings"))
				.ValidateDataAnnotations()
				.Validate((LocalizerSettings _, LocalizerDb db) =>
				{
					db.Database.OpenConnection();
					return true;
				});
			services.AddScoped(provider => provider.GetRequiredService<IOptionsSnapshot<LocalizerSettings>>().Value);
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
						ValidIssuer = "localizer",
						ValidAudience = "localizer",
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("localizer_demo_secret_key")),
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