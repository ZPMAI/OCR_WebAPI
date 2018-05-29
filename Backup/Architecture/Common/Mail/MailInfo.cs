using System;
using System.Text;
using System.Net.Mail;

using CCT.Common.Configuration;

namespace CCT.Common.Mail
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
        private string[] fileNames;

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
        public string CC
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
        public string[] FileNames
        {
            get { return fileNames; }
            set { fileNames = value; }
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

            mailMessage.From = new MailAddress(this.from);

            AddMailAddress(mailMessage.To, this.to);
            AddMailAddress(mailMessage.CC, this.cc);

            mailMessage.Subject = this.subject;
            mailMessage.Body = this.body;
            mailMessage.IsBodyHtml = true;

            if ((this.fileNames != null)
                && (this.fileNames.Length > 0))
            {
                foreach (string fileName in fileNames)
                {
                    Attachment attachment = new Attachment(fileName);
                    mailMessage.Attachments.Add(attachment);
                }
            }

            return mailMessage;
        }

        private void AddMailAddress(MailAddressCollection mailAddressCollection, string mailAddresses)
        {
            string[] ss = mailAddresses.Split(';');

            foreach (string s in ss)
            {
                if (string.IsNullOrEmpty(s)) { continue; }

                mailAddressCollection.Add(new MailAddress(s));
            }
        }
    }
}
