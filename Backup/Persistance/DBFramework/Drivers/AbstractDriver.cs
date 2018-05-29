using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace DBFramework.Drivers
{
    internal delegate int ExecuteNonQueryMethod();
    internal delegate object ExecuteScalarMethod();
    internal delegate int FillMethod(DataSet dataSet);
    internal delegate IDataReader ExecuteReaderMethod(CommandBehavior commandBehavior);

    public abstract class AbstractDriver
    {
        protected readonly string connectionString;
        protected readonly TransactionContainer transactionContainer;

        /// 构造函数
        public AbstractDriver(string connectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.connectionString = connectionString;
            this.transactionContainer = new TransactionContainer();
        }


        /// 采用泛型与委托技术异步调用 IDbCommand        
        internal T ExecuteDbCommand<T>(Delegate method, params object[] args)
        {
            object target = method.Target;
            IDbCommand command = null;

            // 运行时类型判断
            if (target is IDbCommand)
            {
                command = target as IDbCommand;
            }
            else if (target is IDbDataAdapter)
            {
                command = (target as IDbDataAdapter).SelectCommand;
            }
            
            IDbTransaction transaction = this.transactionContainer.GetCurrentTransaction();
            
            if (transaction != null)
            {
                command.Transaction = transaction;
                command.Connection = transaction.Connection;

                return (T)method.DynamicInvoke(args);
            }
            else
            {
                // 不能使用 using (IDbConnection connection = OpenDbConnection())
                // 已有打开的与此命令相关联的 DataReader，必须首先将它关闭。
                IDbConnection connection = OpenDbConnection();
                
                try
                {                    
                    command.Connection = connection;
                    //command.Prepare();
                    return (T)method.DynamicInvoke(args);
                }
                finally
                {
                    if (!(method is ExecuteReaderMethod))
                    {
                        connection.Close();
                    }
                }
            }
        }
        
        protected abstract IDbConnection OpenDbConnection();

        internal abstract IDbDataAdapter CreateDbDataAdapter();

        internal abstract IDataParameter CreateDbParameter(string propertyName);

        /// <example>
        /// SQL Server : EHR..t_App, ORACLE : CCTDB.T_CIC_CHECKTYPE, DB2 : PCTCSE..CTHNDL
        /// </example>
        internal abstract string ToFullTableName(string dbName, string tableName);

        /// <example>
        /// :ParameterName, @ParameterName
        /// </example>
        internal abstract string ToSQLParameterName(string name);

        public abstract IDbCommand CreateDbCommand();

        /// <remarks>
        /// 可以使用缓存策略
        /// </remarks>
        public abstract IDbCommand CreateProcedureCommand(string procedureName);


        #region 数据库事务方法系列

        /// 开始数据库事务       
        public void BeginDbTransaction(IsolationLevel isolationLevel)
        {
            IDbTransaction transaction = this.transactionContainer.GetCurrentTransaction();

            if (transaction == null)
            {
                IDbConnection connection = OpenDbConnection();

                // 只支持 IsolationLevel.ReadCommitted & IsolationLevel.Serializable ？
                IDbTransaction newTransaction = connection.BeginTransaction(isolationLevel);

                this.transactionContainer.Add(newTransaction);
            }
        }

        /// 提交数据库事务
        public void CommitDbTransaction()
        {
            IDbTransaction transaction = this.transactionContainer.GetCurrentTransaction();

            if (transaction != null)
            {
                try
                {
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    this.transactionContainer.Remove();
                }
            }
        }

        /// 回滚数据库事务
        public void RollBackDbTransaction()
        {
            IDbTransaction transaction = this.transactionContainer.GetCurrentTransaction();

            if (transaction != null)
            {
                try
                {
                    transaction.Rollback();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    this.transactionContainer.Remove();
                }
            }
        }

        #endregion
    }
}
