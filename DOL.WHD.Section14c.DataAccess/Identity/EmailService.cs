using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace DOL.WHD.Section14c.DataAccess.Identity
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSMTPasync(message);
        }

        // send email via smtp service
        private async Task configSMTPasync(IdentityMessage message)
        {
            var client = new SmtpClient();
            var mailMessage = new MailMessage()
            {
                Subject = message.Subject,
                Body = message.Body
            };
            mailMessage.To.Add(new MailAddress(message.Destination));

            await client.SendMailAsync(mailMessage);
        }
    }
}
