using System;
using System.Data;
using System.Data.SqlClient;

namespace DBFramework.Drivers
{
	/// <summary>
    /// SqlServerEngine 的摘要说明。
	/// </summary>
    public class MsSqlDriver : AbstractDriver
	{
        /// 构造函数
        public MsSqlDriver(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        protected override IDbConnection OpenDbConnection()
        {
            // 等同于 NOLOCK, 不要发生共享锁, 并且不要提供排他锁, 当此选项生效时, 可能会读取未提交的事务或一组在读取中间回滚的页面, 有可能发生脏读, 仅用于 SELECT 语句
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
                    // 如果分配给命令的连接位于本地挂起事务中，DeriveParameters 要求命令拥有事务。命令的 Transaction 属性尚未初始化。
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
