namespace OCRX.WIN
{
    partial class frmQCWorkRecord
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.QC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContainerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContainerType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 310);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "QC作业记录";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QC,
            this.ContainerNo,
            this.ContainerType,
            this.WorkTime});
            this.dataGridView1.Location = new System.Drawing.Point(7, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(456, 277);
            this.dataGridView1.TabIndex = 0;
            // 
            // QC
            // 
            this.QC.DataPropertyName = "QC";
            this.QC.HeaderText = "QC No";
            this.QC.Name = "QC";
            this.QC.ReadOnly = true;
            this.QC.Width = 45;
            // 
            // ContainerNo
            // 
            this.ContainerNo.DataPropertyName = "ContainerNo";
            this.ContainerNo.HeaderText = "Container No";
            this.ContainerNo.Name = "ContainerNo";
            this.ContainerNo.ReadOnly = true;
            this.ContainerNo.Width = 94;
            // 
            // ContainerType
            // 
            this.ContainerType.DataPropertyName = "ContainerType";
            this.ContainerType.HeaderText = "Container Type";
            this.ContainerType.Name = "ContainerType";
            this.ContainerType.ReadOnly = true;
            this.ContainerType.Width = 105;
            // 
            // WorkTime
            // 
            this.WorkTime.DataPropertyName = "Worktime";
            this.WorkTime.HeaderText = "WorkTime";
            this.WorkTime.Name = "WorkTime";
            this.WorkTime.ReadOnly = true;
            this.WorkTime.Width = 78;
            // 
            // frmQCWorkRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 310);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmQCWorkRecord";
            this.Text = "frmQCWorkRecord";
            this.Load += new System.EventHandler(this.frmQCWorkRecord_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn QC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContainerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContainerType;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkTime;
    }
}