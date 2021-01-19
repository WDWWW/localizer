using System;
using System.Threading;
using System.Threading.Tasks;
using Localizer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Localizer.Api.Infrastructure.HealthChecks
{
	public class DbContextHealthCheck : IHealthCheck
	{
		private readonly LocalizerDb _db;

		public DbContextHealthCheck(LocalizerDb db)
		{
			_db = db;
		}

		public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
			CancellationToken cancellationToken = new())
		{
			try
			{
				await _db.Database.OpenConnectionAsync(cancellationToken);
			}
			catch (Exception e)
			{
				return HealthCheckResult.Unhealthy();
			}
			
			return HealthCheckResult.Healthy();
		}
	}
}