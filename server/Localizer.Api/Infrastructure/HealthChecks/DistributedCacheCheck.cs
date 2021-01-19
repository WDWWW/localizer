using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Localizer.Api.Infrastructure.HealthChecks
{
	public class DistributedCacheCheck : IHealthCheck
	{
		private readonly IDistributedCache _cache;

		public DistributedCacheCheck(IDistributedCache cache)
		{
			_cache = cache;
		}

		public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken ctx = new())
		{
			try
			{
				var key = Guid.NewGuid().ToString();
				await _cache.SetStringAsync(key, "HEALTH_CHECK", token: ctx, options: new () { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1)});
				var result = await _cache.GetStringAsync(key, token: ctx);
				return result == "HEALTH_CHECK"
					? HealthCheckResult.Healthy()
					: HealthCheckResult.Degraded();
			}
			catch (Exception e)
			{
				return HealthCheckResult.Unhealthy(exception: e);
			}
		}
	}
}