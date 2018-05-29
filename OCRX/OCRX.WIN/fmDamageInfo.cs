using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OCRX.WIN
{
    public partial class fmDamageInfo : Form
    {
        public fmDamageInfo()
        {
            InitializeComponent();
        }

        public DataTable records = null;
        public delegate void UpdateDGVCallback(object ds);
        private void fmDamageInfo_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = records;
        }

        public void UpdateDGV(object ds)
        {
            if (this.dataGridView1.InvokeRequired)
            {
                UpdateDGVCallback d = new UpdateDGVCallback(UpdateDGV);
                this.Invoke(d, new object[] { ds });
            }
            else
            {
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = ds;
            }
        }
    }
}
