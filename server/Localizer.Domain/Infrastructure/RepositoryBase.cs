using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Localizer.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Localizer.Domain.Infrastructure
{
	public abstract class RepositoryBase<TEntity, TKey> : IDisposable, IAsyncDisposable where TEntity : class, IEntityKey<TKey> where TKey : struct
	{
		private readonly LocalizerDb _db;

		protected RepositoryBase(LocalizerDb db)
		{
			_db = db;
		}

		protected IQueryable<TEntity> Query => _db.Set<TEntity>().AsQueryable();

		public Task<bool> AnyIdAsync(TKey key) => Query.AnyAsync(entity => entity.Id.Equals(key));

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			var entry = await _db.Set<TEntity>().AddAsync(entity);
			await _db.SaveChangesAsync();
			return entry.Entity;
		}

		public int SaveChanges() => _db.SaveChanges();

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) => _db.SaveChangesAsync(cancellationToken);
		public async Task UpdateAsync(TKey key, Action<TEntity> updater)
		{
			var entity = await _db.FindAsync<TEntity>(key);
			updater(entity);
			await _db.SaveChangesAsync();
		} 

		public async Task RemoveAsync(TKey key)
		{
			var entity = await _db.FindAsync<TEntity>(key);
			_db.Remove(entity);
			await _db.SaveChangesAsync();
		}
		
		public TEntity Find(TKey key) => _db.Find<TEntity>(key);

		public ValueTask<TEntity> FindAsync(TKey key) => _db.FindAsync<TEntity>(key);

		public void Dispose()
		{
			_db?.Dispose();
		}

		public ValueTask DisposeAsync() => _db.DisposeAsync();
	}
}