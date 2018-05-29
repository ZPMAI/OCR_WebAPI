using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

using DBFramework.Drivers;
using DBFramework.MSIL;
using DBFramework.Mapping;

namespace DBFramework.Runners
{
    public class MsSqlRunner : AbstractRunner
    {
        /// ���캯��
        public MsSqlRunner(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            this.driver = new MsSqlDriver(connectionString);
        }


        public override void Insert<T>(T entity)
        {
            base.Insert<T>(entity);

            // ��ȡ����ֵ������ PrimaryKeyProperty
            MappingInfo<T> tableInfo = new MappingInfo<T>();

            if (!string.IsNullOrEmpty(tableInfo.IncrementName))
            {
                string sql = string.Format("SELECT IDENT_CURRENT('{0}')", tableInfo.TableName);
                object returnValue = this.ExecuteScalar(sql, CommandType.Text);

                SqlUtil.SetValue(entity, tableInfo.IncrementName, returnValue);
            }
        }

        public override void Insert(DataRow dataRow)
        {
            base.Insert(dataRow);

            // ��ȡ����ֵ������ PrimaryKeyProperty
            DataTable dataTable = dataRow.Table;

            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                if (dataColumn.AutoIncrement)
                {
                    string sql = string.Format("SELECT IDENT_CURRENT('{0}')", dataTable.TableName);
                    object returnValue = this.ExecuteScalar(sql, CommandType.Text);

                    dataRow[dataColumn.ColumnName] = returnValue;
                }
            }
        }

        /// <summary>
        /// ��ҳ��ѯ������SQL Server �洢���� SplitPage ���� T��
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="sql">T-SQL ���ݿ���������ַ���</param>
        /// <param name="currentPage">��ǰҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="pageCount">��ҳ��</param>
        public List<T> SplitPage<T>(string condition, int currentPage, int pageSize, out int recordCount, out int pageCount)
        {
            StringBuilder sql = base.GetQuerySQL<T>();

            if (!string.IsNullOrEmpty(condition))
            {
                sql.AppendFormat("WHERE {0}", condition);
            }

            SqlCommand command = this.CreateProcedureCommand("SplitPage") as SqlCommand;

            command.Parameters["@ExecSql"].Value = sql.ToString();
            command.Parameters["@CurrentPage"].Value = currentPage;
            command.Parameters["@PageSize"].Value = pageSize;

            // ��ִ�� ExecuteNonQuery �����Ի�ȡ��������
            ExecuteNonQuery(command);

            recordCount = Convert.ToInt32(((IDataParameter)command.Parameters["@RecordCount"]).Value);
            pageCount = Convert.ToInt32(((IDataParameter)command.Parameters["@PageCount"]).Value);

            //�洢����SplitPage���ɵļ�¼���ڵڶ�����¼������
            IDataReader dataReader = ExecuteReader(command);
            dataReader.NextResult();

            return DynamicBuilder<T>.DataReader2Entity(dataReader);
        }        
    }
}
