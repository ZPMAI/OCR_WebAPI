using System;
using System.Threading;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Configuration;
using System.Net;

using Excel;


namespace CCT.SystemFramework.ReportingService
{ 
    /// <summary>
    /// 2010-10-28 叶君腾 创建 
    /// </summary>
    public class ReportingServiceHelper
    {

        #region  使用用户验证显示报表 甄耀红
        public static NetworkCredential Credential 
        {
            get {

                /*System.Configuration.Configuration cfg = ConfigurationManager.OpenExeConfiguration("CCT.SystemFramework.dll");
                string user = cfg.AppSettings.Settings["ReportUser"].Value.ToString();
                string pwd = cfg.AppSettings.Settings["ReportPassword"].Value.ToString();
                string domain = cfg.AppSettings.Settings["ReportDomain"].Value.ToString(); */
                string user = Properties.Settings.Default.ReportUser;
                string pwd = Properties.Settings.Default.ReportPassword;
                string domain = string.Empty;// Properties.Settings.Default.ReportDomain;
                return new NetworkCredential(user, pwd, domain);
            }
        }
        #endregion

        #region 导出报表
        /// <summary>
        /// 导出报表
        /// </summary>
        /// <param name="format">PDF,EXCEL,IMAGE,XML,WORD,HTML</param>
        /// <param name="filename">导出文件完整路径+文件名</param>
        /// <param name="imagetype">如果format选择image，则需选择imagetype:BMP、EMF、GIF、JPEG、PNG 或 TIFF</param>
        /// <param name="url">报表服务器名</param>
        /// <param name="path">报表路径</param>
        /// <param name="parms">报表参数</param>
        public static void Export(string format, string filename, string imagetype, string url, string path, ReportParameter[] parms)
        {
            string deviceInfo = String.Format("<DeviceInfo>{0}</DeviceInfo>",
                format.ToUpper() == "IMAGE" ? String.Format("<OutputFormat>{0}</OutputFormat><StartPage>0</StartPage>", 
                imagetype.ToUpper()) : string.Empty);

            Warning[] warnings;
            string mt;
            string fe;
            string en;
            string[] st;

            ReportViewer reportViewer1 = new ReportViewer(); 
            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            reportViewer1.ServerReport.ReportPath = path;
            reportViewer1.ServerReport.ReportServerUrl = new System.Uri(url, System.UriKind.Absolute);
            reportViewer1.ServerReport.ReportServerCredentials.NetworkCredentials = Credential;
            if (parms != null)
            {
                reportViewer1.ServerReport.SetParameters(parms);
            }

            byte[] b = reportViewer1.ServerReport.Render(format, deviceInfo, out mt, out en, out fe, out st, out warnings);

            string directoryName = filename.Substring(0, filename.LastIndexOf('\\'));

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            FileStream f = new FileStream(filename, FileMode.Create);
            f.Write(b, 0, b.Length);
            f.Close();
        }

        #endregion

        #region 打印报表

        /// <summary>
        /// 打印报表
        /// </summary>
        /// <param name="url">报表服务器名</param>
        /// <param name="path">报表路径</param>
        /// <param name="parms">报表参数</param>
        /// <param name="printer">打印机名</param>
        public static void Print(string url, string path, ReportParameter[] parms, string printer, bool landscape, PaperSize papersize)
        {
            Print p = new Print();
            p.PrintDoc(url, path, parms, printer,landscape,papersize);
        }

        #endregion

        #region 显示报表

