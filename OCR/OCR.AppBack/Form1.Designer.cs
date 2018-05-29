namespace OCR.AppBack
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。

        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbRunning = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbStart = new System.Windows.Forms.ToolStripButton();
            this.tsbPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbReRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbJobs = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.dATAJOBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dISPATCHJOBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(150, 150);
            this.toolStripContainer1.Location = new System.Drawing.Point(223, 144);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(150, 175);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRunning,
            this.toolStripSeparator2,
            this.tsbStart,
            this.tsbPause,
            this.toolStripSeparator1,
            this.tsbReRun,
            this.toolStripSeparator3,
            this.tsbJobs,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(592, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbRunning
            // 
            this.tsbRunning.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRunning.Image = global::OCR.AppBack.Properties.Resources.loading;
            this.tsbRunning.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRunning.Name = "tsbRunning";
            this.tsbRunning.Size = new System.Drawing.Size(23, 22);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbStart
            // 
            this.tsbStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStart.Image = global::OCR.AppBack.Properties.Resources.start;
            this.tsbStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStart.Name = "tsbStart";
            this.tsbStart.Size = new System.Drawing.Size(23, 22);
            this.tsbStart.Text = "toolStripButton1";
            this.tsbStart.ToolTipText = "运行";
            this.tsbStart.Click += new System.EventHandler(this.tsbStart_Click);
            // 
            // tsbPause
            // 
            this.tsbPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPause.Image = global::OCR.AppBack.Properties.Resources._098;
            this.tsbPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPause.Name = "tsbPause";
            this.tsbPause.Size = new System.Drawing.Size(23, 22);
            this.tsbPause.Text = "toolStripButton2";
            this.tsbPause.ToolTipText = "停止";
            this.tsbPause.Click += new System.EventHandler(this.tsbPause_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbReRun
            // 
            this.tsbReRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbReRun.Image = global::OCR.AppBack.Properties.Resources._245;
            this.tsbReRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReRun.Name = "tsbReRun";
            this.tsbReRun.Size = new System.Drawing.Size(23, 22);
            this.tsbReRun.Text = "toolStripButton3";
            this.tsbReRun.ToolTipText = "补发";
            this.tsbReRun.Click += new System.EventHandler(this.tsbReRun_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbJobs
            // 
            this.tsbJobs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbJobs.Image = global::OCR.AppBack.Properties.Resources._290;
            this.tsbJobs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbJobs.Name = "tsbJobs";
            this.tsbJobs.Size = new System.Drawing.Size(23, 22);
            this.tsbJobs.ToolTipText = "查看所有任务";
            this.tsbJobs.Click += new System.EventHandler(this.tsbJobs_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dATAJOBToolStripMenuItem,
            this.dISPATCHJOBToolStripMenuItem,
            this.autoJobToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // dATAJOBToolStripMenuItem
            // 
            this.dATAJOBToolStripMenuItem.Name = "dATAJOBToolStripMenuItem";
            this.dATAJOBToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dATAJOBToolStripMenuItem.Text = "DATAJOB";
            this.dATAJOBToolStripMenuItem.Click += new System.EventHandler(this.dATAJOBToolStripMenuItem_Click);
            // 
            // dISPATCHJOBToolStripMenuItem
            // 
            this.dISPATCHJOBToolStripMenuItem.Name = "dISPATCHJOBToolStripMenuItem";
            this.dISPATCHJOBToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dISPATCHJOBToolStripMenuItem.Text = "DISPATCHJOB";
            this.dISPATCHJOBToolStripMenuItem.Click += new System.EventHandler(this.dISPATCHJOBToolStripMenuItem_Click);
            // 
            // autoJobToolStripMenuItem
            // 
            this.autoJobToolStripMenuItem.Name = "autoJobToolStripMenuItem";
            this.autoJobToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.autoJobToolStripMenuItem.Text = "AutoJob";
            this.autoJobToolStripMenuItem.Click += new System.EventHandler(this.autoJobToolStripMenuItem_Click);
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Location = new System.Drawing.Point(0, 25);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(592, 448);
            this.txtResult.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 473);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OCR平台实时处理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.ToolStripButton tsbStart;
        private System.Windows.Forms.ToolStripButton tsbPause;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbReRun;
        private System.Windows.Forms.ToolStripButton tsbRunning;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbJobs;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem dATAJOBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dISPATCHJOBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoJobToolStripMenuItem;
    }
}

