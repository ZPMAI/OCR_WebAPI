using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using CCT.SystemFramework.Data;
using OCR.Model;

namespace OCR.DAL
{
    /// <summary>
    /// 二次开发
    /// </summary>
    public class cctdbDAL
    {
        /// <summary>
        /// 查当前最大ID 识别记录
        /// </summary>
        /// <returns></returns>
        public static int SelectMaxId()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_maxid", CommandType.StoredProcedure);
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command))
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0]["maxid"]);
            }
        }

        /// <summary>
        /// 查当前最大ID 图片
        /// </summary>
        /// <returns></returns>
        public static int SelectMaxIdPhoto()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_maxidphoto", CommandType.StoredProcedure);
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command))
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0]["maxid"]);
            }
        }

        /// <summary>
        /// 查当前最大ID 车号
        /// </summary>
        /// <returns></returns>
        public static int SelectMaxIdTruck()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_maxidtruck", CommandType.StoredProcedure);
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command))
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0]["maxid"]);
            }
        }

        /// <summary>
        /// 插入新识别记录
        /// </summary>
        /// <returns></returns>
        public static void InsertCnt(DataRow dr)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_insert_cnt3", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_dock_id"]).Value = dr["dock_id"];
            ((IDataParameter)command.Parameters["p_container_no"]).Value = dr["container_no"];
            ((IDataParameter)command.Parameters["p_container_shape"]).Value = dr["container_shape"];
            ((IDataParameter)command.Parameters["p_container_size"]).Value = dr["container_size"];
            ((IDataParameter)command.Parameters["p_container_height"]).Value = dr["container_height"];
            ((IDataParameter)command.Parameters["p_plate_no"]).Value = dr["plate_no"];
            ((IDataParameter)command.Parameters["p_ctype"]).Value = dr["ctype"];
            ((IDataParameter)command.Parameters["p_cweigt"]).Value = dr["cweigt"];
            ((IDataParameter)command.Parameters["p_dock_status"]).Value = dr["dock_status"];
            ((IDataParameter)command.Parameters["p_container_pos"]).Value = dr["container_pos"];
            ((IDataParameter)command.Parameters["p_cstatus"]).Value = dr["cstatus"];
            ((IDataParameter)command.Parameters["p_confidence"]).Value = dr["confidence"];
            ((IDataParameter)command.Parameters["p_trvalcrane_id"]).Value = dr["trvalcrane_id"];
            ((IDataParameter)command.Parameters["p_trval_no"]).Value = dr["trval_no"];
            ((IDataParameter)command.Parameters["p_driver_no"]).Value = dr["driver_no"];
            ((IDataParameter)command.Parameters["p_lane_no"]).Value = dr["lane_no"];
            ((IDataParameter)command.Parameters["p_begin_time"]).Value = dr["begin_time"];
            ((IDataParameter)command.Parameters["p_end_time"]).Value = dr["end_time"];
            ((IDataParameter)command.Parameters["p_user_id"]).Value = dr["user_id"];
            ((IDataParameter)command.Parameters["p_cnam"]).Value = dr["cnam"];
            ((IDataParameter)command.Parameters["p_enam"]).Value = dr["enam"];
            ((IDataParameter)command.Parameters["p_ship_code"]).Value = dr["ship_code"];
            ((IDataParameter)command.Parameters["p_c_voyage"]).Value = dr["c_voyage"];
            ((IDataParameter)command.Parameters["p_pic_num"]).Value = dr["pic_num"];
            ((IDataParameter)command.Parameters["p_msg_index"]).Value = dr["msg_index"];
            ((IDataParameter)command.Parameters["p_carcont"]).Value = dr["carcont"];
            ((IDataParameter)command.Parameters["p_gangs"]).Value = dr["gangs"];
            ((IDataParameter)command.Parameters["p_container_dir"]).Value = dr["container_dir"];
            ((IDataParameter)command.Parameters["p_stream_dir"]).Value = dr["stream_dir"];
            ((IDataParameter)command.Parameters["p_loading_port"]).Value = dr["loading_port"];
            ((IDataParameter)command.Parameters["p_unloading_port"]).Value = dr["unloading_port"];
            ((IDataParameter)command.Parameters["p_dest_port"]).Value = dr["dest_port"];
            ((IDataParameter)command.Parameters["p_bay_horizontal"]).Value = dr["bay_horizontal"];
            ((IDataParameter)command.Parameters["p_bay_vertical"]).Value = dr["bay_vertical"];
            ((IDataParameter)command.Parameters["p_bay"]).Value = dr["bay"];
            ((IDataParameter)command.Parameters["p_ctime"]).Value = dr["ctime"];
            ((IDataParameter)command.Parameters["p_pms_id"]).Value = dr["pms_id"];
            ((IDataParameter)command.Parameters["p_COMMEND_ID"]).Value = dr["COMMEND_ID"];
            ((IDataParameter)command.Parameters["p_BERTH_NUM"]).Value = dr["BERTH_NUM"];
            ((IDataParameter)command.Parameters["p_isarchived"]).Value = dr["isarchived"];
            ((IDataParameter)command.Parameters["p_SERVICECODE"]).Value = dr["SERVICECODE"];
            ((IDataParameter)command.Parameters["p_SHIPAGENT"]).Value = dr["SHIPAGENT"];
            ((IDataParameter)command.Parameters["p_SHIPOWNER"]).Value = dr["SHIPOWNER"];

            if (dr["plc_open_time"] != DBNull.Value)
            {
                ((IDataParameter)command.Parameters["p_plc_open_time"]).Value = dr["plc_open_time"];
            }
            if (dr["plc_close_time"] != DBNull.Value)
            {
                ((IDataParameter)command.Parameters["p_plc_close_time"]).Value = dr["plc_close_time"];
            }
            if (dr["ident_start_time"] != DBNull.Value)
            {
                ((IDataParameter)command.Parameters["p_ident_start_time"]).Value = dr["ident_start_time"];
            }
            if (dr["ident_end_time"] != DBNull.Value)
            {
                ((IDataParameter)command.Parameters["p_ident_end_time"]).Value = dr["ident_end_time"];
            }

            SqlHelper.Oracle.ExecuteNonQuery(Config.ConnectionString, command);
        }


        /// <summary>
        /// 插入新图片记录
        /// </summary>
        /// <returns></returns>
        public static void InsertPhoto(DataRow dr)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_insert_photo2", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_photo_id"]).Value = dr["photo_id"];
            ((IDataParameter)command.Parameters["p_dock_id"]).Value = dr["dock_id"];
            ((IDataParameter)command.Parameters["p_photo_url"]).Value = dr["photo_url"];
            ((IDataParameter)command.Parameters["p_ctime"]).Value = dr["ctime"];
            ((IDataParameter)command.Parameters["p_photo_name"]).Value = dr["photo_name"];
            ((IDataParameter)command.Parameters["p_photo_pos"]).Value = dr["photo_pos"];
            ((IDataParameter)command.Parameters["p_cont_order"]).Value = dr["cont_order"];


            ((IDataParameter)command.Parameters["p_data_delay"]).Value = dr["data_delay"];
            ((IDataParameter)command.Parameters["p_plcdata_boxheight"]).Value = dr["plcdata_boxheight"];
            ((IDataParameter)command.Parameters["p_plcdata_boxdisplacement"]).Value = dr["plcdata_boxdisplacement"];

            SqlHelper.Oracle.ExecuteNonQuery(Config.ConnectionString, command);
        }

        /// <summary>
        /// 插入新车号记录
        /// </summary>
        /// <returns></returns>
        public static void InsertTruck(DataRow dr)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_insert_truck", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_seqno"]).Value = dr["temp_dock_id"];
            ((IDataParameter)command.Parameters["p_qcno"]).Value = dr["trval_no"];
            ((IDataParameter)command.Parameters["p_truckno"]).Value = dr["car_no"];
            ((IDataParameter)command.Parameters["p_createtime"]).Value = dr["begin_time"];

            SqlHelper.Oracle.ExecuteNonQuery(Config.ConnectionString, command);
        }

        /// <summary>
        /// 查桥吊作业配置
        /// </summary>
        /// <returns></returns>
        public static QcSet.T_OCR_QCSETDataTable SelectQCSet()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_qcset", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_nextexectime"]).Value = execTime;

            QcSet ds = new QcSet();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_QCSET.TableName });
            return ds.T_OCR_QCSET;
        }

        /// <summary>
        /// 查桥吊作业配置
        /// </summary>
        /// <returns></returns>
        public static QcSet.T_OCR_QCSETRow SelectQCSet(string qcno)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_qcset1", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_qc"]).Value = qcno;

            QcSet ds = new QcSet();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_QCSET.TableName });

            if (ds.T_OCR_QCSET.Count > 0) return ds.T_OCR_QCSET[0];
            else return null;
        }

        /// <summary>
        /// 更新桥吊作业配置
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateQCSet(QcSet.T_OCR_QCSETRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocr.p_update_qcset2", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 获取新记录
        /// </summary>
        /// <returns></returns>
        public static OcrCnt.T_OCR_CNTDataTable SelectNextRecord(string userName)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_nextrecord", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_username"]).Value = userName;

            OcrCnt ds = new OcrCnt();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });

            return ds.T_OCR_CNT;
        }

        /// <summary>
        /// 获取新记录
        /// </summary>
        /// <returns></returns>
        public static OcrCnt.T_OCR_CNTDataTable SelectNextRecord(string userName, string terminal)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_nextrecord3", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_username"]).Value = userName;
            ((IDataParameter)command.Parameters["p_terminal"]).Value = terminal;

            OcrCnt ds = new OcrCnt();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });

            return ds.T_OCR_CNT;
        }

        /// <summary>
        /// 查对应的图片
        /// </summary>
        /// <returns></returns>
        public static OcrPhoto.T_OCR_PHOTODataTable SelectPhotos(decimal dock_id)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_photo", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_dock_id"]).Value = dock_id;

            OcrPhoto ds = new OcrPhoto();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_PHOTO.TableName });

            return ds.T_OCR_PHOTO;
        }

        /// <summary>
        /// 查取系统参数
        /// </summary>
        /// <returns></returns>
        public static SysParms SelectParams()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_params", CommandType.StoredProcedure);
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command))
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
        /// 更新识别记录处理结果
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateCntStatus(OcrCnt.T_OCR_CNTRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocr.p_update_cntstatus3", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 更新验箱处理结果
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateCntCheckStatus(OcrCnt.T_OCR_CNTRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocr.p_update_cntcheck", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 查需自动处理装船的记录
        /// </summary>
        /// <returns></returns>
        public static OcrCnt.T_OCR_CNTDataTable SelectLoadAuto()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_loadauto", CommandType.StoredProcedure);
      
            OcrCnt ds = new OcrCnt();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });

            return ds.T_OCR_CNT;
        }

        /// <summary>
        /// 查需自动处理装船的记录 按QC
        /// </summary>
        /// <returns></returns>
        public static OcrCnt.T_OCR_CNTDataTable SelectLoadAuto(string qc)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_loadauto1", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_qc"]).Value = qc;

            OcrCnt ds = new OcrCnt();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });

            return ds.T_OCR_CNT;
        }

        /// <summary>
        /// 查操作日志
        /// </summary>
        /// <returns></returns>
        public static OpLog.T_OCR_LOGDataTable SelectLogs(string tablename, string colname)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_log", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_tablename"]).Value = tablename;
            ((IDataParameter)command.Parameters["p_colname"]).Value = colname;

            OpLog ds = new OpLog();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_LOG.TableName });

            return ds.T_OCR_LOG;
        }

        /// <summary>
        /// 查QC监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectQcMonitor()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_qcmonitor", CommandType.StoredProcedure);
            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查看拖车监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectTruckMonitor()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_truckmonitor", CommandType.StoredProcedure);
            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 归档
        /// </summary>
        /// <returns></returns>
        public static void Archive()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_archive", CommandType.StoredProcedure);
            SqlHelper.Oracle.ExecuteNonQuery(Config.ConnectionString, command);
        }

        /// <summary>
        /// 查所有未处理的异常
        /// </summary>
        /// <returns></returns>
        public static OcrCnt.T_OCR_CNTDataTable SelectExcep()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_excep", CommandType.StoredProcedure);

            OcrCnt ds = new OcrCnt();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });

            return ds.T_OCR_CNT;
        }

        /// <summary>
        /// 更新异常处理结果
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateExcepStatus(OcrCnt.T_OCR_CNTRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocr.p_update_excepstatus", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 更新异常处理结果
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateExcepStatus2(OcrCnt.T_OCR_CNTRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocr.p_update_excepstatus2", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 查剩余记录数
        /// </summary>
        /// <returns></returns>
        public static int SelectLeft()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_left", CommandType.StoredProcedure);
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command))
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0]["left"]);
            }
        }

        /// <summary>
        /// 查剩余记录数
        /// </summary>
        /// <returns></returns>
        public static int SelectLeft(string terminal)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_left2", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_terminal"]).Value = terminal;

            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command))
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0]["left"]);
            }
        }

        /// <summary>
        /// 查看班轮监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectVslMonitor()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_vslmonitor", CommandType.StoredProcedure);
            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查看箱信息监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCntMonitor()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_cntmonitor", CommandType.StoredProcedure);
            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查看装船监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectLoadMonitor()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_loadmonitor", CommandType.StoredProcedure);
            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查已装船数据
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectLoaded(DateTime from, DateTime to)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_loaded", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_from"]).Value = from;
            ((IDataParameter)command.Parameters["p_to"]).Value = to;

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 转异常数据
        /// </summary>
        /// <returns></returns>
        public static DataTable SelecteExcepMonitor(DateTime from, DateTime to)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_excepmonitor", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_from"]).Value = from;
            ((IDataParameter)command.Parameters["p_to"]).Value = to;

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCnt(DateTime from, DateTime to, string isArchived, string qcno, decimal dock_status, decimal cstatus, string containerno, string truckNo)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_cnt2", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_from"]).Value = from;
            ((IDataParameter)command.Parameters["p_to"]).Value = to;
            ((IDataParameter)command.Parameters["p_isarchived"]).Value = isArchived;
            ((IDataParameter)command.Parameters["p_qcno"]).Value = qcno;
            ((IDataParameter)command.Parameters["p_dock_status"]).Value = dock_status;
            ((IDataParameter)command.Parameters["p_cstatus"]).Value = cstatus;
            ((IDataParameter)command.Parameters["p_containerno"]).Value = containerno;
            ((IDataParameter)command.Parameters["p_truckno"]).Value = truckNo;

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查对应的图片
        /// </summary>
        /// <returns></returns>
        public static OcrPhoto.T_OCR_PHOTODataTable SelectPhotos2(decimal dock_id)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_photo2", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_dock_id"]).Value = dock_id;

            OcrPhoto ds = new OcrPhoto();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_PHOTO.TableName });

            return ds.T_OCR_PHOTO;
        }

        /// <summary>
        /// 查上5条作业指令 
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectLast5(string qcno)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_lastfive", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_qc"]).Value = qcno;

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 双吊快查
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectQcCnt(string qcno)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_cntbyqcno2", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_qcno"]).Value = qcno;

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 更新识别记录move id
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateMoveId(OcrCnt.T_OCR_CNTRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocr.p_update_moveid", CommandType.StoredProcedure, row);
        }


        /// <summary>
        /// 箱号快查
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCntQuery(string containerno)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_cntquery", CommandType.StoredProcedure);
           
            ((IDataParameter)command.Parameters["p_containerno"]).Value = containerno;

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }


        /// <summary>
        /// 查QC识别率
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectQcRate(string qcNo, int rateCntType)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_qcrate", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_qcno"]).Value = qcNo;
            if (command.Parameters.Contains("p_type"))
            {
                ((IDataParameter)command.Parameters["p_type"]).Value = rateCntType;
            }

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 船期监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectVslMonitor2()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "ocr.pkg_ocr.p_select_vslmonitor21", CommandType.StoredProcedure);

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// QC监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectQcMonitor2()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "ocr.pkg_ocr.p_select_vslmonitor31", CommandType.StoredProcedure);

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 保存错误日志
        /// </summary>
        /// <param name="row"></param>
        public static void SaveErrorLog(ErrorLog.T_OCR_ERRORLOGRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocr.p_save_errorlog", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 查错误日志
        /// </summary>
        /// <returns></returns>
        public static ErrorLog.T_OCR_ERRORLOGDataTable SelectErrorLog(string qcNo, string deviceType, DateTime from, DateTime to)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOST, "OCR.pkg_ocr.p_select_errorlog", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_qcno"]).Value = qcNo;
            ((IDataParameter)command.Parameters["p_devicetype"]).Value = deviceType;
            ((IDataParameter)command.Parameters["p_fromtime"]).Value = from;
            ((IDataParameter)command.Parameters["p_endtime"]).Value = to;

            ErrorLog ds = new ErrorLog();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_ERRORLOG.TableName });

            return ds.T_OCR_ERRORLOG;
        }


        /// <summary>
        /// 保存监控操作日志
        /// </summary>
        /// <param name="row"></param>
        public static void SaveMonitorLog(ErrorLog.T_OCR_MONITORLOGRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocr.p_save_monitorlog", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 新增异常
        /// </summary>
        /// <returns></returns>
        public static void AddExp(decimal dockId, string userId)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_update_expadd", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_dock_id"]).Value = dockId;
            ((IDataParameter)command.Parameters["p_user_id"]).Value = userId;

            SqlHelper.Oracle.ExecuteNonQuery(Config.ConnectionString, command);
        }


        /// <summary>
        /// 业务监控 箱信息
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCntBllMonitor()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_bllmonitor", CommandType.StoredProcedure);

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 业务监控 识别率
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectRateBllMonitor()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_bllmonitorrate", CommandType.StoredProcedure);

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

    }
}
