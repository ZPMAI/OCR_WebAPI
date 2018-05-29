using System;
using System.Net.Mail;

using CCT.SystemFramework.Configuration;

namespace CCT.SystemFramework.Email
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

		
		/// ���캯��
		public EmailInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
