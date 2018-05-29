using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;

namespace CCT.SystemFramework.Office
{
	/// <summary>
	/// ExcelHelper 的摘要说明。
	/// </summary>
	public class ExcelHelper
	{

        //64位电脑不支持olede.4.0 增加2007的驱动，如有可向下兼容，如没有可使用03的驱动 2014/8/11增加  甄耀红

        //const string ConnectionStringFormat2003 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties='Excel 8.0;HDR=yes;IMEX=1'";
        //const string ConnectionStringFormat2007 = "Provider = Microsoft.ACE.OLEDB.12.0 ; Data Source ={0};Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'";

        const string ConnectionStringFormat2003 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties='Excel 8.0;HDR=yes'";
        const string ConnectionStringFormat2007 = "Provider = Microsoft.ACE.OLEDB.12.0 ; Data Source ={0};Extended Properties='Excel 12.0;HDR=Yes'";


        private static void ExcelConn(string filename, ref OleDbConnection myConn)
        {

            try
            {
                string con = string.Format(ConnectionStringFormat2007, filename);
                myConn = new OleDbConnection(con);
                myConn.Open();
                
            }
            catch (System.Exception ex)
            {
                string con = string.Format(ConnectionStringFormat2003, filename);
                myConn = new OleDbConnection(con);
                myConn.Open();
               
            }

        }
        

        /// 构造函数
		public ExcelHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}		
		
		#region 导出EXCEL
		/// <summary>
		/// 导出EXCEL
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="fileName"></param>
		public static void ExportToExcel(DataSet ds, string fileName)
		{
            OleDbConnection conn = null;

			try
			{
				//加工文件名
				string[] strTemp = fileName.Split('\\');
				string NewName = string.Empty;
				foreach(string s in strTemp)
				{
					NewName = NewName + "\\\\" + s;
				}
				NewName = NewName.TrimStart('\\');

				if (ds.Tables.Count == 0)
				{
					return;
				}			
				
				Excel._Application xlApp = new Excel.ApplicationClass();
				Excel._Workbook xlBook = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);

				if (ds.Tables.Count > 1) 
				{
					int i = ds.Tables.Count;
					while (i > 1)
					{
						xlBook.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
						i--;
					}
				}

				int loop = 1;
				foreach (DataTable dt2 in ds.Tables)
				{	
					FillExcelCaption((Excel._Worksheet)xlBook.Worksheets[loop], dt2);
					loop++;
				}	
			
				xlBook.SaveCopyAs(fileName);	
				xlBook.Saved = true;				
				xlApp.Quit();				
				Kill((Excel.Application)xlApp);	

				//取数据表			
                //string con = string.Format("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = {0}; Extended Properties = Excel 8.0", NewName);				
                //conn = new OleDbConnection(con);
                //conn.Open();  
                ExcelConn(NewName, ref conn);
				System.Data.OleDb.OleDbCommand cmd = new OleDbCommand();
				cmd.Connection = conn;				
				
				foreach (DataTable dt in ds.Tables)
				{
					StringBuilder sql = new StringBuilder();
					sql.AppendFormat("INSERT INTO [{0}$] (", dt.TableName);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(dt.Columns[i].Caption))
                        {
                            sql.AppendFormat("[{0}],", dt.Columns[i].Caption);
                        }
                    }
                    sql.Remove(sql.Length - 1, 1);
					sql.Append(") VALUES ");

