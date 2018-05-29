using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCR.DAL;
using OCR.Model;

namespace OCR.BLL
{
    /// <summary>
    /// OCR�Զ�����
    /// </summary>
    public class jobBLL
    {
        /// <summary>
        /// �鵱ǰ���ID
        /// </summary>
        /// <returns></returns>
        public long SelectMaxId()
        {
            return cctdbDAL.SelectMaxId();
        }

        /// <summary>
        /// �鵱ǰ���ID ʶ���¼
        /// </summary>
        /// <returns></returns>
        public static long SelectMaxId1()
        {
            return cctdbDAL.SelectMaxId();
        }

        /// <summary>
        /// �鵱ǰ���ID ͼƬ
        /// </summary>
        /// <returns></returns>
        public static long SelectMaxId2()
        {
            return cctdbDAL.SelectMaxIdPhoto();
        }


        /// <summary>
        /// �鵱ǰ���ID ����
        /// </summary>
        /// <returns></returns>
        public static long SelectMaxId3()
        {
            return cctdbDAL.SelectMaxIdTruck();
        }

        /// <summary>
        /// ��ȡOCR DB��ʶ������
        /// </summary>
        /// <param name="maxid">��ͬ�������ID</param>
        public DataSet GetOcrDBCnt(long maxid)
        {
            return ocrdbDAL.GetOcrDBCnt(maxid);
        }

        /// <summary>
        /// ������ʶ���¼
        /// </summary>
        /// <returns></returns>
        public void InsertCnt(DataRow dr)
        {
            cctdbDAL.InsertCnt(dr);
        }

        /// <summary>
        /// ��ȡOCR DBͼƬ����
        /// </summary>
        /// <param name="dock_id">���ID</param>
        public DataSet GetOcrDBPhoto(int dock_id)
        {
            return ocrdbDAL.GetOcrDBPhoto(dock_id);
        }

               
        /// <summary>
        /// ������ͼƬ��¼
        /// </summary>
        /// <returns></returns>
        public void InsertPhoto(DataRow dr)
        {
            cctdbDAL.InsertPhoto(dr);
        }

        /// <summary>
        /// �����³��ż�¼
        /// </summary>
        /// <returns></returns>
        public void InsertTruck(DataRow dr)
        {
            cctdbDAL.InsertTruck(dr);
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

        /// <summary>
        /// ��ȡOCR DBͼƬ����
        /// </summary>
        /// <param name="photo_id">ͼƬID</param>
        public DataSet GetOcrDBPhoto1(long photo_id)
        {
            return DAL.ocrdbDAL.GetOcrDBPhoto1(photo_id);
        }

        /// <summary>
        /// ��ȡOCR DB��������
        /// </summary>
        /// <param name="truck_id">����ID</param>
        public DataSet GetOcrDBTruck(long truck_id)
        {
            return DAL.ocrdbDAL.GetOcrDBTruck(truck_id);
        }

        private static SysParms parms;
        /// <summary>
        /// ϵͳ����
        /// </summary>
        public static SysParms Parms
        {
            get
            {
                if (parms == null)
                {
                    parms = cctdbDAL.SelectParams();
                }

                return parms;
            }
        }

        /// <summary>
        /// �����Զ�����װ���ļ�¼
        /// </summary>
        /// <returns></returns>
        public OcrCnt.T_OCR_CNTDataTable SelectLoadAuto()
        {
            return DAL.cctdbDAL.SelectLoadAuto();
        }

        /// <summary>
        /// �����Զ�����װ���ļ�¼ ��QC
        /// </summary>
        /// <returns></returns>
        public OcrCnt.T_OCR_CNTDataTable SelectLoadAuto(string qc)
        {
            return DAL.cctdbDAL.SelectLoadAuto(qc);
        }

        /// <summary>
        /// ���ŵ���ҵ����
        /// </summary>
        /// <returns></returns>
        public QcSet.T_OCR_QCSETRow SelectQCSet(string qcno)
        {
            return DAL.cctdbDAL.SelectQCSet(qcno);
        }

        /// <summary>
        /// ����ʶ���¼������
        /// </summary>
        /// <param name="row"></param>
        public void UpdateCntStatus(OcrCnt.T_OCR_CNTRow row)
        {
            DAL.cctdbDAL.UpdateCntStatus(row);
        }

        /// <summary>
        /// �������뺬��
        /// </summary>
        /// <returns></returns>
        public string SelectErrcode(int errCode)
        {
            return DAL.ctosDAL.SelectErrcode(errCode);
        }

        /// <summary>
        /// �鵵
        /// </summary>
        /// <returns></returns>
        public void Archive()
        {
            DAL.cctdbDAL.Archive();
        }

        /// <summary>
        /// ����ʶ�����ݿ��ܷ���������
        /// </summary>
        /// <returns></returns>
        public string CheckOCRDB()
        {
            try
            {
                ocrdbDAL.GetOcrDBPmsServer();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private const string TICKET = @"kNCUj6D3+QwmRjohRS+bSvn9HEFpiht9zjSA4lL/2xu5S2mr0XRHH3z3peTnFoZqfoGcW2wIc+OXZce8zIfYWzT9sYGY/KSUn+SxhOGF0nDtLKskCCxSiZAFznC/MVkJL2MydABYRUazVg3n9MxaVqdqN1oxGWIPJPSUZqjMvsUoGvpPel31be/FgQcsMWuiwVt+PxOe3fChHK5uG1Z3OMTNIXNj9qdc1YOSRmxFVfJZak1Y8++3GqY70W6EeptFtWQYp/8r5dE=";

        /// <summary>
        /// ����CTOS OCR�ܷ���������
        /// </summary>
        /// <returns></returns>
        public string CheckCTOSAPI()
        {
            try
            {
                CtosAPIBLL.CM005001("TEST1234567", TICKET);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// ����ʶ���¼move id
        /// </summary>
        /// <param name="row"></param>
        public void UpdateMoveId(OcrCnt.T_OCR_CNTRow row)
        {
            DAL.cctdbDAL.UpdateMoveId(row);
        }


        /// <summary>
        /// ��ISOCODE
        /// </summary>
        /// <param name="isocode"></param>
        /// <returns></returns>
        public IsoCode CheckIsoCode(string isocode)
        {
            IsoCode isoCode = DAL.ctosDAL.SelectIsoCode(isocode);

            //if (isoCode != null)
            //{
            //    if (isoCode.CONTAINERSIZE > 20)
            //    {
            //        cntCtrl.txtPosOnTruck.Text = "M";
            //    }
            //}
            return isoCode;
        }

        public static DataTable GetTruckPos(string truck, string truckSeq)
        {
            return DAL.ctosDAL.SelectTruck(truck, truckSeq);
        }

        public static DataTable SelectService(string velaliase)
        {
            return DAL.cctdbDAL.SelectService(velaliase);
        }
    }
}
