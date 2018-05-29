namespace OCRX.WIN
{
    partial class fmMonitor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvQC = new System.Windows.Forms.DataGridView();
            this.QCNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecordLeft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXCEP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.copytime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trval_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operatorname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dock_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.container_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.starttime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.end_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnLoad = new System.Windows.Forms.Button();
            this.dgvLoad = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CARRYDEVICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carrytime1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvQC);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 862);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "桥吊监控";
            // 
            // dgvQC
            // 
            this.dgvQC.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvQC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvQC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QCNO,
            this.RecordLeft,
            this.EXCEP});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvQC.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvQC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQC.Location = new System.Drawing.Point(3, 17);
            this.dgvQC.Name = "dgvQC";
            this.dgvQC.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQC.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvQC.RowTemplate.Height = 23;
            this.dgvQC.Size = new System.Drawing.Size(332, 842);
            this.dgvQC.TabIndex = 0;
            this.toolTip1.SetToolTip(this.dgvQC, "ddddddd");
            // 
            // QCNO
            // 
            this.QCNO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.QCNO.DataPropertyName = "QCNO";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.QCNO.DefaultCellStyle = dataGridViewCellStyle2;
            this.QCNO.HeaderText = "桥吊";
            this.QCNO.Name = "QCNO";
            this.QCNO.ReadOnly = true;
            this.QCNO.Width = 54;
            // 
            // RecordLeft
            // 
            this.RecordLeft.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RecordLeft.DataPropertyName = "RecordLeft";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RecordLeft.DefaultCellStyle = dataGridViewCellStyle3;
            this.RecordLeft.HeaderText = "待处理";
            this.RecordLeft.Name = "RecordLeft";
            this.RecordLeft.ReadOnly = true;
            this.RecordLeft.Width = 66;
            // 
            // EXCEP
            // 
            this.EXCEP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.EXCEP.DataPropertyName = "EXCEP";
            this.EXCEP.HeaderText = "异常";
            this.EXCEP.Name = "EXCEP";
            this.EXCEP.ReadOnly = true;
            this.EXCEP.Width = 54;
            // 
            // reportViewer2
            // 
            this.reportViewer2.Dock = System.Windows.Forms.DockStyle.Top;
            this.reportViewer2.Location = new System.Drawing.Point(338, 0);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.ShowToolBar = false;
            this.reportViewer2.Size = new System.Drawing.Size(926, 319);
            this.reportViewer2.TabIndex = 4;
            this.reportViewer2.ReportError += new Microsoft.Reporting.WinForms.ReportErrorEventHandler(this.reportViewer2_ReportError);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.copytime,
            this.trval_no,
            this.operatorname,
            this.cstatus,
            this.dock_status,
            this.container_no,
            this.starttime,
            this.end_time});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(338, 319);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(926, 543);
            this.dataGridView1.TabIndex = 5;
            // 
            // copytime
            // 
            this.copytime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.copytime.DataPropertyName = "copytime";
            dataGridViewCellStyle7.Format = "G";
            this.copytime.DefaultCellStyle = dataGridViewCellStyle7;
            this.copytime.HeaderText = "识别时间";
            this.copytime.Name = "copytime";
            this.copytime.ReadOnly = true;
            this.copytime.Width = 78;
            // 
            // trval_no
            // 
            this.trval_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.trval_no.DataPropertyName = "trval_no";
            this.trval_no.HeaderText = "桥号";
            this.trval_no.Name = "trval_no";
            this.trval_no.ReadOnly = true;
            this.trval_no.Width = 54;
            // 
            // operatorname
            // 
            this.operatorname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.operatorname.DataPropertyName = "operatorname";
            this.operatorname.HeaderText = "理货员";
            this.operatorname.Name = "operatorname";
            this.operatorname.ReadOnly = true;
            this.operatorname.Width = 66;
            // 
            // cstatus
            // 
            this.cstatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cstatus.DataPropertyName = "cstatus";
            this.cstatus.HeaderText = "处理状态";
            this.cstatus.Name = "cstatus";
            this.cstatus.ReadOnly = true;
            this.cstatus.Width = 78;
            // 
            // dock_status
            // 
            this.dock_status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dock_status.DataPropertyName = "dock_status";
            this.dock_status.HeaderText = "装/卸";
            this.dock_status.Name = "dock_status";
            this.dock_status.ReadOnly = true;
            this.dock_status.Width = 60;
            // 
            // container_no
            // 
            this.container_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.container_no.DataPropertyName = "container_no";
            this.container_no.HeaderText = "箱号";
            this.container_no.Name = "container_no";
            this.container_no.ReadOnly = true;
            this.container_no.Width = 54;
            // 
            // starttime
            // 
            this.starttime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.starttime.DataPropertyName = "starttime";
            dataGridViewCellStyle8.Format = "G";
            this.starttime.DefaultCellStyle = dataGridViewCellStyle8;
            this.starttime.HeaderText = "开始处理时间";
            this.starttime.Name = "starttime";
            this.starttime.ReadOnly = true;
            this.starttime.Width = 102;
            // 
            // end_time
            // 
            this.end_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.end_time.DataPropertyName = "end_time";
            dataGridViewCellStyle9.Format = "G";
            this.end_time.DefaultCellStyle = dataGridViewCellStyle9;
            this.end_time.HeaderText = "处理结束时间";
            this.end_time.Name = "end_time";
            this.end_time.ReadOnly = true;
            this.end_time.Width = 102;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(0, 0);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 0;
            // 
            // dgvLoad
            // 
            this.dgvLoad.Location = new System.Drawing.Point(0, 0);
            this.dgvLoad.Name = "dgvLoad";
            this.dgvLoad.RowTemplate.Height = 23;
            this.dgvLoad.Size = new System.Drawing.Size(240, 150);
            this.dgvLoad.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "trval_no";
            this.dataGridViewTextBoxColumn2.HeaderText = "桥号";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "container_no";
            this.dataGridViewTextBoxColumn6.HeaderText = "箱号";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // CARRYDEVICE
            // 
            this.CARRYDEVICE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CARRYDEVICE.DataPropertyName = "CARRYDEVICE";
            this.CARRYDEVICE.HeaderText = "车号";
            this.CARRYDEVICE.Name = "CARRYDEVICE";
            this.CARRYDEVICE.ReadOnly = true;
            // 
            // carrytime1
            // 
            this.carrytime1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.carrytime1.DataPropertyName = "carrytime";
            this.carrytime1.HeaderText = "压车时间(分)";
            this.carrytime1.Name = "carrytime1";
            this.carrytime1.ReadOnly = true;
            // 
            // fmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 862);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.reportViewer2);
            this.Controls.Add(this.groupBox1);
            this.Name = "fmMonitor";
            this.Text = "实时监控";
            this.Load += new System.EventHandler(this.fmMonitor_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fmMonitor_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmMonitor_FormClosing);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoad)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvQC;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridView dgvLoad;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn CARRYDEVICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn carrytime1;
        private System.Windows.Forms.DataGridViewTextBoxColumn copytime;
        private System.Windows.Forms.DataGridViewTextBoxColumn trval_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn operatorname;
        private System.Windows.Forms.DataGridViewTextBoxColumn cstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dock_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn container_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn starttime;
        private System.Windows.Forms.DataGridViewTextBoxColumn end_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordLeft;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXCEP;
    }
}