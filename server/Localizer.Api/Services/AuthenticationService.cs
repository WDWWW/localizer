// unset

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Localizer.Api.Repositories;
using Localizer.Common;
using Microsoft.IdentityModel.Tokens;

namespace Localizer.Api.Services
{
	public class AuthenticationService
	{
		private readonly IDateTimeOffsetProvider _provider;

		private readonly AccountRepository _repository;

		public AuthenticationService(IDateTimeOffsetProvider provider,
			AccountRepository repository)
		{
			_provider = provider;
			_repository = repository;
		}

		public async Task<string> CreateTokenAsync(string email)
		{
			var account = await _repository.GetAccountByEmailAsync(email);

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("localizer_demo_secret_key"));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var jwt = new JwtSecurityToken("localizer",
				"localizer",
				new List<Claim>
				{
					new(ClaimTypes.Email, account.Email),
					new(ClaimTypes.NameIdentifier, account.Id.ToString()),
					new(ClaimTypes.Name, account.Name),
					new(ClaimTypes.Role, "Admin"),
				},
				expires: _provider.Now.AddMilliseconds(15).UtcDateTime,
				signingCredentials: credentials);
			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}
	}
}