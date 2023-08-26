using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SocilaMediaProject.helper;
using SocilaMediaProject.Interfaces;

namespace SocilaMediaProject.Repository
{
    public class SendGridEmail : ISendGridEmail
    {
        // private readonly string key;
        private readonly ILogger<SendGridEmail> _logger;
        public AuthMessageSenderOptions Options { get; set; }

        public SendGridEmail(IConfiguration configuration, IOptions<AuthMessageSenderOptions> options, ILogger<SendGridEmail> logger)
        {
            // key = configuration.GetValue<string>("mySendGridKey");
            _logger = logger;
            Options = options.Value;

        }
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.ApiKey))

            {
                throw new Exception("Null SendGrid Key");
            }
            await Execute(Options.ApiKey, subject, message, toEmail);
        }

        private async Task Execute(string apikey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apikey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("karim.abdelhameed99@gmail.com"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            var dummy = response.StatusCode;
            var dummy2 = response.Headers;
            _logger.LogInformation(response.IsSuccessStatusCode
                                   ? $"Email to {toEmail} queued successfully!"
                                   : $"Failure Email to {toEmail}");
        }
    }
}