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
        /// 构造函数
        public MsSqlRunner(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.driver = new MsSqlDriver(connectionString);
        }


        public override void Insert<T>(T entity)
        {
            base.Insert<T>(entity);

            // 获取自增值并赋给 PrimaryKeyProperty
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

            // 获取自增值并赋给 PrimaryKeyProperty
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
        /// 分页查询操作（SQL Server 存储过程 SplitPage 返回 T）
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="sql">T-SQL 数据库操作命令字符串</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageCount">总页数</param>
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

            // 先执行 ExecuteNonQuery 方法以获取传出参数
            ExecuteNonQuery(command);

            recordCount = Convert.ToInt32(((IDataParameter)command.Parameters["@RecordCount"]).Value);
            pageCount = Convert.ToInt32(((IDataParameter)command.Parameters["@PageCount"]).Value);

            //存储过程SplitPage生成的记录放在第二个记录集里面
            IDataReader dataReader = ExecuteReader(command);
            dataReader.NextResult();

            return DynamicBuilder<T>.DataReader2Entity(dataReader);
        }        
    }
}
