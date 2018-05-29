namespace OCRX.WIN
{
    partial class fmDamageInfo
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dockid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContainerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RCONTAINERNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DMGCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DMGMEMO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DMGSIZE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dockid,
            this.ContainerNo,
            this.RCONTAINERNO,
            this.DMGCODE,
            this.DMGMEMO,
            this.DMGSIZE});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(528, 370);
            this.dataGridView1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(534, 390);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "残损记录";
            // 
            // dockid
            // 
            this.dockid.DataPropertyName = "dock_id";
            this.dockid.HeaderText = "记录id";
            this.dockid.Name = "dockid";
            this.dockid.ReadOnly = true;
            this.dockid.Width = 66;
            // 
            // ContainerNo
            // 
            this.ContainerNo.DataPropertyName = "CONTAINER_NO";
            this.ContainerNo.HeaderText = "箱号";
            this.ContainerNo.Name = "ContainerNo";
            this.ContainerNo.ReadOnly = true;
            this.ContainerNo.Width = 54;
            // 
            // RCONTAINERNO
            // 
            this.RCONTAINERNO.DataPropertyName = "RCONTAINER_NO";
            this.RCONTAINERNO.HeaderText = "实际箱号";
            this.RCONTAINERNO.Name = "RCONTAINERNO";
            this.RCONTAINERNO.Width = 78;
            // 
            // DMGCODE
            // 
            this.DMGCODE.DataPropertyName = "DMGCODE";
            this.DMGCODE.HeaderText = "残损代码";
            this.DMGCODE.Name = "DMGCODE";
            this.DMGCODE.ReadOnly = true;
            this.DMGCODE.Width = 78;
            // 
            // DMGMEMO
            // 
            this.DMGMEMO.DataPropertyName = "DMGMEMO";
            this.DMGMEMO.HeaderText = "残损描述";
            this.DMGMEMO.Name = "DMGMEMO";
            this.DMGMEMO.ReadOnly = true;
            this.DMGMEMO.Width = 78;
            // 
            // DMGSIZE
            // 
            this.DMGSIZE.DataPropertyName = "DMGSIZE";
            this.DMGSIZE.HeaderText = "残损尺寸";
            this.DMGSIZE.Name = "DMGSIZE";
            this.DMGSIZE.ReadOnly = true;
            this.DMGSIZE.Width = 78;
            // 
            // fmDamageInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 390);
            this.Controls.Add(this.groupBox1);
            this.Name = "fmDamageInfo";
            this.Text = "fmDamageInfo";
            this.Load += new System.EventHandler(this.fmDamageInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dockid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContainerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RCONTAINERNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DMGCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DMGMEMO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DMGSIZE;
    }
}