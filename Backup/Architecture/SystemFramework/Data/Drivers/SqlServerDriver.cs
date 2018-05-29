using System;
using System.Data;
using System.Data.SqlClient;

namespace CCT.SystemFramework.Data.Drivers
{
	/// <summary>
	/// SqlServerDriver 的摘要说明。
	/// </summary>
	public class SqlServerDriver : IDriver
	{
		/// 等同于 NOLOCK, 不要发生共享锁, 并且不要提供排他锁, 当此选项生效时, 可能会读取未提交的事务或
		/// 一组在读取中间回滚的页面, 有可能发生脏读, 仅用于 SELECT 语句
		private const string SET_ISOLATION_UNCOMMITTED = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";

        protected string connectionString;

		/// 构造函数
		public SqlServerDriver()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        public SqlServerDriver(string connectionString)
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
            IDbConnection connection = new SqlConnection(this.connectionString);

            using (IDbCommand command = this.GetDbCommand())
            {
                command.Connection = connection;
                command.CommandText = SET_ISOLATION_UNCOMMITTED;
                command.CommandTimeout = 0;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }

            return connection;
        }

		public IDbConnection GetDbConnection(string connectionString)
		{
			IDbConnection connection = new SqlConnection(connectionString);

			using ( IDbCommand command = this.GetDbCommand() )
			{
				command.Connection = connection;
				command.CommandText = SET_ISOLATION_UNCOMMITTED;
				command.CommandTimeout = 0;
				command.Connection.Open();
				command.ExecuteNonQuery();
			}
			
			return connection;
		}


		public IDbCommand GetDbCommand()
		{
			IDbCommand command = new SqlCommand();
			command.CommandTimeout = 1200;

			return command;
		}


		public IDbDataAdapter GetDbDataAdapter()
		{
			return new SqlDataAdapter();
		}

		
		public void DeriveParameters(IDbCommand command)
		{
			SqlCommandBuilder.DeriveParameters( (SqlCommand) command );
		}
	}
}
