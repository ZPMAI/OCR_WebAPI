using System;
using System.Data;

using CCT.SystemFramework.Text;
using CCT.SystemFramework.Data.DbRunners;
using CCT.Common;

namespace CCT.SystemFramework.Data
{
	/// <summary>
	/// SqlHelper ��ժҪ˵����
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


		/// ˽�й��캯��
		private SqlHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		/// <summary>
		/// ��ȡ SQL Server ���ݿ������ַ���
		/// </summary>
		/// <param name="databaseName">���ݿ�����</param>
		/// <returns>���ݿ������ַ���</returns>
        //public static string GetSqlServerConnectionString(string databaseName)
        //{
        //    // return string.Format("server=172.16.1.78;user id=dbuser;password=zgrhcumw;database={0}", databaseName);
        //    //return ConStr.DataCenterString(databaseName);			
           
        //}


		/// <summary>
		/// ��ȡ SQL Server ���ݿ������ַ���
		/// </summary>
		/// <param name="ipAddress">IP ��ַ</param>
		/// <param name="databaseName">���ݿ�����</param>
		/// <returns>���ݿ������ַ���</returns>
        //public static string GetSqlServerConnectionString(string ipAddress, string databaseName)
        //{
        //    // return string.Format("server=172.16.1.78;user id=dbuser;password=zgrhcumw;database={0}", databaseName);
        //    return ConStr.SQLServerString(ipAddress, databaseName);			
        //}


		/// <summary>
		/// ��ȡ AS400 ���ݿ������ַ���
		/// </summary>
		/// <returns>���ݿ������ַ���</returns>
        //public static string GetAS400ConnectionString()
        //{
        //    // return "Provider=IBMDA400;Data Source=172.16.1.81;User ID=dbuser;Password=zgrhcumw";
        //    return ConStr.AS400String();			
        //}


		/// <summary>
		/// ��ȡ Oracle ���ݿ������ַ���
		/// </summary>
		/// <param name="serverName">���ݿ��������</param>
		/// <returns>���ݿ������ַ���</returns>
        //public static string GetOracleConnectioniString(string serverName)
        //{
        //    return string.Format("Data Source={0};User Id=bmis;Password=bmis;Persist Security Info=True", serverName);
        //    //return ConStr.ORACLEString(serverName);
        //}
	}
}
