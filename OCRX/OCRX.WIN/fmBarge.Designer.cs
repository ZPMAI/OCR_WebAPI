namespace OCRX.WIN
{
    partial class fmBarge
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dg = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbbVessel = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbbInVoyage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbOutVoyage = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblBerthplanno = new System.Windows.Forms.Label();
            this.SHIP_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IN_VOYAGE_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUT_VOYAGE_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblBerthplanno);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.cbbOutVoyage);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbbInVoyage);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cbbVessel);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1048, 50);
            this.panel1.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(807, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(725, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SHIP_CODE,
            this.IN_VOYAGE_CODE,
            this.OUT_VOYAGE_CODE,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg.DefaultCellStyle = dataGridViewCellStyle11;
            this.dg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg.Location = new System.Drawing.Point(0, 50);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dg.RowTemplate.Height = 23;
            this.dg.Size = new System.Drawing.Size(1048, 567);
            this.dg.TabIndex = 9;
            this.dg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellClick);
            // 
            // cbbVessel
            // 
            this.cbbVessel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbVessel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbVessel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbVessel.FormattingEnabled = true;
            this.cbbVessel.Location = new System.Drawing.Point(75, 13);
            this.cbbVessel.Name = "cbbVessel";
            this.cbbVessel.Size = new System.Drawing.Size(121, 20);
            this.cbbVessel.TabIndex = 23;
            this.cbbVessel.SelectedIndexChanged += new System.EventHandler(this.cbbVessel_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "船名代码：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbbInVoyage
            // 
            this.cbbInVoyage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbInVoyage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbInVoyage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbInVoyage.FormattingEnabled = true;
            this.cbbInVoyage.Location = new System.Drawing.Point(274, 12);
            this.cbbInVoyage.Name = "cbbInVoyage";
            this.cbbInVoyage.Size = new System.Drawing.Size(121, 20);
            this.cbbInVoyage.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(203, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "进口航次：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(401, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "出口航次：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbbOutVoyage
            // 
            this.cbbOutVoyage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbOutVoyage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbOutVoyage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbOutVoyage.FormattingEnabled = true;
            this.cbbOutVoyage.Location = new System.Drawing.Point(472, 12);
            this.cbbOutVoyage.Name = "cbbOutVoyage";
            this.cbbOutVoyage.Size = new System.Drawing.Size(121, 20);
            this.cbbOutVoyage.TabIndex = 28;
            this.cbbOutVoyage.SelectedIndexChanged += new System.EventHandler(this.cbbOutVoyage_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(644, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 29;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblBerthplanno
            // 
            this.lblBerthplanno.AutoSize = true;
            this.lblBerthplanno.Location = new System.Drawing.Point(600, 13);
            this.lblBerthplanno.Name = "lblBerthplanno";
            this.lblBerthplanno.Size = new System.Drawing.Size(11, 12);
            this.lblBerthplanno.TabIndex = 30;
            this.lblBerthplanno.Text = "0";
            this.lblBerthplanno.Visible = false;
            // 
            // SHIP_CODE
            // 
            this.SHIP_CODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SHIP_CODE.DataPropertyName = "SHIP_CODE";
            this.SHIP_CODE.HeaderText = "船名代码";
            this.SHIP_CODE.Name = "SHIP_CODE";
            this.SHIP_CODE.ReadOnly = true;
            this.SHIP_CODE.Width = 78;
            // 
            // IN_VOYAGE_CODE
            // 
            this.IN_VOYAGE_CODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.IN_VOYAGE_CODE.DataPropertyName = "IN_VOYAGE_CODE";
            this.IN_VOYAGE_CODE.HeaderText = "进口航次";
            this.IN_VOYAGE_CODE.Name = "IN_VOYAGE_CODE";
            this.IN_VOYAGE_CODE.ReadOnly = true;
            this.IN_VOYAGE_CODE.Width = 78;
            // 
            // OUT_VOYAGE_CODE
            // 
            this.OUT_VOYAGE_CODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.OUT_VOYAGE_CODE.DataPropertyName = "OUT_VOYAGE_CODE";
            this.OUT_VOYAGE_CODE.HeaderText = "出口航次";
            this.OUT_VOYAGE_CODE.Name = "OUT_VOYAGE_CODE";
            this.OUT_VOYAGE_CODE.ReadOnly = true;
            this.OUT_VOYAGE_CODE.Width = 78;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "CREATEDBY";
            this.dataGridViewTextBoxColumn3.HeaderText = "创建人";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 66;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "CREATETIME";
            this.dataGridViewTextBoxColumn4.HeaderText = "创建时间";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 78;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "UPDATEDBY";
            this.dataGridViewTextBoxColumn5.HeaderText = "更新人";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 66;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "UPDATETIME";
            this.dataGridViewTextBoxColumn6.HeaderText = "更新时间";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 78;
            // 
            // fmBarge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 617);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "fmBarge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "驳船分发规则管理";
            this.Load += new System.EventHandler(this.fmVessel_Load);
            this.Activated += new System.EventHandler(this.fmVessel_Activated);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cbbVessel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbInVoyage;
        private System.Windows.Forms.ComboBox cbbOutVoyage;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblBerthplanno;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHIP_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn IN_VOYAGE_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUT_VOYAGE_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}