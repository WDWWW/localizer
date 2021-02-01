// unset

using System.Threading.Tasks;
using Localizer.Domain;
using Localizer.Domain.Entities;
using Localizer.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Localizer.Api.Resources.AccountResource
{
	public class AccessTokenHistoryRepository : RepositoryBase<AccountAccessTokenHistory, int>
	{
		public AccessTokenHistoryRepository(LocalizerDb db) : base(db)
		{
		}

		public async Task<bool> RefreshHistoryExistsAsync(string fromToken)
		{
			return await Query.AnyAsync(history => history.FromToken == fromToken);
		}
	}
}