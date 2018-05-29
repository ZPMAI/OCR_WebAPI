using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using CCT.SystemFramework.Data;
using OCRX.Model;
using OCR.Model;

namespace OCRX.DAL
{
    /// <summary>
    /// ���ο���
    /// </summary>
    public class cctdbDAL
    {

        /// <summary>
        /// ����������˾����
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_CODataTable SelectCompany()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_company", CommandType.StoredProcedure);

            DataSet1 ds = new DataSet1();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CO.TableName });
            return ds.T_OCRX_CO;
        }

        /// <summary>
        /// ��������˾
        /// </summary>
        /// <param name="row"></param>
        public static void InsertCompany(DataSet1.T_OCRX_CORow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_company", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// ɾ������˾
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteCompany(DataSet1.T_OCRX_CORow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_company", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// ������˾�������
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_CORow SelectCompany(string companyCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_companybycode", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            DataSet1 ds = new DataSet1();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CO.TableName });

            if (ds.T_OCRX_CO.Count == 0) return null;
            else return ds.T_OCRX_CO[0];
        }


        /// <summary>
        /// ����������˾�û�
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_USERSDataTable SelectUsers()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_users", CommandType.StoredProcedure);

            DataSet1 ds = new DataSet1();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_USERS.TableName });
            return ds.T_OCRX_USERS;
        }

        /// <summary>
        /// ���û�������
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_USERSRow SelectUser(string userId)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_user", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_USERID"]).Value = userId;

            DataSet1 ds = new DataSet1();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_USERS.TableName });

            if (ds.T_OCRX_USERS.Count == 0) return null;
            else return ds.T_OCRX_USERS[0];
        }

        /// <summary>
        /// ��������˾�û�
        /// </summary>
        /// <param name="row"></param>
        public static void InsertUsers(DataSet1.T_OCRX_USERSRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_users", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// ɾ������˾�û�
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteUsers(DataSet1.T_OCRX_USERSRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_users", CommandType.StoredProcedure, row);
        }


        /// <summary>
        /// �����а��ַַ�����
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_VESSELDataTable SelectVessel()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_vessel", CommandType.StoredProcedure);

            DataSet1 ds = new DataSet1();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_VESSEL.TableName });
            return ds.T_OCRX_VESSEL;
        }

        /// <summary>
        /// �������ַַ�����
        /// </summary>
        /// <param name="row"></param>
        public static void InsertVessel(DataSet1.T_OCRX_VESSELRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_vessel", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// �޸İ��ַַ�����
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateVessel(DataSet1.T_OCRX_VESSELRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_vessel", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// ɾ�����ַַ�����
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteVessel(DataSet1.T_OCRX_VESSELRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_vessel", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// �����߲���ַַ�����
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_VESSELDataTable SelectVesselByService(string serviceCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_vesselbyservice", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_servicecode"]).Value = serviceCode;

            DataSet1 ds = new DataSet1();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_VESSEL.TableName });
            return ds.T_OCRX_VESSEL;
        }


        /// <summary>
        /// �����в����ַ�����
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_BARGEDataTable SelectBarge(string companyCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_barge", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            DataSet1 ds = new DataSet1();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_BARGE.TableName });
            return ds.T_OCRX_BARGE;
        }

        /// <summary>
        /// ���������ַ�����
        /// </summary>
        /// <param name="row"></param>
        public static void InsertBarge(DataSet1.T_OCRX_BARGERow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_barge", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// �޸Ĳ����ַ�����
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateBarge(DataSet1.T_OCRX_BARGERow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_barge", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// ɾ�������ַ�����
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteBarge(DataSet1.T_OCRX_BARGERow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_barge", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// �������鲵���ַ�����
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_BARGEDataTable SelectBargeByAgent(string serviceCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_bargebyagent", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_shipagent"]).Value = serviceCode;

            DataSet1 ds = new DataSet1();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_BARGE.TableName });
            return ds.T_OCRX_BARGE;
        }

        /// <summary>
        /// ��ȡϵͳ����
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
        /// �����д��ַ���ʶ���¼
        /// </summary>
        /// <returns></returns>
        public static OCR.Model.OcrCnt.T_OCR_CNTDataTable SelectDispatch()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_dispatch", CommandType.StoredProcedure);

            OCR.Model.OcrCnt ds = new OCR.Model.OcrCnt();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });
            return ds.T_OCR_CNT;
        }

        /// <summary>
        /// �ַ�ʶ���¼
        /// </summary>
        /// <param name="row"></param>
        public static void InsertCntx(DataSet1.T_OCRX_CNTRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_cntx", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// ����ʶ���¼�ַ����
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateDispatched(decimal dock_id, string lineCode, string companyCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_update_dispatcthed", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_dock_id"]).Value = dock_id;
            ((IDataParameter)command.Parameters["p_linecode"]).Value = lineCode;
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            SqlHelper.Oracle.ExecuteNonQuery(Config.ConnectionString, command);
        }

        /// <summary>
        /// �����д��ַ���ʶ���¼ �ɹ�ȷ����������ʶ���¼
        /// </summary>
        /// <returns></returns>
        public static OCR.Model.OcrCnt.T_OCR_CNTDataTable SelectDispatch2()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_dispatch2", CommandType.StoredProcedure);

            OCR.Model.OcrCnt ds = new OCR.Model.OcrCnt();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });
            return ds.T_OCR_CNT;
        }

        /// <summary>
        /// �����д��ַ���ʶ���¼ �ɹ�ȷ����������ʶ���¼
        /// </summary>
        /// <returns></returns>
        public static OCR.Model.OcrCnt.T_OCR_CNTDataTable SelectDispatch3()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_dispatch3", CommandType.StoredProcedure);

            OCR.Model.OcrCnt ds = new OCR.Model.OcrCnt();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });
            return ds.T_OCR_CNT;
        }

        /// <summary>
        /// ����һ��ʶ���¼
        /// </summary>
        /// <returns></returns>
        public static OCRX.Model.DataSet1.T_OCRX_CNTDataTable SelectNextRecord(string userName, string companyCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_nextrecord", CommandType.StoredProcedure);

            ((IDataParameter)command.Parameters["p_username"]).Value = userName;
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            OCRX.Model.DataSet1 ds = new DataSet1();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CNT.TableName });
            return ds.T_OCRX_CNT;
        }

        /// <summary>
        /// ��ʣ���¼��
        /// </summary>
        /// <returns></returns>
        public static int SelectLeft(string companyCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_left", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command))
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0]["left"]);
            }
        }

        /// <summary>
        /// ����ʶ���¼������
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateCntStatus(DataSet1.T_OCRX_CNTRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_cntstatus3", CommandType.StoredProcedure, row);
        }


        /// <summary>
        /// ���Ӧ��ͼƬ
        /// </summary>
        /// <returns></returns>
        public static OcrPhoto.T_OCR_PHOTODataTable SelectPhotos(decimal dock_id)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_photo", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_dock_id"]).Value = dock_id;

            OcrPhoto ds = new OcrPhoto();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_PHOTO.TableName });

            return ds.T_OCR_PHOTO;
        }


        /// <summary>
        /// ����һ��seq_ocrxֵ
        /// </summary>
        /// <returns></returns>
        public static Int64 SelectSeqOcrx()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_seq_ocrx", CommandType.StoredProcedure);

            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command))
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
        /// ����ͼƬ
        /// </summary>
        /// <returns></returns>
        public static void CopyPhotos(decimal dock_id1, decimal dock_id2)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_copy_photos", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_dock_id1"]).Value = dock_id1;
            ((IDataParameter)command.Parameters["p_dock_id2"]).Value = dock_id2;

            SqlHelper.Oracle.ExecuteNonQuery(Config.ConnectionString, command);
        }


        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCnt(DateTime from, DateTime to, string isArchived, string qcno, decimal dock_status, decimal cstatus, string containerno, string companyCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_cnt", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_from"]).Value = from;
            ((IDataParameter)command.Parameters["p_to"]).Value = to;
            ((IDataParameter)command.Parameters["p_isarchived"]).Value = isArchived;
            ((IDataParameter)command.Parameters["p_qcno"]).Value = qcno;
            ((IDataParameter)command.Parameters["p_dock_status"]).Value = dock_status;
            ((IDataParameter)command.Parameters["p_cstatus"]).Value = cstatus;
            ((IDataParameter)command.Parameters["p_containerno"]).Value = containerno;
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// ��������������˾�����ѯ��ҵ�嵥
        /// </summary>
        /// <returns></returns>
        public static DataSet SelectReport(string eName, string companyCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_report", CommandType.StoredProcedure);

            ((IDataParameter)command.Parameters["p_ename"]).Value = eName;
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command);
        }

        /// <summary>
        /// �鿴����Ϣ���
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCntMonitor(string companyCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_cntmonitor", CommandType.StoredProcedure);

            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }


        /// <summary>
        /// ��QC���
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectQcMonitor(string companyCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_qcmonitor", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;
            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// ������δ������쳣
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_CNTDataTable SelectExcep(string companyCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_excep", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            DataSet1 ds = new DataSet1();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CNT.TableName });

            return ds.T_OCRX_CNT;
        }

        /// <summary>
        /// �����쳣������
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateExcepStatus(DataSet1.T_OCRX_CNTRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_excepstatus", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// ���ŵ���ҵ����
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
        /// ���ڼ��
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectVslMonitor2()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "ocr.pkg_ocr.p_select_vslmonitor21", CommandType.StoredProcedure);

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// QC���
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectQcMonitor2()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "ocr.pkg_ocr.p_select_vslmonitor31", CommandType.StoredProcedure);

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// �麽��
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectService()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "ocr.pkg_ocrx.p_select_service", CommandType.StoredProcedure);

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// ���������鲵���ַ�����
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_BARGERow SelectBargeByVelaliase(string velaliase)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_bargebyvelaliase", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_velaliase"]).Value = velaliase;

            DataSet1 ds = new DataSet1();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_BARGE.TableName });
            return ds.T_OCRX_BARGE.Count > 0 ? ds.T_OCRX_BARGE[0] : null;
        }

        /// <summary>
        /// �����Ƿ�ȷ�Ϲ�
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCntConfirmed(string velaliase, string contNo, decimal dockStatus)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_cntconfirmed", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_velaliase"]).Value = velaliase;
            ((IDataParameter)command.Parameters["p_contno"]).Value = contNo;
            ((IDataParameter)command.Parameters["p_inout"]).Value = dockStatus;

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// ���Ƿ��������ж
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectOverDisCnt(decimal containerid)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_overdiscnt", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_containerid"]).Value = containerid;

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }


        /// <summary>
        /// ���������麽�ߺʹ���
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectService(string velaliase)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.p_select_service", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_velaliase"]).Value = velaliase;

            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

    }
}
