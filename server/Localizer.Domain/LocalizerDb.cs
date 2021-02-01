using Localizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Localizer.Domain
{
	public class LocalizerDb : DbContext
	{
		public LocalizerDb(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Account> Accounts { get; set; }

		public DbSet<AccountAccessTokenHistory> AccessTokenHistories { get; set; }
	}
}