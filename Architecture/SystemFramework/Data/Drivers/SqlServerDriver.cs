using System;
using System.Data;
using System.Data.SqlClient;

namespace CCT.SystemFramework.Data.Drivers
{
	/// <summary>
	/// SqlServerDriver ��ժҪ˵����
	/// </summary>
	public class SqlServerDriver : IDriver
	{
		/// ��ͬ�� NOLOCK, ��Ҫ����������, ���Ҳ�Ҫ�ṩ������, ����ѡ����Чʱ, ���ܻ��ȡδ�ύ�������
		/// һ���ڶ�ȡ�м�ع���ҳ��, �п��ܷ������, ������ SELECT ���
		private const string SET_ISOLATION_UNCOMMITTED = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";

        protected string connectionString;

		/// ���캯��
		public SqlServerDriver()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        public SqlServerDriver(string connectionString)
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
