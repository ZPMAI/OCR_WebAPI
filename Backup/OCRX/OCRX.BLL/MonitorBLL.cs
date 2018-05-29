using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCRX.DAL;
using OCRX.Model;

namespace OCRX.BLL
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
        public DataTable SelectQcMonitor(string companyCode)
        {
            return OCRX.DAL.cctdbDAL.SelectQcMonitor(companyCode);
        }

        /// <summary>
        /// 查看箱信息监控
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCntMonitor(string companyCode)
        {
            return OCRX.DAL.cctdbDAL.SelectCntMonitor(companyCode);
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

    }
}
