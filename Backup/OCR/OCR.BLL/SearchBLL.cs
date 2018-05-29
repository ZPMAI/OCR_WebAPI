using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using OCR.DAL;
using OCR.Model;

namespace OCR.BLL
{
    /// <summary>
    /// 查询
    /// </summary>
    public class SearchBLL
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCnt(DateTime from, DateTime to, string isArchived, string qcno, decimal dock_status, decimal cstatus, string containerno, string truckNo)
        {
            return DAL.cctdbDAL.SelectCnt(from, to, isArchived, qcno, dock_status, cstatus, containerno, truckNo);
        }

        /// <summary>
        /// 读取图片数据
        /// </summary>
        /// <param name="dock_id">箱号ID</param>
        public static OcrPhoto.T_OCR_PHOTODataTable GetPhoto(decimal dock_id)
        {
            return DAL.cctdbDAL.SelectPhotos2(dock_id);
        }

        /// <summary>
        /// 图片服务器表
        /// </summary>
        /// <returns></returns>
        public static IDictionary<int, OcrDBPmsServer> GetOcrDBPmsServer()
        {
            return DAL.ocrdbDAL.GetOcrDBPmsServer();
        }

        /// <summary>
        /// 箱号快查
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCntQuery(string containerno)
        {
            return DAL.cctdbDAL.SelectCntQuery(containerno);
        }
        
    }
}
