// unset

using System.Threading.Tasks;
using Localizer.Domain;
using Localizer.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Localizer.Api.Resources.AccountResource
{
	public class AccountRepository : RepositoryBase<Domain.Entities.Account, int>
	{
		public AccountRepository(LocalizerDb db) : base(db)
		{
		}

		public async Task<Domain.Entities.Account> GetAccountByEmailAsync(string email)
		{
			return await Query.FirstAsync(account => account.Email == email);
		}

		public async Task<bool> EmailExistsAsync(string email)
		{
			return await Query.AnyAsync(account => account.Email == email);
		}
	}
}