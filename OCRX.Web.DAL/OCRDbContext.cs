using CCT.Common;
using CCT.Common.Configuration;
using CCT.Common.Text;
using DBFramework;
using DBFramework.Runners;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCRX.Web.DAL
{

    public static class Config
    {
        private static string _conn = string.Empty;

        #region 数据库连接字符串
        /// <summary>
		/// 数据库连接字符串
		/// </summary>
        public static string ConnectionString
        {
            get
            {
                //if (_conn == string.Empty)
                //{
                //    //return ConnectionHelper.GetOracleConnectionString("CCTD");
                //    _conn = ConfigurationHelper.Section["OCR"]["IsDebug"].ToString() == "false" ?
                //        ConnectionHelper.GetOracleConnectionString("CCTD") :
                //        ConnectionHelper.GetOracleConnectionString("CTOST");
                //}
                //return _conn;
                return ConnectionStringCTOSP;
            }
        }
        #endregion  

        private static string ocrDbConn = string.Empty;

        #region ocr数据库连接字符串
        /// <summary>
        /// ocr数据库连接字符串
        /// </summary>
        public static string OcrDbConn
        {
            get
            {
                if (ocrDbConn == string.Empty)
                {
                    ocrDbConn = ConfigurationHelper.Section["OCR"]["OCRDB"].ToString();
                }
                return ocrDbConn;
            }
        }
        #endregion  

        private static string _connCTOSP = string.Empty;

        #region CTOSP数据库连接字符串
        /// <summary>
        /// CTOSP数据库连接字符串
        /// </summary>
        public static string ConnectionStringCTOSP
        {
            get
            {
                if (_connCTOSP == string.Empty)
                {
                    //return ConnectionHelper.GetOracleConnectionString("CCTD");
                    _connCTOSP = Config.IsDebug ?
                        ConnectionHelper.GetOracleConnectionString("SCT_OCR_TEST") :
                        ConnectionHelper.GetOracleConnectionString("SCT_OCR_RC2");
                }
                return _connCTOSP;
            }
        }
        #endregion  


        private static string _connCTOST = string.Empty;

        #region CTOSP数据库连接字符串
        /// <summary>
        /// CTOSP数据库连接字符串
        /// </summary>
        public static string ConnectionStringCTOST
        {
            get
            {
                if (_connCTOST == string.Empty)
                {
                    _connCTOST = ConnectionHelper.GetOracleConnectionString("CTOS_RC2");
                }
                return _connCTOST;
            }
        }
        #endregion 

        #region 系统发件人
        /// <summary>
        /// 系统发件人
        /// </summary>
        public static string MailFrom
        {
            get
            {
                return "ocr@cwcct.com";
            }
        }
        #endregion

        #region reporting service url
        /// <summary>
        /// reporting service url
        /// </summary>
        public static string ReportingServiceUrl
        {
            get
            {
                return ConfigurationHelper.Section["OCR"]["REPORT_URL"].ToString();
            }
        }
        #endregion  

        #region reporting service url backup
        /// <summary>
        /// reporting service url backup
        /// </summary>
        public static string ReportingServiceUrl2
        {
            get
            {
                return ConfigurationHelper.Section["OCR"]["REPORT_URL2"].ToString();
            }
        }
        #endregion  

        #region 默认日期
        /// <summary>
        /// 默认日期
        /// </summary>
        public static DateTime DefaultDate
        {
            get
            {
                return new DateTime(1900, 1, 1);
            }
        }
        #endregion

        public static bool IsDebug
        {
            get
            {
                return TextHelper.ConvertToBool(ConfigurationHelper.Section["Main"]["IsDebug"]);
            }
        }
        public static bool IsProduct
        {
            get
            {
                return TextHelper.ConvertToBool(ConfigurationHelper.Section["Main"]["IsProduct"]);
            }
        }
    }

    public static class OCRDbContext
    {


        private static DBContext dbContext;//readonly

        public static AbstractDbRunner Runner
        {
            get
            {
                return dbContext.OracleManaged;
            }
        }


        /// 静态构造函数
        static OCRDbContext()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //                 
            string connectionString = Config.IsDebug
                ? (Config.IsProduct ? ConnectionHelper.GetOracleConnectionString("SCT_OCR_TEST") : ConnectionHelper.GetOracleConnectionString("SCT_OCR_TEST"))
                : ConnectionHelper.GetOracleConnectionString("SCT_OCR_RC2");

            dbContext = new DBContext(connectionString);
        }


        public static bool Login(string user, string pwd)
        {
            OCRDbContext.dbContext = new DBContext(ConnectionHelper.GetOracleConnectionString("SCT_OCR_PRO"));
            IDbCommand command = OCRDbContext.Runner.CreateDbCommand("ocr.pkg_ocr_ctos.Login", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_User"]).Value = user;
            ((IDataParameter)command.Parameters["p_Pwd"]).Value = pwd;
            ((IDbDataParameter)command.Parameters["p_ErrorCode"]).Size = 5;

            OCRDbContext.Runner.ExecuteNonQuery(command);


            string errorCode = (string)((IDataParameter)command.Parameters["p_ErrorCode"]).Value.ToString();
            OCRDbContext.dbContext = new DBContext(ConnectionHelper.GetOracleConnectionString("SCT_OCR_RC2"));
            return errorCode.Equals("0");
        }

        public static DataSet GetMenu(string logid)
        {   /*新环境*/
            OCRDbContext.dbContext = new DBContext(ConnectionHelper.GetOracleConnectionString("SCT_OCR_PRO"));
            string sql = string.Format(@" SELECT M.MENUID, M.MENUCODE, M1.MENUNAME, M.PARENTID, M.ASSEMBLYNAME, M.TYPENAME, M.ORDERID, M.ISSHOW, M.MENUFLAG, M.TIPSINFO
                                             FROM CTOS.SC_MENU M
                                             LEFT JOIN CTOS.SC_MENU_LANG M1 on M.menuid=M1.menuid and m1.languageno='zh-CN'
                                            WHERE EXISTS
                                            (
                                                SELECT 1
                                                  FROM CTOS.PL_GROUPRIGHT GR
                                            INNER JOIN CTOS.PL_USERINGROUP UIG ON GR.GR_ID = UIG.UI_GR_ID 
                                            INNER JOIN CTOS.PL_USER U ON U.US_ID = UIG.UI_US_ID AND U.Us_Logid = '{0}'               
                                                 WHERE GR.RIGHTID = M.MENUID
                                            )
                                              AND M.ISSHOW = 1
                                            START WITH M.MENUID = 200100000
                                      CONNECT BY PRIOR M.MENUID = M.PARENTID
                                              ORDER BY LEVEL, M.ORDERID", logid);

            /*   string sql = string.Format(@" SELECT M.MENUID, M.MENUCODE, M.MENUNAME, M.PARENTID, M.ASSEMBLYNAME, M.TYPENAME, M.ORDERID, M.ISSHOW, M.MENUFLAG, M.TIPSINFO
                                               FROM CTOS.SC_MENU M
                                              WHERE EXISTS
                                              (
                                                  SELECT 1
                                                    FROM CTOS.PL_GROUPRIGHT GR
                                              INNER JOIN CTOS.PL_USERINGROUP UIG ON GR.GR_ID = UIG.UI_GR_ID 
                                              INNER JOIN CTOS.PL_USER U ON U.US_ID = UIG.UI_US_ID AND U.Us_Logid = '{0}'               
                                                   WHERE GR.RIGHTID = M.MENUID
                                              )
                                                AND M.ISSHOW = 1
                                              START WITH M.MENUID = 200100000
                                        CONNECT BY PRIOR M.MENUID = M.PARENTID
                                                ORDER BY LEVEL, M.ORDERID", logid);*/
            DataSet result = OCRDbContext.Runner.ExecuteDataSet(sql, CommandType.Text);
            OCRDbContext.dbContext = new DBContext(ConnectionHelper.GetOracleConnectionString("SCT_OCR_RC2"));
            return result;
        }


    }
}
