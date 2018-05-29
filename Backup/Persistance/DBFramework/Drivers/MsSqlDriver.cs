using System;
using System.Data;
using System.Data.SqlClient;

namespace DBFramework.Drivers
{
	/// <summary>
    /// SqlServerEngine ��ժҪ˵����
	/// </summary>
    public class MsSqlDriver : AbstractDriver
	{
        /// ���캯��
        public MsSqlDriver(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        protected override IDbConnection OpenDbConnection()
        {
            // ��ͬ�� NOLOCK, ��Ҫ����������, ���Ҳ�Ҫ�ṩ������, ����ѡ����Чʱ, ���ܻ��ȡδ�ύ�������һ���ڶ�ȡ�м�ع���ҳ��, �п��ܷ������, ������ SELECT ���
            const string SET_ISOLATION_UNCOMMITTED = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";

            IDbConnection connection = new SqlConnection(this.connectionString);

            using (IDbCommand command = this.CreateDbCommand())
            {                
                command.Connection = connection;
                command.CommandText = SET_ISOLATION_UNCOMMITTED;
                command.CommandTimeout = 0;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }

            return connection;
        }


        internal override IDbDataAdapter CreateDbDataAdapter()
		{
			return new SqlDataAdapter();
		}


        internal override IDataParameter CreateDbParameter(string propertyName)
        {
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = string.Format("@{0}", propertyName);
            //sqlParameter.DbType = dbType;
            //sqlParameter.Size = size;
            return sqlParameter;
        }

        internal override string ToFullTableName(string dbName, string tableName)
        {
            if (string.IsNullOrEmpty(dbName))
            {
                return tableName;
            }

            return string.Format("{0}..{1}", dbName, tableName);
        }

        internal override string ToSQLParameterName(string name)
        {
            return string.Format("@{0}", name);
        }


        public override IDbCommand CreateDbCommand()
        {
            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 1200;            
            return command;
        }


        public override IDbCommand CreateProcedureCommand(string procedureName)
        {
            using (IDbConnection connection = OpenDbConnection())
            {
                IDbCommand command = CreateDbCommand();
                command.Connection = connection;
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    // �����������������λ�ڱ��ع��������У�DeriveParameters Ҫ������ӵ����������� Transaction ������δ��ʼ����
                    SqlCommandBuilder.DeriveParameters(command as SqlCommand);
                    return command;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
	}
}
