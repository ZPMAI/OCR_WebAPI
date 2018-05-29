using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCRX.DAL;
using OCRX.Model;

namespace OCRX.BLL
{
    /// <summary>
    /// ʵʱ���
    /// </summary>
    public class MonitorBLL
    {
        /// <summary>
        /// ��QC���
        /// </summary>
        /// <returns></returns>
        public DataTable SelectQcMonitor(string companyCode)
        {
            return OCRX.DAL.cctdbDAL.SelectQcMonitor(companyCode);
        }

        /// <summary>
        /// �鿴����Ϣ���
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCntMonitor(string companyCode)
        {
            return OCRX.DAL.cctdbDAL.SelectCntMonitor(companyCode);
        }

        /// <summary>
        /// ���ڼ��
        /// </summary>
        /// <returns></returns>
        public DataTable SelectVslMonitor2()
        {
            return DAL.cctdbDAL.SelectVslMonitor2();
        }

        /// <summary>
        /// QC���
        /// </summary>
        /// <returns></returns>
        public DataTable SelectQcMonitor2()
        {
            return DAL.cctdbDAL.SelectQcMonitor2();
        }

    }
}
