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
        public DataTable SelectCnt(DateTime from, DateTime to, string isArchived, string qcno, decimal dock_status, decimal cstatus, string containerno, string companyCode,string isdmg)
        {
            return DAL.cctdbDAL.SelectCnt(from, to, isArchived, qcno, dock_status, cstatus, containerno, companyCode,isdmg);
        }

        /// <summary>
        /// ��ѯ������������ҵ��Ϣ
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_CNTDataTable SelectDamageInfo(decimal dockid)
        {
            return OCRX.DAL.cctdbDAL.SelectCntDmgRecord(dockid);
        }

        /// <summary>
        /// ��ѯ�����������Ϣ
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_DAMAGEDataTable SelectDamageRecord(decimal dockid)
        {
            return OCRX.DAL.cctdbDAL.SelectDamageRecord(dockid);
        }
        /// <summary>
        /// ���������������Ϣ
        /// </summary>
        /// <returns></returns>
        public void InsertDamageInfo(DataSet1.T_OCRX_DAMAGERow row)
        {
            OCRX.DAL.cctdbDAL.InsertDamage(row);
        }

        /// <summary>
        /// ���µ����������Ϣ
        /// </summary>
        /// <returns></returns>
        public void UpdateDamageInfo(DataSet1.T_OCRX_DAMAGERow row)
        {
            OCRX.DAL.cctdbDAL.UpdateDamageInfo(row);
        }

        /// <summary>
        /// ɾ�������������Ϣ
        /// </summary>
        /// <returns></returns>
        public void DeleteDamageInfo(DataSet1.T_OCRX_DAMAGERow row)
        {
            OCRX.DAL.cctdbDAL.DeleteDamage(row);
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

        /// ��������������˾�����ѯ��������ҵ�嵥
        /// </summary>
        /// <returns></returns>
        public static DataSet SelectDamageReport(string eName, string companyCode)
        {
            DataSet ds = OCRX.DAL.cctdbDAL.SelectDamageReport(eName, companyCode);

            ds.Tables[0].TableName = "��������ҵ�嵥";
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

            result.TableName = "ж����ҵ�嵥";

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

                result.TableName = "װ����ҵ�嵥";
            }

           
           
            return result;
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
