using OCR.Model;
using OCRX.BLL;
using OCRX.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OCRX.WIN
{
    public partial class fmDmgReport : Form
    {
        MainBLL bll;

        //识别记录
        DataSet1.T_OCRX_CNTRow row;

        //识别记录2
        DataSet1.T_OCRX_CNTRow row2;

        DataSet1.T_OCRX_DAMAGEDataTable damage;

        DataSet1.T_OCRX_DAMAGEDataTable damageEx;

        public fmDmgReport()
        {
            InitializeComponent();
            bll = new MainBLL();
            this.dgvDmgInfo.AutoGenerateColumns = false;
            this.dgvDmgInfoEx.AutoGenerateColumns = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请输入Vessel Code.");
            }

            var dataSource = bll.SelectDmgRecord(textBox1.Text);
            damage = dataSource;
            this.dgvDmgInfo.DataSource = dataSource;
            if (dgvDmgInfo.Columns.Count == 5)
            {
                DataGridViewButtonColumn dgv_rollback_col = new DataGridViewButtonColumn();
                dgv_rollback_col.Name = "Rollback";
                dgv_rollback_col.UseColumnTextForButtonValue = true;
                dgv_rollback_col.Text = "回退";
                dgv_rollback_col.HeaderText = "回退";
                dgvDmgInfo.Columns.Insert(dgvDmgInfo.Columns.Count, dgv_rollback_col);
            }
        }

        private void dgvDmgInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDmgInfo.Columns[e.ColumnIndex].Name == "Rollback")
            {
                try
                {
                    decimal dockid = decimal.Parse(dgvDmgInfo.Rows[e.RowIndex].Cells[1].Value.ToString());
                    DataSet1.T_OCRX_CNTDataTable dt = bll.SelectCnt(dockid);

                    row = dt[0];
                    row2 = dt.Count > 1 ? dt[1] : null;


                    bll.row1 = row;
                    bll.row2 = row2;

                    this.Cursor = Cursors.WaitCursor;
                    if (row == null)
                    {
                        MessageBox.Show("该记录不存在");
                        return;
                    }

                    fmExcepInput fm = new fmExcepInput();
                    fm.reason = row.CTOSERRORMSG;

                    DialogResult drs = fm.ShowDialog();
                    //DialogResult drs = MessageBox.Show("确认转异常处理？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (drs == DialogResult.No || drs == DialogResult.Cancel)
                    {
                        return;
                    }

                    row.CTOSERRORMSG = fm.reason;
                    if (row2 != null)
                    {
                        row2.CTOSERRORMSG = fm.reason;
                    }

                    bll.Rollback();

                    DataSet1.T_OCRX_DAMAGERow damageRow = damage[e.RowIndex];
                    bll.RollbackDamage(damageRow);

                    MessageBox.Show("回退成功.");

                    this.button1.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("请输入Vessel Code.");
            }

            damageEx = bll.SelectDmgRecordEx(textBox2.Text);
            //dataSource = dataSource;
            this.dgvDmgInfoEx.DataSource = damageEx;
            if (dgvDmgInfoEx.Columns.Count == 5)
            {
                DataGridViewButtonColumn dgv_rollback_col = new DataGridViewButtonColumn();
                dgv_rollback_col.Name = "Rollback";
                dgv_rollback_col.UseColumnTextForButtonValue = true;
                dgv_rollback_col.Text = "回退";
                dgv_rollback_col.HeaderText = "回退";
                dgvDmgInfoEx.Columns.Insert(dgvDmgInfoEx.Columns.Count, dgv_rollback_col);
            }
        }

        private void dgvDmgInfoEx_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDmgInfoEx.Columns[e.ColumnIndex].Name == "Rollback")
            {
                try
                {
                    decimal dockid = decimal.Parse(dgvDmgInfoEx.Rows[e.RowIndex].Cells[1].Value.ToString());
                    DataSet1.T_OCRX_CNTDataTable dt = bll.SelectCnt(dockid);

                    row = dt[0];
                    row2 = dt.Count > 1 ? dt[1] : null;


                    bll.row1 = row;
                    bll.row2 = row2;

                    this.Cursor = Cursors.WaitCursor;
                    if (row == null)
                    {
                        MessageBox.Show("该记录不存在");
                        return;
                    }

                    fmExcepInput fm = new fmExcepInput();
                    fm.reason = row.CTOSERRORMSG;

                    DialogResult drs = fm.ShowDialog();
                    //DialogResult drs = MessageBox.Show("确认转异常处理？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (drs == DialogResult.No || drs == DialogResult.Cancel)
                    {
                        return;
                    }

                    row.CTOSERRORMSG = fm.reason;
                    if (row2 != null)
                    {
                        row2.CTOSERRORMSG = fm.reason;
                    }

                    bll.MarkExpcetion();

                    DataSet1.T_OCRX_DAMAGERow damageRow = damage[e.RowIndex];
                    damageRow.ROLLBACK = "Y";
                    damageRow.ROLLBACKREASON = fm.reason;
                    bll.RollbackDamage(damageRow);

                    MessageBox.Show("回退成功.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

            }
        }

        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Cells[0].Value = row.Index + 1;
            }
        }

        private void btnDmgImport_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in damageEx.Rows)
            {
                try
                {
                    string containerid = bll.GetContainerId(dr["CONTAINERNO"].ToString());
                    CtosResult result = OCR.BLL.CtosAPIBLL.OP007095(containerid, dr["DMGCODE"].ToString(), string.Format("{0} {1}",dr["DMGMEMO"].ToString(),dr["DMGSIZE"].ToString()), MainBLL.Parms.TICKETID);

                    if (result.ERRORCODE != "0")
                    {
                        throw new Exception(result.ERRORMSG);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("导入残损信息失败:{0} dockid:{1}.EXCEPTION:{2}",dr["CONTAINERNO"].ToString(),dr["DOCKID"].ToString()),ex.Message);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //if (string.IsNullOrEmpty(txtVessel.Text.Trim()))
                //{
                //    throw new Exception("请输入船名简称");
                //}
                //if (string.IsNullOrEmpty(txtVoyage.Text.Trim()))
                //{
                //    throw new Exception("请输入出口航次");
                //}

                //using (DataSet ds = SearchBLL.SelectDamageReport(string.Format("{0}{1}", txtVessel.Text.Trim(), txtVoyage.Text.Trim()), fmMain.CompanyCode))
                //{
                //    if (ds.Tables[0].Rows.Count == 0)
                //    {
                //        throw new Exception("该船无残损箱作业数据！");
                //    }

                //    DialogResult drt = folderBrowserDialog1.ShowDialog(this);

                //    if (drt == DialogResult.OK)
                //    {
                //        string dir = string.Format(@"{0}\{1}{2}", folderBrowserDialog1.SelectedPath, txtVessel.Text.Trim(), txtVoyage.Text.Trim());
                //        if (!Directory.Exists(dir))
                //        {
                //            Directory.CreateDirectory(dir);
                //        }
                //        String column = string.Empty;
                //        //foreach (DataColumn c in ds.Tables[0].Columns)
                //        //{
                //        //    column += c.ColumnName + ",";
                //        //}
                //        //MessageBox.Show(column);

                //        //作业清单
                //        string fileName = string.Empty;

                //        fileName = string.Format(@"{0}\{1}{2}-残损箱.xlsx", dir, txtVessel.Text.Trim(), txtVoyage.Text.Trim());
                //        CCT.SystemFramework.Office.ExcelHelper.ExportToExcel(ds, fileName);

                //        MessageBox.Show("导出成功！");

                //    }

                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
