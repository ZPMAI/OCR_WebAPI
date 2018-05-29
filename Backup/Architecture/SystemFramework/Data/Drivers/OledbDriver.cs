using System;
using System.Data;
using System.Data.OleDb;

namespace CCT.SystemFramework.Data.Drivers
{
	/// <summary>
	/// OledbDriver 的摘要说明。
	/// </summary>
	public class OledbDriver : IDriver
	{
		/// 等同于 NOLOCK, 不要发生共享锁, 并且不要提供排他锁, 当此选项生效时, 可能会读取未提交的事务或
		/// 一组在读取中间回滚的页面, 有可能发生脏读, 仅用于 SELECT 语句
		// private const string SET_ISOLATION_UNCOMMITTED = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";

        protected string connectionString;

		/// 构造函数
		public OledbDriver()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        public OledbDriver(string connectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.connectionString = connectionString;
        }

        //2010-12-20 甄耀红增加，为了和小吴DBFramework方式一样
        public string GetDbConnectionString()
        {
            return this.connectionString;
        }


        public IDbConnection GetDbConnection()
        {
            IDbConnection connection = new OleDbConnection(this.connectionString);
            connection.Open();
            return connection;
        }


		public IDbConnection GetDbConnection(string connectionString)
		{
			IDbConnection connection = new OleDbConnection(connectionString);
			connection.Open();

			// using ( IDbCommand command = this.GetDbCommand() )
			// {
			// 	command.Connection = connection;
			// 	command.CommandText = SET_ISOLATION_UNCOMMITTED;
			// 	command.CommandTimeout = 0;
			// 	command.Connection.Open();
			// 	command.ExecuteNonQuery();
			// }
			
			return connection;
		}


		public IDbCommand GetDbCommand()
		{
			return new OleDbCommand();
		}

		public IDbDataAdapter GetDbDataAdapter()
		{
			return new OleDbDataAdapter();
		}


		public void DeriveParameters(IDbCommand command)
		{
			OleDbCommandBuilder.DeriveParameters( (OleDbCommand) command );
		}
	}
}