        /// <summary>
        /// 显示报表
        /// </summary>
        /// <param name="url">报表服务器名</param>
        /// <param name="path">报表路径</param>
        /// <param name="parms">报表参数</param>
        /// <param name="showParameterPrompts">是否显示报表参数</param>
        public static void Show(string url, string path, ReportParameter[] parms, bool showParameterPrompts)
        {
            FrmShow f = new FrmShow(url, path, parms, Credential,showParameterPrompts,string.Empty,null);
            f.Show();
        }
        /// <summary>
        /// 显示报表
        /// </summary>
        /// <param name="url">报表服务器名</param>
        /// <param name="path">报表路径</param>
        /// <param name="parms">报表参数</param>
        /// <param name="showParameterPrompts">是否显示报表参数</param>
        /// <param name="captionText">显示在窗口的标题</param>
        public static void Show(string url, string path, ReportParameter[] parms, bool showParameterPrompts,string captionText)
        {
            FrmShow f = new FrmShow(url, path, parms, Credential, showParameterPrompts,captionText,null);
            f.Show();
        }
        /// <summary>
        /// 显示报表
        /// </summary>
        /// <param name="url">报表服务器名</param>
        /// <param name="path">报表路径</param>
        /// <param name="parms">报表参数</param>
        /// <param name="showParameterPrompts">是否显示报表参数</param>
        /// <param name="captionText">显示在窗口的标题</param>
        /// <param name="parentForm">父窗口，报表可以显示在主窗口之内</param>
        public static void Show(string url, string path, ReportParameter[] parms, bool showParameterPrompts, string captionText,Form parentForm)
        {
            FrmShow f = new FrmShow(url, path, parms, Credential, showParameterPrompts, captionText, parentForm);
            f.Show();
        }
        #endregion

        #region 多张报表合成一个EXCEL文件
        /// <summary>
        /// 多张报表合成一个EXCEL文件
        /// </summary>
        /// <param name="tempDir">临时文件存放目录</param>
        /// <param name="filename">指定导出的文件名，不指定的情况下，弹出对话框让用户选择</param>
        /// <param name="sheetNames">工作表名列表</param>
        /// <param name="url">报表服务器路径</param>
        /// <param name="path">报表路径</param>
        /// <param name="parms">参数列表</param>
        /// <param name="isVisible">是否显示EXCEL，若不显示EXCEL，则自动保存到filename</param>
        /// <remarks>
        /// 最后清除临时文件
        /// </remarks>
        public static void ExportToOneExcel(string tempDir, string filename, string[] sheetNames, string url, string[] path, 
            ReportParameter[][] parms, bool isVisible)
        {
            if (filename == string.Empty)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "EXCEL(*.xls)|*.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filename = sfd.FileName;
                }
                if (sfd.FileName == string.Empty)
                {
                    return;
                }
            }

            string[] excels = new string[path.Length];
            string dt = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            for (int j = 0; j < excels.Length; j++)
            {
                excels[j] = string.Format(@"{0}\{1}{2}.xls", tempDir, dt, j);
            }

            for (int i = 0; i < path.Length; i++)
            {
                Export("EXCEL", excels[i], string.Empty, url, path[i], 
                    (parms == null ? null : parms[i]));
            }

            Excel.Application excel = new Excel.Application();
            Excel.Workbook workbook1 = excel.Workbooks.Open(excels[0], Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Excel.Worksheet worksheet1 = (Excel.Worksheet)workbook1.Sheets[1];
            worksheet1.Name = sheetNames[0];
            worksheet1.PageSetup.CenterFooter = "第 &P 页，共 &N 页";                //加页码


            for (int k = excels.Length - 1; k >= 1 ; k--)
            {
                Excel.Workbook workbook2 = excel.Workbooks.Open(excels[k], Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                Excel.Worksheet worksheet2 = (Excel.Worksheet)workbook2.Sheets[1];
                worksheet2.Name = sheetNames[k];
                worksheet2.PageSetup.CenterFooter = "第 &P 页，共 &N 页";                //加页码

                worksheet2.Copy(Type.Missing, worksheet1);
                workbook2.Close(false, Type.Missing, Type.Missing);
            }

            if (!isVisible)
            {
                workbook1.Save(); 

                Office.ExcelHelper.Kill(excel);
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }

                Thread.Sleep(100);
                
                File.Move(excels[0], filename);                
            }

            for (int m = 1; m < excels.Length; m++)
            {
                File.Delete(excels[m]);
            }

            //excel.Visible = isVisible;
        }

