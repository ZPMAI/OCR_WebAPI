using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using DBFramework.Drivers;
using CCT.DBFramework;

namespace DBFramework.Runners
{
    public abstract partial class AbstractDbRunner: IDbRunner
    {


        #region 填充 DataSet 对象

        /// <summary>
        /// 填充 DataSet 对象
        /// </summary>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="dataSet">DataSet 对象</param>
        //[Obsolete("不再使用该方法，请使用 ExecuteXReader", false)]
        public void FillDataSet(string commandText, CommandType commandType, DataSet dataSet)
        {
            IDbCommand command = this.driver.CreateDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            FillDataSet(command, dataSet);
        }

        /// <summary>
        /// 填充 DataSet 对象
        /// </summary>
        /// <param name="command">IDbCommand 对象</param>
        /// <param name="dataSet">DataSet 对象</param>
        //[Obsolete("不再使用该方法，请使用 ExecuteXReader", false)]
        public void FillDataSet(IDbCommand command, DataSet dataSet)
        {
            IDbDataAdapter dataAdapter = this.driver.CreateDbDataAdapter();
            dataAdapter.SelectCommand = command;
            
            string tableName = "Table";

            for (int index = 0; index < dataSet.Tables.Count; index++)
            {
                dataAdapter.TableMappings.Add(tableName, dataSet.Tables[index].TableName);
                tableName += (index + 1).ToString();
            }

            FillMethod method = dataAdapter.Fill;
            this.driver.ExecuteDbCommand<int>(method, dataSet);
        }

        #endregion

        #region 执行数据库操作命令并返回 DataSet 对象

        /// <summary>
        /// 执行数据库操作命令并返回 DataSet 对象
        /// </summary>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        //[Obsolete("不再使用该方法，请使用 ExecuteXReader", false)]
        public DataSet ExecuteDataSet(string commandText, CommandType commandType)
        {
            DataSet dataSet = new DataSet();
            FillDataSet(commandText, commandType, dataSet);

            return dataSet;
        }

        /// <summary>
        /// 执行数据库操作命令并返回 DataSet 对象
        /// </summary>
        /// <param name="command">IDbCommand 对象</param>
        //[Obsolete("不再使用该方法，请使用 ExecuteXReader", false)]
        public DataSet ExecuteDataSet(IDbCommand command)
        {
            DataSet dataSet = new DataSet();
            FillDataSet(command, dataSet);

            return dataSet;
        }

        #endregion


        #region 执行数据库操作命令不返回任何参数

        /// <summary>
        /// 执行数据库操作命令不返回任何参数
        /// </summary>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <returns>操作是否成功</returns>
        public void ExecuteNonQuery(string commandText, CommandType commandType)
        {
            IDbCommand command = this.driver.CreateDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            ExecuteNonQuery(command);
        }

        /// <summary>
        /// 执行数据库操作命令不返回任何参数
        /// </summary>
        /// <param name="command">IDbCommand 对象</param>
        /// <returns>操作是否成功</returns>
        public void ExecuteNonQuery(IDbCommand command)
        {
            ExecuteNonQueryMethod method = command.ExecuteNonQuery;
            this.driver.ExecuteDbCommand<int>(method);
        }

        #endregion

        #region 执行数据库操作命令并返回结果集中的第一行的第一列

        /// <summary>
        /// 执行数据库操作命令并返回结果集中的第一行的第一列
        /// </summary>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <returns>结果集中的第一行的第一列</returns>
        public object ExecuteScalar(string commandText, CommandType commandType)
        {
            IDbCommand command = this.driver.CreateDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteScalar(command);
        }


        /// <summary>
        /// 执行数据库操作命令并返回结果集中的第一行的第一列
        /// </summary>
        /// <param name="command">IDbCommand 对象</param>
        /// <returns>结果集中的第一行的第一列</returns>
        public object ExecuteScalar(IDbCommand command)
        {
            ExecuteScalarMethod method = command.ExecuteScalar;
            return this.driver.ExecuteDbCommand<object>(method);
        }

        #endregion


        #region 执行数据库操作命令并返回 IDataReader 对象

        /// <summary>
        /// 执行数据库操作命令并返回 IDataReader 对象
        /// </summary>		
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <returns>IDataReader 对象</returns>
        internal IDataReader ExecuteReader(string commandText, CommandType commandType)
        {
            IDbCommand command = this.driver.CreateDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteReader(command);
        }


        /// <summary>
        /// 执行数据库操作命令并返回 IDataReader 对象
        /// </summary>		
        /// <param name="command">Command对象</param>
        /// <returns>IDataReader 对象</returns>
        internal IDataReader ExecuteReader(IDbCommand command)
        {
            ExecuteReaderMethod method = command.ExecuteReader;
            return this.driver.ExecuteDbCommand<IDataReader>(method, CommandBehavior.Default);
        }

        #endregion
    
        #region 执行数据库操作命令并返回 XDataReader 对象

        /// <summary>
        /// 执行数据库操作命令并返回 XDataReader 对象
        /// </summary>		
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <returns>XDataReader 对象</returns>
        public XDataReader ExecuteXReader(string commandText, CommandType commandType)
        {
            IDataReader dataReader = ExecuteReader(commandText, commandType);
            return new XDataReader(dataReader);
        }


        /// <summary>
        /// 执行数据库操作命令并返回 XDataReader 对象
        /// </summary>		
        /// <param name="command">Command对象</param>
        /// <returns>XDataReader 对象</returns>
        public XDataReader ExecuteXReader(IDbCommand command)
        {
            IDataReader dataReader = ExecuteReader(command);
            return new XDataReader(dataReader);
        }

        #endregion



        #region 数据库事务方法系列

        /// 开始数据库事务
        public void BeginDbTransaction()
        {
            BeginDbTransaction(IsolationLevel.ReadCommitted);
        }

        /// 开始数据库事务
        public void BeginDbTransaction(IsolationLevel isolationLevel)
        {
            this.driver.BeginDbTransaction(isolationLevel);
        }

        /// 提交数据库事务
        public void CommitDbTransaction()
        {
            this.driver.CommitDbTransaction();
        }

        /// 回滚数据库事务
        public void RollBackDbTransaction()
        {
            this.driver.RollBackDbTransaction();
        }

        #endregion


        public IDbCommand CreateDbCommand()
        {            
            return this.driver.CreateDbCommand();
        }

        public IDbCommand CreateProcedureCommand(string procedureName)
        {
            return this.driver.CreateProcedureCommand(procedureName);
        }

        #region 根据存储过程与实体对象执行数据库操作命令

        /// <summary>
        /// 根据存储过程名称与实体对象执行数据库操作命令
        /// </summary>
        /// <param name="storedProcedureName">存储过程名称</param>
        /// <param name="entity">实体对象</param>
        public void ExecuteProcedure(string procedureName, object entity)
        {
            IDbCommand command = this.driver.CreateProcedureCommand(procedureName);

            // 从实体对象中取参数值赋给数据库操作命令参数
            SqlUtil.AssignParameters(command.Parameters, entity);

            this.ExecuteNonQuery(command);

            // 从数据库操作命令参数取返回值赋给实体对象
            SqlUtil.AssignProperties(entity, command.Parameters);
        }

        #endregion
    }
}
