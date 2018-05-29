using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Reporting.WinForms;

using System.Net;

namespace OCRX.WIN
{
    public partial class fmMonitor : Form
    {
        public fmMonitor()
        {
            InitializeComponent();

            dgvQC.AutoGenerateColumns = false;
            dataGridView1.AutoGenerateColumns = false;
        }

        OCRX.BLL.MonitorBLL bll = new OCRX.BLL.MonitorBLL();

        //DataTable qcDT = new DataTable();

        private void fmMonitor_Load(object sender, EventArgs e)
        {

            StartThreads();

            //this.reportViewer2.ServerReport.ReportPath = "/报表中心/生产报表/OCR平台/report2";
            //this.reportViewer2.ServerReport.ReportServerUrl = new System.Uri(OCR.Model.Config.ReportingServiceUrl, System.UriKind.Absolute);        

            this.reportViewer2.LocalReport.ReportEmbeddedResource = "OCRX.WIN.RptMonitor.rdlc";

            //NetworkCredential Credential = CCT.SystemFramework.ReportingService.ReportingServiceHelper.Credential;
            //if (Credential != null)
            //{
            //    this.reportViewer2.ServerReport.ReportServerCredentials.NetworkCredentials = Credential;
            //}

            //this.reportViewer2.RefreshReport();
        }      

        //QC线程
        private Thread qcThread;
        private bool runQCThread = true;
        private void RunQCThread()
        {
            while (runQCThread)
            {
                LoadQC();
                Thread.Sleep(10000);
            }
        }       

        //cnt线程
        private Thread cntThread;
        private bool runCntThread = true;
        private void RunCntThread()
        {
            while (runCntThread)
            {
                LoadCnt();
                Thread.Sleep(10000);
            }
        }

        //qc rpt线程
        private Thread qcRptThread;
        private bool runqcRptThread = true;
        private void RunqcRptThread()
        {
            while (runqcRptThread)
            {
                LoadqcRpt();
                Thread.Sleep(30000);
            }
        }

        private delegate void InvokeCallBack();

        //
        private void LoadQC()
        {
            //this.Cursor = Cursors.WaitCursor;
            try
            {
                if (dgvQC.InvokeRequired)
                {
                    InvokeCallBack d = new InvokeCallBack(LoadQC);
                    this.Invoke(d);
                }
                else
                {
                    DataTable dt = bll.SelectQcMonitor(fmMain.CompanyCode);
                    //dt.Columns.Add("LASTTIMEMIN", typeof(string));
                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    dr["LASTTIMEMIN"] = string.Format("{0}{1}", dr["lasttime"].ToString(), "分钟"); 
                    //}
                    dgvQC.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }
        }

        private DateTime CntTime = DateTime.Now;

        private DataTable dtCnt = new DataTable();
        private void LoadCnt()
        {
            //this.Cursor = Cursors.WaitCursor;
            try
            {
                if (this.dataGridView1.InvokeRequired)
                {
                    InvokeCallBack d = new InvokeCallBack(LoadCnt);
                    this.Invoke(d);
                }
                else
                {
                    dtCnt.Dispose();
                    dtCnt = bll.SelectCntMonitor(fmMain.CompanyCode);
                    this.dataGridView1.DataSource = dtCnt;
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        string status = string.Format("{0}-{1}", dataGridView1[3, i].Value.ToString(), dataGridView1[4, i].Value.ToString());
                        switch (status)
                        {
                            case "成功处理-装":
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                                break;
                            case "成功处理-卸":
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.RoyalBlue;
                                break;
                            case "待处理-装":
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.MediumOrchid;
                                break;
                            case "待处理-卸":
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                                break;
                            case "正在处理-装":
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                break;
                            case "正在处理-卸":
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                break;
                            case "转异常-装":
                            case "转异常-卸":
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }
        }

        private void LoadqcRpt()
        {
            try
            {
                if (reportViewer2.InvokeRequired)
                {
                    InvokeCallBack d = new InvokeCallBack(LoadqcRpt);
                    this.Invoke(d);
                }
                else
                {
                    DataTable dt1 = bll.SelectVslMonitor2();
                    DataTable dt2 = bll.SelectQcMonitor2();


                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = "Monitor_T_VESSEL";
                    rds.Value = dt1;

                    ReportDataSource rds2 = new ReportDataSource();
                    rds2.Name = "Monitor_T_QC";
                    rds2.Value = dt2;


                    reportViewer2.LocalReport.DataSources.Clear();
                    reportViewer2.LocalReport.DataSources.Add(rds);
                    reportViewer2.LocalReport.DataSources.Add(rds2);


                    this.reportViewer2.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }

        }

       

        //启动所有进程
        private void StartThreads()
        {
            runQCThread = true;
            qcThread = new Thread(new ThreadStart(RunQCThread));
            qcThread.Start();

            runCntThread = true;
            cntThread = new Thread(new ThreadStart(RunCntThread));
            cntThread.Start();

            runqcRptThread = true;
            qcRptThread = new Thread(new ThreadStart(RunqcRptThread));
            qcRptThread.Start();
        }

        //停止所有线程
        private void StopThreads()
        {
            runQCThread = false;
            qcThread.Abort();

            runCntThread = false;
            cntThread.Abort();

            runqcRptThread = false;
            qcRptThread.Abort();
        }

        private void fmMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void fmMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                reportViewer2.ServerReport.ReportPath = "";
                //reportViewer2.RefreshReport();
            }
            catch (Exception ex)
            {
            }

            try
            {
                StopThreads();
            }
            catch (Exception ex)
            {
            }
        }

        private void reportViewer2_ReportError(object sender, ReportErrorEventArgs e)
        {
            try
            {
                //this.reportViewer2.ServerReport.ReportServerUrl = new System.Uri(OCR.Model.Config.ReportingServiceUrl2, System.UriKind.Absolute);
                //reportViewer2.RefreshReport();
            }
            catch (Exception ex)
            {
            }
            
        }

    }
}