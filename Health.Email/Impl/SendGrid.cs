using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SendGrid;

namespace Health.Email.Impl
{
    public class SendGrid : IEmail
    {
        private const string UserName = "azure_319cea33fc47810d80cdf9feb551e4db@azure.com";
        private const string Password = "xtdD6531F828zR4";

        public async Task SendEmail(string subject, string fromEmail, string fromName, List<string> toEmailsList, string plainBody, string htmlBody)
        {
            var myMessage = new SendGridMessage {From = new MailAddress(fromEmail, fromName)};
            myMessage.AddTo(toEmailsList);
            myMessage.Subject = subject;
            if (!string.IsNullOrEmpty(htmlBody))
            {
                myMessage.Html = htmlBody;
            }
            if (!string.IsNullOrEmpty(plainBody))
            {
                myMessage.Text = plainBody;
            }
            //myMessage.EnableFooter("PLAIN TEXT FOOTER", "<p><em>HTML FOOTER</em></p>");
            myMessage.EnableClickTracking(true);
            var credentials = new NetworkCredential(UserName, Password);
            var transportWeb = new Web(credentials);
            await transportWeb.DeliverAsync(myMessage);
        }
    }
}