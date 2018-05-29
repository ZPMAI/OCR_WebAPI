using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using CCT.SystemFramework.Data;
using OCR.Model;

namespace OCR.DAL
{
    /// <summary>
    /// ctos
    /// </summary>
    public class ctosDAL
    {
        /// <summary>
        /// 查承包商
        /// </summary>
        /// <returns></returns>
        public static List<string> SelectContractor()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOSP, "OCR.pkg_ocr_ctos.p_select_contractor", CommandType.StoredProcedure);
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionStringCTOSP, command))
            {
                List<string> list = new List<string>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(dr["contractorcode"].ToString());
                }
                return list;
            }
        }

        /// <summary>
        /// 按承包商查指挥手
        /// </summary>
        /// <returns></returns>
        public static List<string> SelectCommend(string contractor)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOSP, "OCR.pkg_ocr_ctos.p_select_commend", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_contractor"]).Value = contractor;
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionStringCTOSP, command))
            {
                List<string> list = new List<string>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(dr["us_logid"].ToString());
                }
                return list;
            }
        }

        /// <summary>
        /// 查激活船期
        /// </summary>
        /// <returns></returns>
        public static DataSet SelectBerthplan()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOSP, "OCR.pkg_ocr_ctos.p_select_berthplanno", CommandType.StoredProcedure);
            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionStringCTOSP, command);
        }

        /// <summary>
        /// 查激活船期 驳船
        /// </summary>
        /// <returns></returns>
        public static DataSet SelectBerthplanBg()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOSP, "OCR.pkg_ocr_ctos.p_select_berthplannobg", CommandType.StoredProcedure);
            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionStringCTOSP, command);
        }

        /// <summary>
        /// 查虚拟手持终端号
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> SelectRemote()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOSP, "OCR.pkg_ocr_ctos.p_select_remote", CommandType.StoredProcedure);
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionStringCTOSP, command))
            {
                Dictionary<string, string> list = new Dictionary<string, string>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(dr["deviceno"].ToString(), dr["terminalno"].ToString());
                }
                return list;
            }
        }

        /// <summary>
        /// 查箱信息
        /// </summary>
        /// <returns></returns>
        public static DataRow SelectCnt(string cnt)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOSP, "OCR.pkg_ocr_ctos.p_select_cnt", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_cnt"]).Value = cnt;
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionStringCTOSP, command))
            {
                return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0] : null;
            }
        }

        /// <summary>
        /// 查错误代码含义
        /// </summary>
        /// <returns></returns>
        public static string SelectErrcode(int errCode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOSP, "OCR.pkg_ocr_ctos.p_select_errcode", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_errcode"]).Value = errCode;
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionStringCTOSP, command))
            {
                return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["messagecontent"].ToString() : string.Empty;
               
            }
        }

        /// <summary>
        /// 查isocode
        /// </summary>
        /// <returns></returns>
        public static IsoCode SelectIsoCode(string isocode)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOSP, "OCR.pkg_ocr_ctos.p_select_isocode", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_isocode"]).Value = isocode;
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionStringCTOSP, command))
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
        /// 查拖车作业情况
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectTruck(string truck, string truckSeq)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOSP, "OCR.pkg_ocr_ctos.p_select_truck2", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_truckno"]).Value = truck;
            ((IDataParameter)command.Parameters["p_truckseq"]).Value = truckSeq;
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionStringCTOSP, command))
            {
                return ds.Tables[0];

            }
        }

        /// <summary>
        /// 查箱装卸船时间
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCntTime(string cntno, string isLoad)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOSP, "OCR.pkg_ocr_ctos.p_select_cnttime", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_cntno"]).Value = cntno;
            ((IDataParameter)command.Parameters["p_isload"]).Value = isLoad;
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionStringCTOSP, command))
            {
                return ds.Tables[0];

            }
        }

        /// <summary>
        /// 查车上位置
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectPosOnTrcuk(decimal containerid)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOSP, "OCR.pkg_ocr_ctos.p_select_posontruck", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_containerid"]).Value = containerid;
            using (DataSet ds = SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionStringCTOSP, command))
            {
                return ds.Tables[0];

            }
        }
    }
}
