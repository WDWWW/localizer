using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Localizer.Api.Infrastructure.Filters
{
	public class HealthCheckStartupFilter : IStartupFilter
	{
		private readonly HealthCheckService _service;

		public HealthCheckStartupFilter(HealthCheckService service)
		{
			_service = service;
		}

		public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
		{
			var report = _service.CheckHealthAsync().Result;

			if (report.Status == HealthStatus.Unhealthy)
				throw new LocalizerUnhealthyException(report);

			return next;
		}
		
		private class LocalizerUnhealthyException : Exception
		{
			public LocalizerUnhealthyException(HealthReport report) : base(CreateMessage(report))
			{
			}

			private static string CreateMessage(HealthReport report)
			{
				var unhealthies = report.Entries
					.Where(entry => entry.Value.Status == HealthStatus.Unhealthy)
					.Select(pair => $"{pair.Key}(description: {pair.Value.Description})");

				return "Localizer can not start with unhealthy infrastructures. " + string.Join(", ", unhealthies);
			}
		}
	}
}