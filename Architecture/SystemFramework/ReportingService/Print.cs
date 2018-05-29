using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Net;
using System.Configuration;
using System.Collections.Specialized;

namespace CCT.SystemFramework.ReportingService
{
    public class Print : IDisposable
    {
        private int m_currentPageIndex;
        private IList<byte[]> b;

        #region  使用用户验证显示报表 甄耀红
        //20120310多环境增加配置文件处理
        private static NetworkCredential Credential
        {
            get
            {
               /*System.Configuration.Configuration cfg = ConfigurationManager.OpenExeConfiguration("CCT.SystemFramework.dll");
                string user=cfg.AppSettings.Settings["ReportUser"].Value.ToString();
                string pwd = cfg.AppSettings.Settings["ReportPassword"].Value.ToString();
                string domain = cfg.AppSettings.Settings["ReportDomain"].Value.ToString(); */
                string user = Properties.Settings.Default.ReportUser;
                string pwd = Properties.Settings.Default.ReportPassword;
                string domain = string.Empty;// Properties.Settings.Default.ReportDomain;
                return new NetworkCredential(user, pwd, domain);
            }
        }
        #endregion

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            using (MemoryStream ms = new MemoryStream(b[m_currentPageIndex]))
            {
                ms.Position = 0;
                Metafile pageImage = new Metafile(ms);
                ev.Graphics.DrawImage(pageImage, ev.PageBounds);
                pageImage.Dispose();

                m_currentPageIndex++;
                ev.HasMorePages = m_currentPageIndex < b.Count;
            }
        }

        public void PrintDoc(string url, string path, ReportParameter[] parms, string printer,bool landscape,PaperSize papersize)
        {
            ReportViewer reportViewer1 =  new Microsoft.Reporting.WinForms.ReportViewer();
            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            reportViewer1.ServerReport.ReportPath = path;
            reportViewer1.ServerReport.ReportServerUrl = new System.Uri(url, System.UriKind.Absolute);
            reportViewer1.ServerReport.ReportServerCredentials.NetworkCredentials = Credential;
            if (parms != null)
            {
                reportViewer1.ServerReport.SetParameters(parms);
            }            

            Warning[] warnings;
            string mt;
            string fe;
            string en;
            string[] st;

            b = new List<byte[]>();

            int i = 1;
            while(true)
            {
                StringBuilder deviceInfo = new StringBuilder();
                deviceInfo.Append("<DeviceInfo>");
                deviceInfo.Append("  <OutputFormat>EMF</OutputFormat>");
                deviceInfo.AppendFormat("  <StartPage>{0}</StartPage>", i);
                deviceInfo.Append("</DeviceInfo>");

                byte[] bt = reportViewer1.ServerReport.Render("Image", deviceInfo.ToString(), out mt, out en, out fe, out st, out warnings);
                if (bt.Length > 0)
                {
                    b.Add(bt);
                    i++;
                }
                else
                {
                    break;
                }
            }

            if (b.Count == 0)
            {
                return;
            }

            m_currentPageIndex = 0;
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printer;
            printDoc.DefaultPageSettings.Landscape = landscape;
            printDoc.DefaultPageSettings.PaperSize = papersize;
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();
        }
       
        public void Dispose()
        {
            b = null;
        }  
    }
}
