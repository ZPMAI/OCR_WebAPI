using System;
using System.Web.Mail;

using CCT.Common.Configuration;

namespace CCT.SystemFramework.Web.Mail
{
	/// <summary>
	/// EmailInfo ��ժҪ˵����
	/// </summary>
	public class EmailInfo
	{
		private string from;
		private string to;
		private string cc;
		private string subject;
		private string body;
		private string[] attachments;
        private string smtpServer;
        private string userName;
        private string password;
        private string bcc;

		/// <summary>
		/// ��ȡ�����÷����˵ĵ����ʼ���ַ
		/// </summary>
		public string From
		{
			get { return from; }
			set { from = value; }
		}

		/// <summary>
		/// ��ȡ�������Էֺŷָ����ռ��˵����ʼ���ַ�б�
		/// </summary>
		public string To
		{
			get { return to; }
			set { to = value; }
		}

		/// <summary>
		/// ��ȡ�������Էֺŷָ��ĵ����ʼ���ַ�б���Щ��ַ���յ����ʼ��ĳ��͸���
		/// </summary>
		public string Cc
		{
			get { return cc; }
			set { cc = value; }
		}

		/// <summary>
		/// ��ȡ�����õ����ʼ���������
		/// </summary>
		public string Subject
		{
			get { return subject; }
			set { subject = value; }
		}

		/// <summary>
		/// ��ȡ�����õ����ʼ�������
		/// </summary>
		public string Body
		{
			get { return body; }
			set { body = value; }
		}

		/// <summary>
		/// ָ��������ʼ�һ���͵ĸ�����ַ����
		/// </summary>
		public string[] Attachments
		{
			get { return attachments; }
			set { attachments = value; }
		}

        /// <summary>
        /// SMTP SERVER
        /// </summary>
        public string SmtpServer
        {
            get { return smtpServer; }
            set { smtpServer = value; }
        }

        /// <summary>
        /// �û���
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Bcc
        {
            get { return bcc; }
            set { bcc = value; }
        }


		
		/// ���캯��
		public EmailInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		public MailMessage ToMailMessage()
		{
			MailMessage mailMessage = new MailMessage();
				
			mailMessage.From       = this.from;
			mailMessage.To         = this.to;
			mailMessage.Cc         = this.cc;
			mailMessage.Subject    = this.subject;
			mailMessage.Body       = this.body;
			mailMessage.BodyFormat = MailFormat.Text;
            mailMessage.Bcc = this.bcc;

			mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", ConfigurationHelper.Section["Mail"]["Host"].ToString());
			mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", userName);
			mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);

			if ( ( this.attachments != null )
				&& ( this.attachments.Length > 0 ) )
			{
				foreach ( string attachment in attachments )
				{
					MailAttachment mailAttachment = new MailAttachment(attachment);
					mailMessage.Attachments.Add(mailAttachment);
				}
			}

			return mailMessage;
		}
	}
}
