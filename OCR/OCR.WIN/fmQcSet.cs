using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using OCR.Model;
using OCR.BLL;

namespace OCR.WIN
{
    public partial class fmQcSet : Form
    {
        public fmQcSet()
        {
            InitializeComponent();
            dgv1.AutoGenerateColumns = false;
        }

        OCR.BLL.QcSetBLL bll = new QcSetBLL();

        private void fmQcSet_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;

            InitCbb();

            GetALLData();

            
        }

        private void GetALLData()
        {
            try
            {
                dgv1.DataSource = bll.SelectQCSet();

                

                //QcSet.T_OCR_QCSETDataTable dt = bll.SelectQCSet();
                //foreach (QcSet.T_OCR_QCSETRow row in dt)
                //{
                //    CtosAPIBLL.OP007006(row.COMMEND_ID, row.DEVICE_NO, row.TRVALCRANE_NO, row.DRIVER_ID, row.CONTRACTOR_CODE, row.TICKET_ID);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgv1_CurrentCellChanged(object sender, EventArgs e)
        {
            //if (dgv1.DataSource == null)
            //{
            //    return;
            //}

            ////给界面赋值
            //QcSet.T_OCR_QCSETRow row = (QcSet.T_OCR_QCSETRow)((DataRowView)dgv1.CurrentRow.DataBoundItem).Row;

            //btnSave.Enabled = true;

            //lblQC.Text = row.TRVALCRANE_NO;
            //cbbCommend.Text = row.COMMEND_ID;
            //txtPwd.Text = row.COMMEND_PAW;
            //cbbContractor.Text = row.CONTRACTOR_CODE;
            //cbbDriver.Text = row.DRIVER_ID;
            //cbbVessel.Text = row.SHIP_CODE;
            //cbbInVoyage.Text = row.IN_VOYAGE_CODE;
            //cbbOutVoyage.Text = row.OUT_VOYAGE_CODE;
            //txtBerth.Text = row.BERTH_NUM;
            //txtRemoteId.Text = row.TERMINAL_NO;

            //rbnClose.Checked = row.SHIPMENT_DEAL != "开";
            //rbnLeft.Checked = row.BERTH_WAY == "左";
        }

        private void Cancel()
        {
            lblQC.Text = string.Empty;
            cbbCommend.Text = string.Empty;
            //txtPwd.Text = string.Empty;
            cbbContractor.Text = string.Empty;
            cbbDriver.Text = string.Empty;
            cbbVessel.Text = string.Empty;
            cbbInVoyage.Text = string.Empty;
            cbbOutVoyage.Text = string.Empty;
            txtBerth.Text = string.Empty;
            txtRemoteId.Text = string.Empty;

            rbnClose.Checked = true;
            rbnLeft.Checked = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void dgv1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                if (dgv1.DataSource == null)
                {
                    return;
                }

                //给界面赋值
                QcSet.T_OCR_QCSETRow row = (QcSet.T_OCR_QCSETRow)((DataRowView)dgv1.CurrentRow.DataBoundItem).Row;

                btnSave.Enabled = true;

                lblQC.Text = row.TRVALCRANE_NO;                
                //txtPwd.Text = row.COMMEND_PAW;
                cbbContractor.Text = row.CONTRACTOR_CODE;
                cbbCommend.Text = row.COMMEND_ID;
                cbbDriver.Text = row.DRIVER_ID;
                cbbVessel.Text = row.SHIP_CODE;
                cbbInVoyage.Text = row.IN_VOYAGE_CODE;
                cbbOutVoyage.Text = row.OUT_VOYAGE_CODE;
                txtBerth.Text = row.BERTH_NUM;
                txtRemoteId.Text = row.TERMINAL_NO;

                rbnClose.Checked = row.SHIPMENT_DEAL != "开";
                rbnOpen.Checked = row.SHIPMENT_DEAL == "开";
                rbnLeft.Checked = row.BERTH_WAY == "L";
                rbnRight.Checked = row.BERTH_WAY == "R";

                rbnInner.Checked = row.WORKTYPE == "智能";
                rbnEx.Checked = row.WORKTYPE == "现场";
                rbnVs.Checked = row.WORKTYPE == "核封";

                //如果没有无线终端号则获取一个新的

                if (string.IsNullOrEmpty(txtRemoteId.Text))
                {
                    string str = bll.SelectRemote();
                    string[] s2 = str.Split(new char[] { ',' });

                    txtRemoteId.Text = s2[1];
                    row.DEVICE_NO = s2[0];
                }

                //作业中不能切换内现场
                if (row.STATUS == "停止作业")
                {
                    rbnInner.Enabled = true;
                    rbnEx.Enabled = true;
                    rbnVs.Enabled = true;
                }
                else
                {
                    rbnInner.Enabled = false;
                    rbnEx.Enabled = false;
                    rbnVs.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DataSet dsBerthplan;

        //初始化下拉框
        private void InitCbb()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                cbbContractor.DataSource = bll.SelectContractor();
                cbbDriver.DataSource = bll.SelectCommend("CCT");
                dsBerthplan = bll.SelectBerthplan();

                List<string> vessels = new List<string>();
                foreach (DataRow dr in dsBerthplan.Tables[0].Rows)
                {
                    if (!vessels.Contains(dr["evesselname"].ToString()))
                    {
                        vessels.Add(dr["evesselname"].ToString());
                    }
                }
                cbbVessel.DataSource = vessels;
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

        private void cbbContractor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                cbbCommend.DataSource = bll.SelectCommend(cbbContractor.Text);
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

        private void cbbVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                List<string> list1 = new List<string>();
                List<string> list2 = new List<string>();

                foreach (DataRow dr in dsBerthplan.Tables[0].Rows)
                {
                    if (dr["EVESSELNAME"].ToString() == cbbVessel.Text)
                    {
                        list1.Add(dr["inboundvoy"].ToString());
                        list2.Add(dr["outboundvoy"].ToString());
                    }
                }

                cbbInVoyage.DataSource = list1;
                cbbOutVoyage.DataSource = list2;

                if (list1.Count > 0)
                {
                    cbbInVoyage.SelectedIndex = 0;
                }
                if (list2.Count > 0)
                {
                    cbbOutVoyage.SelectedIndex = 0;
                }
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

        private void cbbOutVoyage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                txtBerth.Text = string.Empty;

                foreach (DataRow dr in dsBerthplan.Tables[0].Rows)
                {
                    if (dr["EVESSELNAME"].ToString() == cbbVessel.Text && dr["outboundvoy"].ToString() == cbbOutVoyage.Text)
                    {
                        txtBerth.Text = dr["berthno"].ToString();
                        rbnLeft.Checked = dr["actualberthway"].ToString() == "L";
                        rbnRight.Checked = dr["actualberthway"].ToString() != "L";
                    }
                }

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult drs = MessageBox.Show("请确认是否保存修改", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (drs == DialogResult.No)
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            try
            {
                QcSet.T_OCR_QCSETRow row = (QcSet.T_OCR_QCSETRow)((DataRowView)dgv1.CurrentRow.DataBoundItem).Row;

                if (!row.IsERROR_MESSNull() && !string.IsNullOrEmpty(row.ERROR_MESS))
                {
                    MessageBox.Show(string.Format("{0} {1}\r\n不能配置为作业中", row.TRVALCRANE_NO, row.ERROR_MESS));
                    return;
                }

                row.COMMEND_ID = cbbCommend.Text.Trim().ToUpper();
                //row.COMMEND_PAW = txtPwd.Text.Trim();
                row.CONTRACTOR_CODE = cbbContractor.Text.Trim().ToUpper();
                row.DRIVER_ID = cbbDriver.Text.Trim().ToUpper();
                row.SHIP_CODE = cbbVessel.Text.Trim().ToUpper();
                row.IN_VOYAGE_CODE = cbbInVoyage.Text.Trim().ToUpper();
                row.OUT_VOYAGE_CODE = cbbOutVoyage.Text.Trim().ToUpper();
                row.BERTH_NUM = txtBerth.Text.Trim();
                row.TERMINAL_NO = txtRemoteId.Text.Trim();
                row.COMMEND_PAW = "123456";

                row.SHIPMENT_DEAL = rbnClose.Checked ? "关" : "开";
                row.BERTH_WAY = rbnLeft.Checked ? "L" : "R";

                row.WORKTYPE = rbnInner.Checked ? "智能" : rbnEx.Checked ? "现场" : "核封";

                foreach (DataRow dr in dsBerthplan.Tables[0].Rows)
                {
                    if (dr["EVESSELNAME"].ToString() == cbbVessel.Text.Trim().ToUpper() && dr["outboundvoy"].ToString() == cbbOutVoyage.Text.Trim().ToUpper())
                    {
                        row.VESSELALIASE = dr["velaliase"].ToString();
                        row.BERTHPLANNO = Convert.ToDecimal(dr["berthplanno"]);
                        row.AVESSELNAME = dr["avesselname"].ToString();
                        //row.INAGENT = dr["inagent"].ToString();
                        //row.OUTAGENT = dr["outagent"].ToString();
                        row.INAGENT = string.Empty;
                        row.OUTAGENT = string.Empty;
                        row.INVESSELLINECODE = dr["INVESSELLINECODE"].ToString();
                        row.OUTVESSELLINECODE = dr["OUTVESSELLINECODE"].ToString();
                        row.OWNER = dr["OWNER"].ToString();
                    }
                }

                row.STATUS = "作业中";
                

                bll.UpdateQCSet(row);

                GetALLData();

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

        private void btnPause_Click(object sender, EventArgs e)
        {
            DialogResult drs = MessageBox.Show("请确认是否暂停作业", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (drs == DialogResult.No)
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            try
            {
                QcSet.T_OCR_QCSETRow row = (QcSet.T_OCR_QCSETRow)((DataRowView)dgv1.CurrentRow.DataBoundItem).Row;

                row.STATUS = "暂停作业";


                bll.UpdateQCSet(row);

                GetALLData();

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

        private void btnStop_Click(object sender, EventArgs e)
        {
            DialogResult drs = MessageBox.Show("请确认是否停止作业", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (drs == DialogResult.No)
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            try
            {
                QcSet.T_OCR_QCSETRow row = (QcSet.T_OCR_QCSETRow)((DataRowView)dgv1.CurrentRow.DataBoundItem).Row;

                row.STATUS = "停止作业";

                bll.UpdateQCSet(row);

                GetALLData();

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

        private void btnLog_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                QcSet.T_OCR_QCSETRow row = (QcSet.T_OCR_QCSETRow)((DataRowView)dgv1.CurrentRow.DataBoundItem).Row;

                OpLog.T_OCR_LOGDataTable dt = bll.SelectLogs("T_OCR_QCSET", row.TRVALCRANE_NO);
                fmLog fm = new fmLog();
                fm.dt = dt;
                fm.ShowDialog();

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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitCbb();
            GetALLData();
        }

        private void rbnInner_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = rbnInner.Checked;

            if (isChecked)
            {
                cbbCommend.Enabled = isChecked;
                cbbContractor.Enabled = isChecked;
                rbnClose.Enabled = isChecked;
                rbnOpen.Enabled = isChecked;
                cbbDriver.Enabled = isChecked;
                rbnLeft.Enabled = isChecked;
                rbnRight.Enabled = isChecked;

                cbbVessel.Enabled = isChecked;
                cbbInVoyage.Enabled = isChecked;
                cbbOutVoyage.Enabled = isChecked;
            }
        }

        private void dgv1_DataSourceChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    for (int i = 0; i < dgv1.RowCount; i++)
            //    {
            //        QcSet.T_OCR_QCSETRow row = (QcSet.T_OCR_QCSETRow)((DataRowView)dgv1.Rows[i].DataBoundItem).Row;
            //        if (row.STATUS == "停止作业" || row.STATUS == "暂停作业")
            //        {
            //            dgv1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
            //        }
            //        else if (row.WORKTYPE == "智能")
            //        {
            //            dgv1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
            //        }
            //        else if (row.WORKTYPE == "现场")
            //        {
            //            dgv1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
            //        }
            //        else
            //        {
            //            dgv1.Rows[i].DefaultCellStyle.BackColor = Color.White;
            //        }

            //    }
            //}
            //catch { }
        }

        private void dgv1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //try
            //{
                
            //    int i = e.RowIndex;
            //        QcSet.T_OCR_QCSETRow row = (QcSet.T_OCR_QCSETRow)((DataRowView)dgv1.Rows[i].DataBoundItem).Row;
            //        if (row.STATUS == "停止作业" || row.STATUS == "暂停作业")
            //        {
            //            //dgv1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
            //            dgv1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
            //        }
            //        else if (row.WORKTYPE == "智能")
            //        {
            //            dgv1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
            //        }
            //        else if (row.WORKTYPE == "现场")
            //        {
            //            dgv1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
            //        }
            //        else
            //        {
            //            dgv1.Rows[i].DefaultCellStyle.BackColor = Color.White;
            //        }

                
            //}
            //catch { }
        }

        private void dgv1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {

                int i = e.RowIndex;
                QcSet.T_OCR_QCSETRow row = (QcSet.T_OCR_QCSETRow)((DataRowView)dgv1.Rows[i].DataBoundItem).Row;
                if (row.STATUS == "停止作业" || row.STATUS == "暂停作业")
                {
                    if (!row.IsERROR_MESSNull() && !string.IsNullOrEmpty(row.ERROR_MESS))
                    {
                        dgv1.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    }
                    else
                    {
                        dgv1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    //dgv1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
                }
                else if (row.WORKTYPE == "智能")
                {
                    dgv1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (row.WORKTYPE == "现场")
                {
                    dgv1.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else if (row.WORKTYPE == "核封")
                {
                    dgv1.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                }

                else
                {
                    dgv1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }


            }
            catch { }
        }

        private void rbnEx_CheckedChanged(object sender, EventArgs e)
        {
            bool isNotChecked = !rbnEx.Checked;

            if (!isNotChecked)
            {
                cbbCommend.Enabled = isNotChecked;
                cbbContractor.Enabled = isNotChecked;
                rbnClose.Enabled = isNotChecked;
                rbnOpen.Enabled = isNotChecked;
                cbbDriver.Enabled = isNotChecked;
                rbnLeft.Enabled = isNotChecked;
                rbnRight.Enabled = isNotChecked;

                cbbVessel.Enabled = isNotChecked;
                cbbInVoyage.Enabled = isNotChecked;
                cbbOutVoyage.Enabled = isNotChecked;
            }
        }

        private void rbnVs_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = rbnVs.Checked;

            if (isChecked)
            {
                cbbCommend.Enabled = !isChecked;
                cbbContractor.Enabled = !isChecked;
                rbnClose.Enabled = !isChecked;
                rbnOpen.Enabled = !isChecked;
                cbbDriver.Enabled = !isChecked;
                rbnLeft.Enabled = !isChecked;
                rbnRight.Enabled = !isChecked;

                cbbVessel.Enabled = isChecked;
                cbbInVoyage.Enabled = isChecked;
                cbbOutVoyage.Enabled = isChecked;
            }
        }
    }
}