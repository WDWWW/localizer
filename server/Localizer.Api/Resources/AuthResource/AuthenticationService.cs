// unset

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Localizer.Api.Resources.AccountResource;
using Localizer.Api.Resources.AuthResource.Models;
using Localizer.Common;
using Localizer.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Localizer.Api.Resources.AuthResource
{
	public class AuthenticationService
	{
		private readonly IMapper _mapper;

		private readonly IDateTimeOffsetProvider _provider;

		private readonly AccountRepository _repository;

		private readonly AuthenticationSecretSettings _secretSettings;

		public AuthenticationService(IDateTimeOffsetProvider provider,
			IMapper mapper,
			AccountRepository repository,
			LocalizerSettings settings)
		{
			_provider = provider;
			_mapper = mapper;
			_repository = repository;
			_secretSettings = settings.Authentication;
		}

		public async Task<string> CreateTokenAsync(string email)
		{
			var account = await _repository.GetAccountByEmailAsync(email);

			var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretSettings.TokenSigningKey));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var jwt = new JwtSecurityToken(_secretSettings.ServiceName,
				_secretSettings.ServiceName,
				new List<Claim>
				{
					new(ClaimTypes.Email, account.Email),
					new(ClaimTypes.NameIdentifier, account.Id.ToString()),
					new(ClaimTypes.Name, account.Name),
				},
				expires: _provider.Now.AddMilliseconds(15).UtcDateTime,
				signingCredentials: credentials);
			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}

		public async Task<int> CreateAccountAsync(SignUpRequest request)
		{
			var account = _mapper.Map<Account>(request);
			await _repository.AddAsync(account);
			return account.Id;
		}
	}
}