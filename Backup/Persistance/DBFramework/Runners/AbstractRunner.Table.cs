using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace DBFramework.Runners
{
    partial class AbstractRunner
    {

        #region �ж���¼�Ƿ��Ѿ�����

        /// <summary>
        /// �ж���¼�Ƿ��Ѿ�����
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

        #region ��Ӽ�¼

        /// <summary>
        /// ��Ӽ�¼
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

                // �����������ֶ�
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

        #region �޸ļ�¼

        /// <summary>
        /// �޸ļ�¼
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

                // ����������������ֶ�
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

                //������Ϊ����
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

        #region ɾ����¼

        /// <summary>
        /// ɾ����¼
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

        #region �����¼

        /// <summary>
        /// �����¼
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

        #region ���ݴ洢���������� DataRow ����ִ�����ݿ��������

        /// <summary>
        /// ���ݴ洢���������� DataRow ����ִ�����ݿ��������
        /// </summary>
        /// <param name="procedureName">�洢��������</param>
        /// <param name="dataRow">DataRow ����</param>
        public void ExecuteDataRow(string procedureName, DataRow dataRow)
        {
            IDbCommand command = this.CreateProcedureCommand(procedureName);

            // ��DataRow������ȡ����ֵ�������ݿ�����������
            SqlUtil.AssignParameterValues(command.Parameters, dataRow);

            this.ExecuteNonQuery(command);

            // �����ݿ�����������ȡ����ֵ����DataRow����
            SqlUtil.AssignDataRowValues(dataRow, command.Parameters);
        }

        #endregion


        #region ��Ӽ�¼

        /// <summary>
        /// ��Ӽ�¼
        /// </summary>
        public virtual void Insert(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Insert(dataRow);
            }
        }

        #endregion

        #region �޸ļ�¼

        /// <summary>
        /// �޸ļ�¼
        /// </summary>
        public void Update(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Update(dataRow);
            }
        }

        #endregion

        #region ɾ����¼

        /// <summary>
        /// ɾ����¼
        /// </summary>
        public void Delete(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Delete(dataRow);
            }
        }

        #endregion

        #region �����¼

        /// <summary>
        /// �����¼
        /// </summary>
        public void Save(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Save(dataRow);
            }
        }

        #endregion

        #region ���ݴ洢���������� DataTable ����ִ�����ݿ��������

        /// <summary>
        /// ���ݴ洢���������� DataRow ����ִ�����ݿ��������
        /// </summary>
        /// <param name="procedureName">�洢��������</param>
        /// <param name="dataTable">DataRow ����</param>
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