					string head = sql.ToString();				
					for (int j = 0; j < dt.Rows.Count; j++)
					{	
						sql = new StringBuilder();
						sql.Append(head);
						sql.Append(" (");
						for (int k = 0; k < dt.Columns.Count; k++)
						{
                            if (!string.IsNullOrEmpty(dt.Columns[k].Caption))
                            {
                                if (dt.Columns[k].DataType.Equals(typeof(String)) ||
                                    dt.Columns[k].DataType.Equals(typeof(DateTime)) ||
                                    dt.Columns[k].DataType.Equals(typeof(Char)))
                                {
                                    sql.AppendFormat("'{0}'", dt.Rows[j][k].ToString());
                                }
                                else
                                {
                                    sql.AppendFormat("{0}", dt.Rows[j][k].ToString().Trim().Equals(string.Empty) ? 0 : Convert.ToDecimal(dt.Rows[j][k]));
                                }

                                sql.Append(",");

                            }
						}
                        sql.Remove(sql.Length - 1, 1);
						sql.Append(") ");
						cmd.CommandText = sql.ToString();			
						cmd.ExecuteNonQuery();
					}	
				}				
				
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (conn != null)   
				{   
					conn.Close();   
					conn.Dispose();   
				}   
			}						
		}
		#endregion


        #region 导入EXCEL with NPOI

        private static HSSFWorkbook InitializeWorkbook(string path)
        {
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            } 
            return hssfworkbook;
        }
        private static XSSFWorkbook InitializeWorkbookXLSX(string path)
        {
            XSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new XSSFWorkbook(file);
            }
            return hssfworkbook;
        }
        /// <summary>
        /// 用NPOI导入EXCEL
        /// 2015/10/30增加处理EXCEL2007部分
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static DataSet ImportToDataSetWithNPOI(string fileName)
        {
            DataSet ds = new DataSet();
            try
            {
                //加工文件名
                string[] strTemp = fileName.Split('\\');
                string NewName = string.Empty;
                foreach (string s in strTemp)
                {
                    NewName = NewName + "\\\\" + s;
                }
                NewName = NewName.TrimStart('\\');

                ISheet sheet;
                if (NewName.Substring(NewName.LastIndexOf('.')).ToUpper() == ".XLSX")
                {
                    XSSFWorkbook hssfworkbook = InitializeWorkbookXLSX(NewName);
                    sheet = hssfworkbook.GetSheetAt(0);
                }
                else
                {
                    HSSFWorkbook hssfworkbook = InitializeWorkbook(NewName);
                    sheet = hssfworkbook.GetSheetAt(0);
                }
                // HSSFWorkbook hssfworkbook = InitializeWorkbook(NewName);
                // ISheet sheet=hssfworkbook.GetSheetAt(0);
                if (sheet == null) return null;

                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                DataTable dt = new DataTable();
                //COLUMN
                IRow header = sheet.GetRow(sheet.FirstRowNum);

                for (int i = 0; i < header.LastCellNum; i++)
                {
                    ICell cell = header.GetCell(i);
                    if (cell != null && cell.ToString() != string.Empty)
                        dt.Columns.Add(cell.ToString());

                }
                //行处理
                for (int j = sheet.FirstRowNum + 1; j <= sheet.LastRowNum; j++)
                {
                    IRow row = sheet.GetRow(j);
                    if (row == null) break;
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ICell cell = row.GetCell(i);

                        if (cell == null)
                        {
                            dr[i] = string.Empty;
                        }
                        else
                        {
                            switch (cell.CellType)
                            {
                                case CellType.Numeric:
                                    if (HSSFDateUtil.IsCellDateFormatted(cell))
                                    {
                                        dr[i] = cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss");
                                    }
                                    else
                                    {
                                        dr[i] = cell.ToString().Trim();
                                    }
                                    break;
                                case CellType.Formula:
                                    dr[i] = handleFormual(cell);
                                    break;
                                default:
                                    dr[i] = cell.ToString().Trim();
                                    break;

                            }
                        }
                    }
                    dt.Rows.Add(dr);
                }

                ds.Tables.Add(dt);
                return ds;

            }
            catch(System.Exception ex)
            {
                string temp = ex.Message;
                return null; }


        }

        private static string handleFormual(ICell cell)
        {
            string s = string.Empty;
            if (cell.Sheet.Workbook is NPOI.XSSF.UserModel.XSSFWorkbook)
            {
                XSSFFormulaEvaluator e = new XSSFFormulaEvaluator(cell.Sheet.Workbook);
                CellValue c = e.Evaluate(cell);
                if (c.CellType == CellType.Numeric)
                    s = c.NumberValue.ToString();
                else
                    s = c.StringValue.Trim();

            }
            else
            {
                HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                CellValue c = e.Evaluate(cell);
                if (c.CellType == CellType.Numeric)
                    s = c.NumberValue.ToString();
                else
                    s = c.StringValue.Trim();
            }
            return s;
        }
        #endregion

        #region 导入EXCEL
        /// <summary>
		/// 导入EXCEL
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static DataSet ImportToDataSet(string fileName)
		{
			DataSet ds = new DataSet();
			OleDbConnection myConn = null;
			DataTable sheets = null;

			try
			{
				//加工文件名
				string[] strTemp = fileName.Split('\\');
				string NewName = string.Empty;
				foreach(string s in strTemp)
				{
					NewName = NewName + "\\\\" + s;
				}
				NewName = NewName.TrimStart('\\');

				//取数据表			
                //string con = string.Format("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = {0}; Extended Properties = Excel 8.0", NewName);
                //myConn = new OleDbConnection(con);
                //myConn.Open();
                ExcelConn(NewName, ref myConn);
				sheets = myConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);         
				if (sheets == null)   
				{   
					return null;
				}   
				
				foreach (DataRow row in sheets.Rows)   
				{   					
					string com = String.Format("SELECT * FROM [{0}] ", row["TABLE_NAME"].ToString());					
					OleDbDataAdapter myCommand = new OleDbDataAdapter(com, myConn);	
					DataSet tmp = new DataSet();
					myCommand.Fill(tmp);	
					tmp.Tables[0].TableName = row["TABLE_NAME"].ToString().TrimEnd(new char[]{'$'});
					ds.Tables.Add(tmp.Tables[0].Copy());
					tmp.Dispose();
				}   										
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (myConn != null)   
				{   
					myConn.Close();   
					myConn.Dispose();   
				}   
				if (sheets != null)
				{
					sheets.Dispose();
				}				
			}
				
			return ds;
		}
		#endregion
	
		#region 打开EXCEL
		/// <summary>
		/// 打开EXCEL
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static Excel._Workbook OpenXls(string fileName)
		{			
			object objMissing = Type.Missing;

			Object[] objArgs = new Object[]{fileName, objMissing, objMissing, objMissing, objMissing,
											   objMissing, objMissing, objMissing, objMissing, objMissing, objMissing,
											   objMissing, objMissing, objMissing, objMissing};

			Type t = typeof(Excel.ApplicationClass);				
			Object objApp = t.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);
			Object objVer = objApp.GetType().InvokeMember("Version", BindingFlags.GetProperty, null, objApp, null);
			if(objVer.ToString().Trim().Equals("11.0")) 
			{
				objArgs = new Object[]{fileName, objMissing, objMissing, objMissing, objMissing,
										  objMissing, objMissing, objMissing, objMissing, objMissing, objMissing,
										  objMissing, objMissing, objMissing, objMissing};
			}
			else
			{
				objArgs = new Object[]{fileName, objMissing, objMissing, objMissing, objMissing,
										  objMissing, objMissing, objMissing, objMissing, objMissing,
										  objMissing, objMissing, objMissing};
			}

			Object objBooks = objApp.GetType().InvokeMember("WorkBooks", BindingFlags.GetProperty, null, objApp, null);
			Object objBook = objBooks.GetType().InvokeMember("Open", BindingFlags.InvokeMethod, null, objBooks, objArgs);
			return (Excel._Workbook) objBook;
		}
		#endregion

		#region 私有方法
		private static void FillExcelCaption(Excel._Worksheet xlSheet, System.Data.DataTable dt)
		{
			if (dt.Rows.Count == 0)
			{
				return;
			}

			long rows = dt.Rows.Count;
			long cols = dt.Columns.Count;

			//在第一行写字段名
			for (int i = 1; i <= cols; i++)
			{					
				xlSheet.Cells[1, i] = dt.Columns[i - 1].Caption;
			}			

			xlSheet.Name = dt.TableName;
		}

		private static void FillExcel(Excel._Worksheet xlSheet, System.Data.DataTable dt)
		{
			if (dt.Rows.Count == 0)
			{
				return;
			}

			long rows = dt.Rows.Count;
			long cols = dt.Columns.Count;

			//在第一行写字段名
			for (int i = 1; i <= cols; i++)
			{					
				xlSheet.Cells[1, i] = dt.Columns[i - 1].Caption;
			}
			//逐行写数据
			for (int j = 1; j <= rows; j++) 
			{
				for (int i = 1; i <= cols; i++) 
				{					
					xlSheet.Cells[j + 1, i] = dt.Rows[j - 1][i - 1].ToString().Trim();
				}   													 
			}

			xlSheet.Name = dt.TableName;
		}

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern int GetWindowThreadProcessId(IntPtr hwnd,out int ID);
		public static void Kill(Excel.Application excel)
		{
			//得到这个句柄，具体作用是得到这块内存入口
			IntPtr t = new IntPtr(excel.Hwnd); 
			int k = 0;
			//得到本进程唯一标志k
			GetWindowThreadProcessId(t,out k); 
			//得到对进程k的引用
			System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k); 
			//关闭进程k
			p.Kill(); 
		}	
		#endregion
	}
}
