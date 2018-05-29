using CCT.SystemFramework.Data;
using OCR.Model;
using OCRX.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCRX.Web.DAL
{
    public class webDAL
    {

        /// <summary>
        /// 查所有外理公司代码
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_CODataTable SelectCompany()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_company", CommandType.StoredProcedure);

            DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CO.TableName });
            return ds.T_OCRX_CO;
        }

        /// <summary>
        /// 新增外理公司
        /// </summary>
        /// <param name="row"></param>
        public static void InsertCompany(DataSet1.T_OCRX_CORow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_company", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 删除外理公司
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteCompany(DataSet1.T_OCRX_CORow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_company", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 按外理公司代码查找
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_CODataTable SelectCompany(string companyCode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_companybycode", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CO.TableName });

            if (ds.T_OCRX_CO.Count == 0) return null;
            else return ds.T_OCRX_CO;
        }


        /// <summary>
        /// 查所有外理公司用户
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_USERSDataTable SelectUsers()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_users", CommandType.StoredProcedure);

            DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_USERS.TableName });
            return ds.T_OCRX_USERS;
        }

        /// <summary>
        /// 按用户名查找
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_USERSDataTable SelectUser(string userId)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_user", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_USERID"]).Value = userId;

            DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_USERS.TableName });

            if (ds.T_OCRX_USERS.Count == 0) return null;
            else return ds.T_OCRX_USERS;
        }

        /// <summary>
        /// 新增外理公司用户
        /// </summary>
        /// <param name="row"></param>
        public static void InsertUsers(DataSet1.T_OCRX_USERSRow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_users", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 删除外理公司用户
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteUsers(DataSet1.T_OCRX_USERSRow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_users", CommandType.StoredProcedure, row);
        }


        /// <summary>
        /// 查所有班轮分发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_VESSELDataTable SelectVessel()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_vessel", CommandType.StoredProcedure);

            DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_VESSEL.TableName });
            return ds.T_OCRX_VESSEL;
        }

        /// <summary>
        /// 新增班轮分发规则
        /// </summary>
        /// <param name="row"></param>
        public static void InsertVessel(DataSet1.T_OCRX_VESSELRow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_vessel", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 修改班轮分发规则
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateVessel(DataSet1.T_OCRX_VESSELRow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_vessel", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 删除班轮分发规则
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteVessel(DataSet1.T_OCRX_VESSELRow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_vessel", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 按航线查班轮分发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_VESSELDataTable SelectVesselByService(string serviceCode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_vesselbyservice", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_servicecode"]).Value = serviceCode;

            DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_VESSEL.TableName });
            return ds.T_OCRX_VESSEL;
        }

        /// <summary>
        /// 按航线查班轮分发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_VESSELDataTable SelectVesselByCompany(string serviceCode, string companyCode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_vesselbycompany", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_servicecode"]).Value = serviceCode;
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_VESSEL.TableName });
            return ds.T_OCRX_VESSEL;
        }

        /// <summary>
        /// 按航线查班轮分发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_VESSELDataTable SelectVesselByBoth(string companyCode, string serviceCode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_vesselbycompany", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;
            ((IDataParameter)command.Parameters["p_servicecode"]).Value = serviceCode;

            DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_VESSEL.TableName });
            return ds.T_OCRX_VESSEL;
        }


        /// <summary>
        /// 查所有驳船分发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_BARGEDataTable SelectBarge(string companyCode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_barge", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_BARGE.TableName });
            return ds.T_OCRX_BARGE;
        }

        /// <summary>
        /// 新增驳船分发规则
        /// </summary>
        /// <param name="row"></param>
        public static void InsertBarge(DataSet1.T_OCRX_BARGERow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_barge", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 修改驳船分发规则
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateBarge(DataSet1.T_OCRX_BARGERow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_barge", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 删除驳船分发规则
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteBarge(DataSet1.T_OCRX_BARGERow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_barge", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 按船代查驳船分发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_BARGEDataTable SelectBargeByAgent(string serviceCode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_bargebyagent", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_shipagent"]).Value = serviceCode;

            DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_BARGE.TableName });
            return ds.T_OCRX_BARGE;
        }

        /// <summary>
        /// 查取系统参数
        /// </summary>
        /// <returns></returns>
        public static SysParms SelectParams()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_params", CommandType.StoredProcedure);
            using (DataSet ds = OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SysParms parms = new SysParms();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        switch (dr["paramname"].ToString())
                        {
                            case "CONFIDENCELEVEL":
                                parms.CONFIDENCELEVEL = dr["paramvalue"].ToString();
                                break;
                            case "CONTAINERINFOKEEPTIME":
                                parms.CONTAINERINFOKEEPTIME = dr["paramvalue"].ToString();
                                break;
                            case "CTOSIP":
                                parms.CTOSIP = dr["paramvalue"].ToString();
                                break;
                            case "DEVICEIP":
                                parms.DEVICEIP = dr["paramvalue"].ToString();
                                break;
                            case "DEVICEPORT":
                                parms.DEVICEPORT = dr["paramvalue"].ToString();
                                break;
                            case "DISPORT":
                                parms.DISPORT = dr["paramvalue"].ToString();
                                break;
                            case "LOADPORT":
                                parms.LOADPORT = dr["paramvalue"].ToString();
                                break;
                            case "ONBORADSWITCH":
                                parms.ONBORADSWITCH = dr["paramvalue"].ToString();
                                break;
                            case "PASSWORD":
                                parms.PASSWORD = dr["paramvalue"].ToString();
                                break;
                            case "TICKETID":
                                parms.TICKETID = dr["paramvalue"].ToString();
                                break;
                            case "USERNAME":
                                parms.USERNAME = dr["paramvalue"].ToString();
                                break;
                        }
                    }


                    return parms;
                }
            }

            return null;
        }


        /// <summary>
        /// 查所有待分发的识别记录
        /// </summary>
        /// <returns></returns>
        public static OCR.Model.OcrCnt.T_OCR_CNTDataTable SelectDispatch()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_dispatch", CommandType.StoredProcedure);

            OCR.Model.OcrCnt ds = new OCR.Model.OcrCnt();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });
            return ds.T_OCR_CNT;
        }

        /// <summary>
        /// 分发识别记录
        /// </summary>
        /// <param name="row"></param>
        public static void InsertCntx(DataSet1.T_OCRX_CNTRow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_cntx", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 更新识别记录分发结果
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateDispatched(decimal dock_id, string lineCode, string companyCode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_update_dispatcthed", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_dock_id"]).Value = dock_id;
            ((IDataParameter)command.Parameters["p_linecode"]).Value = lineCode;
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            OCRDbContext.Runner.ExecuteNonQuery(Config.ConnectionString, command);
        }

        /// <summary>
        /// 查所有待分发的识别记录 成功确认有箱主的识别记录
        /// </summary>
        /// <returns></returns>
        public static OCR.Model.OcrCnt.T_OCR_CNTDataTable SelectDispatch2()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_dispatch2", CommandType.StoredProcedure);

            OCR.Model.OcrCnt ds = new OCR.Model.OcrCnt();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });
            return ds.T_OCR_CNT;
        }

        /// <summary>
        /// 查所有待分发的识别记录 成功确认无箱主的识别记录
        /// </summary>
        /// <returns></returns>
        public static OCR.Model.OcrCnt.T_OCR_CNTDataTable SelectDispatch3()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_dispatch3", CommandType.StoredProcedure);

            OCR.Model.OcrCnt ds = new OCR.Model.OcrCnt();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });
            return ds.T_OCR_CNT;
        }

        /// <summary>
        /// 查下一条识别记录
        /// </summary>
        /// <returns></returns>
        public static OCRX.Model.DataSet1.T_OCRX_CNTDataTable SelectNextRecord(string userName, string companyCode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_nextrecord", CommandType.StoredProcedure);

            ((IDataParameter)command.Parameters["p_username"]).Value = userName;
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            OCRX.Model.DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CNT.TableName });
            return ds.T_OCRX_CNT;
        }

        /// <summary>
        /// 查剩余记录数
        /// </summary>
        /// <returns></returns>
        public static int SelectLeft(string companyCode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_left", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            using (DataSet ds = OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command))
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0]["left"]);
            }
        }

        /// <summary>
        /// 更新识别记录处理结果
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateCntStatus(DataSet1.T_OCRX_CNTRow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_cntstatus3", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 更新回退记录处理结果
        /// </summary>
        /// <param name="row"></param>
        public static void RollbackCntStatus(DataSet1.T_OCRX_CNTRow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_cntstatus4", CommandType.StoredProcedure, row);
        }


        /// <summary>
        /// 查对应的图片
        /// </summary>
        /// <returns></returns>
        public static OcrPhoto.T_OCR_PHOTODataTable SelectPhotos(decimal dock_id)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_photo", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_dock_id"]).Value = dock_id;

            OcrPhoto ds = new OcrPhoto();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_PHOTO.TableName });

            return ds.T_OCR_PHOTO;
        }


        /// <summary>
        /// 查下一个seq_ocrx值
        /// </summary>
        /// <returns></returns>
        public static Int64 SelectSeqOcrx()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_seq_ocrx", CommandType.StoredProcedure);

            using (DataSet ds = OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command))
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt64(ds.Tables[0].Rows[0]["val"]);
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 复制图片
        /// </summary>
        /// <returns></returns>
        public static void CopyPhotos(decimal dock_id1, decimal dock_id2)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_copy_photos", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_dock_id1"]).Value = dock_id1;
            ((IDataParameter)command.Parameters["p_dock_id2"]).Value = dock_id2;

            OCRDbContext.Runner.ExecuteNonQuery(Config.ConnectionString, command);
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCnt(DateTime from, DateTime to, string isArchived, string qcno, decimal dock_status, decimal cstatus, string containerno, string companyCode, string isdmg)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_cnt3", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_from"]).Value = from;
            ((IDataParameter)command.Parameters["p_to"]).Value = to;
            ((IDataParameter)command.Parameters["p_isarchived"]).Value = isArchived;
            ((IDataParameter)command.Parameters["p_qcno"]).Value = qcno;
            ((IDataParameter)command.Parameters["p_dock_status"]).Value = dock_status;
            ((IDataParameter)command.Parameters["p_cstatus"]).Value = cstatus;
            ((IDataParameter)command.Parameters["p_containerno"]).Value = containerno;
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;
            ((IDataParameter)command.Parameters["p_isdmg"]).Value = isdmg;

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 按船别名和外理公司代码查询残损作业清单
        /// </summary>
        /// <returns></returns>
        public static DataSet SelectDamageReport(string eName, string companyCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_damagereport", CommandType.StoredProcedure);

            ((IDataParameter)command.Parameters["p_ename"]).Value = eName;
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command);
        }

        /// <summary>
        /// 按船别名和外理公司代码查询作业清单
        /// </summary>
        /// <returns></returns>
        public static DataSet SelectReport(string eName, string companyCode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_report", CommandType.StoredProcedure);

            ((IDataParameter)command.Parameters["p_ename"]).Value = eName;
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command);
        }

        /// <summary>
        /// 查看箱信息监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCntMonitor(string companyCode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_cntmonitor", CommandType.StoredProcedure);

            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }


        /// <summary>
        /// 查QC监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectQcMonitor(string companyCode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_qcmonitor", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;
            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查所有未处理的异常
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_CNTDataTable SelectExcep(string companyCode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_excep", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CNT.TableName });

            return ds.T_OCRX_CNT;
        }

        /// <summary>
        /// 更新异常处理结果
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateExcepStatus(DataSet1.T_OCRX_CNTRow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_excepstatus", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 查桥吊作业配置
        /// </summary>
        /// <returns></returns>
        public static QcSet.T_OCR_QCSETDataTable SelectQCSet()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_qcset", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_nextexectime"]).Value = execTime;

            QcSet ds = new QcSet();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_QCSET.TableName });
            return ds.T_OCR_QCSET;
        }

        /// <summary>
        /// 船期监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectVslMonitor2()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "ocr.pkg_ocr.p_select_vslmonitor21", CommandType.StoredProcedure);

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// QC监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectQcMonitor2()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "ocr.pkg_ocr.p_select_vslmonitor31", CommandType.StoredProcedure);

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查航线
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectService()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "ocr.pkg_ocrx.p_select_service", CommandType.StoredProcedure);

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 按船别名查驳船分发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_BARGEDataTable SelectBargeByVelaliase(string velaliase)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_bargebyvelaliase", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_velaliase"]).Value = velaliase;

            DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_BARGE.TableName });
            return ds.T_OCRX_BARGE.Count > 0 ? ds.T_OCRX_BARGE : null;
        }

        /// <summary>
        /// 查箱是否确认过
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCntConfirmed(string velaliase, string contNo, decimal dockStatus)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_cntconfirmed", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_velaliase"]).Value = velaliase;
            ((IDataParameter)command.Parameters["p_contno"]).Value = contNo;
            ((IDataParameter)command.Parameters["p_inout"]).Value = dockStatus;

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查是否是箱号溢卸
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectOverDisCnt(decimal containerid)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_overdiscnt", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_containerid"]).Value = containerid;

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }


        /// <summary>
        /// 按船别名查航线和船型
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectService(string velaliase)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.p_select_service", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_velaliase"]).Value = velaliase;

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查残损方位代码
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectDamagePositionCode()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.p_select_damagecode_P", CommandType.StoredProcedure);

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查残损类型代码
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectDamageTypeCode()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.p_select_damagecode_T", CommandType.StoredProcedure);

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查QC作业记录
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectQCWorkRecord(string qcNo, string containerId)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.p_select_qcwork", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_qcno"]).Value = qcNo;
            ((IDataParameter)command.Parameters["p_containerid"]).Value = containerId;

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查询单个残损箱作业信息
        /// </summary>
        /// <param name="row"></param>
        public static OCRX.Model.DataSet1.T_OCRX_CNTDataTable SelectCntDmgRecord(decimal dockid)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "pkg_ocrx.p_select_damagerecord", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_dock_id"]).Value = dockid;

            OCRX.Model.DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CNT.TableName });
            return ds.T_OCRX_CNT;
        }

        /// <summary>
        /// 查询单个箱残损信息
        /// </summary>
        /// <param name="row"></param>
        public static OCRX.Model.DataSet1.T_OCRX_DAMAGEDataTable SelectDamageRecord(decimal dockid)
        {

            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "pkg_ocrx.p_select_damagerecord2", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_dock_id"]).Value = dockid;

            OCRX.Model.DataSet1 ds = new DataSet1();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_DAMAGE.TableName });
            return ds.T_OCRX_DAMAGE;
        }


        /// <summary>
        /// 查询残损记录(内理)
        /// </summary>
        /// <param name="row"></param>
        public static DataSet1.T_OCRX_DAMAGEDataTable SelectDmgRecord(string velaliase)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_damagecnt", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_velaliase"]).Value = velaliase;

            OCRX.Model.DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_DAMAGE.TableName });
            return ds.T_OCRX_DAMAGE;
        }

        /// <summary>
        /// 查询残损记录(外理)
        /// </summary>
        /// <param name="row"></param>
        public static DataSet1.T_OCRX_DAMAGEDataTable SelectDmgRecordEx(string velaliase)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_damagecntx", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_velaliase"]).Value = velaliase;

            OCRX.Model.DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_DAMAGE.TableName });
            return ds.T_OCRX_DAMAGE;
        }

        /// <summary>
        /// 查询同一吊次记录
        /// </summary>
        /// <param name="row"></param>
        public static OCRX.Model.DataSet1.T_OCRX_CNTDataTable SelectCnt(decimal dockid)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_cnt2", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_dockid"]).Value = dockid;

            OCRX.Model.DataSet1 ds = new DataSet1();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CNT.TableName });
            return ds.T_OCRX_CNT;
        }

        /// <summary>
        /// 插入残损记录
        /// </summary>
        /// <param name="row"></param>
        public static void InsertDamage(DataSet1.T_OCRX_DAMAGERow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_damage", CommandType.StoredProcedure, row);
        }


        /// <summary>
        /// 更新残损记录信息
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateDamageInfo(DataSet1.T_OCRX_DAMAGERow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_damage", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 更新残损记录状态
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateDamageStatus(DataSet1.T_OCRX_DAMAGERow row)
        {
            OCRDbContext.Runner.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_damagecntx", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 删除残损记录
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteDamage(DataSet1.T_OCRX_DAMAGERow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_damage", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 单箱查询
        /// </summary>
        /// <returns></returns>
        public static DataTable GetContainerInfo(string containerno)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.p_get_containerinfo", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_containerno"]).Value = containerno;

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 装船查询
        /// </summary>
        /// <returns></returns>
        public static DataTable GetLoadingList(string vslaliase, string lineid)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.P_SCT_LoadingList", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_vslaliase"]).Value = vslaliase;
            ((IDataParameter)command.Parameters["p_line_id"]).Value = lineid;

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 卸船查询
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDischargeList(string vslaliase, string lineid)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.P_SCT_DischargeList", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_vslaliase"]).Value = vslaliase;
            ((IDataParameter)command.Parameters["p_line_id"]).Value = lineid;

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 获取船下的船东
        /// </summary>
        /// <returns></returns>
        public static List<string> SelectVesselOwners(string vslaliase)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.p_getvesselowner", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_vslaliase"]).Value = vslaliase;
            DataSet ds = OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command);
            List<string> owners = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                owners.Add(dr["CONTAINEROWNER"].ToString());
            }

            return new List<string>(owners);
        }

        /// <summary>
        /// 获取该公司在该船的分发船东
        /// </summary>
        /// <param name="vslaliase"></param>
        /// <param name="CompanyCode"></param>
        /// <param name="disc"></param>
        /// <returns></returns>
        public static List<string> SelectCompanyVesselOwner(string vslaliase, string CompanyCode, bool disc)
        {
            if (vslaliase.StartsWith("8"))
            {
                DataSet1.T_OCRX_BARGEDataTable barge = SelectBargeByVelaliase(vslaliase);

                if (barge.Count > 0)
                {
                    if (barge[0].COMPANYCODE == CompanyCode)
                    {
                        List<string> line = new List<string> { "ALL" };
                        return line;
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }

            DataTable vslService = SelectService(vslaliase);
            string service = string.Empty;
            if (vslService.Rows.Count > 0)
            {
                service = disc ? vslService.Rows[0]["INVESSELLINECODE"].ToString() : vslService.Rows[0]["OUTVESSELLINECODE"].ToString();
            }
            else
            {
                return null;
            }
            DataSet1.T_OCRX_VESSELDataTable serviceRule = SelectVesselByService(service);
            DataSet1.T_OCRX_VESSELDataTable companyRule = SelectVesselByCompany(service, CompanyCode);
            List<string> VesselOwners = SelectVesselOwners(vslaliase);

            HashSet<string> others = new HashSet<string>();
            HashSet<string> company = new HashSet<string>();
            HashSet<string> vessel = new HashSet<string>(VesselOwners);

            foreach (DataSet1.T_OCRX_VESSELRow dr in serviceRule)
            {
                if (dr.COMPANYCODE != CompanyCode)
                {
                    string[] owners = dr.LINECODE.Split(',');
                    foreach (string s in owners)
                    {
                        others.Add(s);
                    }
                }
            }
            foreach (DataSet1.T_OCRX_VESSELRow dr in companyRule)
            {
                string[] owners = dr.LINECODE.Split(',');
                foreach (string s in owners)
                {
                    company.Add(s);
                }
            }

            company.ExceptWith(others);
            vessel.IntersectWith(company);
            return vessel.ToList<string>();
        }

        public static DataTable SelectocrdbPmsServer()
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "SELECT * FROM ocr.T_OCR_PMS_SERVER", CommandType.Text);

            return OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查桥吊作业配置
        /// </summary>
        /// <returns></returns>
        public static QcSet.T_OCR_QCSETDataTable SelectQCSet(string qcno)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_qcset1", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_qc"]).Value = qcno;
            QcSet ds = new QcSet();
            OCRDbContext.Runner.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_QCSET.TableName });

            if (ds.T_OCR_QCSET.Count > 0) return ds.T_OCR_QCSET;
            else return null;
        }

        /// <summary>
        /// 查isocode
        /// </summary>
        /// <returns></returns>
        public static IsoCode SelectIsoCode(string isocode)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.p_select_isocode", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_isocode"]).Value = isocode;
            using (DataSet ds = OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionStringCTOSP, command))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return new IsoCode(Convert.ToDecimal(ds.Tables[0].Rows[0]["containersize"]),
                        ds.Tables[0].Rows[0]["containertype"].ToString(), Convert.ToDecimal(ds.Tables[0].Rows[0]["containerheight"]));
                }
                else
                {
                    return null;
                }

            }
        }

        /// <summary>
        /// 查isocode
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCTOSDamageRecord99XX(decimal containerid)
        {
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.P_GETDamageRecord", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_isocode"]).Value = containerid;
            using (DataSet ds = OCRDbContext.Runner.ExecuteDataSet(Config.ConnectionStringCTOSP, command))
            {
                return ds.Tables[0];
            }
        }
    }
}
