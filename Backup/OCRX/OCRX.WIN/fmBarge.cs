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
    public partial class fmBarge : Form
    {
        public fmBarge()
        {
            InitializeComponent();
            dg.AutoGenerateColumns = false;
        }

        private void fmVessel_Load(object sender, EventArgs e)
        {
            GetData();

            InitCbb();
        }

        BLL.BargeBLL bll = new OCRX.BLL.BargeBLL();

        private DataSet dsBerthplan;

        //初始化下拉框
        private void InitCbb()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
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

        private void GetData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                dg.DataSource = bll.SelectBarge(fmMain.CompanyCode);
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
                DataSet1.T_OCRX_BARGERow row = (DataSet1.T_OCRX_BARGERow)((DataRowView)dg.CurrentRow.DataBoundItem).Row;

                cbbVessel.Text = row.SHIP_CODE;
                cbbInVoyage.Text = row.IN_VOYAGE_CODE;
                cbbOutVoyage.Text = row.OUT_VOYAGE_CODE;

            }
        }

        private void fmVessel_Activated(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;


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

                    bll.InsertBarge(fmMain.CompanyCode, cbbVessel.Text.Trim(), cbbInVoyage.Text.Trim(), cbbOutVoyage.Text.Trim(), Convert.ToDecimal(lblBerthplanno.Text));

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

    

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("是否删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dr == DialogResult.Yes)
                {

                    Cursor.Current = Cursors.WaitCursor;

                    bll.DeleteBarge(fmMain.CompanyCode,cbbVessel.Text.Trim(), cbbInVoyage.Text.Trim(), cbbOutVoyage.Text.Trim(), Convert.ToDecimal(lblBerthplanno.Text));

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


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetData();

            InitCbb();
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
                foreach (DataRow dr in dsBerthplan.Tables[0].Rows)
                {
                    if (dr["EVESSELNAME"].ToString() == cbbVessel.Text && dr["outboundvoy"].ToString() == cbbOutVoyage.Text)
                    {
                        lblBerthplanno.Text = dr["berthplanno"].ToString();
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
    }
}