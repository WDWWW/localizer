using System.Threading.Tasks;
using Localizer.Api.Resources.AccountResource;
using Localizer.Api.Resources.AuthResource.Models;
using Microsoft.AspNetCore.Mvc;

namespace Localizer.Api.Resources.AuthResource
{
	[ApiController]
	[Route("api/auth")]
	public class AuthController : LocalizerControllerBase
	{
		private readonly AccountService _accountService;

		private readonly AuthenticationService _service;

		public AuthController(AccountService accountService, AuthenticationService service)
		{
			_accountService = accountService;
			_service = service;
		}
		
		/// <summary>
		///		Create new account
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("sign-up")]
		public async Task<ActionResult> SignUpAsync([FromBody] SignUpRequest request)
		{
			if (await _accountService.EmailExistsAsync(request.Email))
				return BadRequest("Email of request is already used by other user.");

			await _service.CreateAccountAsync(request);

			return Ok();
		}

		[HttpPost("sign-in")]
		public async Task<ActionResult<SignInResponse>> SignInAsync([FromBody] SignInRequest request)
		{
			if (!await _accountService.EmailExistsAsync(request.Email))
				return NotFound("there are no account for email.");


			if (!await _service.VerifyEmailAndPasswordAsync(request))
				return Unauthorized();


			if (!await _service.VerifyEmailConfirmedByEmailAsync(request.Email))
				return Forbid();
			
			return Ok(new SignInResponse
			{
				Token = await _service.CreateTokenAsync(request.Email),
			});
		}

		/// <summary>
		///		Refresh access token.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("refresh")]
		public async Task<ActionResult<RefreshTokenResponse>> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
		{
			if (!_service.TryValidateToken(request.Token, out _))
				return BadRequest("Invalid jwt access token.");

			if (!await _service.IsRefreshableTokenAsync(request.Token))
				return Forbid();

			var newToken = await _service.RefreshTokenAsync(request.Token);

			return Ok(new RefreshTokenResponse {Token = newToken});
		}
	}
}