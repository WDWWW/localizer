// unset

using HealthChecks.Network;
using HealthChecks.Network.Core;

namespace Localizer.Api.Infrastructure.HealthChecks
{
	public class LocalizerMailServerHealthCheck : SmtpHealthCheck
	{
		public LocalizerMailServerHealthCheck(LocalizerSettings settings) : base(Options(settings.EmailServer))
		{
		}

		private static SmtpHealthCheckOptions Options(EmailServerSettings settings)
		{
			var option = new SmtpHealthCheckOptions
			{
				Host = settings.Host,
				Port = settings.Port,
				ConnectionType = SmtpConnectionType.AUTO,
			};
			option.LoginWith(settings.UserName, settings.Password);
			return option;
		}
	}
}