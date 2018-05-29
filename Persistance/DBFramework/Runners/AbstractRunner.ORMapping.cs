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
        #region �ж���¼�Ƿ��Ѿ�����

        /// <summary>
        /// �ж���¼�Ƿ��Ѿ�����
        /// </summary>
        /// <returns>��/��</returns>
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

        #region �����¼

        /// <summary>
        /// �����¼
        /// </summary>
        public virtual void Save<T>(T entity)
        {
            if (IsExist<T>(entity))
            {
                if (MessageBox.Show("��ģ���µĹ����Ѿ����ڣ��Ƿ񸲸ǣ�", string.Empty, MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        #region ��Ӽ�¼

        /// <summary>
        /// ��Ӽ�¼
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

                // �����������ֶ�
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

        #region �޸ļ�¼

        /// <summary>
        /// �޸ļ�¼
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

                // ���ܶ������ֶθ�ֵ
                if (!columnAttribute.IsIncrement)
                {
                    sql.Append(isFirst ? "SET " : ", ")
                        .AppendFormat("{0}={1}", columnName, sqlParameterName);

                    if (isFirst) { isFirst = false; }
                }

                //������Ϊ����
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

        #region ɾ����¼

        /// <summary>
        /// ɾ����¼
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

        #region ��ȡ��ѯ PL/SQL ���

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

        #region ����������ѯ��¼

        /// <summary>
        /// ����������ѯ��¼
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

        #region ��ѯ��¼

        /// <summary>
        /// ��ѯ��¼
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">����</param>
        /// <returns>T ����</returns>
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

        #region ִ�����ݿ����������� T ����

        /// <summary>
        /// ִ�����ݿ����������� T ����
        /// </summary>		
        /// <param name="commandType">���ݿ������������</param>
        /// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>        
        /// <returns>T ����</returns>
        public List<T> ExecuteEntities<T>(string commandText, CommandType commandType)
        {
            return DynamicBuilder<T>.DataReader2Entity(ExecuteReader(commandText, commandType));
        }


        /// <summary>
        /// ִ�����ݿ����������� T ����
        /// </summary>		
        /// <param name="command">IDbCommand ����</param>        
        /// <returns>T ����</returns>
        public List<T> ExecuteEntities<T>(IDbCommand command)
        {
            return DynamicBuilder<T>.DataReader2Entity(ExecuteReader(command));
        }

        #endregion
    }
}
