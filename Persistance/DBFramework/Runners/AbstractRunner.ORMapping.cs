using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using DBFramework.Drivers;
using DBFramework.Mapping;
using DBFramework.MSIL;

namespace DBFramework.Runners
{
    partial class AbstractRunner
    {
        #region 判读记录是否已经存在

        /// <summary>
        /// 判读记录是否已经存在
        /// </summary>
        /// <returns>是/否</returns>
        /// <example>
        /// SELECT COUNT(*) FROM Table WHERE ID=@ID
        /// SELECT COUNT(*) FROM Table WHERE ID=:ID
        /// </example>
        public virtual bool IsExist<T>(T entity)
        {
            MappingInfo<T> mappingInfo = new MappingInfo<T>();
            IDbCommand command = this.driver.CreateDbCommand();

            StringBuilder sql = new StringBuilder();
            string fullTableName = this.driver.ToFullTableName(mappingInfo.DbName, mappingInfo.TableName);
            sql.AppendFormat("SELECT COUNT(*) FROM {0} WHERE 1=1", fullTableName);

            foreach (string propertyName in mappingInfo.PrimaryKeyColumns.Keys)
            {
                ColumnAttribute columnAttribute = mappingInfo.Columns[propertyName];

                IDataParameter parameter = this.driver.CreateDbParameter(propertyName);
                command.Parameters.Add(parameter);

                string sqlParameterName = this.driver.ToSQLParameterName(propertyName);
                sql.AppendFormat(" AND {0}={1}", columnAttribute.Name, sqlParameterName);
            }

            command.CommandText = sql.ToString();
            command.CommandType = CommandType.Text;

            SqlUtil.AssignParameters(command.Parameters, entity);
            object returnValue = this.ExecuteScalar(command);
            return Convert.ToInt32(returnValue) > 0;
        }

        #endregion

        #region 保存记录

