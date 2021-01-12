using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Localizer.Common;
using Localizer.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Localizer.Domain.Infrastructure
{
	public class WriteTimeInterceptor : SaveChangesInterceptor
	{
		private readonly IDateTimeOffsetProvider _provider;

		public WriteTimeInterceptor(IDateTimeOffsetProvider provider)
		{
			_provider = provider;
		}

		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
			InterceptionResult<int> result)
		{
			WriteDates(eventData.Context.ChangeTracker);

			return result;
		}

		private void WriteDates(ChangeTracker changeTracker)
		{
			foreach (var entry in changeTracker.Entries<ICreatedAt>().Where(entry => entry.State == EntityState.Added))
				entry.Entity.CreatedAt = _provider.Now;

			foreach (var entry in changeTracker.Entries<IUpdatedAt>()
				.Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified))
				entry.Entity.UpdatedAt = _provider.Now;
		}

		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
			InterceptionResult<int> result,
			CancellationToken cancellationToken = new())
		{
			WriteDates(eventData.Context.ChangeTracker);
			return ValueTask.FromResult(result);
		}
	}
}