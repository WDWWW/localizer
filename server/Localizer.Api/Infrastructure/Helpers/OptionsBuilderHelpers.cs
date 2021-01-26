using Localizer.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Localizer.Api.Infrastructure.Helpers
{
	public static class OptionsBuilderHelpers
	{
		public static OptionsBuilder<TOptions> ValidateOnStartupTime<TOptions>(this OptionsBuilder<TOptions> builder)
			where TOptions : class
		{
			builder.Services.AddTransient<IStartupFilter, OptionsValidateFilter<TOptions>>();
			return builder;
		}
	}
}