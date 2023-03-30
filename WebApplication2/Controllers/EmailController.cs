
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;


namespace WebApplication2.Controllers
{
    public class EmailController : Controller
    {


        public IActionResult SendEmail()
        {
            return View();
        }






        private static async Task SendGridEmailAsync(string toEmail, string username,
             string subject, string message)
        {
            string apiKey = "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";
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("ahmad.o.agha1986@gmail.com", "WebApplication2.com");
            var to = new EmailAddress(toEmail, username);
            var plainTextContent = message;
            var htmlContent = "<p>" + message + "</p>";

            var msg = MailHelper.CreateSingleEmail(
                from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string toEmail, string username, string subject, string message)
        {
            await SendGridEmailAsync(toEmail, username, subject, message);

            return RedirectToAction("AdminPage", "Account");
        }


    }
}

