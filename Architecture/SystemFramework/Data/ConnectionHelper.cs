using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CCT.Common.Encrypt;
using System.Configuration;

namespace CCT.SystemFramework.Data
{
    public static class ConnectionHelper
    {
        static readonly Dictionary<string, string> dictionary = new Dictionary<string, string>();

        /// 静态构造函数
        //static ConnectionHelper()
        //{
        //    const string sql = "SELECT ServerName, ServerType, UserName, passWord, ISNULL(oracleTNS, ''), CPlusStr FROM [Right].dbo.Sys_supervisor";
        //    const string connectionString = "server=172.16.1.78;user id=CCTUSER;password=CCTUSER";
        //    SqlConnection sqlConnection = new SqlConnection(connectionString);

        //    try
        //    {
        //        sqlConnection.Open();
        //        SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
        //        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

        //        while (sqlDataReader.Read())
        //        {
        //            string serverName = sqlDataReader.GetString(0);
        //            string serverType = sqlDataReader.GetString(1);
        //            string userName = sqlDataReader.GetString(2);
        //            string passWord = sqlDataReader.GetString(3);
        //            string oracleTNS = sqlDataReader.GetString(4);
        //            string cPlusStr = sqlDataReader.GetString(5);

        //            string value;

        //            if (serverType.Equals("ORACLE"))
        //            {
        //                value = string.Format("Password={0};User ID={1};Data Source={2};Persist Security Info=True", Encryption.Decrypt(passWord), userName, oracleTNS);
        //            }
        //            else
        //            {
        //                value = Encryption.DesDecrypt(cPlusStr);
        //            }

        //            dictionary.Add(serverName, value);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //string s = exp.Message;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //}

        static ConnectionHelper()
        {
            string[] keys = System.Configuration.ConfigurationManager.AppSettings.AllKeys;
            foreach (var c in keys)
            {

                dictionary.Add(c, EncryptHelper.DecryptDES(ConfigurationManager.AppSettings[c], EncryptHelper.encryptKey));
            }
        }

        /// 获取 MsSqlServer 数据库连接字符串
        public static string GetMsSqlConnectionString(string ipAddress, string dbName)
        {
            return string.Format("{0};database={1}", dictionary[ipAddress], dbName);
        }

        /// 获取 Oracle 数据库连接字符串
        public static string GetOracleConnectionString(string database)
        {
            return dictionary[database];
        }
    }
}