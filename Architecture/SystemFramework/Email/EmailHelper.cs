using System;
using System.Net.Mail;

using CCT.SystemFramework.Configuration;

namespace CCT.SystemFramework.Email
{
	/// <summary>
	/// EmailHelper 的摘要说明。
	/// </summary>
	public class EmailHelper
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
			
            //SmtpMail.SmtpServer = ConfigurationHelper.Section["Email"]["SmtpServer"].ToString();
		}


		/// 发送电子邮件
		public static void Send(EmailInfo emailInfo)
		{
            smtpClient.Send(emailInfo.ToMailMessage());

            //SmtpMail.Send(emailInfo.ToMailMessage());
		}


		/// 发送电子邮件
		public static void Send(string from, string to, string subject, string messageText)
		{
            smtpClient.Send(from, to, subject, messageText);

            //SmtpMail.Send(from, to, subject, messageText);
		}
	}
}
