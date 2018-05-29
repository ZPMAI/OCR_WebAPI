using System;
using System.Text;
using System.Net.Mail;

using CCT.Common.Configuration;

namespace CCT.Common.Mail
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
        private string[] fileNames;

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
        public string CC
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
        public string[] FileNames
        {
            get { return fileNames; }
            set { fileNames = value; }
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
