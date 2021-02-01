using System.ComponentModel.DataAnnotations;

namespace Localizer.Api.Resources.AuthResource.Models
{
	public class SignInRequest
	{
		/// <summary>
		///		Email
		/// </summary>
		[Required]
		public string Email { get; set; }

		/// <summary>
		///		Password
		/// </summary>
		[Required]
		public string Password { get; set; }
	}
}