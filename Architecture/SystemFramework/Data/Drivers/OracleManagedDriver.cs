using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace CCT.SystemFramework.Data.Drivers
{
	/// <summary>
	/// OracleDriver ��ժҪ˵����
	/// </summary>
	public class OracleManagedDriver : IDriver
	{
        protected string connectionString;

        public OracleManagedDriver()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        public OracleManagedDriver(string connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
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
