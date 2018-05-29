using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using OCR.Model;

namespace OCR.WIN
{
    public partial class fmLog : Form
    {
        public fmLog()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        public OpLog.T_OCR_LOGDataTable dt;

        private void fmLog_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
        }
    }
}