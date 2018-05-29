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
                    throw new Exception("���ܲ�ѯ����һ�ܵ����ݣ�");
                }

                this.dataGridView1.DataSource = bll.SelectCnt(this.dtpFrom.Value,
                    this.dtpTo.Value,
                    cbbPart.Text == "��ǰ" ? "Y" : "ALL",
                    cbbQC.Text,
                    cbbLoad.Text == "ALL" ? 999 : (cbbLoad.Text == "װ��" ? 0 : 1),
                    DealCstatus(cbbStatus.Text),
                    txtCntNo.Text.Trim(),
                    fmMain.CompanyCode);
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
                case "������":
                    rtn = 0;
                    break;
                case "���ڴ���":
                    rtn = 1;
                    break;
                case "�ɹ�����":
                    rtn = 2;
                    break;
                case "ת�쳣":
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
                @"/��������/��������/���ؼƻ�/ZXCraneLeft",
                null,
                CCT.SystemFramework.ReportingService.ReportingServiceHelper.Credential,
                true,
                "����������ѯ",
                this.MdiParent);
            //fm.MdiParent = this;
            //fm.Text = "����������ѯ";
            fm.Show();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (string.IsNullOrEmpty(txtCarrierCode.Text.Trim()))
                {
                    throw new Exception("�����봬�����");
                }
                if (string.IsNullOrEmpty(txtVoyageOut.Text.Trim()))
                {
                    throw new Exception("��������ں���");
                }

                using (DataSet ds = SearchBLL.SelectReport(string.Format("{0}{1}", txtCarrierCode.Text.Trim(), txtVoyageOut.Text.Trim()), fmMain.CompanyCode))
                {
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("�ô�����ҵ���ݣ�");
                    }                    

                    DialogResult drt = folderBrowserDialog1.ShowDialog(this);

                    if (drt == DialogResult.OK)
                    {
                        string dir = string.Format(@"{0}\{1}{2}", folderBrowserDialog1.SelectedPath, txtCarrierCode.Text.Trim(), txtVoyageOut.Text.Trim());
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }

                        //���װ��ж������ ����EXCEL�ļ�
                        DataSet load = new DataSet();
                        DataSet dis = new DataSet();
                        DataSet restow = new DataSet();

                        SplitDataset(ds, ref load, ref dis, ref restow);

                        //��ҵ�嵥
                        string fileName = string.Empty;

                        if (load.Tables[0].Rows.Count > 0)
                        {
                            fileName = string.Format(@"{0}\{1}{2}-װ��.xls", dir, txtCarrierCode.Text.Trim(), txtVoyageOut.Text.Trim());
                            CCT.SystemFramework.Office.ExcelHelper.ExportToExcel(load, fileName);
                        }

                        if (dis.Tables[0].Rows.Count > 0)
                        {
                            fileName = string.Format(@"{0}\{1}{2}-ж��.xls", dir, txtCarrierCode.Text.Trim(), txtVoyageOut.Text.Trim());
                            CCT.SystemFramework.Office.ExcelHelper.ExportToExcel(dis, fileName);
                        }

                        if (restow.Tables[0].Rows.Count > 0)
                        {

                            fileName = string.Format(@"{0}\{1}{2}-����.xls", dir, txtCarrierCode.Text.Trim(), txtVoyageOut.Text.Trim());
                            CCT.SystemFramework.Office.ExcelHelper.ExportToExcel(restow, fileName);
                        }

                        if (ckbPic.Checked)
                        {

                            //ͼƬ
                            string dirPic = string.Format(@"{0}\ͼƬ", dir);
                            if (!Directory.Exists(dirPic))
                            {
                                Directory.CreateDirectory(dirPic);
                            }
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                //ÿ��ͼƬһ���ļ���
                                string dirPic1 = string.Format(@"{0}\{1}", dirPic, dr["���"].ToString());
                                if (!Directory.Exists(dirPic1))
                                {
                                    Directory.CreateDirectory(dirPic1);
                                }
                                LoadPhotos(Convert.ToDecimal(dr["dock_id"]), Convert.ToDecimal(dr["pms_id"]), dirPic1);
                            }
                        }

                        MessageBox.Show("�����ɹ���");

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
                if (dr["��������"] != DBNull.Value && dr["��������"].ToString() == "R")
                {
                    //ֻдװ�ļ�¼
                    if (dr["װ/ж"].ToString() == "װ��")
                    {
                        restow.Tables[0].Rows.Add(dr.ItemArray);
                    }
                }
                else if (dr["װ/ж"].ToString() == "ж��")
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


        //����ͼƬ
        private void LoadPhotos(decimal dock_id, decimal pms_id, string dir)
        {
            using (OCR.Model.OcrPhoto.T_OCR_PHOTODataTable ds = SearchBLL.GetPhoto(dock_id))
            {
                OCR.Model.OcrDBPmsServer pms = OcrDBPmsServerList[Convert.ToInt32(pms_id)];

                if (pms == null)
                {
                    throw new Exception("ͼƬ�����������쳣");
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
        /// ����ͼƬ������
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


    }
}