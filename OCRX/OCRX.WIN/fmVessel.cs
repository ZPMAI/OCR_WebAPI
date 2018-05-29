using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using OCRX.Model;

namespace OCRX.WIN
{
    public partial class fmVessel : Form
    {
        public fmVessel()
        {
            InitializeComponent();
            dg.AutoGenerateColumns = false;
        }

        private void fmVessel_Load(object sender, EventArgs e)
        {
            

            GetData();


        }

        BLL.VesselBLL bll = new OCRX.BLL.VesselBLL();

        private void GetData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                dg.DataSource = bll.SelectVessel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dg.DataSource != null && e.RowIndex >= 0)
            {
                DataSet1.T_OCRX_VESSELRow row = (DataSet1.T_OCRX_VESSELRow)((DataRowView)dg.CurrentRow.DataBoundItem).Row;

                cbbCompanyCode.Text = row.COMPANYCODE;
                cbbService.Text = row.SERVICECODE;
                txtLine.Text = row.LINECODE;

                cbbCompanyCode.Enabled = false;
                cbbService.Enabled = false;
            }
        }

        private void fmVessel_Activated(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                BLL.UsersBLL users = new OCRX.BLL.UsersBLL();
                cbbCompanyCode.DataSource = users.SelectCompanyList();
                cbbService.DataSource = bll.SelectService();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("是否新增？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dr == DialogResult.Yes)
                {

                    Cursor.Current = Cursors.WaitCursor;

                    bll.InsertVessel(cbbCompanyCode.Text, cbbService.Text.Trim(), txtLine.Text.Trim());

                    GetData();
                    MessageBox.Show("新增成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }       
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("是否修改？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dr == DialogResult.Yes)
                {

                    Cursor.Current = Cursors.WaitCursor;

                    bll.UpdateVessel(cbbCompanyCode.Text, cbbService.Text.Trim(), txtLine.Text.Trim());

                    GetData();
                    MessageBox.Show("修改成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }       
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("是否删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dr == DialogResult.Yes)
                {

                    Cursor.Current = Cursors.WaitCursor;

                    bll.DeleteVessel(cbbCompanyCode.Text, cbbService.Text.Trim());

                    GetData();
                    MessageBox.Show("删除成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }       
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbbCompanyCode.Enabled = true;
            cbbService.Enabled = true;

            cbbService.Text = "ALL";
            txtLine.Text = string.Empty;
        }
    }
}