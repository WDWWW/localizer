using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Localizer.Domain;
using Microsoft.EntityFrameworkCore;
using Wd3w.AspNetCore.EasyTesting;

namespace Localizer.Test.Utils
{
	public static class EasyTestingHelper
	{
		public static SystemUnderTest VerifyEntityExistsByCondition<TEntity>(this SystemUnderTest sut, Expression<Func<TEntity, bool>> condition) where TEntity : class
		{
			sut.UsingService((LocalizerDb db) => db.Set<TEntity>().Any(condition).Should().BeTrue());
			return sut;
		}

		public static SystemUnderTest VerifyEntity<TEntity>(this SystemUnderTest sut, Action<DbSet<TEntity>> verifier) where TEntity : class
		{
			sut.UsingService((LocalizerDb db) => verifier(db.Set<TEntity>()));
			return sut;
		}

		public static TEntity FindEntity<TEntity>(this SystemUnderTest sut, object key) where TEntity : class
		{
			TEntity? entity = default;
			sut.UsingService((LocalizerDb db) => entity = db.Set<TEntity>().Find(key));
			return entity ?? throw new KeyNotFoundException($"Couldn't find any entity(Type: {typeof(TEntity).Name}) for Key({key}).");
		}
		
	}
}