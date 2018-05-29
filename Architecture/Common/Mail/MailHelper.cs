using System;
using System.Net.Mail;

using CCT.Common.Configuration;

namespace CCT.Common.Mail
{
	/// <summary>
	/// EmailHelper 的摘要说明。
	/// </summary>
	public static class EmailHelper
	{
        private static SmtpClient smtpClient;

		/// 构造函数
		static EmailHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
            smtpClient = new SmtpClient();
            smtpClient.Host = ConfigurationHelper.Section["Mail"]["Host"].ToString();
		}


		/// 发送电子邮件
		public static void Send(EmailInfo emailInfo)
		{
            smtpClient.Send(emailInfo.ToMailMessage());

            //SmtpMail.Send(emailInfo.ToMailMessage());
		}


		/// 发送电子邮件
		public static void Send(string from, string to, string cc, string subject, string body)
		{
            EmailInfo emailInfo = new EmailInfo();
            emailInfo.From = from;
            emailInfo.To = to;
            emailInfo.CC = cc;
            emailInfo.Subject = subject;
            emailInfo.Body = body;

            smtpClient.Send(emailInfo.ToMailMessage());

            //SmtpMail.Send(from, to, subject, messageText);
		}
	}
}
