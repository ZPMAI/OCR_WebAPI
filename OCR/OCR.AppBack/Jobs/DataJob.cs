using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCR.BLL;
using OCR.Model;
using System.Configuration;

namespace OCR.AppBack.Jobs
{
    /// <summary>
    /// 同步数据
    /// </summary>
    public class DataJob : Job
    {
        public DataJob(UIDelegate delegate1)
            : base(delegate1)
        {
        }

        public override string MailTo
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        private static long maxId;

        /// <summary>
        /// 识别记录最大ID
        /// </summary>
        private static long MaxId
        {
            get
            {
                if (maxId == 0)
                {
                    //jobBLL bll = new jobBLL();
                    //已同步最大ID
                    maxId = jobBLL.SelectMaxId1();
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

        private static string lastCntNo;

        public override string Deal()
        {
            StringBuilder msg = new StringBuilder();

            try
            {
                jobBLL bll = new jobBLL();

                //读取待同步数据
                using (DataSet ds = bll.GetOcrDBCnt(MaxId))
                {
                    //查桥吊作业配置
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        QcSet.T_OCR_QCSETDataTable qc = bll.SelectQCSet();

                        //逐条写入二次开发数据库
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            try
                            {
                                MaxId = Convert.ToInt64(dr["dock_id"]);

                                bool flag = false;
                                dr["isarchived"] = "N";
                                foreach (QcSet.T_OCR_QCSETRow row in qc)
                                {
                                    if (row.TRVALCRANE_NO == dr["trval_no"].ToString() && row.STATUS == "作业中")
                                    {
                                        int dock_status = Convert.ToInt32(dr["dock_status"]);
                                        dr["driver_no"] = row.DRIVER_ID;
                                        dr["ship_code"] = row.SHIP_CODE;
                                        dr["c_voyage"] = dock_status == 0 ? row.OUT_VOYAGE_CODE : row.IN_VOYAGE_CODE;
                                        dr["COMMEND_ID"] = row.COMMEND_ID;
                                        dr["BERTH_NUM"] = row.BERTH_NUM;
                                        dr["ENAM"] = row.VESSELALIASE;
                                        //dr["Cstatus"] = dock_status == 0 && row.SHIPMENT_DEAL == "关" ? Convert.ToInt32(Config.CStatus.LoadWaitAuto) : Convert.ToInt32(Config.CStatus.WaitHandle);
                                        //2017-9-29 叶君腾 增加卸船自动确认
                                        dr["Cstatus"] = row.SHIPMENT_DEAL == "关" ? Convert.ToInt32(Config.CStatus.LoadWaitAuto) : Convert.ToInt32(Config.CStatus.WaitHandle);
                                        dr["SERVICECODE"] = dock_status == 0 ? row.OUTVESSELLINECODE : row.INVESSELLINECODE;


                                        if (row.WORKTYPE == "外理" || row.WORKTYPE == "现场" || row.WORKTYPE == "核封")
                                        {
                                            //外理作业模式
                                            dr["Cstatus"] = Convert.ToInt32(Config.CStatus.ExOnly);
                                            
                                        }

                                        flag = true;
                                        break;
                                    }

                                }

                                if (!flag)
                                {
                                    string TicketId = ConfigurationManager.AppSettings["TICKET_ID"];
                                    if (dr["container_no"] != null && dr["container_no"].ToString() != "未识别")
                                    {
                                        CtosResult cntInfo = CtosAPIBLL.CM005001(dr["container_no"].ToString(), TicketId);
                                        if (cntInfo.ERRORCODE == CtosAPIBLL.SUCCESSCODE && cntInfo.DS.Tables[0].Rows.Count > 0)
                                        {
                                            int dock_status = Convert.ToInt32(dr["dock_status"]);
                                            dr["ship_code"] = dock_status == 0 ? cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OUTEVESSELNAME"].ToString() : cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["INEVESSELNAME"].ToString();
                                            dr["c_voyage"] = dock_status == 0 ? cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OUTBOUNDVOY"].ToString() : cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["INBOUNDVOY"].ToString();
                                            //dr["COMMEND_ID"] = row.COMMEND_ID;
                                            //dr["BERTH_NUM"] = row.BERTH_NUM;
                                            dr["ENAM"] = dock_status == 0 ? cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OUTVELALIASE"].ToString() : cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["INVELALIASE"].ToString();
                                            //dr["Cstatus"] = dock_status == 0 && row.SHIPMENT_DEAL == "关" ? Convert.ToInt32(Config.CStatus.LoadWaitAuto) : Convert.ToInt32(Config.CStatus.WaitHandle);
                                            //2017-9-29 叶君腾 增加卸船自动确认
                                            dr["Cstatus"] =  Convert.ToInt32(Config.CStatus.LoadWaitAuto);
                                            DataTable dt = OCR.BLL.jobBLL.SelectService(dr["ENAM"].ToString());
                                            dr["SERVICECODE"] = dock_status == 0 ? dt.Rows[0]["outvessellinecode"].ToString() : dt.Rows[0]["invessellinecode"].ToString();
                                            //外理作业模式
                                            dr["Cstatus"] = Convert.ToInt32(Config.CStatus.ExOnly);

                                            flag = true;
                                        }
                                    }
                                }

                                if (!flag)
                                {
                                    dr["Cstatus"] = Convert.ToInt32(Config.CStatus.NoQcSet);
                                    dr["isarchived"] = "A";
                                    AddResult(string.Format("箱{0} {1}", dr["trval_no"].ToString(), "桥吊作业配置异常"));
                                }

                                bll.InsertCnt(dr);

                                AddResult(string.Format("箱{0} {1} {2}", dr["dock_id"].ToString(), dr["container_no"].ToString(), dr["trval_no"].ToString()));
                            }
                            catch (Exception ex)
                            {
                                SendEmail("zpmai@sctcn.com", "[OCR]分发错误",$"Error:{ex.StackTrace}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddResult(string.Format("箱 {0}", ex.Message));
            }

            return msg.ToString();
        }


    }
}
