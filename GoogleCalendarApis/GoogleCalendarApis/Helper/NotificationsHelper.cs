using System.Net;
using System.Net.Mail;
namespace GoogleCalendarApis.Helper
{
    public static class NotificationsHelper
    {
        public static MailMessage CreateMessage(string mail, string eventSummary)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("nourhank285@gmail.com");
            message.Subject = "New Calendar Event Created";
            message.To.Add(new MailAddress(mail));
            string bodyMessage = $"The {eventSummary} Event Created Now you can check your Google Calendar to see its details";
            message.Body = $"<html><body> {bodyMessage} </body></html>";
            message.IsBodyHtml = true;
            return message; 

        }
        public static void SendEmail(string mail, string eventSummary)
        {

            MailMessage message = CreateMessage(mail, eventSummary);
            var client = new SmtpClient("smtp.sendgrid.net", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("apikey", "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");
            client.Send(message);
        }
    }
}

