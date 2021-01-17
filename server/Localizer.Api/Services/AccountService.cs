// unset

using Localizer.Api.Repositories;

namespace Localizer.Api.Services
{
	public class AccountService
	{
		private readonly AccountRepository _repository;

		public AccountService(AccountRepository repository)
		{
			_repository = repository;
		}
	}
}