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
        public int SelectMaxId()
        {
            return cctdbDAL.SelectMaxId();
        }

        /// <summary>
        /// �鵱ǰ���ID ʶ���¼
        /// </summary>
        /// <returns></returns>
        public static int SelectMaxId1()
        {
            return cctdbDAL.SelectMaxId();
        }

        /// <summary>
        /// �鵱ǰ���ID ͼƬ
        /// </summary>
        /// <returns></returns>
        public static int SelectMaxId2()
        {
            return cctdbDAL.SelectMaxIdPhoto();
        }


        /// <summary>
        /// �鵱ǰ���ID ����
        /// </summary>
        /// <returns></returns>
        public static int SelectMaxId3()
        {
            return cctdbDAL.SelectMaxIdTruck();
        }

        /// <summary>
        /// ��ȡOCR DB��ʶ������
        /// </summary>
        /// <param name="maxid">��ͬ�������ID</param>
        public DataSet GetOcrDBCnt(int maxid)
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
        public DataSet GetOcrDBPhoto1(int photo_id)
        {
            return DAL.ocrdbDAL.GetOcrDBPhoto1(photo_id);
        }

        /// <summary>
        /// ��ȡOCR DB��������
        /// </summary>
        /// <param name="truck_id">����ID</param>
        public DataSet GetOcrDBTruck(int truck_id)
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

        private const string TICKET = @"TuF5efc795YhKbQRvka7nMMrrp9Owb60ouvC6+RziV3ihuf/uVbTcoMXPmfXlJ6KLDOKb9S2wd0Jhe57slC8bhYVyK5SdAibkIA91J7vINkV/LoA3L7dv4J38OZQ870hbAz288tGjzUeM8bkfYKThhNJ8qzEbIKtRvo6v8q3M5fjjCpVvOEd6b+ELMb0+T7qD21hwH5CK0CEjRQlQadHISnuk6VUygKtzGtgiweYk6vUrwN3L3T4i7egZGRXcmRv8ByZUjXzTHQ=";

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
    }
}