        /// <summary>
        /// 保存记录
        /// </summary>
        public virtual void Save<T>(T entity)
        {
            if (IsExist<T>(entity))
            {
                if (MessageBox.Show("该模板下的规则已经存在，是否覆盖？", string.Empty, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Update<T>(entity);
                }
                else
                {
                    Insert<T>(entity);
                }
            }
            else
            {
                Insert<T>(entity);
 
            }
           
        }

        #endregion

        #region 添加记录

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <example>
        /// INSERT INTO Configure(UserName,Password) VALUES(@UserName,@Password)
        /// INSERT INTO Configure(UserName,Password) VALUES(:UserName,:Password)
        /// 
        /// MSSQL: SELECT @ID = IDENT_CURRENT('Table')
        /// ORACLE: SELECT Sequence.Nextval FROM Sequence
        /// </example>
        public virtual void Insert<T>(T entity)
        {
            MappingInfo<T> mappingInfo = new MappingInfo<T>();
            IDbCommand command = this.driver.CreateDbCommand();

            StringBuilder sql = new StringBuilder();
            string fullTableName = this.driver.ToFullTableName(mappingInfo.DbName, mappingInfo.TableName);
            sql.AppendFormat("INSERT INTO {0} ", fullTableName);

            StringBuilder columnsExpression = new StringBuilder();
            StringBuilder valuesExpression = new StringBuilder();
            bool isFirst = true;

            foreach (string propertyName in mappingInfo.Columns.Keys)
            {
                ColumnAttribute columnAttribute = mappingInfo.Columns[propertyName];

                // 不处理自增字段
                if (columnAttribute == null || columnAttribute.IsIncrement) { continue; }

                IDataParameter parameter = this.driver.CreateDbParameter(propertyName);
                command.Parameters.Add(parameter);

                columnsExpression.Append(isFirst ? string.Empty : ",")
                    .Append(columnAttribute.Name);

                string sqlParameterName = this.driver.ToSQLParameterName(propertyName);
                valuesExpression.Append(isFirst ? string.Empty : ",")
                    .Append(sqlParameterName);

                if (isFirst) { isFirst = false; }
            }

            sql.AppendFormat("({0})", columnsExpression.ToString())
            .AppendFormat(" VALUES({0})", valuesExpression.ToString());

            command.CommandText = sql.ToString();
            command.CommandType = CommandType.Text;

            SqlUtil.AssignParameters(command.Parameters, entity);
            this.ExecuteNonQuery(command);
        }

        #endregion

        #region 修改记录

        /// <summary>
        /// 修改记录
        /// </summary>
        /// <example>
        /// UPDATE Menu SET MenuItem=@MenuItem,ParentID=@ParentID WHERE ID=@ID
        /// UPDATE Menu SET MenuItem=:MenuItem,ParentID=:ParentID WHERE ID=:ID
        /// </example>
        public void Update<T>(T entity)
        {
            MappingInfo<T> mappingInfo = new MappingInfo<T>();
            IDbCommand command = this.driver.CreateDbCommand();

            StringBuilder sql = new StringBuilder();
            string fullTableName = this.driver.ToFullTableName(mappingInfo.DbName, mappingInfo.TableName);
            sql.AppendFormat("UPDATE {0} ", fullTableName);
            StringBuilder where = new StringBuilder();
            where.Append(" WHERE 1=1");
            bool isFirst = true;

            foreach (string propertyName in mappingInfo.Columns.Keys)
            {
                ColumnAttribute columnAttribute = mappingInfo.Columns[propertyName];

                if (columnAttribute == null || (columnAttribute.IsIncrement && !columnAttribute.IsPrimaryKey)) { continue; }

                string columnName = columnAttribute.Name;
                string sqlParameterName = this.driver.ToSQLParameterName(propertyName);

                IDataParameter parameter = this.driver.CreateDbParameter(propertyName);
                command.Parameters.Add(parameter);

                // 不能对自增字段赋值
                if (!columnAttribute.IsIncrement)
                {
                    sql.Append(isFirst ? "SET " : ", ")
                        .AppendFormat("{0}={1}", columnName, sqlParameterName);

                    if (isFirst) { isFirst = false; }
                }

                //主键作为条件
                if (columnAttribute.IsPrimaryKey)
                {
                    where.AppendFormat(" AND {0}={1}", columnName, sqlParameterName);
                }
            }

            command.CommandText = sql.Append(where).ToString();
            command.CommandType = CommandType.Text;

            SqlUtil.AssignParameters(command.Parameters, entity);
            this.ExecuteNonQuery(command);
        }

        #endregion

        #region 删除记录

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <example>
        /// DELETE Menu WHERE ID=@ID
        /// DELETE Menu WHERE ID=:ID
        /// </example>
        public void Delete<T>(T entity)
        {
            MappingInfo<T> mappingInfo = new MappingInfo<T>();
            IDbCommand command = this.driver.CreateDbCommand();

            StringBuilder sql = new StringBuilder();
            string fullTableName = this.driver.ToFullTableName(mappingInfo.DbName, mappingInfo.TableName);
            sql.AppendFormat("DELETE FROM {0} WHERE 1=1", fullTableName);

            foreach (string propertyName in mappingInfo.PrimaryKeyColumns.Keys)
            {
                ColumnAttribute columnAttribute = mappingInfo.Columns[propertyName];

                IDataParameter parameter = this.driver.CreateDbParameter(propertyName);
                command.Parameters.Add(parameter);

                string sqlParameterName = this.driver.ToSQLParameterName(propertyName);
                sql.AppendFormat(" AND {0}={1}", columnAttribute.Name, sqlParameterName);
            }

            command.CommandText = sql.ToString();
            command.CommandType = CommandType.Text;

            SqlUtil.AssignParameters(command.Parameters, entity);
            this.ExecuteNonQuery(command);
        }

        #endregion

        #region 获取查询 PL/SQL 语句

        public StringBuilder GetQuerySQL<T>()
        {
            MappingInfo<T> mappingInfo = new MappingInfo<T>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");

            bool isFirst = true;

            foreach (string propertyName in mappingInfo.Columns.Keys)
            {
                ColumnAttribute columnAttribute = mappingInfo.Columns[propertyName];

                sql.Append(isFirst ? string.Empty : ", ")
                    .Append(columnAttribute.Name);

                if (isFirst) { isFirst = false; }
            }

            string fullTableName = this.driver.ToFullTableName(mappingInfo.DbName, mappingInfo.TableName);
            sql.AppendFormat(" FROM {0} ", fullTableName);

            return sql;
        }

        #endregion

        #region 根据主键查询记录

        /// <summary>
        /// 根据主键查询记录
        /// </summary>
        /// <example>
        /// SELECT MenuItem,ParentID FROM Menu WHERE ID=@ID
        /// SELECT MenuItem,ParentID FROM Menu WHERE ID=:ID
        /// </example>
        public T Get<T>(params KeyValuePair<string, object>[] primaryKeys)
        {
            MappingInfo<T> mappingInfo = new MappingInfo<T>();
            IDbCommand command = this.driver.CreateDbCommand();
            StringBuilder sql = GetQuerySQL<T>();
            sql.Append(" WHERE 1=1");

            foreach (KeyValuePair<string, object> primaryKey in primaryKeys)
            {
                string propertyName = primaryKey.Key;
                ColumnAttribute columnAttribute = mappingInfo.Columns[propertyName];

                IDataParameter parameter = this.driver.CreateDbParameter(propertyName);
                parameter.Value = primaryKey.Value;
                command.Parameters.Add(parameter);

                string sqlParameterName = this.driver.ToSQLParameterName(propertyName);
                sql.AppendFormat(" AND {0}={1}", columnAttribute.Name, sqlParameterName);
            }

            command.CommandText = sql.ToString();
            command.CommandType = CommandType.Text;

            List<T> list = this.ExecuteEntities<T>(command);
            return (list.Count == 0) ? default(T) : list[0];
        }

        #endregion

        #region 查询记录

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">条件</param>
        /// <returns>T 集合</returns>
        public List<T> Select<T>(string condition)
        {
            StringBuilder sql = GetQuerySQL<T>();

            if (condition.Length > 0)
            {
                sql.AppendFormat("WHERE {0}", condition);
            }

            return this.ExecuteEntities<T>(sql.ToString(), CommandType.Text);
        }

        #endregion

        #region 执行数据库操作命令并返回 T 集合

        /// <summary>
        /// 执行数据库操作命令并返回 T 集合
        /// </summary>		
        /// <param name="commandType">数据库操作命令类型</param>
        /// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>        
        /// <returns>T 集合</returns>
        public List<T> ExecuteEntities<T>(string commandText, CommandType commandType)
        {
            return DynamicBuilder<T>.DataReader2Entity(ExecuteReader(commandText, commandType));
        }


        /// <summary>
        /// 执行数据库操作命令并返回 T 集合
        /// </summary>		
        /// <param name="command">IDbCommand 对象</param>        
        /// <returns>T 集合</returns>
        public List<T> ExecuteEntities<T>(IDbCommand command)
        {
            return DynamicBuilder<T>.DataReader2Entity(ExecuteReader(command));
        }

        #endregion
    }
}
