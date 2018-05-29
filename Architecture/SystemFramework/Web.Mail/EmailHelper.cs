using System;
using System.Web.Mail;

using CCT.Common.Configuration;

namespace CCT.SystemFramework.Web.Mail
{
	/// <summary>
	/// EmailHelper 的摘要说明。
	/// </summary>
	public class EmailHelper
	{
		/// 构造函数
		static EmailHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
            SmtpMail.SmtpServer = ConfigurationHelper.Section["Mail"]["Host"].ToString();
		}


		/// 发送电子邮件
		public static void Send(EmailInfo emailInfo)
		{
			SmtpMail.Send(emailInfo.ToMailMessage());
		}


		/// 发送电子邮件
		public static void Send(string from, string to, string subject, string messageText)
		{
			SmtpMail.Send(from, to, subject, messageText);
		}
	}
}
