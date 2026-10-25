
using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace PM.Common.Clients
{
    public class EmailClient : IDisposable
    {
        private readonly SendGridClient client;

        public EmailClient()
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException(
                    "SENDGRID_API_KEY environment variable is not configured."
                );
            }

            client = new SendGridClient(apiKey);
        }

        public Task SendEmail(string subject, string body, string address)
        {
            var msg = new SendGridMessage();

            msg.SetFrom("support@pmanager.com", "PManager support");
            msg.AddTo(address);
            msg.SetSubject(subject);
            msg.AddContent(MimeType.Html, body);

            return client.SendEmailAsync(msg);
        }

        public void Dispose()
        {
            // SendGridClient does not require explicit disposal.
        }
    }
}
