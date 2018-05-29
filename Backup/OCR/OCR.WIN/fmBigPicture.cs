using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OCR.WIN
{
    public partial class fmBigPicture : Form
    {
        public fmBigPicture()
        {
            InitializeComponent();
        }

        public fmBigPicture(string url)
        {
            InitializeComponent();

            pictureBox1.ImageLocation = url;
        }

        public fmBigPicture(List<string> urls, int index)
        {
            InitializeComponent();

            this.urls = urls;
            this.index = index;


            pictureBox1.ImageLocation = urls[index];

            
        }

        List<string> urls = new List<string>();
        int index = 0;

        private void fmBigPicture_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmi1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = saveFileDialog1.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                }
            }
            catch
            {
            }
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (urls.Count > 0)
            {
                if (index > 0)
                {
                    index = index - 1;
                    pictureBox1.ImageLocation = urls[index];
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (urls.Count > 0)
            {
                if (index < 7)
                {
                    index = index + 1;
                    pictureBox1.ImageLocation = urls[index];
                }
            }
        }

        private void fmBigPicture_Load(object sender, EventArgs e)
        {
            pnlBtn.Location = new Point(pnlBtn.Location.X, this.Height / 2 + 10);

            this.Focus();
        }

        private void fmBigPicture_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    MessageBox.Show(e.KeyChar.ToString());
            //}
            //catch { }
        }
    }
}