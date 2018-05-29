namespace OCRX.WIN
{
    partial class fmDmgReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvDmgInfo = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDmgImport = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDmgInfoEx = new System.Windows.Forms.DataGridView();
            this.RowNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DockId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContainerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DmgCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DmgMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DmgSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DmgSizeEX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnExportX = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDmgInfo)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDmgInfoEx)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(587, 539);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.dgvDmgInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(579, 513);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "岸边助理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(573, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(89, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(164, 21);
            this.textBox1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "岸边助理发现残损";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Vessel Code:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(259, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "查  询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvDmgInfo
            // 
            this.dgvDmgInfo.AllowUserToAddRows = false;
            this.dgvDmgInfo.AllowUserToDeleteRows = false;
            this.dgvDmgInfo.AllowUserToOrderColumns = true;
            this.dgvDmgInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDmgInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDmgInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RowNum,
            this.DockId,
            this.ContainerNo,
            this.DmgCode,
            this.DmgMemo,
            this.DmgSize});
            this.dgvDmgInfo.Location = new System.Drawing.Point(-5, 106);
            this.dgvDmgInfo.Name = "dgvDmgInfo";
            this.dgvDmgInfo.ReadOnly = true;
            this.dgvDmgInfo.RowTemplate.Height = 23;
            this.dgvDmgInfo.Size = new System.Drawing.Size(576, 401);
            this.dgvDmgInfo.TabIndex = 0;
            this.dgvDmgInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDmgInfo_CellContentClick);
            this.dgvDmgInfo.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_RowPostPaint);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.dgvDmgInfoEx);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(579, 513);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "外理";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExportX);
            this.groupBox2.Controls.Add(this.btnDmgImport);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 100);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // btnDmgImport
            // 
            this.btnDmgImport.Location = new System.Drawing.Point(340, 14);
            this.btnDmgImport.Name = "btnDmgImport";
            this.btnDmgImport.Size = new System.Drawing.Size(75, 23);
            this.btnDmgImport.TabIndex = 9;
            this.btnDmgImport.Text = "导  入";
            this.btnDmgImport.UseVisualStyleBackColor = true;
            this.btnDmgImport.Click += new System.EventHandler(this.btnDmgImport_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(259, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "查  询";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(89, 14);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(164, 21);
            this.textBox2.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(309, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "外理发现残损";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Vessel Code:";
            // 
            // dgvDmgInfoEx
            // 
            this.dgvDmgInfoEx.AllowUserToAddRows = false;
            this.dgvDmgInfoEx.AllowUserToDeleteRows = false;
            this.dgvDmgInfoEx.AllowUserToOrderColumns = true;
            this.dgvDmgInfoEx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDmgInfoEx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDmgInfoEx.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.DmgSizeEX});
            this.dgvDmgInfoEx.Location = new System.Drawing.Point(3, 109);
            this.dgvDmgInfoEx.Name = "dgvDmgInfoEx";
            this.dgvDmgInfoEx.ReadOnly = true;
            this.dgvDmgInfoEx.RowTemplate.Height = 23;
            this.dgvDmgInfoEx.Size = new System.Drawing.Size(573, 401);
            this.dgvDmgInfoEx.TabIndex = 9;
            this.dgvDmgInfoEx.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDmgInfoEx_CellContentClick);
            this.dgvDmgInfoEx.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_RowPostPaint);
            // 
            // RowNum
            // 
            this.RowNum.HeaderText = "序号";
            this.RowNum.Name = "RowNum";
            this.RowNum.ReadOnly = true;
            // 
            // DockId
            // 
            this.DockId.DataPropertyName = "dock_id";
            this.DockId.HeaderText = "DockId";
            this.DockId.Name = "DockId";
            this.DockId.ReadOnly = true;
            this.DockId.Visible = false;
            // 
            // ContainerNo
            // 
            this.ContainerNo.DataPropertyName = "Container_No";
            this.ContainerNo.HeaderText = "箱号";
            this.ContainerNo.Name = "ContainerNo";
            this.ContainerNo.ReadOnly = true;
            // 
            // DmgCode
            // 
            this.DmgCode.DataPropertyName = "DmgCode";
            this.DmgCode.HeaderText = "残损代码";
            this.DmgCode.Name = "DmgCode";
            this.DmgCode.ReadOnly = true;
            // 
            // DmgMemo
            // 
            this.DmgMemo.DataPropertyName = "DmgMemo";
            this.DmgMemo.HeaderText = "残损名称";
            this.DmgMemo.Name = "DmgMemo";
            this.DmgMemo.ReadOnly = true;
            // 
            // DmgSize
            // 
            this.DmgSize.HeaderText = "残损尺寸";
            this.DmgSize.Name = "DmgSize";
            this.DmgSize.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "序号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "dock_id";
            this.dataGridViewTextBoxColumn2.HeaderText = "DockId";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Container_No";
            this.dataGridViewTextBoxColumn3.HeaderText = "箱号";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "DmgCode";
            this.dataGridViewTextBoxColumn4.HeaderText = "残损代码";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "DmgMemo";
            this.dataGridViewTextBoxColumn5.HeaderText = "残损名称";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // DmgSizeEX
            // 
            this.DmgSizeEX.DataPropertyName = "DMGSIZE";
            this.DmgSizeEX.HeaderText = "残损尺寸";
            this.DmgSizeEX.Name = "DmgSizeEX";
            this.DmgSizeEX.ReadOnly = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(349, 14);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnExportX
            // 
            this.btnExportX.Location = new System.Drawing.Point(421, 14);
            this.btnExportX.Name = "btnExportX";
            this.btnExportX.Size = new System.Drawing.Size(75, 23);
            this.btnExportX.TabIndex = 10;
            this.btnExportX.Text = "导出";
            this.btnExportX.UseVisualStyleBackColor = true;
            // 
            // fmDmgReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 539);
            this.Controls.Add(this.tabControl1);
            this.Name = "fmDmgReport";
            this.Text = "残损记录";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDmgInfo)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDmgInfoEx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDmgImport;
        private System.Windows.Forms.DataGridView dgvDmgInfo;
        private System.Windows.Forms.DataGridView dgvDmgInfoEx;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn DockId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContainerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DmgCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DmgMemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DmgSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn DmgSizeEX;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnExportX;
    }
}