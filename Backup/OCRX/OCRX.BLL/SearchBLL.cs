using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using OCRX.DAL;
using OCRX.Model;
using OCR.Model;

namespace OCRX.BLL
{
    /// <summary>
    /// ��ѯ
    /// </summary>
    public class SearchBLL
    {
        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCnt(DateTime from, DateTime to, string isArchived, string qcno, decimal dock_status, decimal cstatus, string containerno, string companyCode)
        {
            return DAL.cctdbDAL.SelectCnt(from, to, isArchived, qcno, dock_status, cstatus, containerno, companyCode);
        }

        /// <summary>
        /// ��ȡͼƬ����
        /// </summary>
        /// <param name="dock_id">���ID</param>
        public static OCR.Model.OcrPhoto.T_OCR_PHOTODataTable GetPhoto(decimal dock_id)
        {
            return OCR.DAL.cctdbDAL.SelectPhotos2(dock_id);
        }

        /// <summary>
        /// ͼƬ��������
        /// </summary>
        /// <returns></returns>
        public static IDictionary<int, OCR.Model.OcrDBPmsServer> GetOcrDBPmsServer()
        {
            return OCR.DAL.ocrdbDAL.GetOcrDBPmsServer();
        }

                /// <summary>
        /// ��������������˾�����ѯ��ҵ�嵥
        /// </summary>
        /// <returns></returns>
        public static DataSet SelectReport(string eName, string companyCode)
        {
            DataSet ds = OCRX.DAL.cctdbDAL.SelectReport(eName, companyCode);

            ds.Tables[0].TableName = "��ҵ�嵥";
            ds.Tables[0].Columns["dock_id"].Caption = string.Empty;
            ds.Tables[0].Columns["pms_id"].Caption = string.Empty;

            return ds;
        }

        /// <summary>
        /// ���ŵ���ҵ����
        /// </summary>
        /// <returns></returns>
        public QcSet.T_OCR_QCSETDataTable SelectQCSet()
        {
            QcSet.T_OCR_QCSETDataTable dt = DAL.cctdbDAL.SelectQCSet();

            return dt;
        }

    }
}
