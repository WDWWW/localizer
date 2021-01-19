// unset

using System.ComponentModel.DataAnnotations;

namespace Localizer.Api.Resources.AuthResource.Models
{
	public abstract class SignUpRequest
	{
		[Required]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }
		
		[Required]
		public string Password { get; set; }
	}
}