using System.ComponentModel.DataAnnotations;

namespace Localizer.Api
{
	public class LocalizerSettings
	{
		[Required]
		public AuthenticationSecretSettings SecretSettings { get; set; } = new();
	}

	public class AuthenticationSecretSettings
	{
		[Required]
		public string ServiceName { get; set; } = string.Empty;

		[Required]
		public string TokenSigningKey { get; set; } = string.Empty;
	}
}