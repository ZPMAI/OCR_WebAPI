namespace CCT.MainFrame
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.xtraTabbedMdiManager = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslEnvironment = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslbUsername = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslUsername = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.dockPanel.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "folder_close.gif");
            this.imageList.Images.SetKeyName(1, "folder_open.gif");
            this.imageList.Images.SetKeyName(2, "FILE.GIF");
            this.imageList.Images.SetKeyName(3, "XLW16.GIF");
            this.imageList.Images.SetKeyName(4, "DOC16.GIF");
            // 
            // xtraTabbedMdiManager
            // 
            this.xtraTabbedMdiManager.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xtraTabbedMdiManager.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabbedMdiManager.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeader;
            this.xtraTabbedMdiManager.MdiParent = this;
            // 
            // dockManager
            // 
            this.dockManager.Form = this;
            this.dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel});
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "System.Windows.Forms.ToolStrip",
            "System.Windows.Forms.StatusStrip"});
            // 
            // dockPanel
            // 
            this.dockPanel.Controls.Add(this.dockPanel1_Container);
            this.dockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel.FloatSize = new System.Drawing.Size(200, 285);
            this.dockPanel.ID = new System.Guid("7831aaa0-8c21-4af7-a110-37aea7e009be");
            this.dockPanel.Location = new System.Drawing.Point(0, 25);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Options.AllowDockBottom = false;
            this.dockPanel.Options.AllowDockTop = false;
            this.dockPanel.Options.ShowCloseButton = false;
            this.dockPanel.Size = new System.Drawing.Size(200, 594);
            this.dockPanel.Text = "导航菜单";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.treeView);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(194, 566);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(194, 566);
            this.treeView.TabIndex = 1;
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslEnvironment,
            this.tsslbUsername,
            this.tsslUsername});
            this.statusStrip.Location = new System.Drawing.Point(0, 619);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(892, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsslEnvironment
            // 
            this.tsslEnvironment.Name = "tsslEnvironment";
            this.tsslEnvironment.Size = new System.Drawing.Size(53, 17);
            this.tsslEnvironment.Text = "正式环境";
            // 
            // tsslbUsername
            // 
            this.tsslbUsername.Name = "tsslbUsername";
            this.tsslbUsername.Size = new System.Drawing.Size(53, 17);
            this.tsslbUsername.Text = "用户名：";
            // 
            // tsslUsername
            // 
            this.tsslUsername.Name = "tsslUsername";
            this.tsslUsername.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip
            // 
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(892, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip1";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(892, 641);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "业务管理系统";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.dockPanel.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager;
        private System.Windows.Forms.ImageList imageList;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private System.Windows.Forms.TreeView treeView;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsslbUsername;
        private System.Windows.Forms.ToolStripStatusLabel tsslUsername;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsslEnvironment;
        


    }
}

