using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using CCT.SystemFramework.Email;
using OCR.Model;
using OCR.BLL;

namespace OCR.AppBack
{
    /// <summary>
    /// Job
    /// </summary>
    public abstract class Job : IJob
    {
        public Job(UIDelegate delegate1)
        {
            this.delegate1 = delegate1;
        }

        private UIDelegate delegate1;
        /// <summary>
        /// �ʼ�������
        /// </summary>
        public abstract string MailTo
        {
            get;
        }

        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="row">��ҵִ�����</param>
        protected void SendEmail(string mailTo, string subject, string content)
        {
            CCT.SystemFramework.Email.EmailInfo ei = new CCT.SystemFramework.Email.EmailInfo();
            ei.From = Config.MailFrom;
            ei.To = mailTo;
            ei.Subject = subject;
            ei.Body = content;

            CCT.SystemFramework.Email.EmailHelper.Send(ei);
        }

        /// <summary>
        /// ��¼ִ�н��
        /// </summary>
        public void AddResult(string msg)
        {
            //jobResultDA.Insert(_jobRow);
            msg = msg.Insert(0, string.Format(">{0} ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            msg = msg.Insert(msg.Length, "\r\n");
            delegate1(msg);
        }

        public abstract string Deal();

        #region IJob ��Ա

        public void DealJobAsy()
        {
            //UIDelegate delegate1 = (UIDelegate)state;

            try
            {
                Thread.Sleep(1000);

                Deal();
                                            
            }
            catch (Exception ex)
            {
                AddResult(ex.Message);
            }
        }

        #endregion
    }
}
