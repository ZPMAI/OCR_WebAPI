using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using CCT.DBFramework;
using System.Collections;
using System.Reflection;

namespace DBFramework.Runners
{
    public abstract class AbstractDbRunner: IDbRunner
    {

        protected IDriver driver = null;

        /// ���캯��
        public AbstractDbRunner()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        public AbstractDbRunner(string connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        protected virtual string DriverType
        {
            get
            {
                return "SQLSERVER";
            }
        }


        #region ��ʽ���洢���̲�������

        /// <summary>
        /// ��ʽ���洢���̲�������
        /// </summary>
        /// <param name="spParameterName">�洢���̲�������</param>
        /// <returns>��������</returns>
        private string FormatSpParameterName(string spParameterName)
        {
            string parameterName = spParameterName;

            if (DriverType == "SQLSERVER")
            {
                // ȥ��@�ַ�
                if (parameterName.Substring(0, 1).Equals("@"))
                {
                    parameterName = spParameterName.Substring(1);
                }
            }
            else if (DriverType == "ORACLE")
            {
                // ȥ��p_�ַ�
                if (parameterName.Substring(0, 2).ToUpper().Equals("P_"))
                {
                    parameterName = spParameterName.Substring(2);
                }
            }

            return parameterName;
        }

        #endregion

        #region ��¡���ݿ���������������

        /// <summary>
        /// ��¡���ݿ���������������
        /// </summary>
        /// <param name="parameters">ԭ���ݿ���������������</param>
        /// <returns>���ݿ���������������</returns>
        private IDataParameter[] CloneParameters(IDataParameter[] parameters)
        {
            IDataParameter[] clonedParameters = new IDataParameter[parameters.Length];

            for (int i = 0, j = parameters.Length; i < j; i++)
            {
                clonedParameters[i] = (IDataParameter)((ICloneable)parameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion

        #region �Ӵ洢�����м���������Ϣ

        /// <summary>
        /// �Ӵ洢�����м���������Ϣ
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandText">���ݿ��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <returns>���ݿ���������������</returns>
        private IDataParameter[] DiscoverSpParameterSet(string connectionString, string commandText, System.Data.CommandType commandType)
        {
            #region ������ʾ

            if ((connectionString == null)
                || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("Connection");
            }

            if ((commandText == null)
                || (commandText.Length == 0))
            {
                throw new ArgumentNullException("storedProcedureName");
            }

            #endregion

            // string xmlFilePath = ( ConfigurationSettings.AppSettings["XMLFilePath"] == null ) ? "XMLFilePath" : ConfigurationSettings.AppSettings["XMLFilePath"];
            // string filePath = String.Format(@"{0}\{1}.xml", xmlFilePath, commandText);

            // ��ȡXML�ļ���IDataParameter[]������з����л�
            // IDataParameter[] dataParameters = CacheHelper.CacheDeserialize(filePath, typeof(IDataParameter[]), true) as IDataParameter[];

            // if ( dataParameters == null )
            // {

            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            // Note: �˴������ݿ����Ӷ���������Transaction.Connection	
            IDbConnection connection = driver.GetDbConnection(connectionString);

            try
            {
                command.Connection = connection;
                driver.DeriveParameters(command);
                IDataParameter[] dataParameters = new IDataParameter[command.Parameters.Count];
                command.Parameters.CopyTo(dataParameters, 0);
                return CloneParameters(dataParameters);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            // // ��XML�ж�IDataParameter[]������д��л�
            // CacheHelper.CacheSerialize(filePath, dataParameters, true);
            // }
        }

        #endregion


        #region �� DataRow ������ȡ����ֵ�������ݿ�����������

        /// <summary>
        /// �� DataRow ������ȡ����ֵ�������ݿ�����������
        /// </summary>
        /// <param name="dataParameters">DataParameter���󼯺�</param>
        /// <param name="dataRow">����ֵ(DataRow)</param>
        private void AssignParameterValues(IDataParameterCollection dataParameters, DataRow dataRow)
        {
            #region ������ʾ

            if (dataRow == null)
            {
                return;
            }

            #endregion

            // Reset SqlParameterCollection(���ò���Ĭ��ֵ)
            // SetDefaultValue(dataParameters);

            foreach (IDataParameter dataParameter in dataParameters)
            {
                #region ������ʾ

                if ((dataParameter.ParameterName == null)
                    || (dataParameter.ParameterName.Length <= 1))
                {
                    throw new ArgumentNullException("���ݿ�������������Ϊ��");
                }

                #endregion

                string parameterName = this.FormatSpParameterName(dataParameter.ParameterName);

                if (dataRow.Table.Columns.IndexOf(parameterName) != -1)
                {
                    // �������ֵΪ��, ��ʹ��Ĭ��ֵ
                    // if ( dataRow[parameterName] != DBNull.Value )
                    // {
                    dataParameter.Value = dataRow[parameterName];
                    // }
                }
            }
        }


        #endregion

        #region �����ݿ�����������ȡ����ֵ���� DataRow ����

        /// <summary>
        /// �����ݿ�����������ȡ����ֵ���� DataRow ����
        /// </summary>
        /// <param name="dataRow">DataRow ����</param>
        /// <param name="dataParameters">IDataParameter ���󼯺�</param>
        private void AssignDataRowValues(DataRow dataRow, IDataParameterCollection dataParameters)
        {
            #region ������ʾ

            if (dataRow == null)
            {
                return;
            }

            #endregion

            foreach (IDataParameter dataParameter in dataParameters)
            {
                #region ������ʾ

                if ((dataParameter.ParameterName == null)
                    || (dataParameter.ParameterName.Length <= 1))
                {
                    throw new ArgumentNullException("���ݿ�����������Ϊ��");
                }

                #endregion

                if ((dataParameter.Direction == ParameterDirection.InputOutput)
                    || (dataParameter.Direction == ParameterDirection.Output))
                {
                    string parameterName = this.FormatSpParameterName(dataParameter.ParameterName);

                    if (dataRow.Table.Columns.IndexOf(parameterName) != -1)
                    {
                        dataRow[parameterName] = dataParameter.Value;
                    }
                }
            }
        }


        #endregion


        #region ��ʵ�������ȡ����ֵ�������ݿ�����������

        /// <summary>
        /// ��ʵ�������ȡ����ֵ�������ݿ�����������
        /// </summary>
        /// <param name="dataParameters">SqlParameter���󼯺�</param>
        /// <param name="model">ʵ�����</param>
        private void AssignParameterValues(IDataParameterCollection dataParameters, object model)
        {
            // Reset SqlParameterCollection(���ò���Ĭ��ֵ)
            // SetDefaultValue(dataParameters);

            Type type = model.GetType();

            foreach (IDataParameter dataParameter in dataParameters)
            {
                #region ������ʾ

                if ((dataParameter.ParameterName == null)
                    || (dataParameter.ParameterName.Length <= 1))
                {
                    throw new Exception("���ݿ�����������Ϊ��");
                }

                #endregion

                string parameterName = this.FormatSpParameterName(dataParameter.ParameterName);

                PropertyInfo propertyInfo = type.GetProperty(parameterName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);

                if ((propertyInfo != null) && (propertyInfo.CanRead))
                // && ( propertyInfo.GetValue(model, null) != null ) )
                {
                    object _value = propertyInfo.GetValue(model, null);

                    if (_value == null)
                    {
                        dataParameter.Value = DBNull.Value;
                    }
                    else
                    {
                        dataParameter.Value = _value;
                    }
                }
            }
        }


        #endregion

        #region �����ݿ�����������ȡ����ֵ����ʵ�����

        /// <summary>
        /// �����ݿ�����������ȡ����ֵ����ʵ�����
        /// </summary>
        /// <param name="model">ʵ�����</param>
        /// <param name="dataParameters">IDataParameter���󼯺�</param>
        private void AssignModelValues(object model, IDataParameterCollection dataParameters)
        {
            Type type = model.GetType();

            foreach (IDataParameter dataParameter in dataParameters)
            {
                #region ������ʾ

                if ((dataParameter.ParameterName == null)
                    || (dataParameter.ParameterName.Length <= 1))
                {
                    throw new Exception("���ݿ�����������Ϊ��");
                }

                #endregion

                if ((dataParameter.Direction == ParameterDirection.InputOutput)
                    || (dataParameter.Direction == ParameterDirection.Output))
                {
                    string parameterName = this.FormatSpParameterName(dataParameter.ParameterName);

                    PropertyInfo propertyInfo = type.GetProperty(parameterName);

                    if ((propertyInfo != null) && (propertyInfo.CanWrite))
                    {
                        propertyInfo.SetValue(model, dataParameter.Value, null);
                    }
                }
            }
        }


        #endregion

        #region �� IDataReader ����ת��Ϊʵ����󼯺�

        /// <summary>
        /// �� IDataReader ����ת��Ϊʵ����󼯺�
        /// </summary>
        /// <param name="dataReader">IDataReader ����</param>
        /// <param name="type">ʵ���������</param>
        /// <returns>ʵ����󼯺�</returns>
        private IList DataReaderToModel(IDataReader dataReader, Type type)
        {
            PropertyInfo[] propertyInfos = type.GetProperties();

            // Ϊ�˽�ʡʱ��, ���Ե���һ�� GetOrdinal, Ȼ�󽫽����������������Ա���ѭ����ʹ�á�
            int[] indexList = new int[propertyInfos.Length];

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                try
                {
                    indexList[i] = dataReader.GetOrdinal(propertyInfos[i].Name);
                }
                catch (IndexOutOfRangeException)
                {
                    // ��ָ�������Ʋ�����Ч�������ƣ������������������ݿ��ֶ����Ʋ�ƥ��
                    // throw;
                    indexList[i] = -1;
                }
            }

            IList modelList = new ArrayList();

            while (dataReader.Read())
            {
                object obj = Activator.CreateInstance(type);

                for (int j = 0; j < propertyInfos.Length; j++)
                {
                    if ((propertyInfos[j].CanWrite)
                        && (indexList[j] != -1)
                        && (dataReader.GetValue(indexList[j]) != DBNull.Value))
                    {
                        propertyInfos[j].SetValue(obj, dataReader.GetValue(indexList[j]), null);
                    }
                }

                modelList.Add(obj);
            }

            dataReader.Close();

            return modelList;
        }

        #endregion


        #region ���ݴ洢�����������ɴ������� IDbCommand ����

        /// <summary>
        /// ���ݴ洢�����������ɴ������� IDbCommand ����
        /// </summary>
        /// <param name="commandText">���ݿ��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <returns>IDbCommand ����</returns>
        /// 2010-12-22��ҫ������Ϊ�˱�֤��С��ʹ�÷���ͳһ
        public IDbCommand CreateDbCommand(string commandText, CommandType commandType)
        {
            return CreateDbCommand(driver.GetDbConnectionString(), commandText, commandType);

        }

        /// <summary>
        /// ���ݴ洢�����������ɴ������� IDbCommand ����
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandText">���ݿ��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <returns>IDbCommand ����</returns>
        public IDbCommand CreateDbCommand(string connectionString, string commandText, CommandType commandType)
        {
            #region ������ʾ

            if ((connectionString == null)
                || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }

            if ((commandText == null)
                || (commandText.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }

            #endregion

            IDbCommand command = driver.GetDbCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;

            if (commandType == CommandType.StoredProcedure)
            {
                IDataParameter[] dataParameters = this.DiscoverSpParameterSet(connectionString, commandText, commandType);

                foreach (IDataParameter dataParameter in dataParameters)
                {
                    command.Parameters.Add(dataParameter);
                }
            }

            return command;
        }


        #endregion


        #region ���ݴ洢���������� DataTable ����ִ�����ݿ��������

        /// <summary>
        /// ���ݴ洢���������� DataTable ����ִ�����ݿ��������
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandText">���ݿ��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="dataTable">DataTable ����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool ExecuteDataTableTypedParams(string connectionString, string commandText, CommandType commandType, DataTable dataTable)
        {
            IDbCommand command = this.CreateDbCommand(connectionString, commandText, commandType);
            IDbConnection connection = driver.GetDbConnection(connectionString);

            try
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    // ��DataRow������ȡ����ֵ�������ݿ�����������
                    AssignParameterValues(command.Parameters, dataRow);

                    if (ExecuteNonQuery(connection, command))
                    {
                        // �����ݿ�����������ȡ����ֵ����DataRow����
                        AssignDataRowValues(dataRow, command.Parameters);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }

            return true;
        }


        /// <summary>
        /// ���ݴ洢���������� DataTable ����ִ�����ݿ��������
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="commandText">���ݿ��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="dataTable">DataTable ����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool ExecuteDataTableTypedParams(IDbTransaction transaction, string commandText, CommandType commandType, DataTable dataTable)
        {
            string connectionString = transaction.Connection.ConnectionString;
            IDbCommand command = this.CreateDbCommand(connectionString, commandText, commandType);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                // ��DataRow������ȡ����ֵ�������ݿ�����������
                AssignParameterValues(command.Parameters, dataRow);

                if (ExecuteNonQuery(transaction, command))
                {
                    // �����ݿ�����������ȡ����ֵ����DataRow����
                    AssignDataRowValues(dataRow, command.Parameters);
                }
                else
                {
                    return false;
                }
            }

            command.Parameters.Clear();

            return true;
        }


        #endregion

        #region ��ҳ��ѯ������SQL Server �洢���� SplitPage ���� DataSet��

        /// <summary>
        /// ��ҳ��ѯ������SQL Server �洢���� SplitPage ���� DataSet��
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="execSql">T-SQL ���ݿ���������ַ���</param>
        /// <param name="currentPage">��ǰҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="recordCount">�ܼ�¼��</param>
        /// <param name="pageCount">��ҳ��</param>
        /// <param name="dataSet">DataSet ����</param>
        public void SplitPage(string connectionString, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, DataSet dataSet)
        {
            IDbCommand command = CreateDbCommand(connectionString, "SplitPage", CommandType.StoredProcedure);

            ((IDataParameter)command.Parameters["@ExecSql"]).Value = execSql;
            ((IDataParameter)command.Parameters["@CurrentPage"]).Value = currentPage;
            ((IDataParameter)command.Parameters["@PageSize"]).Value = pageSize;

            using (DataSet tempDS = ExecuteDataSet(connectionString, command))
            {
                recordCount = Convert.ToInt32(((IDataParameter)command.Parameters["@RecordCount"]).Value);
                pageCount = Convert.ToInt32(((IDataParameter)command.Parameters["@PageCount"]).Value);

                // Note: �洢����SplitPage���ɵļ�¼���ڱ�2����
                if (tempDS.Tables[1] != null)
                {
                    if (dataSet.Tables.Count > 0)
                    {
                        foreach (DataRow dataRow in tempDS.Tables[1].Rows)
                        {
                            dataSet.Tables[0].ImportRow(dataRow);
                        }
                    }
                    else
                    {
                        // Add By RQ 2008-12-08 11:03 ���ӿ� DataSet �Ŀ���
                        dataSet.Tables.Add(tempDS.Tables[1].Copy());
                        dataSet.Tables[0].TableName = tempDS.Tables[1].TableName;
                    }
                }
            }
        }


        /// <summary>
        /// ��ҳ��ѯ������SQL Server �洢���� SplitPage ���� DataSet��
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="execSql">T-SQL ���ݿ���������ַ���</param>
        /// <param name="currentPage">��ǰҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="recordCount">�ܼ�¼��</param>
        /// <param name="pageCount">��ҳ��</param>
        /// <param name="dataSet">DataSet ����</param>
        public void SplitPage(IDbTransaction transaction, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, DataSet dataSet)
        {
            IDbCommand command = CreateDbCommand(transaction.Connection.ConnectionString, "SplitPage", CommandType.StoredProcedure);

            ((IDataParameter)command.Parameters["@ExecSql"]).Value = execSql;
            ((IDataParameter)command.Parameters["@CurrentPage"]).Value = currentPage;
            ((IDataParameter)command.Parameters["@PageSize"]).Value = pageSize;

            using (DataSet tempDS = ExecuteDataSet(transaction, command))
            {
                recordCount = Convert.ToInt32(((IDataParameter)command.Parameters["@RecordCount"]).Value);
                pageCount = Convert.ToInt32(((IDataParameter)command.Parameters["@PageCount"]).Value);

                // Note: �洢����SplitPage���ɵļ�¼���ڱ�2����
                if (tempDS.Tables[1] != null)
                {
                    if (dataSet.Tables.Count > 0)
                    {
                        foreach (DataRow dataRow in tempDS.Tables[1].Rows)
                        {
                            dataSet.Tables[0].ImportRow(dataRow);
                        }
                    }
                    else
                    {
                        // Add By RQ 2008-12-08 11:03 ���ӿ� DataSet �Ŀ���
                        dataSet.Tables.Add(tempDS.Tables[1].Copy());
                        dataSet.Tables[0].TableName = tempDS.Tables[1].TableName;
                    }
                }
            }
        }


        #endregion


        #region ���ݴ洢����������ʵ�����ִ�����ݿ��������

        /// <summary>
        /// ���ݴ洢����������ʵ�����ִ�����ݿ��������
        /// </summary>
        /// <param name="commandText">���ݿ��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="model">ʵ�����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool ExecuteObjectTypedParams(string commandText, CommandType commandType, object model)
        {

            IDbCommand command = CreateDbCommand(driver.GetDbConnectionString(), commandText, commandType);

            // ��ʵ�������ȡ����ֵ�������ݿ�����������
            AssignParameterValues(command.Parameters, model);

            if (ExecuteNonQuery(driver.GetDbConnectionString(), command))
            {
                // �����ݿ�����������ȡ����ֵ����ʵ�����
                AssignModelValues(model, command.Parameters);
            }
            else
            {
                return false;
            }

            command.Parameters.Clear();

            return true;
        }




        /// <summary>
        /// ���ݴ洢����������ʵ�����ִ�����ݿ��������
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandText">���ݿ��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="model">ʵ�����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool ExecuteObjectTypedParams(string connectionString, string commandText, CommandType commandType, object model)
        {
            IDbCommand command = CreateDbCommand(connectionString, commandText, commandType);

            // ��ʵ�������ȡ����ֵ�������ݿ�����������
            AssignParameterValues(command.Parameters, model);

            if (ExecuteNonQuery(connectionString, command))
            {
                // �����ݿ�����������ȡ����ֵ����ʵ�����
                AssignModelValues(model, command.Parameters);
            }
            else
            {
                return false;
            }

            command.Parameters.Clear();

            return true;
        }


        /// <summary>
        /// ���ݴ洢����������ʵ�����ִ�����ݿ��������
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="commandText">���ݿ��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="model">ʵ�����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool ExecuteObjectTypedParams(IDbTransaction transaction, string commandText, CommandType commandType, object model)
        {
            IDbCommand command = CreateDbCommand(transaction.Connection.ConnectionString, commandText, commandType);

            // ��ʵ�������ȡ����ֵ�������ݿ�����������
            AssignParameterValues(command.Parameters, model);

            if (ExecuteNonQuery(transaction, command))
            {
                // �����ݿ�����������ȡ����ֵ����ʵ�����
                AssignModelValues(model, command.Parameters);
            }
            else
            {
                return false;
            }

            command.Parameters.Clear();

            return true;
        }

        #endregion

        #region ִ�����ݿ�����������ʵ����󼯺�

        /// <summary>
        /// ִ�����ݿ�����������ʵ����󼯺�
        /// </summary>		
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="type">����ģ�Ͷ�������</param>
        /// <returns>ʵ����󼯺�</returns>
        public IList ExecuteModel(string connectionString, string commandText, CommandType commandType, Type type)
        {
            return DataReaderToModel(ExecuteReader(connectionString, commandText, commandType), type);
        }


        /// <summary>
        /// ִ�����ݿ�����������ʵ����󼯺�
        /// </summary>		
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="command">IDbCommand ����</param>
        /// <param name="type">����ģ�Ͷ�������</param>
        /// <returns>ʵ����󼯺�</returns>
        public IList ExecuteModel(string connectionString, IDbCommand command, Type type)
        {
            return DataReaderToModel(ExecuteReader(connectionString, command), type);
        }


        /// <summary>
        /// ִ�����ݿ�����������ʵ����󼯺�
        /// </summary>		
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="type">����ģ�Ͷ�������</param>
        /// <returns>ʵ����󼯺�</returns>
        public IList ExecuteModel(IDbTransaction transaction, string commandText, CommandType commandType, Type type)
        {
            return DataReaderToModel(ExecuteReader(transaction, commandText, commandType), type);
        }


        /// <summary>
        /// ִ�����ݿ�����������ʵ����󼯺�
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="command">IDbCommand ����</param>
        /// <param name="type">����ģ�Ͷ�������</param>
        /// <returns>ʵ����󼯺�</returns>
        public IList ExecuteModel(IDbTransaction transaction, IDbCommand command, Type type)
        {
            return DataReaderToModel(ExecuteReader(transaction, command), type);
        }

        #endregion

        #region ��ҳ��ѯ������SQL Server �洢���� SplitPage ����ʵ�����

        /// <summary>
        /// ��ҳ��ѯ������SQL Server �洢���� SplitPage ����ʵ�����
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="execSql">T-SQL ���ݿ���������ַ���</param>
        /// <param name="currentPage">��ǰҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="pageCount">��ҳ��</param>
        /// <param name="dataSet">DataSet ����</param>
        public IList SplitPage(string connectionString, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, Type type)
        {
            IDbCommand command = CreateDbCommand(connectionString, "SplitPage", CommandType.StoredProcedure);

            ((IDataParameter)command.Parameters["@ExecSql"]).Value = execSql;
            ((IDataParameter)command.Parameters["@CurrentPage"]).Value = currentPage;
            ((IDataParameter)command.Parameters["@PageSize"]).Value = pageSize;

            IDataReader dataReader = ExecuteReader(connectionString, command);

            recordCount = Convert.ToInt32(((IDataParameter)command.Parameters["@RecordCount"]).Value);
            pageCount = Convert.ToInt32(((IDataParameter)command.Parameters["@PageCount"]).Value);

            // Note: �洢����SplitPage���ɵļ�¼���ڵڶ�����¼������
            dataReader.NextResult();

            return DataReaderToModel(dataReader, type);
        }


        /// <summary>
        /// ��ҳ��ѯ������SQL Server �洢���� SplitPage ����ʵ�����
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="execSql">T-SQL ���ݿ���������ַ���</param>
        /// <param name="currentPage">��ǰҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="pageCount">��ҳ��</param>
        /// <param name="dataSet">DataSet ����</param>
        public IList SplitPage(IDbTransaction transaction, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, Type type)
        {
            IDbCommand command = CreateDbCommand(transaction.Connection.ConnectionString, "SplitPage", CommandType.StoredProcedure);

            ((IDataParameter)command.Parameters["@ExecSql"]).Value = execSql;
            ((IDataParameter)command.Parameters["@CurrentPage"]).Value = currentPage;
            ((IDataParameter)command.Parameters["@PageSize"]).Value = pageSize;

            IDataReader dataReader = ExecuteReader(transaction, command);

            recordCount = Convert.ToInt32(((IDataParameter)command.Parameters["@RecordCount"]).Value);
            pageCount = Convert.ToInt32(((IDataParameter)command.Parameters["@PageCount"]).Value);

            // Note: �洢����SplitPage���ɵļ�¼���ڵڶ�����¼������
            dataReader.NextResult();

            return DataReaderToModel(dataReader, type);
        }

        #endregion


        #region ��� DataSet ����


        /// <summary>
        /// ��� DataSet ����
        /// </summary>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="dataSet">DataSet ����</param>
        //2010-12-20 ��ҫ�� ����ֱ��ȡdriver�е������ַ�������
        public void FillDataSet(string commandText, CommandType commandType, DataSet dataSet)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            FillDataSet(driver.GetDbConnectionString(), command, dataSet);
        }

        /// <summary>
        /// ��� DataSet ����
        /// </summary>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="dataSet">DataSet ����</param>
        /// <param name="model">ʵ�����</param>
        //2010-12-22 ��ҫ�� ���Ӱ�ģ�͸���������ʽ����ֶ�
        public void FillDataSet(string commandText, CommandType commandType, DataSet dataSet, object model)
        {
            IDbCommand command = CreateDbCommand(driver.GetDbConnectionString(), commandText, commandType);

            // ��ʵ�������ȡ����ֵ�������ݿ�����������
            AssignParameterValues(command.Parameters, model);

            FillDataSet(driver.GetDbConnectionString(), command, dataSet);
        }

        /// <summary>
        /// ��� DataSet ����
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="dataSet">DataSet ����</param>
        public void FillDataSet(string connectionString, string commandText, CommandType commandType, DataSet dataSet)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            FillDataSet(connectionString, command, dataSet);
        }


        /// <summary>
        /// ��� DataSet ����
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="command">IDbCommand ����</param>
        /// <param name="dataSet">DataSet ����</param>
        public void FillDataSet(string connectionString, IDbCommand command, DataSet dataSet)
        {
            #region ������ʾ

            if ((connectionString == null)
                || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }

            if ((command == null)
                || (dataSet == null))
            {
                throw new ArgumentNullException("dataSet");
            }

            #endregion

            IDbDataAdapter dataAdapter = driver.GetDbDataAdapter();
            dataAdapter.SelectCommand = command;

            string tableName = "Table";

            for (int index = 0; index < dataSet.Tables.Count; index++)
            {
                dataAdapter.TableMappings.Add(tableName, dataSet.Tables[index].TableName);
                tableName += (index + 1).ToString();
            }

            IDbConnection connection = driver.GetDbConnection(connectionString);

            try
            {
                command.Connection = connection;
                dataAdapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// ��� DataSet ����
        /// </summary>
        /// <param name="command">IDbCommand ����</param>
        /// <param name="dataSet">DataSet ����</param>
        /// <param name="tableNames">����</param>
        //2010-12-23���� ��ҫ��
        public void FillDataSet(IDbCommand command, DataSet dataSet, string[] tableNames)
        {
            FillDataSet(driver.GetDbConnectionString(), command, dataSet, tableNames);
        }

        /// <summary>
        /// ��� DataSet ����
        /// </summary>

        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="dataSet">DataSet ����</param>
        /// <param name="tableNames">����</param>
        //2010-12-23���� ��ҫ��
        public void FillDataSet(string commandText, CommandType commandType, DataSet dataSet, string[] tableNames)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            FillDataSet(driver.GetDbConnectionString(), command, dataSet, tableNames);

        }

        /// <summary>
        /// ��� DataSet ����
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="command">IDbCommand ����</param>
        /// <param name="dataSet">DataSet ����</param>
        /// <param name="tableNames">����</param>
        public void FillDataSet(string connectionString, IDbCommand command, DataSet dataSet, string[] tableNames)
        {
            #region ������ʾ

            if ((connectionString == null)
                || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }

            if ((command == null)
                || (dataSet == null))
            {
                throw new ArgumentNullException("dataSet");
            }

            #endregion

            IDbDataAdapter dataAdapter = driver.GetDbDataAdapter();
            dataAdapter.SelectCommand = command;

            string tableName = "Table";
            for (int index = 0; index < tableNames.Length; index++)
            {
                dataAdapter.TableMappings.Add(
                    tableName + (index == 0 ? "" : index.ToString()),
                    tableNames[index]);
            }

            IDbConnection connection = driver.GetDbConnection(connectionString);

            try
            {
                //2010-08-09 Ҷ���� �򾭳���Υ��Լ�����󣬶�Լ����ʵ��ʹ���м������ã������Ρ�
                //�ر�Լ��
                dataSet.EnforceConstraints = false;

                command.Connection = connection;
                dataAdapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }


        /// <summary>
        /// ��� DataSet ����
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="dataSet">DataSet ����</param>
        public void FillDataSet(IDbTransaction transaction, string commandText, CommandType commandType, DataSet dataSet)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            FillDataSet(transaction, command, dataSet);
        }


        /// <summary>
        /// ��� DataSet ����
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="command">IDbCommand ����</param>
        /// <param name="dataSet">DataSet ����</param>
        public void FillDataSet(IDbTransaction transaction, IDbCommand command, DataSet dataSet)
        {
            #region ������ʾ

            if (transaction == null)
            {
                throw new ArgumentNullException("IDbTransaction");
            }

            if ((transaction != null)
                && (transaction.Connection == null))
            {
                throw new ArgumentNullException("The transaction was rollbacked or commited, please provide an open transaction.", "IDbTransaction");
            }

            #endregion

            command.Connection = transaction.Connection;
            command.Transaction = transaction;

            IDbDataAdapter dataAdapter = driver.GetDbDataAdapter();
            dataAdapter.SelectCommand = command;

            string tableName = "Table";

            for (int index = 0; index < dataSet.Tables.Count; index++)
            {
                dataAdapter.TableMappings.Add(tableName, dataSet.Tables[index].TableName);
                tableName += (index + 1).ToString();
            }

            dataAdapter.Fill(dataSet);
        }


        #endregion

        #region ִ�����ݿ������������κβ���

        /// <summary>
        /// ִ�����ݿ������������κβ���
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool ExecuteNonQuery(string connectionString, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteNonQuery(connectionString, command);
        }


        /// <summary>
        /// ִ�����ݿ������������κβ���
        /// </summary>
        /// <param name="command">IDbCommand ����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool ExecuteNonQuery(IDbCommand command)
        {
            return ExecuteNonQuery(driver.GetDbConnectionString(), command);
        }


        /// <summary>
        /// ִ�����ݿ������������κβ���
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="command">IDbCommand ����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool ExecuteNonQuery(string connectionString, IDbCommand command)
        {
            #region ������ʾ

            if ((connectionString == null)
                || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }

            #endregion

            IDbConnection connection = driver.GetDbConnection(connectionString);

            try
            {
                return ExecuteNonQuery(connection, command);
            }
            finally
            {
                connection.Close();
            }
        }


        /// <summary>
        /// ִ�����ݿ������������κβ���
        /// </summary>
        /// <param name="connection">���ݿ����Ӷ���</param>
        /// <param name="command">IDbCommand ����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool ExecuteNonQuery(IDbConnection connection, IDbCommand command)
        {
            try
            {
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }


        /// <summary>
        /// ִ�����ݿ������������κβ���
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool ExecuteNonQuery(IDbTransaction transaction, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteNonQuery(transaction, command);
        }


        /// <summary>
        /// ִ�����ݿ������������κβ���
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="command">IDbCommand ����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool ExecuteNonQuery(IDbTransaction transaction, IDbCommand command)
        {
            #region ������ʾ

            if (transaction == null)
            {
                throw new ArgumentNullException("IDbTransaction");
            }

            if ((transaction != null)
                && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "IDbTransaction");
            }

            #endregion

            try
            {
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }


        #endregion

        #region ִ�����ݿ����������� DataSet ����

        /// <summary>
        /// ִ�����ݿ����������� DataSet ����
        /// </summary>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <returns>DataSet ����</returns>
        public DataSet ExecuteDataSet(string commandText, CommandType commandType)
        {
            return ExecuteDataSet(driver.GetDbConnectionString(), commandText, commandType);

        }

        /// <summary>
		/// ִ�����ݿ����������� DataSet ����
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <returns>DataSet ����</returns>
		public DataSet ExecuteDataSet(string connectionString, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteDataSet(connectionString, command);
        }


        /// <summary>
        /// ִ�����ݿ����������� DataSet ����
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="command">IDbCommand ����</param>
        /// <returns>DataSet ����</returns>
        public DataSet ExecuteDataSet(string connectionString, IDbCommand command)
        {
            #region ������ʾ

            if ((connectionString == null)
                || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }

            #endregion

            IDbDataAdapter dataAdapter = driver.GetDbDataAdapter();
            dataAdapter.SelectCommand = command;

            IDbConnection connection = driver.GetDbConnection(connectionString);

            try
            {
                command.Connection = connection;

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                return dataSet;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// ִ�����ݿ����������� DataSet ����
        /// </summary>
        /// <param name="command">IDbCommand ����</param>
        /// <returns>DataSet ����</returns>
        public DataSet ExecuteDataSet(IDbCommand command)
        {
            return ExecuteDataSet(this.driver.GetDbConnectionString(), command);
        }


        /// <summary>
        /// ִ�����ݿ����������� DataSet ����
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <returns>DataSet ����</returns>
        public DataSet ExecuteDataSet(IDbTransaction transaction, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteDataSet(transaction, command);
        }


        /// <summary>
        /// ִ�����ݿ����������� DataSet ����
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="command">IDbCommand ����</param>
        /// <returns>DataSet ����</returns>
        public DataSet ExecuteDataSet(IDbTransaction transaction, IDbCommand command)
        {
            #region ������ʾ

            if (transaction == null)
            {
                throw new ArgumentNullException("IDbTransaction");
            }

            if ((transaction != null)
                && (transaction.Connection == null))
            {
                throw new ArgumentNullException("The transaction was rollbacked or commited, please provide an open transaction.", "IDbTransaction");
            }

            #endregion

            command.Connection = transaction.Connection;
            command.Transaction = transaction;

            IDbDataAdapter dataAdapter = driver.GetDbDataAdapter();
            dataAdapter.SelectCommand = command;

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            return dataSet;
        }


        #endregion

        #region ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��


        /// <summary>
        /// ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��
        /// </summary>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <returns>������еĵ�һ�еĵ�һ��</returns>
        /// 
        //��ҫ������Ϊ��С��DBFrameʹ�÷�ʽһ��2010-12-20
        public object ExecuteScalar(string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteScalar(driver.GetDbConnectionString(), command);
        }



        /// <summary>
        /// ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <returns>������еĵ�һ�еĵ�һ��</returns>
        public object ExecuteScalar(string connectionString, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteScalar(connectionString, command);
        }

        /// <summary>
        /// ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��
        /// </summary>
        /// <param name="command">IDbCommand ����</param>
        /// <returns>������еĵ�һ�еĵ�һ��</returns>
        public object ExecuteScalar(IDbCommand command)
        {
            return ExecuteScalar(driver.GetDbConnectionString(), command);
        }

        /// <summary>
        /// ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="command">IDbCommand ����</param>
        /// <returns>������еĵ�һ�еĵ�һ��</returns>
        public object ExecuteScalar(string connectionString, IDbCommand command)
        {
            #region ������ʾ

            if ((connectionString == null)
                || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }

            #endregion

            object retval = null;

            IDbConnection connection = driver.GetDbConnection(connectionString);

            try
            {
                command.Connection = connection;
                retval = command.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            return retval;
        }


        /// <summary>
        /// ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <returns>������еĵ�һ�еĵ�һ��</returns>
        public object ExecuteScalar(IDbTransaction transaction, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteScalar(transaction, command);
        }


        /// <summary>
        /// ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="command">IDbCommand ����</param>
        /// <returns>������еĵ�һ�еĵ�һ��</returns>
        public object ExecuteScalar(IDbTransaction transaction, IDbCommand command)
        {
            #region ������ʾ

            if (transaction == null)
            {
                throw new ArgumentNullException("Transaction");
            }

            if ((transaction != null)
                && (transaction.Connection == null))
            {
                throw new ArgumentNullException("The transaction was rollbacked or commited, please provide an open transaction.", "Transaction");
            }

            #endregion

            command.Connection = transaction.Connection;
            command.Transaction = transaction;

            return command.ExecuteScalar();
        }


        #endregion

        #region ִ�����ݿ����������� IDataReader ����

        /// <summary>
        /// ִ�����ݿ����������� IDataReader ����
        /// </summary>		
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <returns>IDataReader ����</returns>
        public IDataReader ExecuteReader(string connectionString, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteReader(connectionString, command);
        }


        /// <summary>
        /// ִ�����ݿ����������� IDataReader ����
        /// </summary>		
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="command">Command����</param>
        /// <returns>IDataReader ����</returns>
        public IDataReader ExecuteReader(string connectionString, IDbCommand command)
        {
            return ExecuteReader(connectionString, command, CommandBehavior.CloseConnection);
        }


        /// <summary>
        /// ִ�����ݿ����������� IDataReader ����
        /// </summary>		
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="command">Command����</param>
        /// <param name="commandBehavior">�ṩ�Բ�ѯ����Ͳ�ѯ�����ݿ��Ӱ���˵��</param>
        /// <returns>IDataReader ����</returns>
        public IDataReader ExecuteReader(string connectionString, IDbCommand command, CommandBehavior commandBehavior)
        {
            #region ������ʾ

            if ((connectionString == null)
                || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }

            #endregion

            IDbConnection connection = driver.GetDbConnection(connectionString);

            try
            {
                command.Connection = connection;

                // Ԥ��ִ�� ExecuteNonQuery �����Ի�ȡ��������
                command.ExecuteNonQuery();

                return command.ExecuteReader(commandBehavior);
            }
            catch (Exception e)
            {
                connection.Close();
                throw e;
            }
        }


        /// <summary>
        /// ִ�����ݿ����������� IDataReader ����
        /// </summary>		
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
        /// <returns>IDataReader ����</returns>
        public IDataReader ExecuteReader(IDbTransaction transaction, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteReader(transaction, command);
        }


        /// <summary>
        /// ִ�����ݿ����������� IDataReader ����
        /// </summary>		
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="oleDbCommand">OleDbCommand����</param>
        /// <returns>IDataReader ����</returns>
        public IDataReader ExecuteReader(IDbTransaction transaction, IDbCommand command)
        {
            return ExecuteReader(transaction, command, CommandBehavior.Default);
        }


        /// <summary>
        /// ִ�����ݿ����������� IDataReader ����
        /// </summary>		
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="oleDbCommand">OleDbCommand����</param>
        /// <param name="commandBehavior">�ṩ�Բ�ѯ����Ͳ�ѯ�����ݿ��Ӱ���˵��</param>
        /// <returns>IDataReader ����</returns>
        public IDataReader ExecuteReader(IDbTransaction transaction, IDbCommand command, CommandBehavior commandBehavior)
        {
            #region ������ʾ

            if (transaction == null)
            {
                throw new ArgumentNullException("Transaction");
            }

            if ((transaction != null)
                && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "Transaction");
            }

            #endregion

            command.Connection = transaction.Connection;
            command.Transaction = transaction;

            // Ԥ��ִ�� ExecuteNonQuery �����Ի�ȡ��������
            command.ExecuteNonQuery();

            return command.ExecuteReader(commandBehavior);
        }

        #endregion


        #region ��ȡ���ݿ�����

        /// <summary>
        /// ��ȡ���ݿ�����
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="transaction">���ݿ�����</param>
        public IDbTransaction BeginDbTransaction(string connectionString)
        {
            IDbConnection connection = this.driver.GetDbConnection(connectionString);

            return connection.BeginTransaction(IsolationLevel.RepeatableRead);
        }

        #endregion

        #region �ύ���ݿ�����

        /// <summary>
        /// �ύ���ݿ�����
        /// </summary>		
        /// <param name="transaction">���ݿ�����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool CommitDbTransaction(IDbTransaction transaction)
        {
            #region ������ʾ

            if (transaction == null)
            {
                return false;
            }

            if ((transaction != null)
                && (transaction.Connection == null))
            {
                return false;
            }

            #endregion

            try
            {
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                if (transaction.Connection != null && transaction.Connection.State != ConnectionState.Closed)
                {
                    transaction.Connection.Close();
                }
            }
        }

        #endregion

        #region �ع����ݿ�����

        /// <summary>
        /// �ع����ݿ�����
        /// </summary>		
        public void RollBackDbTransaction(IDbTransaction transaction)
        {
            #region ������ʾ

            if (transaction == null)
            {
                return;
            }

            if ((transaction != null)
                && (transaction.Connection == null))
            {
                return;
            }

            #endregion

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
                if (transaction.Connection != null && transaction.Connection.State != ConnectionState.Closed)
                {
                    transaction.Connection.Close();
                }
            }
        }

        #endregion
    }
}
