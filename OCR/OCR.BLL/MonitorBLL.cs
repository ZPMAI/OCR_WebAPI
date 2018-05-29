using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCR.DAL;
using OCR.Model;

namespace OCR.BLL
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
        public DataTable SelectQcMonitor()
        {
            return DAL.cctdbDAL.SelectQcMonitor();
        }

        /// <summary>
        /// �鿴�ϳ����
        /// </summary>
        /// <returns></returns>
        public DataTable SelectTruckMonitor()
        {
            return DAL.cctdbDAL.SelectTruckMonitor();
        }

        /// <summary>
        /// �鿴���ּ��
        /// </summary>
        /// <returns></returns>
        public DataTable SelectVslMonitor()
        {
            return DAL.cctdbDAL.SelectVslMonitor();
        }

        /// <summary>
        /// �鿴����Ϣ���
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCntMonitor()
        {
            return DAL.cctdbDAL.SelectCntMonitor();
        }

        /// <summary>
        /// �鿴װ�����
        /// </summary>
        /// <returns></returns>
        public DataTable SelectLoadMonitor()
        {
            return DAL.cctdbDAL.SelectLoadMonitor();
        }


        /// <summary>
        /// ����װ������
        /// </summary>
        /// <returns></returns>
        public DataTable SelectLoaded(DateTime from, DateTime to)
        {
            return DAL.cctdbDAL.SelectLoaded(from, to);
        }

        /// <summary>
        /// ת�쳣����
        /// </summary>
        /// <returns></returns>
        public DataTable SelecteExcepMonitor(DateTime from, DateTime to)
        {
            return DAL.cctdbDAL.SelecteExcepMonitor(from, to);
        }

        /// <summary>
        /// ��QCʶ����
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

        /// <summary>
        /// ���������־
        /// </summary>
        /// <param name="row"></param>
        public void SaveErrorLog(ErrorLog.T_OCR_ERRORLOGRow row)
        {
            DAL.cctdbDAL.SaveErrorLog(row);
        }

        /// <summary>
        /// �������־
        /// </summary>
        /// <returns></returns>
        public ErrorLog.T_OCR_ERRORLOGDataTable SelectErrorLog(string qcNo, string deviceType, DateTime from, DateTime to)
        {
            return DAL.cctdbDAL.SelectErrorLog(qcNo, deviceType, from, to);
        }


    }
}
