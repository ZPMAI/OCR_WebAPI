using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using CCT.SystemFramework.Data;
using OCRX.Model;
using OCR.Model;
using CCT.Common.Web;
using Newtonsoft.Json;

namespace OCRX.DAL
{
    /// <summary>
    /// 二次开发
    /// </summary>
    public class cctdbDAL
    {
        static HttpHelper httpHelper = new HttpHelper();

        /// <summary>
        /// 查所有外理公司代码
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_CODataTable SelectCompany()
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Company/SelectCompany",host);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_CODataTable result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_CODataTable>(resultStr);
            return result;


            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_company", CommandType.StoredProcedure);

            //DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CO.TableName });
            //return ds.T_OCRX_CO;
        }

        /// <summary>
        /// 新增外理公司
        /// </summary>
        /// <param name="row"></param>
        public static void InsertCompany(DataSet1.T_OCRX_CORow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Company/InsertCompany", host);
            DataSet1.T_OCRX_CODataTable table = new DataSet1.T_OCRX_CODataTable();
            row.CREATETIME = DateTime.Now;
            row.UPDATETIME = DateTime.Now;
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);
           

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_company", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 删除外理公司
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteCompany(DataSet1.T_OCRX_CORow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Company/DeleteCompany", host);
            DataSet1.T_OCRX_CODataTable table = new DataSet1.T_OCRX_CODataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_company", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 按外理公司代码查找
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_CORow SelectCompany(string companyCode)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Company/SelectCompany?companyCode={1}", host, companyCode);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_CODataTable result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_CODataTable>(resultStr);
            return result[0];

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_companybycode", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            //DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CO.TableName });

            //if (ds.T_OCRX_CO.Count == 0) return null;
            //else return ds.T_OCRX_CO[0];
        }


