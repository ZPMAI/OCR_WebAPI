using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

using OCR.BLL;
using OCR.Model;

namespace OCR.AppBack
{
    public delegate void UIDelegate(string msg);

    public partial class Form1 : Form
    {
        public Form1()
        {
            if (Process.GetProcessesByName("OCR.AppBack").Length > 1)
            {
                System.Environment.Exit(0);
                return;
            }

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            delegate1 = UpdateResult;

            tsbStart.PerformClick();

            SendMail("OCR平台实时处理 已开启！");
        }

        //更新主界面处理结果代理

        public UIDelegate delegate1;
        public void UpdateResult(string msg)
        {
            if (this.txtResult.InvokeRequired)
            {
                this.Invoke(delegate1, new object[] { msg });
            }
            else
            {
                if (this.txtResult.Text.Length > 30000)
                {
                    this.txtResult.Clear();
                }
                this.txtResult.Text = this.txtResult.Text.Insert(0, msg);
            }
        }

        //同步数据线程
        private Thread dataThread;
        private bool runDataThread = true;
        private void RunDataThread()
        {
            while (runDataThread)
            {
                IJob job = new Jobs.DataJob(delegate1);
                job.DealJobAsy();
            }
        }

        //同步图片线程
        private Thread photoThread;
        private bool runPhotoThread = true;
        private void RunPhotoThread()
        {
            while (runPhotoThread)
            {
                IJob job = new Jobs.PhotoJob(delegate1);
                job.DealJobAsy();
            }
        }

        //同步车号线程
        private Thread truckThread;
        private bool runTruckThread = true;
        private void RunTruckThread()
        {
            while (runTruckThread)
            {
                IJob job = new Jobs.TruckJob(delegate1);
                job.DealJobAsy();
            }
        }

        //归档线程
        private Thread archiveThread;
        private bool runArchiveThread = true;
        private void RunArchiveThread()
        {
            while (runArchiveThread)
            {
                IJob job = new Jobs.ArchiveJob(delegate1);
                job.DealJobAsy();
            }
        }

        //监控线程
        private Thread monitorThread;
        private bool runMonitorThread = true;
        private void RunMonitorThread()
        {
            while (runMonitorThread)
            {
                IJob job = new Jobs.MonitorJob(delegate1);
                job.DealJobAsy();
            }
        }

        //数据分发线程
        private Thread dispatchThread;
        private bool runDispatchThread = true;
        private void RunDispatchThread()
        {
            while (runDispatchThread)
            {
                IJob job = new Jobs.DispatchJob(delegate1);
                job.DealJobAsy();
            }
        }

        //启动所有进程

        private void StartThreads()
        {
            //RunDataThread();

            runDataThread = true;
            dataThread = new Thread(new ThreadStart(RunDataThread));
            dataThread.Start();

            runPhotoThread = true;
            photoThread = new Thread(new ThreadStart(RunPhotoThread));
            photoThread.Start();

            ////runTruckThread = true;
            ////truckThread = new Thread(new ThreadStart(RunTruckThread)); //用于甄工岸桥预报功能 SCT不启用
            ////truckThread.Start();

            runArchiveThread = true;
            archiveThread = new Thread(new ThreadStart(RunArchiveThread));
            archiveThread.Start();

            runMonitorThread = true;
            monitorThread = new Thread(new ThreadStart(RunMonitorThread));
            monitorThread.Start();


            runDispatchThread = true;
            dispatchThread = new Thread(new ThreadStart(RunDispatchThread));
            dispatchThread.Start();
        }


        //停止所有线程

        private void StopThreads()
        {
            runDataThread = false;
            dataThread.Abort();

            runPhotoThread = false;
            photoThread.Abort();

            //runTruckThread = false;
            //truckThread.Abort();

            runArchiveThread = false;
            archiveThread.Abort();

            runMonitorThread = false;
            monitorThread.Abort();

            runDispatchThread = false;
            dispatchThread.Abort();
        }

        private void tsbStart_Click(object sender, EventArgs e)
        {
            tsbStart.Enabled = false;
            tsbPause.Enabled = true;
            tsbReRun.Enabled = false;
            tsbRunning.Visible = true;

            StartThreads();
        }

        private void tsbPause_Click(object sender, EventArgs e)
        {
            tsbStart.Enabled = true;
            tsbPause.Enabled = false;
            tsbReRun.Enabled = true;
            tsbRunning.Visible = false;

            StopThreads();
        }

        private void tsbJobs_Click(object sender, EventArgs e)
        {
            //Form2 form = new Form2();
            //form.Show();
        }

        private void dATAJOBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IJob job = new Jobs.TruckJob(delegate1);
            job.DealJobAsy();

            //IJob job = new Jobs.DataJob(delegate1);
            //job.DealJobAsy();
            //IJob job1 = new Jobs.PhotoJob(delegate1);
            //job1.DealJobAsy();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("关闭程序将可能导致生产中断，是否继续关闭？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            e.Cancel = result == DialogResult.No;


        }

        private const string MAIL_CLOSED = "OCR平台实时处理 已关闭！岸边装卸作业将受影响！\r\n如果在1分钟内未收到开启邮件，请PING服务器10.1.0.93是否正常。";

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                StopThreads();
            }
            catch (Exception ex)
            {
            }



            SendMail(MAIL_CLOSED);
        }

        private void SendMail(string content)
        {
            try
            {
                //return;
                if (Config.IsDebug)
                {
                    Mails ml = new Mails("zpmai@sctcn.com",
                        "【OCR平台实时处理】 开启关闭 提醒警告",
                        content, null);
                    ml.Send();
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void dISPATCHJOBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IJob job = new Jobs.DispatchJob(delegate1);
            job.DealJobAsy();
        }

        private void autoJobToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tsbReRun_Click(object sender, EventArgs e)
        {

        }
    }
}