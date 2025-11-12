using e_commerce_project.Modles;
using Microsoft.Extensions.Options;
//using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace e_commerce_project.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly MailSettings _settings;
        private readonly ILogger<SmtpEmailService> _logger;
        public SmtpEmailService(IOptions<MailSettings> options, ILogger<SmtpEmailService> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            // Create the MIME message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail)); // Sender (the website)
            message.To.Add(MailboxAddress.Parse(to)); // Recipient (the user)
            message.Subject = subject;

            // Build message body depending on format (HTML or plain text)
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = isBodyHtml ? body : null,
                TextBody = isBodyHtml ? null : body
            };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                // Choose SSL/TLS connection type
                SecureSocketOptions socketOptions = SecureSocketOptions.Auto;
                if (_settings.UseSsl)
                    socketOptions = SecureSocketOptions.SslOnConnect;
                else if (_settings.UseStartTls)
                    socketOptions = SecureSocketOptions.StartTls;
                else
                    socketOptions = SecureSocketOptions.None;

                // Connect to SMTP server
                await client.ConnectAsync(_settings.Host, _settings.Port, socketOptions);

                // Authenticate if credentials provided
                if (!string.IsNullOrEmpty(_settings.User))
                    await client.AuthenticateAsync(_settings.User, _settings.Password);

                // Send the message
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                _logger.LogInformation("Email successfully sent to {Email}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to {Email}", to);
                throw; // Propagate to be handled by the caller
            }
        }



    }
}