        /// <summary>
        /// 查所有外理公司用户
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_USERSDataTable SelectUsers()
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/User/SelectUsers", host);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_USERSDataTable result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_USERSDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_users", CommandType.StoredProcedure);

            //DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_USERS.TableName });
            //return ds.T_OCRX_USERS;
        }

        /// <summary>
        /// 按用户名查找
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_USERSRow SelectUser(string userId)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/User/SelectUser?userid={1}", host,userId);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_USERSRow result = (DataSet1.T_OCRX_USERSRow)JsonConvert.DeserializeObject<DataSet1.T_OCRX_USERSDataTable>(resultStr).Rows[0];
            return result;


            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_user", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_USERID"]).Value = userId;

            //DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_USERS.TableName });

            //if (ds.T_OCRX_USERS.Count == 0) return null;
            //else return ds.T_OCRX_USERS[0];
        }

        /// <summary>
        /// 新增外理公司用户
        /// </summary>
        /// <param name="row"></param>
        public static void InsertUsers(DataSet1.T_OCRX_USERSRow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/User/InsertUsers", host);
            DataSet1.T_OCRX_USERSDataTable table = new DataSet1.T_OCRX_USERSDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);
            //DataSet1.T_OCRX_USERSRow result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_USERSRow>(resultStr);
            //return result;

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_users", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 删除外理公司用户
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteUsers(DataSet1.T_OCRX_USERSRow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/User/DeleteUsers", host);
            DataSet1.T_OCRX_USERSDataTable table = new DataSet1.T_OCRX_USERSDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_users", CommandType.StoredProcedure, row);
        }


        /// <summary>
        /// 查所有班轮分发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_VESSELDataTable SelectVessel()
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Vessel/SelectVessel", host);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_VESSELDataTable result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_VESSELDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_vessel", CommandType.StoredProcedure);

            //DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_VESSEL.TableName });
            //return ds.T_OCRX_VESSEL;
        }

        /// <summary>
        /// 新增班轮分发规则
        /// </summary>
        /// <param name="row"></param>
        public static void InsertVessel(DataSet1.T_OCRX_VESSELRow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Vessel/InsertVessel", host);
            DataSet1.T_OCRX_VESSELDataTable table = new DataSet1.T_OCRX_VESSELDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_vessel", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 修改班轮分发规则
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateVessel(DataSet1.T_OCRX_VESSELRow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Vessel/UpdateVessel", host);
            DataSet1.T_OCRX_VESSELDataTable table = new DataSet1.T_OCRX_VESSELDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_vessel", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 删除班轮分发规则
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteVessel(DataSet1.T_OCRX_VESSELRow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Vessel/DeleteVessel", host);
            DataSet1.T_OCRX_VESSELDataTable table = new DataSet1.T_OCRX_VESSELDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_vessel", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 按航线查班轮分发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_VESSELDataTable SelectVesselByService(string serviceCode)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Vessel/SelectVesselByService?serviceCode={1}", host, serviceCode);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_VESSELDataTable result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_VESSELDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_vesselbyservice", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_servicecode"]).Value = serviceCode;

            //DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_VESSEL.TableName });
            //return ds.T_OCRX_VESSEL;
        }

        /// <summary>
        /// 按航线查班轮分发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_VESSELDataTable SelectVesselByCompany(string serviceCode, string companyCode)
        {

            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Vessel/SelectVesselByCompanyCode?serviceCode={1}&companyCode={2}", host, serviceCode, companyCode);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_VESSELDataTable result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_VESSELDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_vesselbycompany", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_servicecode"]).Value = serviceCode;
            //((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            //DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_VESSEL.TableName });
            //return ds.T_OCRX_VESSEL;
        }


        /// <summary>
        /// 查所有驳船分发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_BARGEDataTable SelectBarge(string companyCode)
        {

            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Barge/SelectBarge?companyCode={1}", host, companyCode);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_BARGEDataTable result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_BARGEDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_barge", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            //DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_BARGE.TableName });
            //return ds.T_OCRX_BARGE;
        }

        /// <summary>
        /// 新增驳船分发规则
        /// </summary>
        /// <param name="row"></param>
        public static void InsertBarge(DataSet1.T_OCRX_BARGERow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Barge/InsertBarge", host);
            DataSet1.T_OCRX_BARGEDataTable table = new DataSet1.T_OCRX_BARGEDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_barge", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 修改驳船分发规则
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateBarge(DataSet1.T_OCRX_BARGERow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Barge/UpdateBarge", host);
            DataSet1.T_OCRX_BARGEDataTable table = new DataSet1.T_OCRX_BARGEDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_barge", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 删除驳船分发规则
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteBarge(DataSet1.T_OCRX_BARGERow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Barge/DeleteBarge", host);
            DataSet1.T_OCRX_BARGEDataTable table = new DataSet1.T_OCRX_BARGEDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_barge", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 按船代查驳船分发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_BARGEDataTable SelectBargeByAgent(string serviceCode)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Barge/SelectBargeByAgent?agent={1}", host, serviceCode);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_BARGEDataTable result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_BARGEDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_bargebyagent", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_shipagent"]).Value = serviceCode;

            //DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_BARGE.TableName });
            //return ds.T_OCRX_BARGE;
        }

        /// <summary>
        /// 查取系统参数
        /// </summary>
        /// <returns></returns>
        public static SysParms SelectParams()
        {

            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/SysParms/SelectParams", host);
            string resultStr = httpHelper.HttpGet(url);
            SysParms result = JsonConvert.DeserializeObject<SysParms>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_params", CommandType.StoredProcedure);
            //using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command))
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        SysParms parms = new SysParms();
            //        foreach (DataRow dr in ds.Tables[0].Rows)
            //        {
            //            switch (dr["paramname"].ToString())
            //            {
            //                case "CONFIDENCELEVEL":
            //                    parms.CONFIDENCELEVEL = dr["paramvalue"].ToString();
            //                    break;
            //                case "CONTAINERINFOKEEPTIME":
            //                    parms.CONTAINERINFOKEEPTIME = dr["paramvalue"].ToString();
            //                    break;
            //                case "CTOSIP":
            //                    parms.CTOSIP = dr["paramvalue"].ToString();
            //                    break;
            //                case "DEVICEIP":
            //                    parms.DEVICEIP = dr["paramvalue"].ToString();
            //                    break;
            //                case "DEVICEPORT":
            //                    parms.DEVICEPORT = dr["paramvalue"].ToString();
            //                    break;
            //                case "DISPORT":
            //                    parms.DISPORT = dr["paramvalue"].ToString();
            //                    break;
            //                case "LOADPORT":
            //                    parms.LOADPORT = dr["paramvalue"].ToString();
            //                    break;
            //                case "ONBORADSWITCH":
            //                    parms.ONBORADSWITCH = dr["paramvalue"].ToString();
            //                    break;
            //                case "PASSWORD":
            //                    parms.PASSWORD = dr["paramvalue"].ToString();
            //                    break;
            //                case "TICKETID":
            //                    parms.TICKETID = dr["paramvalue"].ToString();
            //                    break;
            //                case "USERNAME":
            //                    parms.USERNAME = dr["paramvalue"].ToString();
            //                    break;
            //            }
            //        }


            //        return parms;
            //    }
            //}

            //return null;
        }


        /// <summary>
        /// 查所有待分发的识别记录
        /// </summary>
        /// <returns></returns>
        public static OCR.Model.OcrCnt.T_OCR_CNTDataTable SelectDispatch()
        {

            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Dispatch/SelectDispatch", host);
            string resultStr = httpHelper.HttpGet(url);
            OCR.Model.OcrCnt.T_OCR_CNTDataTable result = JsonConvert.DeserializeObject<OCR.Model.OcrCnt.T_OCR_CNTDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_dispatch", CommandType.StoredProcedure);

            //OCR.Model.OcrCnt ds = new OCR.Model.OcrCnt();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });
            //return ds.T_OCR_CNT;
        }

        /// <summary>
        /// 分发识别记录
        /// </summary>
        /// <param name="row"></param>
        public static void InsertCntx(DataSet1.T_OCRX_CNTRow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Dispatch/InsertCntx", host);
            DataSet1.T_OCRX_CNTDataTable table = new DataSet1.T_OCRX_CNTDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_cntx", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 更新识别记录分发结果
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateDispatched(decimal dock_id, string lineCode, string companyCode)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Dispatch/UpdateDispatched?dock_id={1}&lineCode={2}&companyCode={3}", host,dock_id,lineCode,companyCode);
            //string data = JsonConvert.SerializeObject(row);
            string resultStr = httpHelper.HttpPost(url, null);

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_update_dispatcthed", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_dock_id"]).Value = dock_id;
            //((IDataParameter)command.Parameters["p_linecode"]).Value = lineCode;
            //((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            //SqlHelper.Oracle.ExecuteNonQuery(Config.ConnectionString, command);
        }

        /// <summary>
        /// 查所有待分发的识别记录 成功确认有箱主的识别记录
        /// </summary>
        /// <returns></returns>
        public static OCR.Model.OcrCnt.T_OCR_CNTDataTable SelectDispatch2()
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Dispatch/SelectDispatch2", host);
            string resultStr = httpHelper.HttpGet(url);
            OCR.Model.OcrCnt.T_OCR_CNTDataTable result = JsonConvert.DeserializeObject<OCR.Model.OcrCnt.T_OCR_CNTDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_dispatch2", CommandType.StoredProcedure);

            //OCR.Model.OcrCnt ds = new OCR.Model.OcrCnt();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });
            //return ds.T_OCR_CNT;
        }

        /// <summary>
        /// 查所有待分发的识别记录 成功确认无箱主的识别记录
        /// </summary>
        /// <returns></returns>
        public static OCR.Model.OcrCnt.T_OCR_CNTDataTable SelectDispatch3()
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Dispatch/SelectDispatch3", host);
            string resultStr = httpHelper.HttpGet(url);
            OCR.Model.OcrCnt.T_OCR_CNTDataTable result = JsonConvert.DeserializeObject<OCR.Model.OcrCnt.T_OCR_CNTDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_dispatch3", CommandType.StoredProcedure);

            //OCR.Model.OcrCnt ds = new OCR.Model.OcrCnt();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_CNT.TableName });
            //return ds.T_OCR_CNT;
        }

        /// <summary>
        /// 查下一条识别记录
        /// </summary>
        /// <returns></returns>
        public static OCRX.Model.DataSet1.T_OCRX_CNTDataTable SelectNextRecord(string userName, string companyCode)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Record/SelectNextRecord?userName={1}&companyCode={2}", host,userName,companyCode);
            string resultStr = httpHelper.HttpGet(url);
            OCRX.Model.DataSet1.T_OCRX_CNTDataTable result = JsonConvert.DeserializeObject<OCRX.Model.DataSet1.T_OCRX_CNTDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_nextrecord", CommandType.StoredProcedure);

            //((IDataParameter)command.Parameters["p_username"]).Value = userName;
            //((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            //OCRX.Model.DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CNT.TableName });
            //return ds.T_OCRX_CNT;
        }

        /// <summary>
        /// 查剩余记录数
        /// </summary>
        /// <returns></returns>
        public static int SelectLeft(string companyCode)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Record/SelectLeft?companyCode={1}", host, companyCode);
            string resultStr = httpHelper.HttpGet(url);
            int result = JsonConvert.DeserializeObject<int>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_left", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            //using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command))
            //{
            //    return Convert.ToInt32(ds.Tables[0].Rows[0]["left"]);
            //}
        }

        /// <summary>
        /// 更新识别记录处理结果
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateCntStatus(DataSet1.T_OCRX_CNTRow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Container/UpdateCntStatus", host);
            DataSet1.T_OCRX_CNTDataTable table = new DataSet1.T_OCRX_CNTDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_cntstatus4", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 更新回退记录处理结果
        /// </summary>
        /// <param name="row"></param>
        public static void RollbackCntStatus(DataSet1.T_OCRX_CNTRow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Container/RollbackCntStatus", host);
            DataSet1.T_OCRX_CNTDataTable table = new DataSet1.T_OCRX_CNTDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_cntstatus4", CommandType.StoredProcedure, row);
        }


        /// <summary>
        /// 查对应的图片
        /// </summary>
        /// <returns></returns>
        public static OcrPhoto.T_OCR_PHOTODataTable SelectPhotos(decimal dock_id)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Photo/SelectPhotos?dock_id={1}", host, dock_id);
            string resultStr = httpHelper.HttpGet(url);
            OcrPhoto.T_OCR_PHOTODataTable result = JsonConvert.DeserializeObject<OcrPhoto.T_OCR_PHOTODataTable>(resultStr);
            return result;


            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_photo", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_dock_id"]).Value = dock_id;

            //OcrPhoto ds = new OcrPhoto();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_PHOTO.TableName });

            //return ds.T_OCR_PHOTO;
        }


        /// <summary>
        /// 查下一个seq_ocrx值
        /// </summary>
        /// <returns></returns>
        public static Int64 SelectSeqOcrx()
        {

            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Barge/Record?SelectSeqOcrx", host);
            string resultStr = httpHelper.HttpGet(url);
            Int64 result = JsonConvert.DeserializeObject<Int64>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_seq_ocrx", CommandType.StoredProcedure);

            //using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command))
            //{
            //    if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    {
            //        return Convert.ToInt64(ds.Tables[0].Rows[0]["val"]);
            //    }
            //    else
            //    {
            //        return 0;
            //    }
            //}
        }

        /// <summary>
        /// 复制图片
        /// </summary>
        /// <returns></returns>
        public static void CopyPhotos(decimal dock_id1, decimal dock_id2)
        {

            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Photo/CopyPhotos?dock_id1={1}&dock_id2={2}", host, dock_id1, dock_id2);
            string resultStr = httpHelper.HttpPost(url, null);


            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_copy_photos", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_dock_id1"]).Value = dock_id1;
            //((IDataParameter)command.Parameters["p_dock_id2"]).Value = dock_id2;

            //SqlHelper.Oracle.ExecuteNonQuery(Config.ConnectionString, command);
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCnt(DateTime from, DateTime to, string isArchived, string qcno, decimal dock_status, decimal cstatus, string containerno, string companyCode, string isdmg)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = $"{host}/api/Container/SelectCnt?from={from}&to={to}&isArchived={isArchived}&qcno={qcno}&dock_status={dock_status}&cstatus={cstatus}&containerno={containerno}&companyCode={companyCode}&isdmg={isdmg}";
            string resultStr = httpHelper.HttpGet(url);
            DataTable result = JsonConvert.DeserializeObject<DataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_cnt3", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_from"]).Value = from;
            //((IDataParameter)command.Parameters["p_to"]).Value = to;
            //((IDataParameter)command.Parameters["p_isarchived"]).Value = isArchived;
            //((IDataParameter)command.Parameters["p_qcno"]).Value = qcno;
            //((IDataParameter)command.Parameters["p_dock_status"]).Value = dock_status;
            //((IDataParameter)command.Parameters["p_cstatus"]).Value = cstatus;
            //((IDataParameter)command.Parameters["p_containerno"]).Value = containerno;
            //((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;
            //((IDataParameter)command.Parameters["p_isdmg"]).Value = isdmg;

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 按船别名和外理公司代码查询作业清单
        /// </summary>
        /// <returns></returns>
        public static DataSet SelectDamageReport(string eName, string companyCode)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Damage/SelectDamageReport?companyCode={1}&eName={2}", host, companyCode,eName);
            string resultStr = httpHelper.HttpGet(url);
            DataSet result = JsonConvert.DeserializeObject<DataSet>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_damagereport", CommandType.StoredProcedure);

            //((IDataParameter)command.Parameters["p_ename"]).Value = eName;
            //((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command);
        }

        public static DataSet SelectReport(string eName, string companyCode)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Record/SelectReport?companyCode={1}&eName={2}", host, companyCode, eName);
            string resultStr = httpHelper.HttpGet(url);
            DataSet result = JsonConvert.DeserializeObject<DataSet>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_report", CommandType.StoredProcedure);

            //((IDataParameter)command.Parameters["p_ename"]).Value = eName;
            //((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command);
        }

        /// <summary>
        /// 查看箱信息监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCntMonitor(string companyCode)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Container/SelectCntMonitor?companyCode={1}", host, companyCode);
            string resultStr = httpHelper.HttpGet(url);
            DataTable result = JsonConvert.DeserializeObject<DataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_cntmonitor", CommandType.StoredProcedure);

            //((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }


        /// <summary>
        /// 查QC监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectQcMonitor(string companyCode)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/QC/SelectQcMonitor?companyCode={1}", host, companyCode);
            string resultStr = httpHelper.HttpGet(url);
            DataTable result = JsonConvert.DeserializeObject<DataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_qcmonitor", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;
            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查所有未处理的异常
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_CNTDataTable SelectExcep(string companyCode)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Exception/SelectExcep?companyCode={1}", host, companyCode);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_CNTDataTable result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_CNTDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_excep", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_companycode"]).Value = companyCode;

            //DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CNT.TableName });

            //return ds.T_OCRX_CNT;
        }

        /// <summary>
        /// 更新异常处理结果
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateExcepStatus(DataSet1.T_OCRX_CNTRow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Exception/UpdateExcepStatus", host);
            DataSet1.T_OCRX_CNTDataTable table = new DataSet1.T_OCRX_CNTDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_excepstatus", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 查桥吊作业配置
        /// </summary>
        /// <returns></returns>
        public static QcSet.T_OCR_QCSETDataTable SelectQCSet()
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/QC/SelectQCSet", host);
            string resultStr = httpHelper.HttpGet(url);
            QcSet.T_OCR_QCSETDataTable result = JsonConvert.DeserializeObject<QcSet.T_OCR_QCSETDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr.p_select_qcset", CommandType.StoredProcedure);
            ////((IDataParameter)command.Parameters["p_nextexectime"]).Value = execTime;

            //QcSet ds = new QcSet();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_QCSET.TableName });
            //return ds.T_OCR_QCSET;
        }

        /// <summary>
        /// 船期监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectVslMonitor2()
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Vessel/SelectVslMonitor2", host);
            string resultStr = httpHelper.HttpGet(url);
            DataTable result = JsonConvert.DeserializeObject<DataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "ocr.pkg_ocr.p_select_vslmonitor21", CommandType.StoredProcedure);

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// QC监控
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectQcMonitor2()
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/QC/SelectQcMonitor2", host);
            string resultStr = httpHelper.HttpGet(url);
            DataTable result = JsonConvert.DeserializeObject<DataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "ocr.pkg_ocr.p_select_vslmonitor31", CommandType.StoredProcedure);

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查航线
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectService()
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Service/SelectService", host);
            string resultStr = httpHelper.HttpGet(url);
            DataTable result = JsonConvert.DeserializeObject<DataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "ocr.pkg_ocrx.p_select_service", CommandType.StoredProcedure);

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 按船别名查驳船分发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_BARGERow SelectBargeByVelaliase(string velaliase)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Barge/SelectBargeByVelaliase", host);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_BARGERow result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_BARGERow>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_bargebyvelaliase", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_velaliase"]).Value = velaliase;

            //DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_BARGE.TableName });
            //return ds.T_OCRX_BARGE.Count > 0 ? ds.T_OCRX_BARGE[0] : null;
        }

        /// <summary>
        /// 查箱是否确认过
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCntConfirmed(string velaliase, string contNo, decimal dockStatus)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Container/SelectCntConfirmed?velaliase={1}&contNo={2}&dockStatus={3}", host,velaliase,contNo,dockStatus);
            string resultStr = httpHelper.HttpGet(url);
            DataTable result = JsonConvert.DeserializeObject<DataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_cntconfirmed", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_velaliase"]).Value = velaliase;
            //((IDataParameter)command.Parameters["p_contno"]).Value = contNo;
            //((IDataParameter)command.Parameters["p_inout"]).Value = dockStatus;

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查是否是箱号溢卸
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectOverDisCnt(decimal containerid)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Container/SelectOverDisCnt?containerid={1}", host, containerid);
            string resultStr = httpHelper.HttpGet(url);
            DataTable result = JsonConvert.DeserializeObject<DataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_overdiscnt", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_containerid"]).Value = containerid;

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }


        /// <summary>
        /// 按船别名查航线和船型
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectService(string velaliase)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Service/SelectServiceByVslaliase?velaliase={1}", host, velaliase);
            string resultStr = httpHelper.HttpGet(url);
            DataTable result = JsonConvert.DeserializeObject<DataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.p_select_service", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_velaliase"]).Value = velaliase;

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查残损方位代码
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectDamagePositionCode()
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Damage/SelectDamagePositionCode", host);
            string resultStr = httpHelper.HttpGet(url);
            DataTable result = JsonConvert.DeserializeObject<DataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.p_select_damagecode_P", CommandType.StoredProcedure);

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查残损类型代码
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectDamageTypeCode()
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Damage/SelectDamageTypeCode", host);
            string resultStr = httpHelper.HttpGet(url);
            DataTable result = JsonConvert.DeserializeObject<DataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.p_select_damagecode_T", CommandType.StoredProcedure);

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查QC作业记录
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectQCWorkRecord(string qcNo, string containerId)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/QC/SelectQCWorkRecord?qcNo={1}&containerId={2}", host, qcNo,containerId);
            string resultStr = httpHelper.HttpGet(url);
            DataTable result = JsonConvert.DeserializeObject<DataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.p_select_qcwork", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_qcno"]).Value = qcNo;
            //((IDataParameter)command.Parameters["p_containerid"]).Value = containerId;

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        /// <summary>
        /// 查询单个残损箱作业信息
        /// </summary>
        /// <param name="row"></param>
        public static OCRX.Model.DataSet1.T_OCRX_CNTDataTable SelectCntDmgRecord(decimal dockid)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Damage/SelectCntDmgRecord?dockid={1}", host,dockid);
            string resultStr = httpHelper.HttpGet(url);
            OCRX.Model.DataSet1.T_OCRX_CNTDataTable result = JsonConvert.DeserializeObject<OCRX.Model.DataSet1.T_OCRX_CNTDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "pkg_ocrx.p_select_damagerecord", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_dock_id"]).Value = dockid;

            //OCRX.Model.DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CNT.TableName });
            //return ds.T_OCRX_CNT;
        }

        /// <summary>
        /// 查询单个箱残损信息
        /// </summary>
        /// <param name="row"></param>
        public static OCRX.Model.DataSet1.T_OCRX_DAMAGEDataTable SelectDamageRecord(decimal dockid)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Damage/SelectDamageRecord?dockid={1}", host, dockid);
            string resultStr = httpHelper.HttpGet(url);
            OCRX.Model.DataSet1.T_OCRX_DAMAGEDataTable result = JsonConvert.DeserializeObject<OCRX.Model.DataSet1.T_OCRX_DAMAGEDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "pkg_ocrx.p_select_damagerecord2", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_dock_id"]).Value = dockid;

            //OCRX.Model.DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_DAMAGE.TableName });
            //return ds.T_OCRX_DAMAGE;
        }

        /// <summary>
        /// 插入残损记录
        /// </summary>
        /// <param name="row"></param>
        public static void InsertDamage(DataSet1.T_OCRX_DAMAGERow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Damage/InsertDamage", host);
            DataSet1.T_OCRX_DAMAGEDataTable table = new DataSet1.T_OCRX_DAMAGEDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_insert_damage", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 更新残损记录信息
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateDamageInfo(DataSet1.T_OCRX_DAMAGERow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Damage/UpdateDamageInfo", host);
            DataSet1.T_OCRX_DAMAGEDataTable table = new DataSet1.T_OCRX_DAMAGEDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_damage", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 删除残损记录
        /// </summary>
        /// <param name="row"></param>
        public static void DeleteDamage(DataSet1.T_OCRX_DAMAGERow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Damage/DeleteDamageInfo", host);
            DataSet1.T_OCRX_DAMAGEDataTable table = new DataSet1.T_OCRX_DAMAGEDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_delete_damage", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 查询残损记录(内理)
        /// </summary>
        /// <param name="row"></param>
        public static DataSet1.T_OCRX_DAMAGEDataTable SelectDmgRecord(string velaliase)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Damage/SelectDmgRecord?velaliase={1}", host, velaliase);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_DAMAGEDataTable result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_DAMAGEDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_damagecnt", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_velaliase"]).Value = velaliase;

            //OCRX.Model.DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_DAMAGE.TableName });
            //return ds.T_OCRX_DAMAGE;
        }

        /// <summary>
        /// 查询残损记录(外理)
        /// </summary>
        /// <param name="row"></param>
        public static DataSet1.T_OCRX_DAMAGEDataTable SelectDmgRecordEx(string velaliase)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Damage/SelectDmgRecordEx?velaliase={1}", host, velaliase);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_DAMAGEDataTable result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_DAMAGEDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_damagecntx", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_velaliase"]).Value = velaliase;

            //OCRX.Model.DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_DAMAGE.TableName });
            //return ds.T_OCRX_DAMAGE;
        }

        /// <summary>
        /// 查询同一吊次记录
        /// </summary>
        /// <param name="row"></param>
        public static OCRX.Model.DataSet1.T_OCRX_CNTDataTable SelectCnt(decimal dockid)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Container/SelectCnt?dockid={1}", host, dockid);
            string resultStr = httpHelper.HttpGet(url);
            OCRX.Model.DataSet1.T_OCRX_CNTDataTable result = JsonConvert.DeserializeObject<OCRX.Model.DataSet1.T_OCRX_CNTDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocrx.p_select_cnt2", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_dockid"]).Value = dockid;

            //OCRX.Model.DataSet1 ds = new DataSet1();
            //SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCRX_CNT.TableName });
            //return ds.T_OCRX_CNT;
        }

        /// <summary>
        /// 更新残损记录状态
        /// </summary>
        /// <param name="row"></param>
        public static void UpdateDamageStatus(DataSet1.T_OCRX_DAMAGERow row)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Damage/UpdateDamageStatus", host);
            DataSet1.T_OCRX_DAMAGEDataTable table = new DataSet1.T_OCRX_DAMAGEDataTable();
            table.Rows.Add(row.ItemArray);
            string data = JsonConvert.SerializeObject(table);
            string resultStr = httpHelper.HttpPost(url, data);

            //SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocrx.p_update_damagecntx", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 单箱查询
        /// </summary>
        /// <returns></returns>
        public static DataTable GetContainerInfo(string containerno)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Container/SelectCnt?containerno={1}", host, containerno);
            string resultStr = httpHelper.HttpGet(url);
            OCRX.Model.DataSet1.T_OCRX_CNTDataTable result = JsonConvert.DeserializeObject<OCRX.Model.DataSet1.T_OCRX_CNTDataTable>(resultStr);
            return result;

            //IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_ctos.p_get_containerinfo", CommandType.StoredProcedure);
            //((IDataParameter)command.Parameters["p_containerno"]).Value = containerno;

            //return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionString, command).Tables[0];
        }

        public static ReportDataSet.P_SCT_DischargeListDataTable GetDischargeList(string vslaliase,string lineid)
        {
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Vessel/GetDischargeList?vslaliase={1}&lineid={2}", host, vslaliase,lineid);
            string resultStr = httpHelper.HttpGet(url);
            ReportDataSet.P_SCT_DischargeListDataTable result = JsonConvert.DeserializeObject<ReportDataSet.P_SCT_DischargeListDataTable>(resultStr);

            return result;
        }

        public static ReportDataSet.P_SCT_LoadListDataTable GetLoadingList(string vslaliase, string lineid)
        {
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Vessel/GetLoadingList?vslaliase={1}&lineid={2}", host, vslaliase, lineid);
            string resultStr = httpHelper.HttpGet(url);
            ReportDataSet.P_SCT_LoadListDataTable result = JsonConvert.DeserializeObject<ReportDataSet.P_SCT_LoadListDataTable>(resultStr);

            return result;
        }
        /// <summary>
        /// 获取该船下的船东
        /// </summary>
        /// <param name="vslaliase"></param>
        /// <returns></returns>
        public static DataSet1.T_OCRX_VESSELDataTable SelectVesselOwners(string vslaliase)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Vessel/SelectVesselOwners?vslaliase={1}", host, vslaliase);
            string resultStr = httpHelper.HttpGet(url);
            DataSet1.T_OCRX_VESSELDataTable result = JsonConvert.DeserializeObject<DataSet1.T_OCRX_VESSELDataTable>(resultStr);
            return result;
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
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Vessel/SelectCompanyVesselOwner?vslaliase={1}&companyCode={2}&disc={3}", host, vslaliase,CompanyCode,disc);
            string resultStr = httpHelper.HttpGet(url);
            List<string> result = JsonConvert.DeserializeObject<List<string>>(resultStr);
            return result;
        }

        /// <summary>
        /// 获取该公司在该船的分发船东
        /// </summary>
        /// <param name="vslaliase"></param>
        /// <param name="CompanyCode"></param>
        /// <param name="disc"></param>
        /// <returns></returns>
        public static DataTable SelectCTOSDamageRecord99XX(decimal containerid)
        {
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Damage/SelectCTOSDamageRecord99XX?containerid={1}", host, containerid);
            string resultStr = httpHelper.HttpGet(url);
            DataTable result = JsonConvert.DeserializeObject<DataTable>(resultStr);
            return result;
        }


    }
}
