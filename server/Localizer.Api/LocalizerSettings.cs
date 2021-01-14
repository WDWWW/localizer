using System.ComponentModel.DataAnnotations;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace Localizer.Api
{
	public class LocalizerSettings
	{
		/// <summary>
		///		Localizer authentication configuration
		/// </summary>
		[Required]
		public AuthenticationSecretSettings SecretSettings { get; set; } = new();

		/// <summary>
		///		Localizer database connection string. only allowed for postgresql.
		/// </summary>
		[Required]
		public string DatabaseConnection { get; set; } = string.Empty;
	}

	public class AuthenticationSecretSettings
	{
		/// <summary>
		///		System default name for 
		/// </summary>
		[Required]
		public string ServiceName { get; set; } = string.Empty;

		/// <summary>
		///		Token signing key for generating jwt key as access token.
		/// </summary>
		[Required]
		public string TokenSigningKey { get; set; } = string.Empty;
	}
}