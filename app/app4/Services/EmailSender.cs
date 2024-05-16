using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app4.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender()
        {
            //Options = optionsAccessor.Value;
        }

        private AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute("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", subject, message, email);
        }

        private Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("fantomas2213@gmail.com", apiKey),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message,
                ReplyTo = new EmailAddress(email)
            };
            msg.AddTo(new EmailAddress(email));

            return client.SendEmailAsync(msg);
        }
    }
}
