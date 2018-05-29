using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCR.BLL;
using OCR.Model;

namespace OCR.AppBack.Jobs
{
    /// <summary>
    /// ʵʱ���
    /// </summary>
    public class MonitorJob : Job
    {
        public MonitorJob(UIDelegate delegate1)
            : base(delegate1)
        {
        }

        public override string MailTo
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        private static DateTime _nextTime = DateTime.Now;

        /// <summary>
        /// �´�ִ��ʱ��
        /// </summary>
        private static DateTime NextTime
        {
            get
            {
                return _nextTime;
            }
            set
            {
                _nextTime = value;
            }
        }

        public override string Deal()
        {
            //return (new Random().Next()).ToString();
            StringBuilder msg = new StringBuilder();

            try
            {
                if (NextTime <= DateTime.Now)
                {
                    jobBLL bll = new jobBLL();

                    //���OCR���ݿ�
                    string ocrdb = bll.CheckOCRDB();
                    if (!string.IsNullOrEmpty(ocrdb))
                    {
                        SendMail1("��OCRƽ̨ʵʱ���� OCR���ݿ� �쳣���ѣ�", ocrdb);
                    }


                    //���CTOS OCR�ӿ�
                    DateTime d1 = DateTime.Now;
                    string ctosapi = bll.CheckCTOSAPI();
                    DateTime d2 = DateTime.Now;
                    if (!string.IsNullOrEmpty(ctosapi))
                    {
                        SendMail1("��OCRƽ̨ʵʱ���� CTOS�м��OCR�ӿ� �쳣���ѣ�", ctosapi);
                    }
                    else if (((TimeSpan)d2.Subtract(d1)).Seconds >= 20)
                    {
                        SendMail2("��OCRƽ̨ʵʱ���� CTOS�м��OCR�ӿ� ��ʱ���ѣ�", ((TimeSpan)d2.Subtract(d1)).Seconds.ToString());
                    }



                    _nextTime = _nextTime.AddMinutes(1);

                    AddResult(string.Format("ʵʱ��� {0}", _nextTime.ToString("yyyy-MM-dd HH:mm:ss")));
                }

                //

            }
            catch (Exception ex)
            {

                SendMail1("��OCRƽ̨ʵʱ���� ʵʱ����߳� �쳣���ѣ�", ex.Message);

                AddResult(string.Format("ʵʱ��� {0}", ex.Message));
            }

            return msg.ToString();
        }

        private void SendMail1(string subject, string content)
        {
            try
            {
                if (!Config.IsDebug)
                {
                    Mails ml = new Mails("zpmai@sctcn.com",
                        subject, content, null);
                    ml.Send();
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void SendMail2(string subject, string content)
        {
            try
            {
                Mails ml = new Mails("zpmai@sctcn.com",
                    subject, content, null);
                ml.Send();


            }
            catch (Exception ex)
            {
            }
        }

        
    }
}
