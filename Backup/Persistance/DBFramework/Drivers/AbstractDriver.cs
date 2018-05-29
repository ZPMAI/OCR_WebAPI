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

        /// ���캯��
        public AbstractDriver(string connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            this.connectionString = connectionString;
            this.transactionContainer = new TransactionContainer();
        }


        /// ���÷�����ί�м����첽���� IDbCommand        
        internal T ExecuteDbCommand<T>(Delegate method, params object[] args)
        {
            object target = method.Target;
            IDbCommand command = null;

            // ����ʱ�����ж�
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
                // ����ʹ�� using (IDbConnection connection = OpenDbConnection())
                // ���д򿪵��������������� DataReader���������Ƚ����رա�
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
        /// ����ʹ�û������
        /// </remarks>
        public abstract IDbCommand CreateProcedureCommand(string procedureName);


        #region ���ݿ����񷽷�ϵ��

        /// ��ʼ���ݿ�����       
        public void BeginDbTransaction(IsolationLevel isolationLevel)
        {
            IDbTransaction transaction = this.transactionContainer.GetCurrentTransaction();

            if (transaction == null)
            {
                IDbConnection connection = OpenDbConnection();

                // ֻ֧�� IsolationLevel.ReadCommitted & IsolationLevel.Serializable ��
                IDbTransaction newTransaction = connection.BeginTransaction(isolationLevel);

                this.transactionContainer.Add(newTransaction);
            }
        }

        /// �ύ���ݿ�����
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

        /// �ع����ݿ�����
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
