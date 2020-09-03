using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Recruitment.Data;
using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Helper
{
    public class Mailer
    {
        private readonly AppDbContext dbContext;

        public Mailer(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void mailing(string name, string email, BodyBuilder bodyBuilder, MimeMessage message)
        {
            try
            {
                //MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Recruitment System",
                "packagedelivery@ivslng.com");
                message.From.Add(from);
                
                MailboxAddress to = new MailboxAddress(name,
                email);
                message.To.Add(to);

                //bodyBuilder.Attachments.Add(path);
                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                //client.SslProtocols |= SslProtocols.Ssl2;

                //client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                //client.Authenticate("zee.b.ltd@gmail.com", "Mybusiness1");
                //client.Connect("smtp.ionos.com", 465,true);
                client.Connect("smtp.ionos.com", 25);
                //client.Connect("mxbulk.kundenserver.de", 587, SecureSocketOptions.StartTls);
                client.Authenticate("packagedelivery@ivslng.com", "Nurudeen.1");

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.ErrorDate = DateTime.Now;
                errorLog.ErrorMessage = ex.Message;
                errorLog.ErrorSource = ex.Source;
                errorLog.ErrorStackTrace = ex.StackTrace;
                errorLog.InnerException = ex.InnerException.ToString();
                dbContext.ErrorLogs.Add(errorLog);
                dbContext.SaveChanges();
            }
        }
    }
}
