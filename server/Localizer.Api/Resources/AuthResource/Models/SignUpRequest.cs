// unset

using System.ComponentModel.DataAnnotations;

namespace Localizer.Api.Resources.AuthResource.Models
{
	public class SignUpRequest
	{
		/// <summary>
		///		User name
		/// </summary>
		[Required]
		public string Name { get; set; }

		/// <summary>
		///		Email address
		/// </summary>
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		
		/// <summary>
		///		Password.
		/// </summary>
		[Required]
		[MinLength(8)]
		public string Password { get; set; }
	}
}