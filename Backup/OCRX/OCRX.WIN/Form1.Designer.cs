namespace OCRX.WIN
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
            this.用户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据分发规则管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.班轮ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.驳船ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.装卸作业ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.监控ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.异常处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.用户管理ToolStripMenuItem,
            this.数据分发规则管理ToolStripMenuItem,
            this.装卸作业ToolStripMenuItem,
            this.报表ToolStripMenuItem,
            this.监控ToolStripMenuItem,
            this.异常处理ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1136, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 用户管理ToolStripMenuItem
            // 
            this.用户管理ToolStripMenuItem.Name = "用户管理ToolStripMenuItem";
            this.用户管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.用户管理ToolStripMenuItem.Text = "用户管理";
            this.用户管理ToolStripMenuItem.Click += new System.EventHandler(this.用户管理ToolStripMenuItem_Click);
            // 
            // 数据分发规则管理ToolStripMenuItem
            // 
            this.数据分发规则管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.班轮ToolStripMenuItem,
            this.驳船ToolStripMenuItem});
            this.数据分发规则管理ToolStripMenuItem.Name = "数据分发规则管理ToolStripMenuItem";
            this.数据分发规则管理ToolStripMenuItem.Size = new System.Drawing.Size(116, 21);
            this.数据分发规则管理ToolStripMenuItem.Text = "数据分发规则管理";
            // 
            // 班轮ToolStripMenuItem
            // 
            this.班轮ToolStripMenuItem.Name = "班轮ToolStripMenuItem";
            this.班轮ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.班轮ToolStripMenuItem.Text = "班轮";
            this.班轮ToolStripMenuItem.Click += new System.EventHandler(this.班轮ToolStripMenuItem_Click);
            // 
            // 驳船ToolStripMenuItem
            // 
            this.驳船ToolStripMenuItem.Name = "驳船ToolStripMenuItem";
            this.驳船ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.驳船ToolStripMenuItem.Text = "驳船";
            this.驳船ToolStripMenuItem.Click += new System.EventHandler(this.驳船ToolStripMenuItem_Click);
            // 
            // 装卸作业ToolStripMenuItem
            // 
            this.装卸作业ToolStripMenuItem.Name = "装卸作业ToolStripMenuItem";
            this.装卸作业ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.装卸作业ToolStripMenuItem.Text = "装卸作业";
            this.装卸作业ToolStripMenuItem.Click += new System.EventHandler(this.装卸作业ToolStripMenuItem_Click);
            // 
            // 报表ToolStripMenuItem
            // 
            this.报表ToolStripMenuItem.Name = "报表ToolStripMenuItem";
            this.报表ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.报表ToolStripMenuItem.Text = "报表";
            this.报表ToolStripMenuItem.Click += new System.EventHandler(this.报表ToolStripMenuItem_Click);
            // 
            // 监控ToolStripMenuItem
            // 
            this.监控ToolStripMenuItem.Name = "监控ToolStripMenuItem";
            this.监控ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.监控ToolStripMenuItem.Text = "监控";
            this.监控ToolStripMenuItem.Click += new System.EventHandler(this.监控ToolStripMenuItem_Click);
            // 
            // 异常处理ToolStripMenuItem
            // 
            this.异常处理ToolStripMenuItem.Name = "异常处理ToolStripMenuItem";
            this.异常处理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.异常处理ToolStripMenuItem.Text = "异常处理";
            this.异常处理ToolStripMenuItem.Click += new System.EventHandler(this.异常处理ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 262);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 用户管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据分发规则管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 装卸作业ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 监控ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 班轮ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 驳船ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 异常处理ToolStripMenuItem;
    }
}

