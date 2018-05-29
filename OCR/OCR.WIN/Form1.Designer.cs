namespace OCR.WIN
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.qCSETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.qCSETToolStripMenuItem,
            this.查询统计ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(768, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // qCSETToolStripMenuItem
            // 
            this.qCSETToolStripMenuItem.Name = "qCSETToolStripMenuItem";
            this.qCSETToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.qCSETToolStripMenuItem.Text = "桥吊作业配置";
            this.qCSETToolStripMenuItem.Click += new System.EventHandler(this.qCSETToolStripMenuItem_Click);
            // 
            // 查询统计ToolStripMenuItem
            // 
            this.查询统计ToolStripMenuItem.Name = "查询统计ToolStripMenuItem";
            this.查询统计ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.查询统计ToolStripMenuItem.Text = "查询统计";
            this.查询统计ToolStripMenuItem.Click += new System.EventHandler(this.查询统计ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 411);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "OCR平台";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem qCSETToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询统计ToolStripMenuItem;
    }
}

