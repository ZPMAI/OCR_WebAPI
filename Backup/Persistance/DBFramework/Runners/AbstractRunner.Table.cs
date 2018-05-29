using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace DBFramework.Runners
{
    partial class AbstractRunner
    {

        #region 判读记录是否已经存在

        /// <summary>
        /// 判读记录是否已经存在
        /// </summary>
        public bool IsExist(DataRow dataRow)
        {
           IDbCommand command = this.driver.CreateDbCommand();

           DataTable dataTable = dataRow.Table;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT COUNT(*) FROM {0}", dataTable.TableName);

            for (int i = 0; i < dataTable.PrimaryKey.Length; i++)
            {
                bool isFirst = (i == 0);
                DataColumn dataColumn = dataTable.PrimaryKey[i];

                IDataParameter parameter = this.driver.CreateDbParameter(dataColumn.ColumnName);
                command.Parameters.Add(parameter);

                string sqlParameterName = this.driver.ToSQLParameterName(dataColumn.ColumnName);
                sql
                    .Append(isFirst ? " WHERE " : " AND ")
                    .AppendFormat("{0}={1}", dataColumn.ColumnName, sqlParameterName);
            }

            command.CommandText = sql.ToString();
            command.CommandType = CommandType.Text;

            SqlUtil.AssignParameterValues(command.Parameters, dataRow);

            object returnValue = this.ExecuteScalar(command);
            return Convert.ToInt32(returnValue) > 0;
        }

        #endregion

        #region 添加记录

        /// <summary>
        /// 添加记录
        /// </summary>
        public virtual void Insert(DataRow dataRow)
        {
            IDbCommand command = this.driver.CreateDbCommand();

            DataTable dataTable = dataRow.Table;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("INSERT INTO {0} ", dataTable.TableName);

            StringBuilder columnsExpression = new StringBuilder();
            StringBuilder valuesExpression = new StringBuilder();

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                bool isLast = (i == dataTable.Columns.Count - 1);
                DataColumn dataColumn = dataTable.Columns[i];

                // 不处理自增字段
                if (dataColumn.AutoIncrement) { continue; }

                IDataParameter parameter = this.driver.CreateDbParameter(dataColumn.ColumnName);
                command.Parameters.Add(parameter);

                columnsExpression
                    .Append(isLast ? string.Empty : ",")
                    .Append(dataColumn.ColumnName);

                string sqlParameterName = this.driver.ToSQLParameterName(dataColumn.ColumnName);
                valuesExpression
                    .Append(isLast ? string.Empty : ",")
                    .Append(sqlParameterName);
            }

            sql.AppendFormat("({0})", columnsExpression.ToString())
                .AppendFormat(" VALUES({0})", valuesExpression.ToString());

            command.CommandText = sql.ToString();
            command.CommandType = CommandType.Text;

            SqlUtil.AssignParameterValues(command.Parameters, dataRow);
            this.ExecuteNonQuery(command);
        }

        #endregion

        #region 修改记录

        /// <summary>
        /// 修改记录
        /// </summary>
        public void Update(DataRow dataRow)
        {
            IDbCommand command = this.driver.CreateDbCommand();

            DataTable dataTable = dataRow.Table;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("UPDATE {0} SET ", dataTable.TableName);
            StringBuilder where = new StringBuilder();
            where.Append(" WHERE 1=1");

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                bool isLast = (i == dataTable.Columns.Count - 1);
                DataColumn dataColumn = dataTable.Columns[i];

                // 不处理非主键自增字段
                if (dataColumn.AutoIncrement
                    && Array.IndexOf(dataTable.PrimaryKey, dataColumn) == -1)
                {
                    continue;
                }

                IDataParameter parameter = this.driver.CreateDbParameter(dataColumn.ColumnName);
                command.Parameters.Add(parameter);

                string sqlParameterName = this.driver.ToSQLParameterName(dataColumn.ColumnName);

                sql.Append(isLast ? string.Empty : ",")
                    .AppendFormat("{0}={1}", dataColumn.ColumnName, sqlParameterName);

                //主键作为条件
                if (Array.IndexOf(dataTable.PrimaryKey, dataColumn) != -1)
                {
                    where.AppendFormat(" AND {0}={1}", dataColumn.ColumnName, sqlParameterName);
                }
            }

            command.CommandText = sql.Append(where).ToString();
            command.CommandType = CommandType.Text;

            SqlUtil.AssignParameterValues(command.Parameters, dataRow);
            this.ExecuteNonQuery(command);
        }

        #endregion

        #region 删除记录

        /// <summary>
        /// 删除记录
        /// </summary>
        public void Delete(DataRow dataRow)
        {
            IDbCommand command = this.driver.CreateDbCommand();

            DataTable dataTable = dataRow.Table;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("DELETE FROM {0}", dataTable.TableName);

            for (int i = 0; i < dataTable.PrimaryKey.Length; i++)
            {
                bool isFirst = (i == 0);
                DataColumn dataColumn = dataTable.PrimaryKey[i];

                IDataParameter parameter = this.driver.CreateDbParameter(dataColumn.ColumnName);                
                command.Parameters.Add(parameter);

                string sqlParameterName = this.driver.ToSQLParameterName(dataColumn.ColumnName);
                sql.Append(isFirst ? " WHERE " : " AND ")
                    .AppendFormat("{0}={1}", dataColumn.ColumnName, sqlParameterName);
            }

            command.CommandText = sql.ToString();
            command.CommandType = CommandType.Text;

            SqlUtil.AssignParameterValues(command.Parameters, dataRow);
            this.ExecuteNonQuery(command);
        }

        #endregion

        #region 保存记录

        /// <summary>
        /// 保存记录
        /// </summary>
        public void Save(DataRow dataRow)
        {
            if (IsExist(dataRow))
            {
                Update(dataRow);
            }
            else
            {
                Insert(dataRow);
            }
        }

        #endregion

        #region 根据存储过程名称与 DataRow 对象执行数据库操作命令

        /// <summary>
        /// 根据存储过程名称与 DataRow 对象执行数据库操作命令
        /// </summary>
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="dataRow">DataRow 对象</param>
        public void ExecuteDataRow(string procedureName, DataRow dataRow)
        {
            IDbCommand command = this.CreateProcedureCommand(procedureName);

            // 从DataRow对象中取参数值赋给数据库操作命令参数
            SqlUtil.AssignParameterValues(command.Parameters, dataRow);

            this.ExecuteNonQuery(command);

            // 从数据库操作命令参数取返回值赋给DataRow对象
            SqlUtil.AssignDataRowValues(dataRow, command.Parameters);
        }

        #endregion


        #region 添加记录

        /// <summary>
        /// 添加记录
        /// </summary>
        public virtual void Insert(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Insert(dataRow);
            }
        }

        #endregion

        #region 修改记录

        /// <summary>
        /// 修改记录
        /// </summary>
        public void Update(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Update(dataRow);
            }
        }

        #endregion

        #region 删除记录

        /// <summary>
        /// 删除记录
        /// </summary>
        public void Delete(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Delete(dataRow);
            }
        }

        #endregion

        #region 保存记录

        /// <summary>
        /// 保存记录
        /// </summary>
        public void Save(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Save(dataRow);
            }
        }

        #endregion

        #region 根据存储过程名称与 DataTable 对象执行数据库操作命令

        /// <summary>
        /// 根据存储过程名称与 DataRow 对象执行数据库操作命令
        /// </summary>
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="dataTable">DataRow 对象</param>
        public void ExecuteDataTable(string procedureName, DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                ExecuteDataRow(procedureName, dataRow);
            }
        }

        #endregion
    }
}
