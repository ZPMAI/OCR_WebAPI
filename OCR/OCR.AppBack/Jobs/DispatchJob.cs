using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCRX.BLL;
using OCR.BLL;
using OCR.Model;
using System.Threading;

namespace OCR.AppBack.Jobs
{
    /// <summary>
    /// 数据分发
    /// </summary>
    public class DispatchJob : Job
    {
        public DispatchJob(UIDelegate delegate1)
            : base(delegate1)
        {
        }

        public override string MailTo
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string Deal()
        {
            StringBuilder msg = new StringBuilder();

            try
            {
                Thread.Sleep(5000);

                OCRX.BLL.DispatchBLL bll = new DispatchBLL();

                //string msg2 = bll.DealSuccessWithLinecode();
                //if (!string.IsNullOrEmpty(msg2))
                //{
                //    AddResult(msg2);
                //}

                //string msg3 = bll.DealSuccessWithoutLinecode();
                //if (!string.IsNullOrEmpty(msg3))
                //{
                //    AddResult(msg3);
                //}

                string msg1 = bll.DealExOnlySCT();

                if (!string.IsNullOrEmpty(msg1))
                {
                    AddResult(msg1);
                }  

                //if (string.IsNullOrEmpty(msg1) && string.IsNullOrEmpty(msg2) && string.IsNullOrEmpty(msg3))
                //{
                //    AddResult("数据分发 无记录");
                //}
            }
            catch (Exception ex)
            {
                AddResult(string.Format("数据分发 {0}", ex.Message));
            }

            return msg.ToString();
        }

        
    }
}
