using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCR.DAL;
using OCR.Model;

namespace OCR.BLL
{
    /// <summary>
    /// 实时监控
    /// </summary>
    public class MonitorBLL
    {
        /// <summary>
        /// 查QC监控
        /// </summary>
        /// <returns></returns>
        public DataTable SelectQcMonitor()
        {
            return DAL.cctdbDAL.SelectQcMonitor();
        }

        /// <summary>
        /// 查看拖车监控
        /// </summary>
        /// <returns></returns>
        public DataTable SelectTruckMonitor()
        {
            return DAL.cctdbDAL.SelectTruckMonitor();
        }

        /// <summary>
        /// 查看班轮监控
        /// </summary>
        /// <returns></returns>
        public DataTable SelectVslMonitor()
        {
            return DAL.cctdbDAL.SelectVslMonitor();
        }

        /// <summary>
        /// 查看箱信息监控
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCntMonitor()
        {
            return DAL.cctdbDAL.SelectCntMonitor();
        }

        /// <summary>
        /// 查看装船监控
        /// </summary>
        /// <returns></returns>
        public DataTable SelectLoadMonitor()
        {
            return DAL.cctdbDAL.SelectLoadMonitor();
        }


        /// <summary>
        /// 查已装船数据
        /// </summary>
        /// <returns></returns>
        public DataTable SelectLoaded(DateTime from, DateTime to)
        {
            return DAL.cctdbDAL.SelectLoaded(from, to);
        }

        /// <summary>
        /// 转异常数据
        /// </summary>
        /// <returns></returns>
        public DataTable SelecteExcepMonitor(DateTime from, DateTime to)
        {
            return DAL.cctdbDAL.SelecteExcepMonitor(from, to);
        }

        /// <summary>
        /// 查QC识别率
        /// </summary>
        /// <returns></returns>
        public decimal SelectQcRate(string qcNo, int rateCntType)
        {
            try
            {
                using (DataTable dt = DAL.cctdbDAL.SelectQcRate(qcNo, rateCntType))
                {
                    if (dt.Rows.Count > 0)
                    {
                        return Convert.ToDecimal(dt.Rows[0]["rate"]);
                    }
                }

                return decimal.Zero;
            }
            catch (Exception ex)
            {
                return decimal.Zero;
            }
        }


        /// <summary>
        /// 船期监控
        /// </summary>
        /// <returns></returns>
        public DataTable SelectVslMonitor2()
        {
            return DAL.cctdbDAL.SelectVslMonitor2();
        }

        /// <summary>
        /// QC监控
        /// </summary>
        /// <returns></returns>
        public DataTable SelectQcMonitor2()
        {
            return DAL.cctdbDAL.SelectQcMonitor2();
        }

        /// <summary>
        /// 保存错误日志
        /// </summary>
        /// <param name="row"></param>
        public void SaveErrorLog(ErrorLog.T_OCR_ERRORLOGRow row)
        {
            DAL.cctdbDAL.SaveErrorLog(row);
        }

        /// <summary>
        /// 查错误日志
        /// </summary>
        /// <returns></returns>
        public ErrorLog.T_OCR_ERRORLOGDataTable SelectErrorLog(string qcNo, string deviceType, DateTime from, DateTime to)
        {
            return DAL.cctdbDAL.SelectErrorLog(qcNo, deviceType, from, to);
        }


    }
}
