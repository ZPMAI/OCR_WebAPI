using System;
using System.Data;
using System.Data.OleDb;

namespace CCT.SystemFramework.Data.Drivers
{
	/// <summary>
	/// OledbDriver ��ժҪ˵����
	/// </summary>
	public class OledbDriver : IDriver
	{
		/// ��ͬ�� NOLOCK, ��Ҫ����������, ���Ҳ�Ҫ�ṩ������, ����ѡ����Чʱ, ���ܻ��ȡδ�ύ�������
		/// һ���ڶ�ȡ�м�ع���ҳ��, �п��ܷ������, ������ SELECT ���
		// private const string SET_ISOLATION_UNCOMMITTED = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";

        protected string connectionString;

		/// ���캯��
		public OledbDriver()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        public OledbDriver(string connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            this.connectionString = connectionString;
        }

        //2010-12-20 ��ҫ�����ӣ�Ϊ�˺�С��DBFramework��ʽһ��
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
