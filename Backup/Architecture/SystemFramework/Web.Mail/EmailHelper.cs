using System;
using System.Web.Mail;

using CCT.Common.Configuration;

namespace CCT.SystemFramework.Web.Mail
{
	/// <summary>
	/// EmailHelper ��ժҪ˵����
	/// </summary>
	public class EmailHelper
	{
		/// ���캯��
		static EmailHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
            SmtpMail.SmtpServer = ConfigurationHelper.Section["Mail"]["Host"].ToString();
		}


		/// ���͵����ʼ�
		public static void Send(EmailInfo emailInfo)
		{
			SmtpMail.Send(emailInfo.ToMailMessage());
		}


		/// ���͵����ʼ�
		public static void Send(string from, string to, string subject, string messageText)
		{
			SmtpMail.Send(from, to, subject, messageText);
		}
	}
}
