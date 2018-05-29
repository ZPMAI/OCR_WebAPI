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

        /// 构造函数
        public AbstractDbRunner()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public AbstractDbRunner(string connectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 数据库类型
        /// </summary>
        protected virtual string DriverType
        {
            get
            {
                return "SQLSERVER";
            }
        }


        #region 格式化存储过程参数名称

        /// <summary>
        /// 格式化存储过程参数名称
        /// </summary>
        /// <param name="spParameterName">存储过程参数名称</param>
        /// <returns>参数名称</returns>
        private string FormatSpParameterName(string spParameterName)
        {
            string parameterName = spParameterName;

            if (DriverType == "SQLSERVER")
            {
                // 去除@字符
                if (parameterName.Substring(0, 1).Equals("@"))
                {
                    parameterName = spParameterName.Substring(1);
                }
            }
            else if (DriverType == "ORACLE")
            {
                // 去除p_字符
                if (parameterName.Substring(0, 2).ToUpper().Equals("P_"))
                {
                    parameterName = spParameterName.Substring(2);
                }
            }

            return parameterName;
        }

        #endregion

        #region 克隆数据库操作命令参数数组

        /// <summary>
        /// 克隆数据库操作命令参数数组
        /// </summary>
        /// <param name="parameters">原数据库操作命令参数数组</param>
        /// <returns>数据库操作命令参数数组</returns>
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

        #region 从存储过程中检索参数信息

        /// <summary>
        /// 从存储过程中检索参数信息
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">数据库操作命令</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <returns>数据库操作命令参数数组</returns>
        private IDataParameter[] DiscoverSpParameterSet(string connectionString, string commandText, System.Data.CommandType commandType)
        {
            #region 例外提示

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

            // 读取XML文件对IDataParameter[]对象进行反串行化
            // IDataParameter[] dataParameters = CacheHelper.CacheDeserialize(filePath, typeof(IDataParameter[]), true) as IDataParameter[];

            // if ( dataParameters == null )
            // {

            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            // Note: 此处的数据库连接对象不能引用Transaction.Connection	
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

            // // 在XML中对IDataParameter[]对象进行串行化
            // CacheHelper.CacheSerialize(filePath, dataParameters, true);
            // }
        }

        #endregion


        #region 从 DataRow 对象中取参数值赋给数据库操作命令参数

        /// <summary>
        /// 从 DataRow 对象中取参数值赋给数据库操作命令参数
        /// </summary>
        /// <param name="dataParameters">DataParameter对象集合</param>
        /// <param name="dataRow">参数值(DataRow)</param>
        private void AssignParameterValues(IDataParameterCollection dataParameters, DataRow dataRow)
        {
            #region 例外提示

            if (dataRow == null)
            {
                return;
            }

            #endregion

            // Reset SqlParameterCollection(设置参数默认值)
            // SetDefaultValue(dataParameters);

            foreach (IDataParameter dataParameter in dataParameters)
            {
                #region 例外提示

                if ((dataParameter.ParameterName == null)
                    || (dataParameter.ParameterName.Length <= 1))
                {
                    throw new ArgumentNullException("数据库操作命令参数名为空");
                }

                #endregion

                string parameterName = this.FormatSpParameterName(dataParameter.ParameterName);

                if (dataRow.Table.Columns.IndexOf(parameterName) != -1)
                {
                    // 如果参数值为空, 则使用默认值
                    // if ( dataRow[parameterName] != DBNull.Value )
                    // {
                    dataParameter.Value = dataRow[parameterName];
                    // }
                }
            }
        }


        #endregion

        #region 从数据库操作命令参数取返回值赋给 DataRow 对象

        /// <summary>
        /// 从数据库操作命令参数取返回值赋给 DataRow 对象
        /// </summary>
        /// <param name="dataRow">DataRow 对象</param>
        /// <param name="dataParameters">IDataParameter 对象集合</param>
        private void AssignDataRowValues(DataRow dataRow, IDataParameterCollection dataParameters)
        {
            #region 例外提示

            if (dataRow == null)
            {
                return;
            }

            #endregion

            foreach (IDataParameter dataParameter in dataParameters)
            {
                #region 例外提示

                if ((dataParameter.ParameterName == null)
                    || (dataParameter.ParameterName.Length <= 1))
                {
                    throw new ArgumentNullException("数据库操作命令参数为空");
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


        #region 从实体对象中取参数值赋给数据库操作命令参数

        /// <summary>
        /// 从实体对象中取参数值赋给数据库操作命令参数
        /// </summary>
        /// <param name="dataParameters">SqlParameter对象集合</param>
        /// <param name="model">实体对象</param>
        private void AssignParameterValues(IDataParameterCollection dataParameters, object model)
        {
            // Reset SqlParameterCollection(设置参数默认值)
            // SetDefaultValue(dataParameters);

            Type type = model.GetType();

            foreach (IDataParameter dataParameter in dataParameters)
            {
                #region 例外提示

                if ((dataParameter.ParameterName == null)
                    || (dataParameter.ParameterName.Length <= 1))
                {
                    throw new Exception("数据库操作命令参数为空");
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

        #region 从数据库操作命令参数取返回值赋给实体对象

        /// <summary>
        /// 从数据库操作命令参数取返回值赋给实体对象
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="dataParameters">IDataParameter对象集合</param>
        private void AssignModelValues(object model, IDataParameterCollection dataParameters)
        {
            Type type = model.GetType();

            foreach (IDataParameter dataParameter in dataParameters)
            {
                #region 例外提示

                if ((dataParameter.ParameterName == null)
                    || (dataParameter.ParameterName.Length <= 1))
                {
                    throw new Exception("数据库操作命令参数为空");
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

        #region 把 IDataReader 对象转换为实体对象集合

        /// <summary>
        /// 把 IDataReader 对象转换为实体对象集合
        /// </summary>
        /// <param name="dataReader">IDataReader 对象</param>
        /// <param name="type">实体对象类型</param>
        /// <returns>实体对象集合</returns>
        private IList DataReaderToModel(IDataReader dataReader, Type type)
        {
            PropertyInfo[] propertyInfos = type.GetProperties();

            // 为了节省时间, 可以调用一次 GetOrdinal, 然后将结果分配给整数变量以便在循环中使用。
            int[] indexList = new int[propertyInfos.Length];

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                try
                {
                    indexList[i] = dataReader.GetOrdinal(propertyInfos[i].Name);
                }
                catch (IndexOutOfRangeException)
                {
                    // 若指定的名称不是有效的列名称，即类属性名称与数据库字段名称不匹配
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


        #region 根据存储过程名称生成带参数的 IDbCommand 对象

        /// <summary>
        /// 根据存储过程名称生成带参数的 IDbCommand 对象
        /// </summary>
        /// <param name="commandText">数据库操作命令</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <returns>IDbCommand 对象</returns>
        /// 2010-12-22甄耀红增加为了保证和小吴使用方法统一
        public IDbCommand CreateDbCommand(string commandText, CommandType commandType)
        {
            return CreateDbCommand(driver.GetDbConnectionString(), commandText, commandType);

        }

        /// <summary>
        /// 根据存储过程名称生成带参数的 IDbCommand 对象
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">数据库操作命令</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <returns>IDbCommand 对象</returns>
        public IDbCommand CreateDbCommand(string connectionString, string commandText, CommandType commandType)
        {
            #region 例外提示

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


        #region 根据存储过程名称与 DataTable 对象执行数据库操作命令

        /// <summary>
        /// 根据存储过程名称与 DataTable 对象执行数据库操作命令
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">数据库操作命令</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="dataTable">DataTable 对象</param>
        /// <returns>操作是否成功</returns>
        public bool ExecuteDataTableTypedParams(string connectionString, string commandText, CommandType commandType, DataTable dataTable)
        {
            IDbCommand command = this.CreateDbCommand(connectionString, commandText, commandType);
            IDbConnection connection = driver.GetDbConnection(connectionString);

            try
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    // 从DataRow对象中取参数值赋给数据库操作命令参数
                    AssignParameterValues(command.Parameters, dataRow);

                    if (ExecuteNonQuery(connection, command))
                    {
                        // 从数据库操作命令参数取返回值赋给DataRow对象
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
        /// 根据存储过程名称与 DataTable 对象执行数据库操作命令
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandText">数据库操作命令</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="dataTable">DataTable 对象</param>
        /// <returns>操作是否成功</returns>
        public bool ExecuteDataTableTypedParams(IDbTransaction transaction, string commandText, CommandType commandType, DataTable dataTable)
        {
            string connectionString = transaction.Connection.ConnectionString;
            IDbCommand command = this.CreateDbCommand(connectionString, commandText, commandType);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                // 从DataRow对象中取参数值赋给数据库操作命令参数
                AssignParameterValues(command.Parameters, dataRow);

                if (ExecuteNonQuery(transaction, command))
                {
                    // 从数据库操作命令参数取返回值赋给DataRow对象
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

        #region 分页查询操作（SQL Server 存储过程 SplitPage 返回 DataSet）

        /// <summary>
        /// 分页查询操作（SQL Server 存储过程 SplitPage 返回 DataSet）
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="execSql">T-SQL 数据库操作命令字符串</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="dataSet">DataSet 对象</param>
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

                // Note: 存储过程SplitPage生成的记录放在表2里面
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
                        // Add By RQ 2008-12-08 11:03 增加空 DataSet 的拷贝
                        dataSet.Tables.Add(tempDS.Tables[1].Copy());
                        dataSet.Tables[0].TableName = tempDS.Tables[1].TableName;
                    }
                }
            }
        }


        /// <summary>
        /// 分页查询操作（SQL Server 存储过程 SplitPage 返回 DataSet）
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="execSql">T-SQL 数据库操作命令字符串</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="dataSet">DataSet 对象</param>
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

                // Note: 存储过程SplitPage生成的记录放在表2里面
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
                        // Add By RQ 2008-12-08 11:03 增加空 DataSet 的拷贝
                        dataSet.Tables.Add(tempDS.Tables[1].Copy());
                        dataSet.Tables[0].TableName = tempDS.Tables[1].TableName;
                    }
                }
            }
        }


        #endregion


        #region 根据存储过程名称与实体对象执行数据库操作命令

        /// <summary>
        /// 根据存储过程名称与实体对象执行数据库操作命令
        /// </summary>
        /// <param name="commandText">数据库操作命令</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="model">实体对象</param>
        /// <returns>操作是否成功</returns>
        public bool ExecuteObjectTypedParams(string commandText, CommandType commandType, object model)
        {

            IDbCommand command = CreateDbCommand(driver.GetDbConnectionString(), commandText, commandType);

            // 从实体对象中取参数值赋给数据库操作命令参数
            AssignParameterValues(command.Parameters, model);

            if (ExecuteNonQuery(driver.GetDbConnectionString(), command))
            {
                // 从数据库操作命令参数取返回值赋给实体对象
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
        /// 根据存储过程名称与实体对象执行数据库操作命令
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">数据库操作命令</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="model">实体对象</param>
        /// <returns>操作是否成功</returns>
        public bool ExecuteObjectTypedParams(string connectionString, string commandText, CommandType commandType, object model)
        {
            IDbCommand command = CreateDbCommand(connectionString, commandText, commandType);

            // 从实体对象中取参数值赋给数据库操作命令参数
            AssignParameterValues(command.Parameters, model);

            if (ExecuteNonQuery(connectionString, command))
            {
                // 从数据库操作命令参数取返回值赋给实体对象
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
        /// 根据存储过程名称与实体对象执行数据库操作命令
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandText">数据库操作命令</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="model">实体对象</param>
        /// <returns>操作是否成功</returns>
        public bool ExecuteObjectTypedParams(IDbTransaction transaction, string commandText, CommandType commandType, object model)
        {
            IDbCommand command = CreateDbCommand(transaction.Connection.ConnectionString, commandText, commandType);

            // 从实体对象中取参数值赋给数据库操作命令参数
            AssignParameterValues(command.Parameters, model);

            if (ExecuteNonQuery(transaction, command))
            {
                // 从数据库操作命令参数取返回值赋给实体对象
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

        #region 执行数据库操作命令并返回实体对象集合

        /// <summary>
        /// 执行数据库操作命令并返回实体对象集合
        /// </summary>		
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="type">数据模型对象类型</param>
        /// <returns>实体对象集合</returns>
        public IList ExecuteModel(string connectionString, string commandText, CommandType commandType, Type type)
        {
            return DataReaderToModel(ExecuteReader(connectionString, commandText, commandType), type);
        }


        /// <summary>
        /// 执行数据库操作命令并返回实体对象集合
        /// </summary>		
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="command">IDbCommand 对象</param>
        /// <param name="type">数据模型对象类型</param>
        /// <returns>实体对象集合</returns>
        public IList ExecuteModel(string connectionString, IDbCommand command, Type type)
        {
            return DataReaderToModel(ExecuteReader(connectionString, command), type);
        }


        /// <summary>
        /// 执行数据库操作命令并返回实体对象集合
        /// </summary>		
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="type">数据模型对象类型</param>
        /// <returns>实体对象集合</returns>
        public IList ExecuteModel(IDbTransaction transaction, string commandText, CommandType commandType, Type type)
        {
            return DataReaderToModel(ExecuteReader(transaction, commandText, commandType), type);
        }


        /// <summary>
        /// 执行数据库操作命令并返回实体对象集合
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="command">IDbCommand 对象</param>
        /// <param name="type">数据模型对象类型</param>
        /// <returns>实体对象集合</returns>
        public IList ExecuteModel(IDbTransaction transaction, IDbCommand command, Type type)
        {
            return DataReaderToModel(ExecuteReader(transaction, command), type);
        }

        #endregion

        #region 分页查询操作（SQL Server 存储过程 SplitPage 返回实体对象）

        /// <summary>
        /// 分页查询操作（SQL Server 存储过程 SplitPage 返回实体对象）
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="execSql">T-SQL 数据库操作命令字符串</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="dataSet">DataSet 对象</param>
        public IList SplitPage(string connectionString, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, Type type)
        {
            IDbCommand command = CreateDbCommand(connectionString, "SplitPage", CommandType.StoredProcedure);

            ((IDataParameter)command.Parameters["@ExecSql"]).Value = execSql;
            ((IDataParameter)command.Parameters["@CurrentPage"]).Value = currentPage;
            ((IDataParameter)command.Parameters["@PageSize"]).Value = pageSize;

            IDataReader dataReader = ExecuteReader(connectionString, command);

            recordCount = Convert.ToInt32(((IDataParameter)command.Parameters["@RecordCount"]).Value);
            pageCount = Convert.ToInt32(((IDataParameter)command.Parameters["@PageCount"]).Value);

            // Note: 存储过程SplitPage生成的记录放在第二个记录集里面
            dataReader.NextResult();

            return DataReaderToModel(dataReader, type);
        }


        /// <summary>
        /// 分页查询操作（SQL Server 存储过程 SplitPage 返回实体对象）
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="execSql">T-SQL 数据库操作命令字符串</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="dataSet">DataSet 对象</param>
        public IList SplitPage(IDbTransaction transaction, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, Type type)
        {
            IDbCommand command = CreateDbCommand(transaction.Connection.ConnectionString, "SplitPage", CommandType.StoredProcedure);

            ((IDataParameter)command.Parameters["@ExecSql"]).Value = execSql;
            ((IDataParameter)command.Parameters["@CurrentPage"]).Value = currentPage;
            ((IDataParameter)command.Parameters["@PageSize"]).Value = pageSize;

            IDataReader dataReader = ExecuteReader(transaction, command);

            recordCount = Convert.ToInt32(((IDataParameter)command.Parameters["@RecordCount"]).Value);
            pageCount = Convert.ToInt32(((IDataParameter)command.Parameters["@PageCount"]).Value);

            // Note: 存储过程SplitPage生成的记录放在第二个记录集里面
            dataReader.NextResult();

            return DataReaderToModel(dataReader, type);
        }

        #endregion


        #region 填充 DataSet 对象


        /// <summary>
        /// 填充 DataSet 对象
        /// </summary>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="dataSet">DataSet 对象</param>
        //2010-12-20 甄耀红 增加直接取driver中的连接字符串数据
        public void FillDataSet(string commandText, CommandType commandType, DataSet dataSet)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            FillDataSet(driver.GetDbConnectionString(), command, dataSet);
        }

        /// <summary>
        /// 填充 DataSet 对象
        /// </summary>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="dataSet">DataSet 对象</param>
        /// <param name="model">实体对象</param>
        //2010-12-22 甄耀红 增加按模型给定参数方式填充字段
        public void FillDataSet(string commandText, CommandType commandType, DataSet dataSet, object model)
        {
            IDbCommand command = CreateDbCommand(driver.GetDbConnectionString(), commandText, commandType);

            // 从实体对象中取参数值赋给数据库操作命令参数
            AssignParameterValues(command.Parameters, model);

            FillDataSet(driver.GetDbConnectionString(), command, dataSet);
        }

        /// <summary>
        /// 填充 DataSet 对象
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="dataSet">DataSet 对象</param>
        public void FillDataSet(string connectionString, string commandText, CommandType commandType, DataSet dataSet)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            FillDataSet(connectionString, command, dataSet);
        }


        /// <summary>
        /// 填充 DataSet 对象
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="command">IDbCommand 对象</param>
        /// <param name="dataSet">DataSet 对象</param>
        public void FillDataSet(string connectionString, IDbCommand command, DataSet dataSet)
        {
            #region 例外提示

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
        /// 填充 DataSet 对象
        /// </summary>
        /// <param name="command">IDbCommand 对象</param>
        /// <param name="dataSet">DataSet 对象</param>
        /// <param name="tableNames">表名</param>
        //2010-12-23补充 甄耀红
        public void FillDataSet(IDbCommand command, DataSet dataSet, string[] tableNames)
        {
            FillDataSet(driver.GetDbConnectionString(), command, dataSet, tableNames);
        }

        /// <summary>
        /// 填充 DataSet 对象
        /// </summary>

        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="dataSet">DataSet 对象</param>
        /// <param name="tableNames">表名</param>
        //2010-12-23补充 甄耀红
        public void FillDataSet(string commandText, CommandType commandType, DataSet dataSet, string[] tableNames)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            FillDataSet(driver.GetDbConnectionString(), command, dataSet, tableNames);

        }

        /// <summary>
        /// 填充 DataSet 对象
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="command">IDbCommand 对象</param>
        /// <param name="dataSet">DataSet 对象</param>
        /// <param name="tableNames">表名</param>
        public void FillDataSet(string connectionString, IDbCommand command, DataSet dataSet, string[] tableNames)
        {
            #region 例外提示

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
                //2010-08-09 叶君腾 因经常报违反约束错误，而约束在实际使用中几乎不用，故屏蔽。
                //关闭约束
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
        /// 填充 DataSet 对象
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="dataSet">DataSet 对象</param>
        public void FillDataSet(IDbTransaction transaction, string commandText, CommandType commandType, DataSet dataSet)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            FillDataSet(transaction, command, dataSet);
        }


        /// <summary>
        /// 填充 DataSet 对象
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="command">IDbCommand 对象</param>
        /// <param name="dataSet">DataSet 对象</param>
        public void FillDataSet(IDbTransaction transaction, IDbCommand command, DataSet dataSet)
        {
            #region 例外提示

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

        #region 执行数据库操作命令不返回任何参数

        /// <summary>
        /// 执行数据库操作命令不返回任何参数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <returns>操作是否成功</returns>
        public bool ExecuteNonQuery(string connectionString, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteNonQuery(connectionString, command);
        }


        /// <summary>
        /// 执行数据库操作命令不返回任何参数
        /// </summary>
        /// <param name="command">IDbCommand 对象</param>
        /// <returns>操作是否成功</returns>
        public bool ExecuteNonQuery(IDbCommand command)
        {
            return ExecuteNonQuery(driver.GetDbConnectionString(), command);
        }


        /// <summary>
        /// 执行数据库操作命令不返回任何参数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="command">IDbCommand 对象</param>
        /// <returns>操作是否成功</returns>
        public bool ExecuteNonQuery(string connectionString, IDbCommand command)
        {
            #region 例外提示

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
        /// 执行数据库操作命令不返回任何参数
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="command">IDbCommand 对象</param>
        /// <returns>操作是否成功</returns>
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
        /// 执行数据库操作命令不返回任何参数
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <returns>操作是否成功</returns>
        public bool ExecuteNonQuery(IDbTransaction transaction, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteNonQuery(transaction, command);
        }


        /// <summary>
        /// 执行数据库操作命令不返回任何参数
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="command">IDbCommand 对象</param>
        /// <returns>操作是否成功</returns>
        public bool ExecuteNonQuery(IDbTransaction transaction, IDbCommand command)
        {
            #region 例外提示

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

        #region 执行数据库操作命令并返回 DataSet 对象

        /// <summary>
        /// 执行数据库操作命令并返回 DataSet 对象
        /// </summary>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <returns>DataSet 对象</returns>
        public DataSet ExecuteDataSet(string commandText, CommandType commandType)
        {
            return ExecuteDataSet(driver.GetDbConnectionString(), commandText, commandType);

        }

        /// <summary>
		/// 执行数据库操作命令并返回 DataSet 对象
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <returns>DataSet 对象</returns>
		public DataSet ExecuteDataSet(string connectionString, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteDataSet(connectionString, command);
        }


        /// <summary>
        /// 执行数据库操作命令并返回 DataSet 对象
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="command">IDbCommand 对象</param>
        /// <returns>DataSet 对象</returns>
        public DataSet ExecuteDataSet(string connectionString, IDbCommand command)
        {
            #region 例外提示

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
        /// 执行数据库操作命令并返回 DataSet 对象
        /// </summary>
        /// <param name="command">IDbCommand 对象</param>
        /// <returns>DataSet 对象</returns>
        public DataSet ExecuteDataSet(IDbCommand command)
        {
            return ExecuteDataSet(this.driver.GetDbConnectionString(), command);
        }


        /// <summary>
        /// 执行数据库操作命令并返回 DataSet 对象
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <returns>DataSet 对象</returns>
        public DataSet ExecuteDataSet(IDbTransaction transaction, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteDataSet(transaction, command);
        }


        /// <summary>
        /// 执行数据库操作命令并返回 DataSet 对象
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="command">IDbCommand 对象</param>
        /// <returns>DataSet 对象</returns>
        public DataSet ExecuteDataSet(IDbTransaction transaction, IDbCommand command)
        {
            #region 例外提示

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

        #region 执行数据库操作命令并返回结果集中的第一行的第一列


        /// <summary>
        /// 执行数据库操作命令并返回结果集中的第一行的第一列
        /// </summary>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <returns>结果集中的第一行的第一列</returns>
        /// 
        //甄耀红增加为和小吴DBFrame使用方式一样2010-12-20
        public object ExecuteScalar(string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteScalar(driver.GetDbConnectionString(), command);
        }



        /// <summary>
        /// 执行数据库操作命令并返回结果集中的第一行的第一列
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <returns>结果集中的第一行的第一列</returns>
        public object ExecuteScalar(string connectionString, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteScalar(connectionString, command);
        }

        /// <summary>
        /// 执行数据库操作命令并返回结果集中的第一行的第一列
        /// </summary>
        /// <param name="command">IDbCommand 对象</param>
        /// <returns>结果集中的第一行的第一列</returns>
        public object ExecuteScalar(IDbCommand command)
        {
            return ExecuteScalar(driver.GetDbConnectionString(), command);
        }

        /// <summary>
        /// 执行数据库操作命令并返回结果集中的第一行的第一列
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="command">IDbCommand 对象</param>
        /// <returns>结果集中的第一行的第一列</returns>
        public object ExecuteScalar(string connectionString, IDbCommand command)
        {
            #region 例外提示

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
        /// 执行数据库操作命令并返回结果集中的第一行的第一列
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <returns>结果集中的第一行的第一列</returns>
        public object ExecuteScalar(IDbTransaction transaction, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteScalar(transaction, command);
        }


        /// <summary>
        /// 执行数据库操作命令并返回结果集中的第一行的第一列
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="command">IDbCommand 对象</param>
        /// <returns>结果集中的第一行的第一列</returns>
        public object ExecuteScalar(IDbTransaction transaction, IDbCommand command)
        {
            #region 例外提示

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

        #region 执行数据库操作命令并返回 IDataReader 对象

        /// <summary>
        /// 执行数据库操作命令并返回 IDataReader 对象
        /// </summary>		
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <returns>IDataReader 对象</returns>
        public IDataReader ExecuteReader(string connectionString, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteReader(connectionString, command);
        }


        /// <summary>
        /// 执行数据库操作命令并返回 IDataReader 对象
        /// </summary>		
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="command">Command对象</param>
        /// <returns>IDataReader 对象</returns>
        public IDataReader ExecuteReader(string connectionString, IDbCommand command)
        {
            return ExecuteReader(connectionString, command, CommandBehavior.CloseConnection);
        }


        /// <summary>
        /// 执行数据库操作命令并返回 IDataReader 对象
        /// </summary>		
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="command">Command对象</param>
        /// <param name="commandBehavior">提供对查询结果和查询对数据库的影响的说明</param>
        /// <returns>IDataReader 对象</returns>
        public IDataReader ExecuteReader(string connectionString, IDbCommand command, CommandBehavior commandBehavior)
        {
            #region 例外提示

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

                // 预先执行 ExecuteNonQuery 方法以获取传出参数
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
        /// 执行数据库操作命令并返回 IDataReader 对象
        /// </summary>		
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
        /// <returns>IDataReader 对象</returns>
        public IDataReader ExecuteReader(IDbTransaction transaction, string commandText, CommandType commandType)
        {
            IDbCommand command = driver.GetDbCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return ExecuteReader(transaction, command);
        }


        /// <summary>
        /// 执行数据库操作命令并返回 IDataReader 对象
        /// </summary>		
        /// <param name="transaction">数据库事务</param>
        /// <param name="oleDbCommand">OleDbCommand对象</param>
        /// <returns>IDataReader 对象</returns>
        public IDataReader ExecuteReader(IDbTransaction transaction, IDbCommand command)
        {
            return ExecuteReader(transaction, command, CommandBehavior.Default);
        }


        /// <summary>
        /// 执行数据库操作命令并返回 IDataReader 对象
        /// </summary>		
        /// <param name="transaction">数据库事务</param>
        /// <param name="oleDbCommand">OleDbCommand对象</param>
        /// <param name="commandBehavior">提供对查询结果和查询对数据库的影响的说明</param>
        /// <returns>IDataReader 对象</returns>
        public IDataReader ExecuteReader(IDbTransaction transaction, IDbCommand command, CommandBehavior commandBehavior)
        {
            #region 例外提示

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

            // 预先执行 ExecuteNonQuery 方法以获取传出参数
            command.ExecuteNonQuery();

            return command.ExecuteReader(commandBehavior);
        }

        #endregion


        #region 获取数据库事务

        /// <summary>
        /// 获取数据库事务
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="transaction">数据库事务</param>
        public IDbTransaction BeginDbTransaction(string connectionString)
        {
            IDbConnection connection = this.driver.GetDbConnection(connectionString);

            return connection.BeginTransaction(IsolationLevel.RepeatableRead);
        }

        #endregion

        #region 提交数据库事务

        /// <summary>
        /// 提交数据库事务
        /// </summary>		
        /// <param name="transaction">数据库事务</param>
        /// <returns>操作是否成功</returns>
        public bool CommitDbTransaction(IDbTransaction transaction)
        {
            #region 例外提示

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

        #region 回滚数据库事务

        /// <summary>
        /// 回滚数据库事务
        /// </summary>		
        public void RollBackDbTransaction(IDbTransaction transaction)
        {
            #region 例外提示

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
