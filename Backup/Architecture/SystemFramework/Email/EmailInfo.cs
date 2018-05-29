using System;
using System.Net.Mail;

using CCT.SystemFramework.Configuration;

namespace CCT.SystemFramework.Email
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

		
		/// 构造函数
		public EmailInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		public MailMessage ToMailMessage()
		{
			MailMessage mailMessage = new MailMessage(this.from,this.to);
				
			
			//mailMessage.Cc         = this.cc;
			mailMessage.Subject    = this.subject;
			mailMessage.Body       = this.body;
            mailMessage.IsBodyHtml = true;


            if ((this.attachments != null)
              && (this.attachments.Length > 0))
            {
                foreach (string fileName in attachments)
                {
                    Attachment attachment = new Attachment(fileName);
                    mailMessage.Attachments.Add(attachment);
                }
            }

            return mailMessage;
		
		}
	}
}
