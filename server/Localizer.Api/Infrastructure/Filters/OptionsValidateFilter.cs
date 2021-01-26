using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Localizer.Api.Infrastructure.Filters
{
	public class OptionsValidateFilter<TOptions> : IStartupFilter where TOptions : class
	{
		private readonly IOptions<TOptions> _options;

		public OptionsValidateFilter(IOptions<TOptions> options)
		{
			_options = options;
		}

		public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
		{
			_ = _options.Value; // Trigger for validating options.
			return next;
		}
	}
}