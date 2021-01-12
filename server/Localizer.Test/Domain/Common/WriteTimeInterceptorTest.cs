using System;
using System.Threading.Tasks;
using FluentAssertions;
using Localizer.Common;
using Localizer.Domain;
using Localizer.Domain.Entities;
using Localizer.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Localizer.Test.Domain.Common
{
	public class WriteTimeInterceptorTest : IDisposable
	{
		private readonly LocalizerDb _localizerDb;

		public WriteTimeInterceptorTest()
		{
			var options = new DbContextOptionsBuilder<LocalizerDb>().UseInMemoryDatabase(Guid.NewGuid().ToString())
				.AddInterceptors(new WriteTimeInterceptor(new UtcDateTimeOffsetProvider()))
				.Options;
			_localizerDb = new LocalizerDb(options);
		}

		public void Dispose()
		{
			_localizerDb.Dispose();
		}

		[Fact]
		public void CreatedAtTest()
		{
			// Given
			var account = CreateDummyAccount();
			_localizerDb.Accounts.Add(account);

			// When 
			_localizerDb.SaveChanges();

			// Then
			account.CreatedAt.Should().NotBe(default);
		}

		[Fact]
		public async Task UpdatedAtTest()
		{
			// Given
			var account = CreateDummyAccount();
			await _localizerDb.Accounts.AddAsync(account);
			await _localizerDb.SaveChangesAsync();

			// When
			await Task.Delay(100);
			account.Name = "update name";
			await _localizerDb.SaveChangesAsync();

			// Then
			account.CreatedAt.Should().NotBe(default);
			account.UpdatedAt.Should().NotBe(default).And.BeAfter(account.CreatedAt);
		}

		private static Account CreateDummyAccount() =>
			new() {Email = "test@email.com", Name = "user name", PasswordHash = "some_hash"};
	}
}