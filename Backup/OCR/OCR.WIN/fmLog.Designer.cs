namespace OCR.WIN
{
    partial class fmLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.COLNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPDATEDBY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPDATETIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OLDVALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NEWVALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.COLNAME,
            this.UPDATEDBY,
            this.UPDATETIME,
            this.OLDVALUE,
            this.NEWVALUE});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(681, 462);
            this.dataGridView1.TabIndex = 0;
            // 
            // COLNAME
            // 
            this.COLNAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.COLNAME.DataPropertyName = "COLNAME";
            this.COLNAME.HeaderText = "操作对象";
            this.COLNAME.Name = "COLNAME";
            this.COLNAME.ReadOnly = true;
            this.COLNAME.Width = 78;
            // 
            // UPDATEDBY
            // 
            this.UPDATEDBY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UPDATEDBY.DataPropertyName = "UPDATEDBY";
            this.UPDATEDBY.HeaderText = "操作人";
            this.UPDATEDBY.Name = "UPDATEDBY";
            this.UPDATEDBY.ReadOnly = true;
            this.UPDATEDBY.Width = 66;
            // 
            // UPDATETIME
            // 
            this.UPDATETIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UPDATETIME.DataPropertyName = "UPDATETIME";
            this.UPDATETIME.HeaderText = "操作时间";
            this.UPDATETIME.Name = "UPDATETIME";
            this.UPDATETIME.ReadOnly = true;
            this.UPDATETIME.Width = 78;
            // 
            // OLDVALUE
            // 
            this.OLDVALUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.OLDVALUE.DataPropertyName = "OLDVALUE";
            this.OLDVALUE.HeaderText = "原值";
            this.OLDVALUE.Name = "OLDVALUE";
            this.OLDVALUE.ReadOnly = true;
            this.OLDVALUE.Width = 54;
            // 
            // NEWVALUE
            // 
            this.NEWVALUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NEWVALUE.DataPropertyName = "NEWVALUE";
            this.NEWVALUE.HeaderText = "新值";
            this.NEWVALUE.Name = "NEWVALUE";
            this.NEWVALUE.ReadOnly = true;
            this.NEWVALUE.Width = 54;
            // 
            // fmLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 462);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.Name = "fmLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "操作日志";
            this.Load += new System.EventHandler(this.fmLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn COLNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPDATEDBY;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPDATETIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn OLDVALUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NEWVALUE;
    }
}