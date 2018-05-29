using Microsoft.Reporting.WinForms;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace OCRX.WIN
{
    public partial class FrmDischargeListReport : Form
    {
        DataTable datasource = null;
        string vslaliase = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">服务器地址</param>
        /// <param name="path">报表路径</param>
        /// <param name="parms">参数</param>
        /// <param name="showParameterPrompts">是否显示报表参数</param>
        public FrmDischargeListReport(string vslaliase,string formText,DataTable datasource)
        {
            InitializeComponent();

            this.datasource = datasource;
            this.WindowState = FormWindowState.Maximized;
            this.Text = formText;
            this.vslaliase = vslaliase;
            this.reportViewer1.LocalReport.DisplayName = "DischargeList-" + vslaliase;
        }       

        private void FrmShow_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "OCRX.WIN.RDLC.DischargingList.rdlc";
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = @"OCRX.WIN.DischargeList.rdlc";
            ReportDataSource rdsLL1 = new ReportDataSource();
            rdsLL1.Name = "ReportDataSet_P_SCT_DischargeList";
            rdsLL1.Value = datasource;
            this.reportViewer1.LocalReport.DataSources.Add(rdsLL1);

            ReportParameter para1 = new ReportParameter("p_vslaliase", vslaliase);
            ReportParameter para2= new ReportParameter("p_user", CCT.Common.Userinfo.Username);
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { para1,para2 });


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