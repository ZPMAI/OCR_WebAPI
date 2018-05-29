namespace OCRX.WIN
{
    partial class fmDamageInput
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
            this.gbDamageInfo = new System.Windows.Forms.GroupBox();
            this.cmbDmgPart = new System.Windows.Forms.ComboBox();
            this.lblDmgPart = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddDmg = new System.Windows.Forms.Button();
            this.txtDmgSize = new System.Windows.Forms.TextBox();
            this.lblDmgSize = new System.Windows.Forms.Label();
            this.cmbDmgType = new System.Windows.Forms.ComboBox();
            this.lblDmgType = new System.Windows.Forms.Label();
            this.cmbDmgPosition = new System.Windows.Forms.ComboBox();
            this.lblDmgPosition = new System.Windows.Forms.Label();
            this.gbDamageInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDamageInfo
            // 
            this.gbDamageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDamageInfo.Controls.Add(this.cmbDmgPart);
            this.gbDamageInfo.Controls.Add(this.btnCancel);
            this.gbDamageInfo.Controls.Add(this.lblDmgPart);
            this.gbDamageInfo.Controls.Add(this.btnAddDmg);
            this.gbDamageInfo.Controls.Add(this.txtDmgSize);
            this.gbDamageInfo.Controls.Add(this.lblDmgSize);
            this.gbDamageInfo.Controls.Add(this.cmbDmgType);
            this.gbDamageInfo.Controls.Add(this.lblDmgType);
            this.gbDamageInfo.Controls.Add(this.cmbDmgPosition);
            this.gbDamageInfo.Controls.Add(this.lblDmgPosition);
            this.gbDamageInfo.Location = new System.Drawing.Point(12, 13);
            this.gbDamageInfo.Name = "gbDamageInfo";
            this.gbDamageInfo.Size = new System.Drawing.Size(214, 205);
            this.gbDamageInfo.TabIndex = 0;
            this.gbDamageInfo.TabStop = false;
            // 
            // cmbDmgPart
            // 
            this.cmbDmgPart.FormattingEnabled = true;
            this.cmbDmgPart.Items.AddRange(new object[] {
            "前",
            "后",
            "左",
            "右",
            "左前",
            "左后",
            "右前",
            "右后"});
            this.cmbDmgPart.Location = new System.Drawing.Point(72, 18);
            this.cmbDmgPart.Name = "cmbDmgPart";
            this.cmbDmgPart.Size = new System.Drawing.Size(117, 20);
            this.cmbDmgPart.TabIndex = 8;
            // 
            // lblDmgPart
            // 
            this.lblDmgPart.AutoSize = true;
            this.lblDmgPart.Location = new System.Drawing.Point(7, 21);
            this.lblDmgPart.Name = "lblDmgPart";
            this.lblDmgPart.Size = new System.Drawing.Size(59, 12);
            this.lblDmgPart.TabIndex = 7;
            this.lblDmgPart.Text = "残损方位:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(9, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddDmg
            // 
            this.btnAddDmg.Location = new System.Drawing.Point(114, 168);
            this.btnAddDmg.Name = "btnAddDmg";
            this.btnAddDmg.Size = new System.Drawing.Size(75, 23);
            this.btnAddDmg.TabIndex = 6;
            this.btnAddDmg.Text = "增加";
            this.btnAddDmg.UseVisualStyleBackColor = true;
            this.btnAddDmg.Click += new System.EventHandler(this.btnAddDmg_Click);
            // 
            // txtDmgSize
            // 
            this.txtDmgSize.Location = new System.Drawing.Point(72, 106);
            this.txtDmgSize.Multiline = true;
            this.txtDmgSize.Name = "txtDmgSize";
            this.txtDmgSize.Size = new System.Drawing.Size(117, 47);
            this.txtDmgSize.TabIndex = 5;
            // 
            // lblDmgSize
            // 
            this.lblDmgSize.AutoSize = true;
            this.lblDmgSize.Location = new System.Drawing.Point(7, 111);
            this.lblDmgSize.Name = "lblDmgSize";
            this.lblDmgSize.Size = new System.Drawing.Size(59, 12);
            this.lblDmgSize.TabIndex = 4;
            this.lblDmgSize.Text = "残损尺寸:";
            // 
            // cmbDmgType
            // 
            this.cmbDmgType.FormattingEnabled = true;
            this.cmbDmgType.Location = new System.Drawing.Point(72, 75);
            this.cmbDmgType.Name = "cmbDmgType";
            this.cmbDmgType.Size = new System.Drawing.Size(117, 20);
            this.cmbDmgType.TabIndex = 3;
            this.cmbDmgType.SelectedIndexChanged += new System.EventHandler(this.cmbDmgType_SelectedIndexChanged);
            // 
            // lblDmgType
            // 
            this.lblDmgType.AutoSize = true;
            this.lblDmgType.Location = new System.Drawing.Point(7, 80);
            this.lblDmgType.Name = "lblDmgType";
            this.lblDmgType.Size = new System.Drawing.Size(59, 12);
            this.lblDmgType.TabIndex = 2;
            this.lblDmgType.Text = "残损类型:";
            // 
            // cmbDmgPosition
            // 
            this.cmbDmgPosition.FormattingEnabled = true;
            this.cmbDmgPosition.Location = new System.Drawing.Point(72, 45);
            this.cmbDmgPosition.Name = "cmbDmgPosition";
            this.cmbDmgPosition.Size = new System.Drawing.Size(117, 20);
            this.cmbDmgPosition.TabIndex = 1;
            this.cmbDmgPosition.SelectedIndexChanged += new System.EventHandler(this.cmbDmgPosition_SelectedIndexChanged);
            // 
            // lblDmgPosition
            // 
            this.lblDmgPosition.AutoSize = true;
            this.lblDmgPosition.Location = new System.Drawing.Point(7, 51);
            this.lblDmgPosition.Name = "lblDmgPosition";
            this.lblDmgPosition.Size = new System.Drawing.Size(59, 12);
            this.lblDmgPosition.TabIndex = 0;
            this.lblDmgPosition.Text = "残损位置:";
            // 
            // fmDamageInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(238, 230);
            this.Controls.Add(this.gbDamageInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "fmDamageInput";
            this.Text = "frmDamageInput";
            this.Load += new System.EventHandler(this.fmDamageInput_Load);
            this.gbDamageInfo.ResumeLayout(false);
            this.gbDamageInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDamageInfo;
        private System.Windows.Forms.TextBox txtDmgSize;
        private System.Windows.Forms.Label lblDmgSize;
        private System.Windows.Forms.ComboBox cmbDmgType;
        private System.Windows.Forms.Label lblDmgType;
        private System.Windows.Forms.ComboBox cmbDmgPosition;
        private System.Windows.Forms.Label lblDmgPosition;
        private System.Windows.Forms.Button btnAddDmg;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbDmgPart;
        private System.Windows.Forms.Label lblDmgPart;
    }
}