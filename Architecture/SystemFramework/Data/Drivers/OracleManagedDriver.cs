using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace CCT.SystemFramework.Data.Drivers
{
	/// <summary>
	/// OracleDriver 的摘要说明。
	/// </summary>
	public class OracleManagedDriver : IDriver
	{
        protected string connectionString;

        public OracleManagedDriver()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        public OracleManagedDriver(string connectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.connectionString = connectionString;
        }

        public string GetDbConnectionString()
        {
            return this.connectionString;
        }


        public IDbConnection GetDbConnection()
        {
            IDbConnection connection = new OracleConnection(this.connectionString);
            connection.Open();

            return connection;
        }


        public IDbConnection GetDbConnection(string connectionString)
		{
			IDbConnection connection = new OracleConnection(connectionString);
			connection.Open();

			return connection;
		}


		public IDbCommand GetDbCommand()
		{
			return new OracleCommand();
		}


		public IDbDataAdapter GetDbDataAdapter()
		{
			return new OracleDataAdapter();
		}


		public void DeriveParameters(IDbCommand command)
		{
			OracleCommandBuilder.DeriveParameters( (OracleCommand) command );
		}
	}
}
