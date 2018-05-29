using System;
using System.Web.Mail;

using CCT.Common.Configuration;

namespace CCT.SystemFramework.Web.Mail
{
	/// <summary>
	/// EmailInfo 的摘要说明。
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
		/// 获取或设置发件人的电子邮件地址
		/// </summary>
		public string From
		{
			get { return from; }
			set { from = value; }
		}

		/// <summary>
		/// 获取或设置以分号分隔的收件人电子邮件地址列表
		/// </summary>
		public string To
		{
			get { return to; }
			set { to = value; }
		}

		/// <summary>
		/// 获取或设置以分号分隔的电子邮件地址列表，这些地址接收电子邮件的抄送副本
		/// </summary>
		public string Cc
		{
			get { return cc; }
			set { cc = value; }
		}

		/// <summary>
		/// 获取或设置电子邮件的主题行
		/// </summary>
		public string Subject
		{
			get { return subject; }
			set { subject = value; }
		}

		/// <summary>
		/// 获取或设置电子邮件的正文
		/// </summary>
		public string Body
		{
			get { return body; }
			set { body = value; }
		}

		/// <summary>
		/// 指定随电子邮件一起传送的附件地址集合
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
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// 密送
        /// </summary>
        public string Bcc
        {
            get { return bcc; }
            set { bcc = value; }
        }


		
		/// 构造函数
		public EmailInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
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
