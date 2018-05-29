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
    /// 查询
    /// </summary>
    public class SearchBLL
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCnt(DateTime from, DateTime to, string isArchived, string qcno, decimal dock_status, decimal cstatus, string containerno, string companyCode,string isdmg)
        {
            return DAL.cctdbDAL.SelectCnt(from, to, isArchived, qcno, dock_status, cstatus, containerno, companyCode,isdmg);
        }

        /// <summary>
        /// 查询单个残损箱作业信息
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_CNTDataTable SelectDamageInfo(decimal dockid)
        {
            return OCRX.DAL.cctdbDAL.SelectCntDmgRecord(dockid);
        }

        /// <summary>
        /// 查询单个箱残损信息
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_DAMAGEDataTable SelectDamageRecord(decimal dockid)
        {
            return OCRX.DAL.cctdbDAL.SelectDamageRecord(dockid);
        }
        /// <summary>
        /// 新增单个箱残损信息
        /// </summary>
        /// <returns></returns>
        public void InsertDamageInfo(DataSet1.T_OCRX_DAMAGERow row)
        {
            OCRX.DAL.cctdbDAL.InsertDamage(row);
        }

        /// <summary>
        /// 更新单个箱残损信息
        /// </summary>
        /// <returns></returns>
        public void UpdateDamageInfo(DataSet1.T_OCRX_DAMAGERow row)
        {
            OCRX.DAL.cctdbDAL.UpdateDamageInfo(row);
        }

        /// <summary>
        /// 删除单个箱残损信息
        /// </summary>
        /// <returns></returns>
        public void DeleteDamageInfo(DataSet1.T_OCRX_DAMAGERow row)
        {
            OCRX.DAL.cctdbDAL.DeleteDamage(row);
        }
        /// <summary>
        /// 读取图片数据
        /// </summary>
        /// <param name="dock_id">箱号ID</param>
        public static OCR.Model.OcrPhoto.T_OCR_PHOTODataTable GetPhoto(decimal dock_id)
        {
            return OCR.DAL.cctdbDAL.SelectPhotos2(dock_id);
        }

        /// <summary>
        /// 图片服务器表
        /// </summary>
        /// <returns></returns>
        public static IDictionary<int, OCR.Model.OcrDBPmsServer> GetOcrDBPmsServer()
        {
            return OCR.DAL.ocrdbDAL.GetOcrDBPmsServer();
        }

                /// <summary>
        /// 按船别名和外理公司代码查询作业清单
        /// </summary>
        /// <returns></returns>
        public static DataSet SelectReport(string eName, string companyCode)
        {
            DataSet ds = OCRX.DAL.cctdbDAL.SelectReport(eName, companyCode);

            ds.Tables[0].TableName = "作业清单";
            ds.Tables[0].Columns["dock_id"].Caption = string.Empty;
            ds.Tables[0].Columns["pms_id"].Caption = string.Empty;

            return ds;
        }

        /// 按船别名和外理公司代码查询残损箱作业清单
        /// </summary>
        /// <returns></returns>
        public static DataSet SelectDamageReport(string eName, string companyCode)
        {
            DataSet ds = OCRX.DAL.cctdbDAL.SelectDamageReport(eName, companyCode);

            ds.Tables[0].TableName = "残损箱作业清单";
            ds.Tables[0].Columns["dock_id"].Caption = string.Empty;
            ds.Tables[0].Columns["pms_id"].Caption = string.Empty;

            return ds;
        }

        public static DataTable DischargeList(string eName, string companyCode)
        {
            List<string> owners = OCRX.DAL.cctdbDAL.SelectCompanyVesselOwner(eName, companyCode, true);
            ReportDataSet.P_SCT_DischargeListDataTable result = null;
            foreach(string s in owners)
            {
                ReportDataSet.P_SCT_DischargeListDataTable d = OCRX.DAL.cctdbDAL.GetDischargeList(eName, s);
                if (result != null)
                {
                    result.Merge(d,true,MissingSchemaAction.AddWithKey);
                }
                else
                {
                    result = d;
                }
            }

            result.TableName = "卸船作业清单";

            return result;
        }

        public static DataTable LoadingList(string eName, string companyCode)
        {
            List<string> owners = OCRX.DAL.cctdbDAL.SelectCompanyVesselOwner(eName, companyCode, false);
            ReportDataSet.P_SCT_LoadListDataTable result = null;
            if (owners != null)
            {
                foreach (string s in owners)
                {
                    ReportDataSet.P_SCT_LoadListDataTable d = OCRX.DAL.cctdbDAL.GetLoadingList(eName, s);
                    if (result != null)
                    {
                        result.Merge(d);
                    }
                    else
                    {
                        result = d;
                    }
                }

                result.TableName = "装船作业清单";
            }

           
           
            return result;
        }

        /// <summary>
        /// 查桥吊作业配置
        /// </summary>
        /// <returns></returns>
        public QcSet.T_OCR_QCSETDataTable SelectQCSet()
        {
            QcSet.T_OCR_QCSETDataTable dt = DAL.cctdbDAL.SelectQCSet();

            return dt;
        }

    }
}
