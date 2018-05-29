using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCR.BLL;
using OCR.Model;

namespace OCR.AppBack.Jobs
{
    /// <summary>
    /// 归档数据
    /// </summary>
    public class ArchiveJob : Job
    {
        public ArchiveJob(UIDelegate delegate1)
            : base(delegate1)
        {
        }

        public override string MailTo
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        private static DateTime _nextTime = DateTime.Now;

        /// <summary>
        /// 下次执行时间
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
                    bll.Archive();

                    _nextTime = _nextTime.AddDays(1);

                    AddResult(string.Format("归档 {0}", _nextTime.ToString("yyyy-MM-dd HH:mm:ss")));
                }

                //

            }
            catch (Exception ex)
            {
                AddResult(string.Format("归档 {0}", ex.Message));
            }

            return msg.ToString();
        }

        
    }
}
