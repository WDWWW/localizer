// unset

using System.ComponentModel.DataAnnotations;

namespace Localizer.Api.Resources.Account.Models
{
	public abstract class CreateAccountRequest
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