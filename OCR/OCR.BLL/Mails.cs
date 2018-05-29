using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.Mail;

namespace OCR.BLL
{
    /// <summary>
    /// Mails 的摘要说明
    /// </summary>
    public class Mails
    {
        public Mails(string mailto, string subject, string body, string[] attachments)
        {
            this.mailto = mailto;
            this.subject = subject;
            this.body = body;
            this.attachments = attachments;
        }

        private  string password = ConfigurationManager.AppSettings["mailPassWord"].ToString();
        private  string username = ConfigurationManager.AppSettings["mailUserName"].ToString();
        private  string mailfrom = ConfigurationManager.AppSettings["mailfromAddress"].ToString();
        //private const string server = "172.16.1.62";
        private  string server = ConfigurationManager.AppSettings["mailHost"].ToString();

        private string mailto;
        private string subject;
        private string body;
        private string[] attachments;

        public void Send()
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = mailfrom;
            mailMessage.To = mailto;
            //mailMessage.Cc = mailcc;
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.BodyFormat = MailFormat.Text;
            mailMessage.Priority = MailPriority.High;

            mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", server);
            mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", username);
            mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);

            if ((this.attachments != null)
                && (this.attachments.Length > 0))
            {
                foreach (string attachment in attachments)
                {
                    MailAttachment mailAttachment = new MailAttachment(attachment);
                    mailMessage.Attachments.Add(mailAttachment);
                }
            }

            SmtpMail.SmtpServer = server;
            SmtpMail.Send(mailMessage);
        }

    }
}
