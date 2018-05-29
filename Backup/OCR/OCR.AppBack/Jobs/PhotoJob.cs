using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCR.BLL;
using OCR.Model;

namespace OCR.AppBack.Jobs
{
    /// <summary>
    /// 图片同步数据
    /// </summary>
    public class PhotoJob : Job
    {
        public PhotoJob(UIDelegate delegate1)
            : base(delegate1)
        {
        }

        public override string MailTo
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        private static int maxId;

        /// <summary>
        /// 图片记录最大ID
        /// </summary>
        private static int MaxId
        {
            get
            {
                if (maxId == 0)
                {
                    //jobBLL bll = new jobBLL();
                    //已同步最大ID
                    maxId = jobBLL.SelectMaxId2();
                }

                return maxId;
            }
            set
            {
                if (value > maxId)
                {
                    maxId = value;
                }
            }
        }

        public override string Deal()
        {
            //return (new Random().Next()).ToString();
            StringBuilder msg = new StringBuilder();

            try
            {
                jobBLL bll = new jobBLL();

                //AddResult(string.Format("图片 max id {0}", maxId));

                ////已同步最大ID
                //int maxid = bll.SelectMaxId();
                //读取待同步数据
                using (DataSet ds = bll.GetOcrDBPhoto1(MaxId))
                {

                    //逐条写入二次开发数据库
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        try
                        {
                            MaxId = Convert.ToInt32(dr["photo_id"]);
                        
                            bll.InsertPhoto(dr);

                            AddResult(string.Format("图片{0} {1}", dr["dock_id"].ToString(), dr["photo_id"].ToString()));
                        }
                        catch (Exception ex)
                        {
                            AddResult(string.Format("图片{0} {1}", dr["photo_id"].ToString(), ex.Message));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddResult(string.Format("图片 {0}", ex.Message));
            }

            return msg.ToString();
        }

        
    }
}
