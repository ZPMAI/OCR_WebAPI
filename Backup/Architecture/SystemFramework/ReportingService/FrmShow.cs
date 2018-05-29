using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Security.Principal;
using System.Net;

namespace CCT.SystemFramework.ReportingService
{
    public partial class FrmShow : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">服务器地址</param>
        /// <param name="path">报表路径</param>
        /// <param name="parms">参数</param>
        /// <param name="showParameterPrompts">是否显示报表参数</param>
        public FrmShow(string url, string path, ReportParameter[] parms, NetworkCredential Credential,  bool showParameterPrompts,string captionText,Form parentForm)
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            if (captionText != string.Empty) this.Text = captionText;
            if (parentForm != null) this.MdiParent = parentForm;
            

            this.reportViewer1.ServerReport.ReportPath = path;
            this.reportViewer1.ServerReport.ReportServerUrl = new System.Uri(url, System.UriKind.Absolute);
            this.reportViewer1.ShowParameterPrompts = showParameterPrompts;

            if (Credential != null)
            {
                this.reportViewer1.ServerReport.ReportServerCredentials.NetworkCredentials = Credential;
            }
           
            if (parms != null)
            {
                this.reportViewer1.ServerReport.SetParameters(parms);
            }
        }       

        private void FrmShow_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_ViewButtonClick(object sender, CancelEventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Print(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.reportViewer1.PrintDialog();

        }
    }
}