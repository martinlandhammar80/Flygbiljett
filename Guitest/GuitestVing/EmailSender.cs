using System.Net.Mail;
using System.Net;

namespace GuitestVing
{
    public class EmailSender
    {
        public void SendEmailAsync(string email, string subject, string message)
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("martin.landhammar@gmail.com", "tnjo sngm fhcn fpyj"),
                EnableSsl = true
            };

            smtp.Send(email, email, subject, message);
            smtp.Dispose();
        }
    }
}
