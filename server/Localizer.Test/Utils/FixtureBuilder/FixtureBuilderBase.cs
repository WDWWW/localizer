using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Localizer.Test.Utils.FixtureBuilder
{
	public abstract class FixtureBuilderBase<TDbContext> where TDbContext : DbContext
	{
		protected TDbContext DbContext { get; set; }

		private readonly IDictionary<Type, object> _entityFactory = new Dictionary<Type, object>();

		private readonly ICollection<object> _defaultEntities = new List<object>();


		protected FixtureBuilderBase(TDbContext dbContext)
		{
			DbContext = dbContext;
			ConfigureFixture();
			DbContext.AddRange(_defaultEntities);
			DbContext.SaveChanges();
		}

		protected abstract void ConfigureFixture();

		protected void HasData<TEntity>(TEntity entity) where TEntity : class
		{
			_defaultEntities.Add(entity);
		}

		protected void Configure<TEntity>(Func<TEntity>? entityFactory = default) where TEntity : class, new()
		{
			_entityFactory[typeof(Func<TEntity>)] = entityFactory ?? (() => new TEntity());
		}

		public FixtureBuilderBase<TDbContext> Add<TEntity>() where TEntity : class
		{
			Add<TEntity>(null);
			return this;
		}
		
		public FixtureBuilderBase<TDbContext> Add<TEntity>(out TEntity entity, Action<TEntity>? updator = default)
			where TEntity : class, new()
		{
			entity = Add(updator);
			return this;
		}

		public TEntity Add<TEntity>(Action<TEntity>? updator) where TEntity : class
		{
			var entity = ((Func<TEntity>) _entityFactory[typeof(Func<TEntity>)]).Invoke();
			updator?.Invoke(entity);
			DbContext.Add(entity);
			DbContext.SaveChanges();
			return entity;
		}

		public FixtureBuilderBase<TDbContext> Update<TEntity>(TEntity entity, Action<TEntity> update) where TEntity : class
		{
			var entry = DbContext.Attach(entity);
			update(entry.Entity);
			DbContext.SaveChanges();
			return this;
		}

		public FixtureBuilderBase<TDbContext> Remove(object entity)
		{
			DbContext.Remove(entity);
			DbContext.SaveChanges();
			return this;
		}
	}
}