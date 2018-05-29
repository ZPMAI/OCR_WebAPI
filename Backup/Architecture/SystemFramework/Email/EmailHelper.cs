using System;
using System.Net.Mail;

using CCT.SystemFramework.Configuration;

namespace CCT.SystemFramework.Email
{
	/// <summary>
	/// EmailHelper ��ժҪ˵����
	/// </summary>
	public class EmailHelper
	{

       private static SmtpClient smtpClient;

		/// ���캯��
		static EmailHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
            smtpClient = new SmtpClient();
            smtpClient.Host = ConfigurationHelper.Section["Mail"]["Host"].ToString();
			
            //SmtpMail.SmtpServer = ConfigurationHelper.Section["Email"]["SmtpServer"].ToString();
		}


		/// ���͵����ʼ�
		public static void Send(EmailInfo emailInfo)
		{
            smtpClient.Send(emailInfo.ToMailMessage());

            //SmtpMail.Send(emailInfo.ToMailMessage());
		}


		/// ���͵����ʼ�
		public static void Send(string from, string to, string subject, string messageText)
		{
            smtpClient.Send(from, to, subject, messageText);

            //SmtpMail.Send(from, to, subject, messageText);
		}
	}
}
