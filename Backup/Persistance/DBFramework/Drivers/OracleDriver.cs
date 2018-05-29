using System;
using System.Data;
using System.Data.OracleClient;

namespace DBFramework.Drivers
{
	/// <summary>
    /// OracleEngine 的摘要说明。
	/// </summary>
    public class OracleDriver : AbstractDriver
	{
        /// 构造函数
        public OracleDriver(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        protected override IDbConnection OpenDbConnection()
        {
            IDbConnection connection = new OracleConnection(this.connectionString);
            connection.Open();
            return connection;
        }


        internal override IDbDataAdapter CreateDbDataAdapter()
		{
			return new OracleDataAdapter();
		}


        internal override IDataParameter CreateDbParameter(string propertyName)
        {
            OracleParameter oracleParameter = new OracleParameter();
            oracleParameter.ParameterName = propertyName;
            //oracleParameter.DbType = dbType;
            //oracleParameter.Size = size;
            return oracleParameter;
        }


        internal override string ToFullTableName(string dbName, string tableName)
        {
            if (string.IsNullOrEmpty(dbName))
            {
                return tableName;
            }

            return string.Format("{0}.{1}", dbName, tableName);
        }

        internal override string ToSQLParameterName(string name)
        {
            return string.Format(":{0}", name);
        }


        public override IDbCommand CreateDbCommand()
        {
            OracleCommand command = new OracleCommand();
            return command;
        }


        public override IDbCommand CreateProcedureCommand(string procedureName)
        {
            using (IDbConnection connection = OpenDbConnection())
            {
                IDbCommand command = new OracleCommand();
                command.Connection = connection;
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    OracleCommandBuilder.DeriveParameters(command as OracleCommand);
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
