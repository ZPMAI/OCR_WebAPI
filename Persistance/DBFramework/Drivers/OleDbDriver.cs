using System;
using System.Data;
using System.Data.OleDb;

namespace DBFramework.Drivers
{
	/// <summary>
    /// OleDbEngine ��ժҪ˵����
	/// </summary>
    public class OleDbDriver : AbstractDriver
	{
        /// ���캯��
        public OleDbDriver(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        protected override IDbConnection OpenDbConnection()
        {
            IDbConnection connection = new OleDbConnection(this.connectionString);
            connection.Open();
            return connection;
        }



        internal override IDbDataAdapter CreateDbDataAdapter()
		{
			return new OleDbDataAdapter();
		}


        internal override IDataParameter CreateDbParameter(string propertyName)
        {
            OleDbParameter oleDbParameter = new OleDbParameter();
            oleDbParameter.ParameterName = propertyName;
            //oleDbParameter.DbType = dbType;
            //oleDbParameter.Size = size;
            return oleDbParameter;
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
            return name;
        }
        
        
        public override IDbCommand CreateDbCommand()
		{
            OleDbCommand command = new OleDbCommand();
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
                    OleDbCommandBuilder.DeriveParameters(command as OleDbCommand);
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
