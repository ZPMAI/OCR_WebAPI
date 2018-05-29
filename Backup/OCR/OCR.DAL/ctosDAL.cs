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
        /// ��а���
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
        /// ���а��̲�ָ����
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
        /// �鼤���
        /// </summary>
        /// <returns></returns>
        public static DataSet SelectBerthplan()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOSP, "OCR.pkg_ocr_ctos.p_select_berthplanno", CommandType.StoredProcedure);
            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionStringCTOSP, command);
        }

        /// <summary>
        /// �鼤��� ����
        /// </summary>
        /// <returns></returns>
        public static DataSet SelectBerthplanBg()
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionStringCTOSP, "OCR.pkg_ocr_ctos.p_select_berthplannobg", CommandType.StoredProcedure);
            return SqlHelper.Oracle.ExecuteDataSet(Config.ConnectionStringCTOSP, command);
        }

        /// <summary>
        /// �������ֳ��ն˺�
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
        /// ������Ϣ
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
        /// �������뺬��
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
        /// ��isocode
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
        /// ���ϳ���ҵ���
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
        /// ����װж��ʱ��
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
        /// �鳵��λ��
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
