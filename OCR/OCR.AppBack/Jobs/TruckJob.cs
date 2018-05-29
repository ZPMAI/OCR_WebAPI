using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCR.BLL;
using OCR.Model;

namespace OCR.AppBack.Jobs
{
    /// <summary>
    /// 提前抓拍车号同步数据
    /// </summary>
    public class TruckJob : Job
    {
        public TruckJob(UIDelegate delegate1)
            : base(delegate1)
        {
        }

        public override string MailTo
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        private static long maxId;

        /// <summary>
        /// 车号记录最大ID
        /// </summary>
        private static long MaxId
        {
            get
            {
                if (maxId == 0)
                {
                    //jobBLL bll = new jobBLL();
                    //已同步最大ID
                    maxId = jobBLL.SelectMaxId3();
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

                //读取待同步数据
                using (DataSet ds = bll.GetOcrDBTruck(MaxId))
                {

                    //逐条写入二次开发数据库
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        try
                        {
                            MaxId = Convert.ToInt64(dr["temp_dock_id"]);

                            dr["car_no"] = dr["car_no"].ToString().Trim().Length == 4 ? dr["car_no"].ToString().Substring(0, 3) : dr["car_no"].ToString();
                            bll.InsertTruck(dr);

                            AddResult(string.Format("车号{0} {1}", dr["trval_no"].ToString(), dr["car_no"].ToString()));
                        }
                        catch (Exception ex)
                        {
                            AddResult(string.Format("车号{0} {1}", dr["trval_no"].ToString(), ex.Message));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddResult(string.Format("车号 {0}", ex.Message));
            }

            return msg.ToString();
        }

        
    }
}
