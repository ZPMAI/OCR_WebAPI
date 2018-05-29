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


        #region ��� DataSet ����

        /// <summary>
        /// ��� DataSet ����
        /// </summary>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="dataSet">DataSet ����</param>
        //[Obsolete("����ʹ�ø÷�������ʹ�� ExecuteXReader", false)]
        public void FillDataSet(string commandText, CommandType commandType, DataSet dataSet)
        {
            IDbCommand command = this.driver.CreateDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            FillDataSet(command, dataSet);
        }

        /// <summary>
        /// ��� DataSet ����
        /// </summary>
        /// <param name="command">IDbCommand ����</param>
        /// <param name="dataSet">DataSet ����</param>
        //[Obsolete("����ʹ�ø÷�������ʹ�� ExecuteXReader", false)]
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

        #region ִ�����ݿ����������� DataSet ����

        /// <summary>
        /// ִ�����ݿ����������� DataSet ����
        /// </summary>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        //[Obsolete("����ʹ�ø÷�������ʹ�� ExecuteXReader", false)]
        public DataSet ExecuteDataSet(string commandText, CommandType commandType)
        {
            DataSet dataSet = new DataSet();
            FillDataSet(commandText, commandType, dataSet);

            return dataSet;
        }

        /// <summary>
        /// ִ�����ݿ����������� DataSet ����
        /// </summary>
        /// <param name="command">IDbCommand ����</param>
        //[Obsolete("����ʹ�ø÷�������ʹ�� ExecuteXReader", false)]
        public DataSet ExecuteDataSet(IDbCommand command)
        {
            DataSet dataSet = new DataSet();
            FillDataSet(command, dataSet);

            return dataSet;
        }

        #endregion


        #region ִ�����ݿ������������κβ���

        /// <summary>
        /// ִ�����ݿ������������κβ���
        /// </summary>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public void ExecuteNonQuery(string commandText, CommandType commandType)
        {
            IDbCommand command = this.driver.CreateDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            ExecuteNonQuery(command);
        }

        /// <summary>
        /// ִ�����ݿ������������κβ���
        /// </summary>
        /// <param name="command">IDbCommand ����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public void ExecuteNonQuery(IDbCommand command)
        {
            ExecuteNonQueryMethod method = command.ExecuteNonQuery;
            this.driver.ExecuteDbCommand<int>(method);
        }

        #endregion

        #region ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��

        /// <summary>
        /// ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��
        /// </summary>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <returns>������еĵ�һ�еĵ�һ��</returns>
        public object ExecuteScalar(string commandText, CommandType commandType)
        {
            IDbCommand command = this.driver.CreateDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteScalar(command);
        }


        /// <summary>
        /// ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��
        /// </summary>
        /// <param name="command">IDbCommand ����</param>
        /// <returns>������еĵ�һ�еĵ�һ��</returns>
        public object ExecuteScalar(IDbCommand command)
        {
            ExecuteScalarMethod method = command.ExecuteScalar;
            return this.driver.ExecuteDbCommand<object>(method);
        }

        #endregion


        #region ִ�����ݿ����������� IDataReader ����

        /// <summary>
        /// ִ�����ݿ����������� IDataReader ����
        /// </summary>		
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <returns>IDataReader ����</returns>
        internal IDataReader ExecuteReader(string commandText, CommandType commandType)
        {
            IDbCommand command = this.driver.CreateDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteReader(command);
        }


        /// <summary>
        /// ִ�����ݿ����������� IDataReader ����
        /// </summary>		
        /// <param name="command">Command����</param>
        /// <returns>IDataReader ����</returns>
        internal IDataReader ExecuteReader(IDbCommand command)
        {
            ExecuteReaderMethod method = command.ExecuteReader;
            return this.driver.ExecuteDbCommand<IDataReader>(method, CommandBehavior.Default);
        }

        #endregion
    
        #region ִ�����ݿ����������� XDataReader ����

        /// <summary>
        /// ִ�����ݿ����������� XDataReader ����
        /// </summary>		
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <returns>XDataReader ����</returns>
        public XDataReader ExecuteXReader(string commandText, CommandType commandType)
        {
            IDataReader dataReader = ExecuteReader(commandText, commandType);
            return new XDataReader(dataReader);
        }


        /// <summary>
        /// ִ�����ݿ����������� XDataReader ����
        /// </summary>		
        /// <param name="command">Command����</param>
        /// <returns>XDataReader ����</returns>
        public XDataReader ExecuteXReader(IDbCommand command)
        {
            IDataReader dataReader = ExecuteReader(command);
            return new XDataReader(dataReader);
        }

        #endregion



        #region ���ݿ����񷽷�ϵ��

        /// ��ʼ���ݿ�����
        public void BeginDbTransaction()
        {
            BeginDbTransaction(IsolationLevel.ReadCommitted);
        }

        /// ��ʼ���ݿ�����
        public void BeginDbTransaction(IsolationLevel isolationLevel)
        {
            this.driver.BeginDbTransaction(isolationLevel);
        }

        /// �ύ���ݿ�����
        public void CommitDbTransaction()
        {
            this.driver.CommitDbTransaction();
        }

        /// �ع����ݿ�����
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

        #region ���ݴ洢������ʵ�����ִ�����ݿ��������

        /// <summary>
        /// ���ݴ洢����������ʵ�����ִ�����ݿ��������
        /// </summary>
        /// <param name="storedProcedureName">�洢��������</param>
        /// <param name="entity">ʵ�����</param>
        public void ExecuteProcedure(string procedureName, object entity)
        {
            IDbCommand command = this.driver.CreateProcedureCommand(procedureName);

            // ��ʵ�������ȡ����ֵ�������ݿ�����������
            SqlUtil.AssignParameters(command.Parameters, entity);

            this.ExecuteNonQuery(command);

            // �����ݿ�����������ȡ����ֵ����ʵ�����
            SqlUtil.AssignProperties(entity, command.Parameters);
        }

        #endregion
    }
}
