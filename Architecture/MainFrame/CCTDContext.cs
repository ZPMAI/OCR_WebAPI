using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using CCT.Common;
using CCT.Common.Text;
using CCT.Common.Configuration;
using DBFramework;
using CCT.Common.Web;

namespace CCT.MainFrame
{
    public static class Config
    {
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


    public class CCTDContext
    {
        //private static  DBContext dbContext;//readonly
        static HttpHelper httpHelper = new HttpHelper();
        //public static AbstractRunner Runner
        //{
        //    get
        //    {
        //        return dbContext.Oracle;
        //    }
        //}


        /// 静态构造函数
        static CCTDContext()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //                 
            //string connectionString = Config.IsDebug
            //    ? (Config.IsProduct? ConnectionHelper.GetOracleConnectionString("SCT_OCR_TEST") :ConnectionHelper.GetOracleConnectionString("SCT_OCR_TEST"))
            //    : ConnectionHelper.GetOracleConnectionString("CCTD");

            //dbContext = new DBContext(connectionString);
        }


        public static bool Login(string user, string pwd)
        {
            //IDbCommand command = CCTDContext.Runner.CreateProcedureCommand("ocr.pkg_ocr_ctos.Login");
            //((IDataParameter)command.Parameters["p_User"]).Value = user;
            //((IDataParameter)command.Parameters["p_Pwd"]).Value = pwd;
            //((IDbDataParameter)command.Parameters["p_ErrorCode"]).Size = 5;

            //CCTDContext.Runner.ExecuteNonQuery(command);

            //string errorCode = (string)((IDataParameter)command.Parameters["p_ErrorCode"]).Value;
            HttpHelper httpHelper = new HttpHelper();
            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Login?user={1}&pwd={2}", host, user, pwd);
            string result= httpHelper.HttpGet(url).Replace("\"","");
            return result.Equals("True");
        }

        public static DataSet GetMenu(string logid)
        {   /*新环境*/
             //string sql = string.Format(@" SELECT M.MENUID, M.MENUCODE, M1.MENUNAME, M.PARENTID, M.ASSEMBLYNAME, M.TYPENAME, M.ORDERID, M.ISSHOW, M.MENUFLAG, M.TIPSINFO
             //                                FROM CTOS.SC_MENU M
             //                                LEFT JOIN CTOS.SC_MENU_LANG M1 on M.menuid=M1.menuid and m1.languageno='zh-CN'
             //                               WHERE EXISTS
             //                               (
             //                                   SELECT 1
             //                                     FROM CTOS.PL_GROUPRIGHT GR
             //                               INNER JOIN CTOS.PL_USERINGROUP UIG ON GR.GR_ID = UIG.UI_GR_ID 
             //                               INNER JOIN CTOS.PL_USER U ON U.US_ID = UIG.UI_US_ID AND U.Us_Logid = '{0}'               
             //                                    WHERE GR.RIGHTID = M.MENUID
             //                               )
             //                                 AND M.ISSHOW = 1
             //                               START WITH M.MENUID = 200100000
             //                         CONNECT BY PRIOR M.MENUID = M.PARENTID
             //                                 ORDER BY LEVEL, M.ORDERID", logid);
            
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

            string host = ConnectionHelper.GetOracleConnectionString("SCT_API_HOST");
            string url = string.Format("{0}/api/Menu?username={1}", host, logid);
            string resultStr = httpHelper.HttpGet(url);
            DataSet result = JsonConvert.DeserializeObject<DataSet>(resultStr);
            //return CCTDContext.Runner.ExecuteDataSet(sql, CommandType.Text);
            return result;
        }
    }
}
