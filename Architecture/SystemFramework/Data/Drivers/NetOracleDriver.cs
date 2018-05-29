using System;
using System.Data;
using System.Data.OracleClient;

namespace CCT.SystemFramework.Data.Drivers
{
    /// <summary>
    /// NetOracleDriver ��ժҪ˵����
    /// �м���������ʱʹ��
    /// </summary>
    public class NetOracleDriver : IDriver
    {
        protected string connectionString;

        public NetOracleDriver()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
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
            OracleCommandBuilder.DeriveParameters((OracleCommand)command);
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
    }
}
