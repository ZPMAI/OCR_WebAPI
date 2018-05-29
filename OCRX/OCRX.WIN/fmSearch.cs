using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using OCRX.BLL;
using System.Threading;
using OCR.Model;
using OCRX.Model;

namespace OCRX.WIN
{
    public partial class fmSearch : Form
    {
        public fmSearch()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
        }

        OCRX.BLL.SearchBLL bll = new SearchBLL();
        fmDamageInfo fmDamage;
        fmDmgModify fmDmgModify;

        private void fmSearch_Load(object sender, EventArgs e)
        {
            this.dtpFrom.Value = DateTime.Now.Date;
            this.dtpTo.Value = DateTime.Now;

            cbbStatus.SelectedIndex = 0;
            cbbQC.SelectedIndex = 0;
            cbbPart.SelectedIndex = 0;
            cbbLoad.SelectedIndex = 0;

            InitQc();

            OcrDBPmsServerList = SearchBLL.GetOcrDBPmsServer();
        }

        private void InitQc()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                List<string> list = new List<string>();
                list.Add("ALL");

                using (QcSet.T_OCR_QCSETDataTable dt = bll.SelectQCSet())
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
                    fmMain.CompanyCode, cmbIsDmg.Text);
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


                OCR.WIN.fmShowPics fm = new OCR.WIN.fmShowPics(Convert.ToDecimal(dataGridView1[0, cr].Value), Convert.ToDecimal(dataGridView1[1, cr].Value));
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (string.IsNullOrEmpty(txtCarrierCode.Text.Trim()))
                {
                    throw new Exception("请输入船名简称");
                }
                if (string.IsNullOrEmpty(txtVoyageOut.Text.Trim()))
                {
                    throw new Exception("请输入出口航次");
                }

                using (DataSet ds = SearchBLL.SelectReport(string.Format("{0}{1}", txtCarrierCode.Text.Trim(), txtVoyageOut.Text.Trim()), fmMain.CompanyCode))
                {
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("该船无作业数据！");
                    }

                    DialogResult drt = folderBrowserDialog1.ShowDialog(this);

                    if (drt == DialogResult.OK)
                    {
                        string dir = string.Format(@"{0}\{1}{2}", folderBrowserDialog1.SelectedPath, txtCarrierCode.Text.Trim(), txtVoyageOut.Text.Trim());
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        String column = string.Empty;
                        //foreach (DataColumn c in ds.Tables[0].Columns)
                        //{
                        //    column += c.ColumnName + ",";
                        //}
                        //MessageBox.Show(column);

                        //拆成装、卸、倒柜 三个EXCEL文件
                        DataSet load = new DataSet();
                        DataSet dis = new DataSet();
                        DataSet restow = new DataSet();

                        SplitDataset(ds, ref load, ref dis, ref restow);

                        //作业清单
                        string fileName = string.Empty;

                        if (load.Tables[0].Rows.Count > 0)
                        {
                            fileName = string.Format(@"{0}\{1}{2}-装船.xlsx", dir, txtCarrierCode.Text.Trim(), txtVoyageOut.Text.Trim());
                            CCT.SystemFramework.Office.ExcelHelper.ExportToExcel(load, fileName);
                        }

                        if (dis.Tables[0].Rows.Count > 0)
                        {
                            fileName = string.Format(@"{0}\{1}{2}-卸船.xlsx", dir, txtCarrierCode.Text.Trim(), txtVoyageOut.Text.Trim());
                            CCT.SystemFramework.Office.ExcelHelper.ExportToExcel(dis, fileName);
                        }

                        if (restow.Tables[0].Rows.Count > 0)
                        {

                            fileName = string.Format(@"{0}\{1}{2}-倒柜.xlsx", dir, txtCarrierCode.Text.Trim(), txtVoyageOut.Text.Trim());
                            CCT.SystemFramework.Office.ExcelHelper.ExportToExcel(restow, fileName);
                        }

                        if (ckbPic.Checked)
                        {

                            //图片
                            string dirPic = string.Format(@"{0}\图片", dir);
                            if (!Directory.Exists(dirPic))
                            {
                                Directory.CreateDirectory(dirPic);
                            }
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                //每张图片一个文件夹
                                string dirPic1 = string.Format(@"{0}\{1}", dirPic, dr["箱号"].ToString());
                                if (!Directory.Exists(dirPic1))
                                {
                                    Directory.CreateDirectory(dirPic1);
                                }
                                LoadPhotos(Convert.ToDecimal(dr["dock_id"]), Convert.ToDecimal(dr["pms_id"]), dirPic1);
                            }
                        }

                        MessageBox.Show("导出成功！");

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

        private void SplitDataset(DataSet all, ref DataSet load, ref DataSet dis, ref DataSet restow)
        {
            load = all.Clone();
            dis = all.Clone();
            restow = all.Clone();

            foreach (DataRow dr in all.Tables[0].Rows)
            {
                if (dr["进港类型"] != DBNull.Value && dr["进港类型"].ToString() == "R")
                {
                    //只写装的记录
                    if (dr["装/卸"].ToString() == "装船")
                    {
                        restow.Tables[0].Rows.Add(dr.ItemArray);
                    }
                }
                else if (dr["装/卸"].ToString() == "卸船")
                {
                    dis.Tables[0].Rows.Add(dr.ItemArray);
                }
                else
                {
                    load.Tables[0].Rows.Add(dr.ItemArray);
                }
            }
        }


        IDictionary<int, OCR.Model.OcrDBPmsServer> OcrDBPmsServerList;


        //加载图片
        private void LoadPhotos(decimal dock_id, decimal pms_id, string dir)
        {
            using (OCR.Model.OcrPhoto.T_OCR_PHOTODataTable ds = SearchBLL.GetPhoto(dock_id))
            {
                OCR.Model.OcrDBPmsServer pms = OcrDBPmsServerList[Convert.ToInt32(pms_id)];

                if (pms == null)
                {
                    throw new Exception("图片服务器设置异常");
                }

                string path = string.Format(@"http://{0}:{1}", pms.Ip, pms.Port);

                foreach (OCR.Model.OcrPhoto.T_OCR_PHOTORow dr in ds)
                {
                    //int photo_pos = Convert.ToInt32(dr["PHOTO_POS"]);
                    string url = string.Format(@"{0}{1}", path, dr.PHOTO_URL);

                    SavePic(url, string.Format(@"{0}\{1}.jpg", dir, dr.PHOTO_ID));
                }


            }
        }

        /// <summary>
        /// 保存图片到本地
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fileName"></param>
        public void SavePic(string url, string fileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    System.Net.WebRequest imgRequest = System.Net.WebRequest.Create(url);
                    System.Drawing.Image downImage = System.Drawing.Image.FromStream(imgRequest.GetResponse().GetResponseStream());

                    downImage.Save(fileName);

                    downImage.Dispose();
                }
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "coldmgbtn")
            {
                try
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["IsDmg"].Value != null && dataGridView1.Rows[e.RowIndex].Cells["IsDmg"].Value.ToString() == "Y")
                    {
                        decimal dockid = decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells["DOCK_ID"].Value.ToString());
                        DataSet1.T_OCRX_CNTDataTable dmginfto = bll.SelectDamageInfo(Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["DOCK_ID"].Value));

                        //if (fmDamage == null || fmDamage.IsDisposed)
                        //{
                        //    fmDamage = new fmDamageInfo();
                        //}
                        //fmDamage.Text = string.Format("{0} 残损信息", dataGridView1.Rows[e.RowIndex].Cells["container_no"].Value.ToString());
                        //fmDamage.records = dmginfto;
                        //fmDamage.UpdateDGV(dmginfto);
                        //fmDamage.Show();
                        //fmDamage.BringToFront();

                        if (fmDmgModify != null && !fmDmgModify.IsDisposed)
                        {
                            fmDmgModify.Close();
                        }
                        string containerno = string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["RCONTAINER_NO"].Value.ToString())? dataGridView1.Rows[e.RowIndex].Cells["container_no"].Value.ToString():dataGridView1.Rows[e.RowIndex].Cells["RCONTAINER_NO"].Value.ToString();
                         fmDmgModify = new fmDmgModify(dockid, containerno);
                        //}
                        fmDmgModify.Show();
                        fmDmgModify.BringToFront();
                    }
                    else
                    {
                        MessageBox.Show("非残损箱，无法查看");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            // 按快捷键Ctrl+S执行按钮的点击事件方法
            //if (keyData == (Keys.Control | Keys.S))
            if (keyData == (Keys)Shortcut.CtrlQ)
            {
                btnSearch.PerformClick();
                return true;
            }

            if (keyData == (Keys)Shortcut.CtrlE)
            {
                btnExport.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData); // 其他键按默认处理　

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (string.IsNullOrEmpty(txtVessel.Text.Trim()))
                {
                    throw new Exception("请输入船名简称");
                }
                if (string.IsNullOrEmpty(txtVoyage.Text.Trim()))
                {
                    throw new Exception("请输入出口航次");
                }
                string vslaliase = string.Format("{0}{1}", txtVessel.Text.Trim(), txtVoyage.Text.Trim());
                using (DataTable dt = SearchBLL.LoadingList(vslaliase, fmMain.CompanyCode))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        throw new Exception("该船无作业数据！");
                    }

                    FrmLoadingList dis = new FrmLoadingList(vslaliase, "装船清单" + vslaliase, dt);
                    dis.Show();
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

        private void btnDischarge_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (string.IsNullOrEmpty(txtVessel.Text.Trim()))
                {
                    throw new Exception("请输入船名简称");
                }
                if (string.IsNullOrEmpty(txtVoyage.Text.Trim()))
                {
                    throw new Exception("请输入进口航次");
                }
                string vslaliase = string.Format("{0}{1}", txtVessel.Text.Trim(), txtVoyage.Text.Trim());
                using (DataTable dt = SearchBLL.DischargeList(vslaliase, fmMain.CompanyCode))
                {
                    if (dt.Rows.Count == 0)
                    {
                        throw new Exception("该船无作业数据！");
                    }

                    FrmDischargeListReport dis = new FrmDischargeListReport(vslaliase, "卸船清单" + vslaliase, dt);
                    dis.Show();
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

        private void btnDamage_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (string.IsNullOrEmpty(txtVessel.Text.Trim()))
                {
                    throw new Exception("请输入船名简称");
                }
                if (string.IsNullOrEmpty(txtVoyage.Text.Trim()))
                {
                    throw new Exception("请输入出口航次");
                }

                using (DataSet ds = SearchBLL.SelectDamageReport(string.Format("{0}{1}", txtVessel.Text.Trim(), txtVoyage.Text.Trim()), fmMain.CompanyCode))
                {
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("该船无残损箱作业数据！");
                    }

                    DialogResult drt = folderBrowserDialog1.ShowDialog(this);

                    if (drt == DialogResult.OK)
                    {
                        string dir = string.Format(@"{0}\{1}{2}", folderBrowserDialog1.SelectedPath, txtVessel.Text.Trim(), txtVoyage.Text.Trim());
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        String column = string.Empty;
                        //foreach (DataColumn c in ds.Tables[0].Columns)
                        //{
                        //    column += c.ColumnName + ",";
                        //}
                        //MessageBox.Show(column);

                        //作业清单
                        string fileName = string.Empty;

                        fileName = string.Format(@"{0}\{1}{2}-残损箱.xlsx", dir, txtVessel.Text.Trim(), txtVoyage.Text.Trim());
                        CCT.SystemFramework.Office.ExcelHelper.ExportToExcel(ds, fileName);

                        MessageBox.Show("导出成功！");

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