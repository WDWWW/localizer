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
		public async Task<ActionResult<SIgnInResponse>> SignInAsync([FromBody] SignInRequest request)
		{
			if (!await _accountService.EmailExistsAsync(request.Email))
				return NotFound("there are no account for email.");


			if (!await _service.VerifyEmailAndPasswordAsync(request))
				return Unauthorized();


			if (!await _service.VerifyEmailConfirmedByEmailAsync(request.Email))
				return Forbid();
			
			return Ok(new SIgnInResponse
			{
				Token = await _service.CreateTokenAsync(request.Email),
			});
		}
	}
}