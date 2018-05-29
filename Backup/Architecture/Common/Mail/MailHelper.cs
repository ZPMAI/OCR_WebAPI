using System;
using System.Net.Mail;

using CCT.Common.Configuration;

namespace CCT.Common.Mail
{
	/// <summary>
	/// EmailHelper ��ժҪ˵����
	/// </summary>
	public static class EmailHelper
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
		}


		/// ���͵����ʼ�
		public static void Send(EmailInfo emailInfo)
		{
            smtpClient.Send(emailInfo.ToMailMessage());

            //SmtpMail.Send(emailInfo.ToMailMessage());
		}


		/// ���͵����ʼ�
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
