using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using OCR.BLL;
using OCR.Model;

namespace OCR.WIN
{
    public partial class fmSearch : Form
    {
        public fmSearch()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
        }

        OCR.BLL.SearchBLL bll = new SearchBLL();

        private void fmSearch_Load(object sender, EventArgs e)
        {
            this.dtpFrom.Value = DateTime.Now.Date;
            this.dtpTo.Value = DateTime.Now;

            cbbStatus.SelectedIndex = 0;
            cbbQC.SelectedIndex = 0;
            cbbPart.SelectedIndex = 0;
            cbbLoad.SelectedIndex = 0;

            InitQc();
        }

        private void InitQc()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                List<string> list = new List<string>();
                list.Add("ALL");

                OCR.BLL.QcSetBLL b = new QcSetBLL();
                using (QcSet.T_OCR_QCSETDataTable dt = b.SelectQCSet())
                {
                    foreach (QcSet.T_OCR_QCSETRow r in dt)
                    {
                        list.Add(r.TRVALCRANE_NO);
                    }
                }

                cbbQC.DataSource = list;
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

        private void Search()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (((TimeSpan)dtpTo.Value.Subtract(dtpFrom.Value)).Days > 7)
                {
                    throw new Exception("不能查询超过一周的数据！");
                }

                this.dataGridView1.DataSource = bll.SelectCnt(this.dtpFrom.Value,
                    this.dtpTo.Value,
                    cbbPart.Text == "当前" ? "Y" : "ALL",
                    cbbQC.Text,
                    cbbLoad.Text == "ALL" ? 999 : (cbbLoad.Text == "装船" ? 0 : 1),
                    DealCstatus(cbbStatus.Text),
                    txtCntNo.Text.Trim(),
                    txtTruck.Text.Trim());
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

        private decimal DealCstatus(string status)
        {
            decimal rtn = 999;
            switch (status)
            {
                case "待处理":
                    rtn = 0;
                    break;
                case "正在处理":
                    rtn = 1;
                    break;
                case "成功处理":
                    rtn = 2;
                    break;
                case "转异常":
                    rtn = 3;
                    break;
            }

            return rtn;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (dataGridView1.DataSource == null || dataGridView1.Rows.Count == 0)
                {
                    return;
                }

                int cr = dataGridView1.CurrentCell.RowIndex;


                fmShowPics fm = new fmShowPics(Convert.ToDecimal(dataGridView1[0, cr].Value), Convert.ToDecimal(dataGridView1[1, cr].Value));
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

        private void btnLeft_Click(object sender, EventArgs e)
        {
            CCT.SystemFramework.ReportingService.FrmShow fm = new CCT.SystemFramework.ReportingService.FrmShow(
                OCR.Model.Config.ReportingServiceUrl,
                @"/报表中心/生产报表/配载计划/ZXCraneLeft",
                null,
                CCT.SystemFramework.ReportingService.ReportingServiceHelper.Credential,
                true,
                "岸桥余量查询",
                this.MdiParent);
            //fm.MdiParent = this;
            //fm.Text = "岸桥余量查询";
            fm.Show();
        }
    }
}