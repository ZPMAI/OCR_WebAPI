using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using OCR.DAL;
using OCR.Model;

namespace OCR.BLL
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
        public DataTable SelectCnt(DateTime from, DateTime to, string isArchived, string qcno, decimal dock_status, decimal cstatus, string containerno, string truckNo)
        {
            return DAL.cctdbDAL.SelectCnt(from, to, isArchived, qcno, dock_status, cstatus, containerno, truckNo);
        }

        /// <summary>
        /// ��ȡͼƬ����
        /// </summary>
        /// <param name="dock_id">���ID</param>
        public static OcrPhoto.T_OCR_PHOTODataTable GetPhoto(decimal dock_id)
        {
            return DAL.cctdbDAL.SelectPhotos2(dock_id);
        }

        /// <summary>
        /// ͼƬ��������
        /// </summary>
        /// <returns></returns>
        public static IDictionary<int, OcrDBPmsServer> GetOcrDBPmsServer()
        {
            return DAL.ocrdbDAL.GetOcrDBPmsServer();
        }

        /// <summary>
        /// ��ſ��
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCntQuery(string containerno)
        {
            return DAL.cctdbDAL.SelectCntQuery(containerno);
        }
        
    }
}