        #endregion 


        #region 多张报表合成一个EXCEL文件 ,保存固定位置
        /// <summary>
        /// 多张报表合成一个EXCEL文件
        /// </summary>
        /// <param name="tempDir">临时文件存放目录</param>
        /// <param name="filename">指定导出的文件名，不指定的情况下，弹出对话框让用户选择</param>
        /// <param name="sheetNames">工作表名列表</param>
        /// <param name="url">报表服务器路径</param>
        /// <param name="path">报表路径</param>
        /// <param name="parms">参数列表</param>
        /// <param name="isVisible">是否显示EXCEL，若不显示EXCEL，则自动保存到filename</param>
        /// <remarks>
        /// 最后清除临时文件
        /// </remarks>
        public static void ExportToOneExcelSave(string tempDir, string filename, string[] sheetNames, string url, string[] path,
            ReportParameter[][] parms, bool isVisible)
        {
            if (filename == string.Empty)
            {
                SaveFileDialog sfd = new SaveFileDialog(); 
                sfd.Filter = "EXCEL(*.xls)|*.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filename = sfd.FileName;
                }
                if (sfd.FileName == string.Empty)
                {
                    return;
                }
            }

            string[] excels = new string[path.Length];
            string dt = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            for (int j = 0; j < excels.Length; j++)
            {
                excels[j] = string.Format(@"{0}\{1}{2}.xls", tempDir, dt, j);
            }

            for (int i = 0; i < path.Length; i++)
            {
                Export("EXCEL", excels[i], string.Empty, url, path[i],
                    (parms == null ? null : parms[i]));
            }

