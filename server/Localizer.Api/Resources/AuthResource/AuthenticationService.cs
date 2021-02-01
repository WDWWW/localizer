using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Localizer.Api.Infrastructure;
using Localizer.Api.Infrastructure.Helpers;
using Localizer.Api.Resources.AccountResource;
using Localizer.Api.Resources.AuthResource.Models;
using Localizer.Common;
using Localizer.Common.Helpers;
using Localizer.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Localizer.Api.Resources.AuthResource
{
	public class AuthenticationService
	{
		private readonly IMapper _mapper;

		private readonly IEmailService _emailService;

		private readonly IDateTimeOffsetProvider _provider;

		private readonly AccountRepository _repository;

		private readonly AccessTokenHistoryRepository _tokenHistoryRepository;

		private readonly AuthenticationSecretSettings _secretSettings;

		private readonly SigningCredentials _signingCredentials;

		private SymmetricSecurityKey _signingKey;

		public AuthenticationService(IDateTimeOffsetProvider provider,
			IMapper mapper,
			IEmailService emailService,
			AccountRepository repository,
			AccessTokenHistoryRepository tokenHistoryRepository,
			LocalizerSettings settings)
		{
			_provider = provider;
			_mapper = mapper;
			_emailService = emailService;
			_repository = repository;
			_tokenHistoryRepository = tokenHistoryRepository;
			_secretSettings = settings.Authentication;
			_signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretSettings.TokenSigningKey));
			_signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
		}

		public async Task<string> CreateTokenAsync(string email)
		{
			var account = await _repository.GetAccountByEmailAsync(email);
			var token = GenerateAccessToken(account);
			await _tokenHistoryRepository.AddAsync(new AccountAccessTokenHistory {Account = account, NewToken = token});
			return token;
		}

		public async Task<bool> IsRefreshableTokenAsync(string token)
		{
			return !await _tokenHistoryRepository.RefreshHistoryExistsAsync(token);
		}

		public bool TryValidateToken(string token, out JwtSecurityToken? jwtSecurityToken)
		{
			try
			{
				new JwtSecurityTokenHandler().ValidateToken(token,
					new TokenValidationParameters
					{
						ValidateLifetime = false,
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidIssuer = _secretSettings.ServiceName,
						ValidAudience = _secretSettings.ServiceName,
						IssuerSigningKey = _signingKey,
					},
					out var securityToken);

				jwtSecurityToken = securityToken as JwtSecurityToken ?? throw new InvalidOperationException();

				if (_provider.Now.UtcDateTime > jwtSecurityToken.IssuedAt.AddDays(31).ToUniversalTime())
					return false;
			}
			catch (Exception)
			{
				jwtSecurityToken = default;
				return false;
			}

			return true;
		}

		public async Task<string> RefreshTokenAsync(string token)
		{
			TryValidateToken(token, out var securityToken);
			var accountId = securityToken!.Payload["account.id"] as int?
				?? throw new InvalidOperationException("Cannot found account id from jwt");

			var account = await _repository.FindAsync(accountId)
				?? throw new InvalidOperationException("Cannot found account about account.id from access token.");

			var newToken = GenerateAccessToken(account);
			await _tokenHistoryRepository.AddAsync(new AccountAccessTokenHistory
			{
				Account = account, FromToken = token, NewToken = newToken,
			});
			return newToken;
		}

		private string GenerateAccessToken(Account account) =>
			new JwtSecurityTokenHandler().WriteToken(
				new JwtSecurityToken(_secretSettings.ServiceName,
					_secretSettings.ServiceName,
					expires: _provider.Now.AddHours(3).UtcDateTime,
					signingCredentials: _signingCredentials)
				{
					Payload = {{"account.email", account.Email}, {"account.id", account.Id}, {"account.name", account.Name},},
				});

		public async Task<int> CreateAccountAsync(SignUpRequest request)
		{
			var account = _mapper.Map(request,
				new Account
				{
					EmailVerificationCode = CryptoHelper.GenerateToken(KeyLength.EmailVerificationCode),
					PasswordHash = PasswordHelper.HashPassword(request.Password),
				});
			await _repository.AddAsync(account);
			await _emailService.SendMailAsync(request.Email,
				$"[Localizer] Hello {request.Name}, checkout email confirm code.",
				$@"""
Helle {request.Name}!

Welcome to localizer! Your verification code is '{account.EmailVerificationCode}'.
Please use it when you first login time.

Thank you.
From Localizer team.
""");

			return account.Id;
		}

		public async Task<bool> VerifyEmailConfirmedByEmailAsync(string email)
		{
			var account = await _repository.GetAccountByEmailAsync(email);
			return account.EmailConfirmed;
		}

		public async Task<bool> VerifyEmailAndPasswordAsync(SignInRequest request)
		{
			var account = await _repository.GetAccountByEmailAsync(request.Email);
			return PasswordHelper.VerifyPassword(account.PasswordHash, request.Password);
		}
	}
}