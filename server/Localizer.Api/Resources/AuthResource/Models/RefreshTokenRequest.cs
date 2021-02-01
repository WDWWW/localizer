using System.ComponentModel.DataAnnotations;

namespace Localizer.Api.Resources.AuthResource.Models
{
	public class RefreshTokenRequest
	{
		/// <summary>
		///		Account access token for replacing new access token.
		/// </summary>
		[Required]
		public string Token { get; set; }
	}
}