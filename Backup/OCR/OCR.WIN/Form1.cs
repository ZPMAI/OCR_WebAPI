using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OCR.WIN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        private void qCSETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmQcSet fm = new fmQcSet();
            fm.MdiParent = this;
            fm.Show();
        }

       
        private void ²éÑ¯Í³¼ÆToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmSearch fm = new fmSearch();
            fm.MdiParent = this;
            fm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}