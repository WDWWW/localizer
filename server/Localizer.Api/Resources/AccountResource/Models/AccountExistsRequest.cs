using System.ComponentModel.DataAnnotations;

namespace Localizer.Api.Resources.AccountResource.Models
{
	public class AccountExistsRequest
	{
		[EmailAddress]
		public string Email { get; set; }
	}
}