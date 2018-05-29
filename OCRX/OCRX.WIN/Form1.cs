using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OCRX.WIN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmUsers fm = new fmUsers();
            fm.MdiParent = this;
            fm.Show();
        }

        private void 班轮ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmVessel fm = new fmVessel();
            fm.MdiParent = this;
            fm.Show();
        }

        private void 驳船ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmBarge fm = new fmBarge();
            fm.MdiParent = this;
            fm.Show();
        }

        private void 装卸作业ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmMain fm = new fmMain();
            fm.MdiParent = this;
            fm.Show();
        }

        private void 报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmSearch fm = new fmSearch();
            fm.MdiParent = this;
            fm.Show();
        }

        private void 监控ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmMonitor fm = new fmMonitor();
            fm.MdiParent = this;
            fm.Show();
        }

        private void 异常处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmExpHandle fm = new fmExpHandle();
            fm.MdiParent = this;
            fm.Show();
        }
    }
}