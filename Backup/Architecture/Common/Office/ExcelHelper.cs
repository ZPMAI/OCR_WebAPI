using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;

namespace CCT.Common.Office
{
	/// <summary>
	/// ExcelHelper 的摘要说明。
	/// </summary>
	public static class ExcelHelper
	{
        const string ConnectionStringFormat = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties='Excel 8.0;HDR=yes;IMEX=1'";


        private static List<string> GetTableNames(string filePath)
        {
            string connectionString = string.Format(ConnectionStringFormat, filePath);

            using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
            {
                try
                {
                    oleDbConnection.Open();

                    object[] restrictions = new object[] { 
                        null, 
                        null, 
                        null, 
                        "TABLE" 
                    };

                    DataTable dataTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions);
                    List<string> tableNames = new List<string>();

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        tableNames.Add((string)dataRow["TABLE_NAME"]);
                    }

                    return tableNames;
                }
                finally
                {
                    oleDbConnection.Close();
                }
            }
        }


        /// <summary>
        /// 获取 EXCEL 表数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>数据集</returns>
        public static DataTable Excel2DataTable(string filePath,  int tableIndex)
        {
            List<string> tableNames = GetTableNames(filePath);

            if (tableNames.Count == 0
                || tableIndex > tableNames.Count - 1) 
            { 
                return null; 
            }

            string connectionString = string.Format(ConnectionStringFormat, filePath);
            
            using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
            {
                try
                {
                    oleDbConnection.Open();

                    string sql = string.Format("SELECT * FROM [{0}]", tableNames[tableIndex]);
                    
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
                    oleDbDataAdapter.SelectCommand = new OleDbCommand(sql, oleDbConnection);
                    oleDbDataAdapter.SelectCommand.CommandType = CommandType.Text;

                    DataSet dataSet = new DataSet();
                    oleDbDataAdapter.Fill(dataSet, tableNames[tableIndex]);

                    return dataSet.Tables[0];
                }
                catch
                {
                    throw;
                }
                finally
                {
                    oleDbConnection.Close();
                }
            }
        }
	}
}
