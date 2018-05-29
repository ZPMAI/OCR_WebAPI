using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCR.DAL;
using OCR.Model;

namespace OCR.BLL
{
    /// <summary>
    /// OCR自动任务
    /// </summary>
    public class jobBLL
    {
        /// <summary>
        /// 查当前最大ID
        /// </summary>
        /// <returns></returns>
        public int SelectMaxId()
        {
            return cctdbDAL.SelectMaxId();
        }

        /// <summary>
        /// 查当前最大ID 识别记录
        /// </summary>
        /// <returns></returns>
        public static int SelectMaxId1()
        {
            return cctdbDAL.SelectMaxId();
        }

        /// <summary>
        /// 查当前最大ID 图片
        /// </summary>
        /// <returns></returns>
        public static int SelectMaxId2()
        {
            return cctdbDAL.SelectMaxIdPhoto();
        }


        /// <summary>
        /// 查当前最大ID 车号
        /// </summary>
        /// <returns></returns>
        public static int SelectMaxId3()
        {
            return cctdbDAL.SelectMaxIdTruck();
        }

        /// <summary>
        /// 读取OCR DB新识别数据
        /// </summary>
        /// <param name="maxid">已同步的最大ID</param>
        public DataSet GetOcrDBCnt(int maxid)
        {
            return ocrdbDAL.GetOcrDBCnt(maxid);
        }

        /// <summary>
        /// 插入新识别记录
        /// </summary>
        /// <returns></returns>
        public void InsertCnt(DataRow dr)
        {
            cctdbDAL.InsertCnt(dr);
        }

        /// <summary>
        /// 读取OCR DB图片数据
        /// </summary>
        /// <param name="dock_id">箱号ID</param>
        public DataSet GetOcrDBPhoto(int dock_id)
        {
            return ocrdbDAL.GetOcrDBPhoto(dock_id);
        }

               
        /// <summary>
        /// 插入新图片记录
        /// </summary>
        /// <returns></returns>
        public void InsertPhoto(DataRow dr)
        {
            cctdbDAL.InsertPhoto(dr);
        }

        /// <summary>
        /// 插入新车号记录
        /// </summary>
        /// <returns></returns>
        public void InsertTruck(DataRow dr)
        {
            cctdbDAL.InsertTruck(dr);
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

        /// <summary>
        /// 读取OCR DB图片数据
        /// </summary>
        /// <param name="photo_id">图片ID</param>
        public DataSet GetOcrDBPhoto1(int photo_id)
        {
            return DAL.ocrdbDAL.GetOcrDBPhoto1(photo_id);
        }

        /// <summary>
        /// 读取OCR DB车号数据
        /// </summary>
        /// <param name="truck_id">车号ID</param>
        public DataSet GetOcrDBTruck(int truck_id)
        {
            return DAL.ocrdbDAL.GetOcrDBTruck(truck_id);
        }

        private static SysParms parms;
        /// <summary>
        /// 系统参数
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
        /// 查需自动处理装船的记录
        /// </summary>
        /// <returns></returns>
        public OcrCnt.T_OCR_CNTDataTable SelectLoadAuto()
        {
            return DAL.cctdbDAL.SelectLoadAuto();
        }

        /// <summary>
        /// 查需自动处理装船的记录 按QC
        /// </summary>
        /// <returns></returns>
        public OcrCnt.T_OCR_CNTDataTable SelectLoadAuto(string qc)
        {
            return DAL.cctdbDAL.SelectLoadAuto(qc);
        }

        /// <summary>
        /// 查桥吊作业配置
        /// </summary>
        /// <returns></returns>
        public QcSet.T_OCR_QCSETRow SelectQCSet(string qcno)
        {
            return DAL.cctdbDAL.SelectQCSet(qcno);
        }

        /// <summary>
        /// 更新识别记录处理结果
        /// </summary>
        /// <param name="row"></param>
        public void UpdateCntStatus(OcrCnt.T_OCR_CNTRow row)
        {
            DAL.cctdbDAL.UpdateCntStatus(row);
        }

        /// <summary>
        /// 查错误代码含义
        /// </summary>
        /// <returns></returns>
        public string SelectErrcode(int errCode)
        {
            return DAL.ctosDAL.SelectErrcode(errCode);
        }

        /// <summary>
        /// 归档
        /// </summary>
        /// <returns></returns>
        public void Archive()
        {
            DAL.cctdbDAL.Archive();
        }

        /// <summary>
        /// 测试识别数据库能否正常访问
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
        /// 测试CTOS OCR能否正常访问
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
        /// 更新识别记录move id
        /// </summary>
        /// <param name="row"></param>
        public void UpdateMoveId(OcrCnt.T_OCR_CNTRow row)
        {
            DAL.cctdbDAL.UpdateMoveId(row);
        }


        /// <summary>
        /// 查ISOCODE
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
