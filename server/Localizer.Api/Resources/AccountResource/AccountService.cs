// unset

using System.Threading.Tasks;

namespace Localizer.Api.Resources.AccountResource
{
	public class AccountService
	{
		private readonly AccountRepository _repository;

		public AccountService(AccountRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> EmailExistsAsync(string email)
		{
			return await _repository.EmailExistsAsync(email);
		}
	}
}