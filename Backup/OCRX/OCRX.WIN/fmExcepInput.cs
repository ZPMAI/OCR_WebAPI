using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OCRX.WIN
{
    public partial class fmExcepInput : Form
    {
        public fmExcepInput()
        {
            InitializeComponent();
        }

        public string reason = string.Empty;

        private void fmExcepInput_Load(object sender, EventArgs e)
        {
            textBox1.Text = reason;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入转异常原因");
                return;
            }

            reason = textBox1.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}