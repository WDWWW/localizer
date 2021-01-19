// unset

using System.Threading.Tasks;
using Localizer.Api.Resources.AccountResource.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Localizer.Api.Resources.AccountResource
{
	[Authorize]
	[ApiController]
	[Route("api/accounts")]
	public class AccountController : LocalizerControllerBase
	{
		private readonly AccountService _service;

		public AccountController(AccountService service)
		{
			_service = service;
		}

		/// <summary>
		///		Check any account uses the email
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("exists")]
		public async Task<bool> EmailExistsAsync([FromBody] AccountExistsRequest request)
		{
			return await _service.EmailExistsAsync(request.Email);
		}
	}
}