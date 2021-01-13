// unset

using System.Threading.Tasks;
using Localizer.Domain;
using Localizer.Domain.Entities;
using Localizer.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Localizer.Api.Repositories
{
	public class AccountRepository : RepositoryBase<Account, int>
	{
		public AccountRepository(LocalizerDb db) : base(db)
		{
		}

		public async Task<Account> GetAccountByEmailAsync(string email)
		{
			return await Query.FirstAsync(account => account.Email == email);
		}
	}
}