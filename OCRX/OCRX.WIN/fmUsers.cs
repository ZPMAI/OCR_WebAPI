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
    public partial class fmUsers : Form
    {
        public fmUsers()
        {
            InitializeComponent();

            dgUsers.AutoGenerateColumns = false;
            dgCompany.AutoGenerateColumns = false;
        }

        private void fmUsers_Load(object sender, EventArgs e)
        {
            GetCompany();
            GetUsers();
        }

        BLL.UsersBLL bll = new OCRX.BLL.UsersBLL();

        private void GetCompany()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                dgCompany.DataSource = bll.SelectCompany();
                cbbCompanyCode.DataSource = bll.SelectCompanyList();
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

                    bll.InsertCompany(txtCompanyCode.Text.Trim(), txtCompanyName.Text.Trim());

                    GetCompany();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("是否删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dr == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    bll.DeleteCompany(txtCompanyCode.Text.Trim());

                    GetCompany();
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


        private void dgCompany_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgCompany.DataSource != null && e.RowIndex >= 0)
            {
                DataSet1.T_OCRX_CORow row = (DataSet1.T_OCRX_CORow)((DataRowView)dgCompany.CurrentRow.DataBoundItem).Row;

                txtCompanyCode.Text = row.COMPANYCODE;
                txtCompanyName.Text = row.COMPANYNAME;
            }
        }

        private void GetUsers()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                dgUsers.DataSource = bll.SelectUsers();
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

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("是否新增？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dr == DialogResult.Yes)
                {

                    Cursor.Current = Cursors.WaitCursor;

                    bll.InsertUsers(cbbCompanyCode.Text, txtUsers.Text.Trim());

                    GetUsers();
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

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("是否删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dr == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    bll.DeleteUsers(cbbCompanyCode.Text, txtUsers.Text.Trim());

                    GetUsers();
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

        private void dgUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgUsers.DataSource != null && e.RowIndex >= 0)
            {
                DataSet1.T_OCRX_USERSRow row = (DataSet1.T_OCRX_USERSRow)((DataRowView)dgUsers.CurrentRow.DataBoundItem).Row;

                cbbCompanyCode.Text = row.COMPANYCODE;
                txtUsers.Text = row.USERID;
            }
        }
    }
}