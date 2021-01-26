using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
		public AuthenticationSecretSettings Authentication { get; set; } = new();

		/// <summary>
		///		Localizer database connection string. only allowed for postgresql.
		/// </summary>
		[Required]
		public string DatabaseConnection { get; set; } = string.Empty;


		/// <summary>
		///		Email server configuration for sending notification and service notice.
		/// </summary>
		[Required]
		public EmailServerSettings EmailServer { get; set; } = new();
	}

	
	public class EmailServerSettings
	{
		/// <summary>
		///		Email server host.
		/// </summary>
		[Required]
		public string Host { get; set; } = string.Empty;

		/// <summary>
		///		SMTP port : (default: 25)
		/// </summary>
		[DefaultValue(25)]
		public int Port { get; set; } = 25;

		/// <summary>
		///		no-reply address (ex: no-reply@localizer.com)
		/// </summary>
		[Required]
		public string NoReply { get; set; } = string.Empty;

		/// <summary>
		///		SMTP user name.
		/// </summary>
		[Required]
		public string UserName { get; set; } = string.Empty;

		/// <summary>
		///		SMTP user password.
		/// </summary>
		[Required]
		public string Password { get; set; } = string.Empty;
	}

	public class AuthenticationSecretSettings
	{
		/// <summary>
		///		System default name for 
		/// </summary>
		[Required]
		public string ServiceName { get; set; } = string.Empty;

		/// <summary>
		///		Token signing key for generating jwt key as access token. (ASCII only)
		/// </summary>
		[Required]
		public string TokenSigningKey { get; set; } = string.Empty;
	}
}