            Excel.Application excel = new Excel.Application();
            Excel.Workbook workbook1 = excel.Workbooks.Open(excels[0], Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Excel.Worksheet worksheet1 = (Excel.Worksheet)workbook1.Sheets[1];
            worksheet1.Name = sheetNames[0];
            worksheet1.PageSetup.CenterFooter = "第 &P 页，共 &N 页";                //加页码


            for (int k = excels.Length - 1; k >= 1; k--)
            {
                Excel.Workbook workbook2 = excel.Workbooks.Open(excels[k], Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                Excel.Worksheet worksheet2 = (Excel.Worksheet)workbook2.Sheets[1];
                worksheet2.Name = sheetNames[k];
                worksheet2.PageSetup.CenterFooter = "第 &P 页，共 &N 页";                //加页码

                worksheet2.Copy(Type.Missing, worksheet1);
                workbook2.Close(false, Type.Missing, Type.Missing);
            }

            if (!isVisible)
            {
                workbook1.Save();

                Office.ExcelHelper.Kill(excel);
                if (File.Exists(tempDir + filename))
                {
                    File.Delete(tempDir + filename);
                }

                Thread.Sleep(100);
                

                File.Move (excels[0], tempDir+filename);
            }

            for (int m = 1; m < excels.Length; m++)
            {
                File.Delete(excels[m]);
            }

            //excel.Visible = isVisible;
        }

        #endregion 



        public static void Merge(string sourceFile, string baseFile, string targetFile, int rowCount, int columnCount)
        {
            object MISSING = Type.Missing;
            Excel.Application excel = new Excel.Application();

            try
            {
                Excel.Workbook sourceBook = excel.Workbooks.Open(sourceFile,
                    MISSING, MISSING, MISSING, MISSING, MISSING, MISSING, MISSING,
                    MISSING, MISSING, MISSING, MISSING, MISSING, MISSING, MISSING);

                Excel.Worksheet sourceSheet = (Excel.Worksheet)sourceBook.Sheets[1];                

                Excel.Workbook baseBook = excel.Workbooks.Open(baseFile,
                        MISSING, MISSING, MISSING, MISSING, MISSING, MISSING, MISSING,
                        MISSING, MISSING, MISSING, MISSING, MISSING, MISSING, MISSING);

                Excel.Worksheet baseSheet = (Excel.Worksheet)baseBook.Sheets[1];
                //baseSheet.Unprotect(string.Empty);

                for (int column = 1; column <= columnCount; column++)
                {
                    for (int row = 1; row <= rowCount; row++)
                    {
                        Excel.Range baseRange = (Excel.Range)baseSheet.Cells[row, column];

                        if (baseRange.AllowEdit)
                        {
                            Excel.Range sourceRange = (Excel.Range)sourceSheet.Cells[row, column];
                            baseRange.Value2 = sourceRange.Value2;
                        }
                    }
                }

                //baseSheet.Protect(string.Empty,
                //    MISSING, MISSING, MISSING, MISSING, MISSING, MISSING, MISSING, 
                //    MISSING, MISSING, MISSING, MISSING, MISSING, MISSING, MISSING, MISSING);

                sourceBook.Close(false, MISSING, MISSING);

                baseBook.SaveCopyAs(targetFile);                    
                baseBook.Close(false, MISSING, MISSING);
            }
            finally
            {
                Office.ExcelHelper.Kill(excel);
            }

            Thread.Sleep(100);
        }

        public static void AppendSheet(string sourceFile, string targetFile, string sheetName)
        {
            object MISSING = Type.Missing;

            Excel.Application excel = new Excel.Application();

            try
            {
                Excel.Workbook sourceBook = excel.Workbooks.Open(sourceFile,
                    MISSING, MISSING, MISSING, MISSING, MISSING, MISSING, MISSING,
                    MISSING, MISSING, MISSING, MISSING, MISSING, MISSING, MISSING);

               // Excel.Worksheet sourceSheet = (Excel.Worksheet)sourceBook.Sheets.Add(MISSING, sourceBook.Sheets[sourceBook.Sheets.Count], 1, MISSING);
                Excel.Worksheet sourceSheet = (Excel.Worksheet)sourceBook.Sheets.Add(sourceBook.Sheets[1], MISSING, 1, MISSING);
                sourceSheet.Name = sheetName;

                sourceSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlPortrait;  //横打印xlLandscape  竖打印xlPortrait
                sourceSheet.PageSetup.CenterFooter = "第 &P 页，共 &N 页";                //加页码
                sourceSheet.PageSetup.Zoom = 75;



                int row = 1;

                using (StreamReader streamReader = new StreamReader(targetFile))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string s = streamReader.ReadLine();

                        if (!string.IsNullOrEmpty(s))
                        {
                            if (s.Substring(0, 1).Equals("=")) { continue; }
                        }

                        // 异常来自 HRESULT:0x800A03EC
                        Excel.Range sourceRange = (Excel.Range)sourceSheet.Cells[row, 1];
                        sourceRange.Value2 = s;
                        sourceRange.RowHeight = 12.75;
                        sourceRange.ColumnWidth = 150;
                        sourceRange.Font.Name   = "宋体";
                     //   sourceRange.Orientation = Excel.XlPageOrientation.xlPortrait;


                        row++;
                    }

                    streamReader.Close();
                }

                //Excel.Workbook targetBook = excel.Workbooks.Open(targetFile,
                //    MISSING, MISSING, MISSING, MISSING, MISSING, MISSING, MISSING,
                //    MISSING, MISSING, MISSING, MISSING, MISSING, MISSING, MISSING);
                //Excel.Worksheet targetSheet = (Excel.Worksheet)targetBook.Sheets[1];
                // Excel 无法将工作表插入到目标工作簿中，因为目标工作簿的行数和列数比源工作簿少。
                // 若要将数据移动到或复制到目标工作簿，可以选中数据，然后使用“复制”和“粘贴”命令将其插入其他工作簿的工作表中。
                //targetSheet.Copy(MISSING, sourceSheet);
                //targetBook.Close(false, MISSING, MISSING);

                sourceBook.Save();
            }
            catch (Exception e)
            {                
                string s = e.Message;
            }
            finally
            {
                Office.ExcelHelper.Kill(excel);
            }

            Thread.Sleep(100);
        }
    }
}
