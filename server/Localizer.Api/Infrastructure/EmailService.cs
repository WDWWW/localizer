// unset

using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Localizer.Api.Infrastructure
{
	public interface IEmailService
	{
		Task SendMailAsync(string mailTo, string subject, string text);

		MailMessage CreateNoReplyMailMessage(string mailTo, string subject, string text);
	}

	public class EmailService : IEmailService, IDisposable
	{
		private readonly EmailServerSettings _settings;

		private readonly SmtpClient _smtpClient;

		public EmailService(LocalizerSettings settings)
		{
			_settings = settings.EmailServer;

			_smtpClient = new SmtpClient(_settings.Host, _settings.Port)
			{
				Credentials = new NetworkCredential(_settings.UserName, _settings.Password),
			};
		}

		public async Task SendMailAsync(string mailTo, string subject, string text)
		{
			using var mailMessage = CreateNoReplyMailMessage(mailTo, subject, text);
			await _smtpClient.SendMailAsync(mailMessage);
		}

		public MailMessage CreateNoReplyMailMessage(string mailTo, string subject, string text) =>
			new()
			{
				From = new MailAddress(_settings.NoReply),
				To = {mailTo},
				Subject = subject,
				Body = text,
				SubjectEncoding = Encoding.UTF8,
				BodyEncoding = Encoding.UTF8,
				IsBodyHtml = false,
			};

		public void Dispose()
		{
			_smtpClient?.Dispose();
		}
	}
}