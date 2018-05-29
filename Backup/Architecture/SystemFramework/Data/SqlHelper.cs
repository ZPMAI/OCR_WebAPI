using System;
using System.Data;

using CCT.SystemFramework.Text;
using CCT.SystemFramework.Data.DbRunners;
using CCT.Common;

namespace CCT.SystemFramework.Data
{
	/// <summary>
	/// SqlHelper 的摘要说明。
	/// </summary>
	public sealed class SqlHelper
	{
		private static IDbRunner sqlServerRunner;
		private static IDbRunner oledbRunner;
		private static IDbRunner oracleRunner;

		public static IDbRunner SqlServer
		{
			get
			{
				if ( sqlServerRunner == null )
				{
					sqlServerRunner = new SqlServerRunner();
				}

				return sqlServerRunner;
			}
		}

		public static IDbRunner Oledb
		{
			get
			{
				if ( oledbRunner == null )
				{
					oledbRunner = new OledbRunner();
				}

				return oledbRunner;
			}
		}

		public static IDbRunner Oracle
		{
			get
			{
				if ( oracleRunner == null )
				{
					oracleRunner = new OracleRunner();
				}
		 
				return oracleRunner;
			}
		}


		/// 私有构造函数
		private SqlHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		/// <summary>
		/// 获取 SQL Server 数据库连接字符串
		/// </summary>
		/// <param name="databaseName">数据库名称</param>
		/// <returns>数据库连接字符串</returns>
        //public static string GetSqlServerConnectionString(string databaseName)
        //{
        //    // return string.Format("server=172.16.1.78;user id=dbuser;password=zgrhcumw;database={0}", databaseName);
        //    //return ConStr.DataCenterString(databaseName);			
           
        //}


		/// <summary>
		/// 获取 SQL Server 数据库连接字符串
		/// </summary>
		/// <param name="ipAddress">IP 地址</param>
		/// <param name="databaseName">数据库名称</param>
		/// <returns>数据库连接字符串</returns>
        //public static string GetSqlServerConnectionString(string ipAddress, string databaseName)
        //{
        //    // return string.Format("server=172.16.1.78;user id=dbuser;password=zgrhcumw;database={0}", databaseName);
        //    return ConStr.SQLServerString(ipAddress, databaseName);			
        //}


		/// <summary>
		/// 获取 AS400 数据库链接字符串
		/// </summary>
		/// <returns>数据库连接字符串</returns>
        //public static string GetAS400ConnectionString()
        //{
        //    // return "Provider=IBMDA400;Data Source=172.16.1.81;User ID=dbuser;Password=zgrhcumw";
        //    return ConStr.AS400String();			
        //}


		/// <summary>
		/// 获取 Oracle 数据库连接字符串
		/// </summary>
		/// <param name="serverName">数据库服务名称</param>
		/// <returns>数据库连接字符串</returns>
        //public static string GetOracleConnectioniString(string serverName)
        //{
        //    return string.Format("Data Source={0};User Id=bmis;Password=bmis;Persist Security Info=True", serverName);
        //    //return ConStr.ORACLEString(serverName);
        //}
	}
}
