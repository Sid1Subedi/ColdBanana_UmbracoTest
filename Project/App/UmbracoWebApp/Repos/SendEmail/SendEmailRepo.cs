using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Mail;
using Umbraco.Cms.Core.Models.Email;
using UmbracoWebApp.Interfaces.SendEmail;
using UmbracoWebApp.Models.SendEmail;
using UmbracoWebApp.Models.SendEmail.Dtos;

namespace UmbracoWebApp.Repos.SendEmail
{

    // For More Info on (IEmailSender from 'Umbraco.Cms.Core.Mail') umbraco smtp server, we can check this source: https://docs.umbraco.com/umbraco-cms/extending/health-check/guides/smtp
    // On Umbraco Server GoTo: Settings/HealthCheck/Service/CheckGroup
    public class SendEmailRepo : ISendEmail
    {
        private readonly EmailSettingsModel _emailSettings;
        private readonly IEmailSender _emailSender;
        private readonly GlobalSettings _globalSettings;

        public SendEmailRepo(IOptions<EmailSettingsModel> emailSettings, IEmailSender emailSender, IOptions<GlobalSettings> globalSettings)
        {
            _emailSettings = emailSettings.Value;
            _emailSender = emailSender;
            _globalSettings = globalSettings.Value;
        }

        // This is send email using MailKit.Net.Smtp
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            MimeMessage message = new();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            BodyBuilder bodyBuilder = new()
            {
                HtmlBody = body
            };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();

            // Connect using the specified SMTP server and port (587 for STARTTLS)
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort);

            // Authenticate with username and password
            await client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);

            // Send the email
            await client.SendAsync(message);

            // Disconnect from the server
            await client.DisconnectAsync(true);
        }

        // This is send email using Umbraco.Cms.Core.Mail
        public async Task<bool> SendEmailUmbraco(EmailRequestDto model)
        {
            try
            {
                var fromAddress = _globalSettings.Smtp.From;

                var subject = string.Format($"Email To: { model.FullName}, { model.Email }");
                EmailMessage message = new (fromAddress, model.Email, subject, model.Message, false);
                await _emailSender.SendAsync(message, emailType: "Contact");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
