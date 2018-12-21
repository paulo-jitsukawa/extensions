using System.Net;
using System.Net.Mail;

namespace Jitsukawa.Extensions.Email
{
    public static class Email
    {
        public static void Send(this string message, string smtp, int port, bool ssl, string user, string password, string subject, string senderEmail, string receiverEmail, bool html = true, Attachment[] attachments = null)
        {
            var email = new MailMessage(senderEmail, receiverEmail);
            email.Subject = subject;
            email.Body = message;
            email.IsBodyHtml = html;

            if (attachments != null)
                for (int i = 0; i < attachments.Length; i++)
                    email.Attachments.Add(attachments[i]);

            var cliente = new SmtpClient(smtp, port);
            cliente.Credentials = new NetworkCredential(user, password);
            cliente.EnableSsl = ssl;
            cliente.Send(email);
        }

        public static void Send(this SmtpClient smtp, MailMessage email, NetworkCredential credenciais)
        {
            smtp.Credentials = credenciais;
            smtp.Send(email);
        }
    }
}
