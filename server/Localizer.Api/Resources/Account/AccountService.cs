// unset

using System.Threading.Tasks;
using AutoMapper;
using Localizer.Api.Resources.Account.Models;

namespace Localizer.Api.Resources.Account
{
	public class AccountService
	{
		private readonly AccountRepository _repository;

		private readonly IMapper _mapper;

		public AccountService(AccountRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<int> CreateAccountAsync(CreateAccountRequest request)
		{
			var account = _mapper.Map<Domain.Entities.Account>(request);
			await _repository.AddAsync(account);
			return account.Id;
		}
	}
}