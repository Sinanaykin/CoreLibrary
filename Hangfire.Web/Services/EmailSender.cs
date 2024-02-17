using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Hangfire.Web.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Sender(string userId, string message)
        {
            //parametreden gelen userID ile kişin mailini bulunur be aşağıoya o yazılır

            var apiKey = _configuration.GetSection("APIS")["SendGridKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("sinan.aykin@gedik.com", "Example User");
            var subject = "Site bilgilendirme";
            var to = new EmailAddress("sinanaykinnn@gmail.com", "Example User");
           // var plainTextContent = "Selam deneme mailidir";
            var htmlContent = $"<strong>{message}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
            await client.SendEmailAsync(msg);

            
        }
    }
}
