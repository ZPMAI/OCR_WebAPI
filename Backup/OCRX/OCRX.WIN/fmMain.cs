using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

//using OCR.BLL;
using OCR.Model;
using System.Threading;
using System.IO;

using OCRX.BLL;
using OCRX.Model;

namespace OCRX.WIN
{
    public partial class fmMain : Form
    {
        //用户名
        public static string UserId
        {
            get
            {
                return CCT.Common.Userinfo.Username == null ? "SYS" : CCT.Common.Userinfo.Username;
            }
        }

        private static string companyCode;
        /// <summary>
        /// 公司名
        /// </summary>
        public static string CompanyCode
        {
            get
            {
                if (string.IsNullOrEmpty(companyCode))
                {
                    try
                    {
                        DataSet1.T_OCRX_USERSRow row = UsersBLL.SelectUser(UserId);
                        if (row == null)
                        {
                            //MessageBox.Show("该用户未设置对应的公司！");
                        }
                        else
                        {
                            companyCode = row.COMPANYCODE;
                        }
                    }
                    catch { }
                }

                return companyCode;
            }
        }

        public fmMain()
        {
            InitializeComponent();

            InitScaler();

            bll = new MainBLL(InitCntCtrl());
        }

        private OCRX.Model.CntCtrl InitCntCtrl()
        {
            OCRX.Model.CntCtrl cntCtrl = new OCRX.Model.CntCtrl();
            cntCtrl.numBndl = this.numBndl;
            cntCtrl.numBndl.Value = 1;
            cntCtrl.numMoves = this.numMoves;
            cntCtrl.numMoves.Value = 1;
            cntCtrl.txtFle = this.txtFle;
            cntCtrl.txtFle.Text = string.Empty;
            cntCtrl.rbnDis = this.rbnDis;
            cntCtrl.rbnLoad = this.rbnLoad;
            cntCtrl.ckbIsDmg = this.ckbIsDmg;
            cntCtrl.ckbIsDmg.Checked = false;
            cntCtrl.txtBndl1 = this.txtBndl1;
            cntCtrl.txtBndl1.Text = "0";
            cntCtrl.txtContNo = this.txtContNo;
            cntCtrl.txtDmg = this.txtDmg;
            cntCtrl.txtDmg.Text = string.Empty;
            cntCtrl.txtImdg1 = this.txtImdg1;
            cntCtrl.txtImdg1.Text = string.Empty;
            cntCtrl.txtImdg2 = this.txtImdg2;
            cntCtrl.txtImdg2.Text = string.Empty;
            cntCtrl.txtImdg3 = this.txtImdg3;
            cntCtrl.txtImdg3.Text = string.Empty;
            cntCtrl.txtISO = this.txtISO;
            cntCtrl.txtISO.Text = string.Empty;
            cntCtrl.txtMove1 = this.txtMove1;
            cntCtrl.txtMove1.Text = "1";
            cntCtrl.txtOA = this.txtOA;
            cntCtrl.txtOA.Text = string.Empty;
            cntCtrl.txtOF = this.txtOF;
            cntCtrl.txtOF.Text = string.Empty;
            cntCtrl.txtOH = this.txtOH;
            cntCtrl.txtOH.Text = string.Empty;
            cntCtrl.txtOL = this.txtOL;
            cntCtrl.txtOL.Text = string.Empty;
            cntCtrl.txtOR = this.txtOR;
            cntCtrl.txtOR.Text = string.Empty;
            cntCtrl.ckbDanger = this.ckbDanger;
            cntCtrl.ckbDanger.Checked = false;
            cntCtrl.ckbOOG = this.ckbOOG;
            cntCtrl.ckbOOG.Checked = false;
            cntCtrl.ckbOverDis = this.ckbOverDis;
            cntCtrl.ckbOverDis.Checked = false;
            cntCtrl.lblLoadPos = this.lblLoadPos;
            cntCtrl.lblLoadPos.Text = string.Empty;

            cntCtrl.lblBerth = this.lblBerth;
            cntCtrl.lblBerth.Text = string.Empty;
            cntCtrl.lblShipAgent = this.lblShipAgent;
            cntCtrl.lblShipAgent.Text = string.Empty;
            cntCtrl.lblService = this.lblService;
            cntCtrl.lblService.Text = string.Empty;
            cntCtrl.lblLinecode = this.lblLinecode;
            cntCtrl.lblLinecode.Text = string.Empty;
            cntCtrl.lblVessel = this.lblVessel;
            cntCtrl.lblVessel.Text = string.Empty;
            cntCtrl.lblVoyage = this.lblVoyage;
            cntCtrl.lblVoyage.Text = string.Empty;

            this.lblQC.Text = string.Empty;

            //cntCtrl.rbnDis.Checked = true;
            cntCtrl.rbnDis.Enabled = true;
            cntCtrl.rbnLoad.Enabled = true;

            cntCtrl.btnNext = this.btnNext;

            cntCtrl.lblPicNum = this.lblPicNum;
            lblPicNum.Text = string.Empty;

            txtISOEntered = false;
            txtContNoEntered = false;
            
            return cntCtrl;
        }

        MainBLL bll;

        //识别记录
        DataSet1.T_OCRX_CNTRow row;

        //识别记录2
        DataSet1.T_OCRX_CNTRow row2;

        //服务器设置
        IDictionary<int, OcrDBPmsServer> OcrDBPmsServerList;

        //CTOS返回信息 含箱信息
        CtosResult cntResult;

        private void fmMain_Load(object sender, EventArgs e)
        {
            this.Focus();

            SetCompany();

            OcrDBPmsServerList = bll.GetOcrDBPmsServer();

            StartThreads();
            
        }

        private void SetCompany()
        {
            try
            {
                DataSet1.T_OCRX_CORow row = UsersBLL.SelectCompany(CompanyCode);
                lblCompany.Text = row.COMPANYNAME;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }
        }

        //剩余记录数线程
        private Thread leftThread;
        private bool runLeftThread = true;
        private void RunLeftThread()
        {
            while (runLeftThread)
            {
                CountLeft();
                Thread.Sleep(1000);
            }
        }

        //启动所有进程
        private void StartThreads()
        {
            runLeftThread = true;
            leftThread = new Thread(new ThreadStart(RunLeftThread));
            leftThread.Start();
        }

        //停止所有线程
        private void StopThreads()
        {
            try
            {
                runLeftThread = false;
                leftThread.Abort();
            }
            catch
            {
            }
        }

        private delegate void InvokeLeftCallBack();

        //
        private void CountLeft()
        {
            //this.Cursor = Cursors.WaitCursor;
            try
            {
                if (this.lblLeft.InvokeRequired)
                {
                    InvokeLeftCallBack d = new InvokeLeftCallBack(CountLeft);
                    this.Invoke(d);
                }
                else
                {
                    this.lblLeft.Text = string.Format("剩余记录数：{0}", bll.SelectLeft(CompanyCode));
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NextRecord();
        }

        string datetimeFormat = "yyyy-MM-dd HH:mm:ss:fff";

        private void AddLog(string action, ref DateTime lastTime, StringBuilder sb)
        {
            sb.AppendFormat("{0} {1}结束 耗时{2}毫秒\r\n", DateTime.Now.ToString(datetimeFormat), action, Math.Round(((TimeSpan)DateTime.Now.Subtract(lastTime)).TotalMilliseconds,0));
            lastTime = DateTime.Now;
        }

        private void NextRecord()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if ((row != null && row.CSTATUS == Convert.ToInt32(Config.CStatus.Fetched)) ||
                    (numMoves.Value > 1 && row2 != null && row2.CSTATUS == Convert.ToInt32(Config.CStatus.Fetched)))
                {
                    //加载图片
                    LoadPhotos();

                    MessageBox.Show("当前记录未处理！");
                    return;
                }

                InitCntCtrl();

                DataSet1.T_OCRX_CNTDataTable dt = bll.SelectNextRecord(UserId, CompanyCode);

                if (dt == null || dt.Rows.Count == 0)
                {
                    ClearImg();
                    MessageBox.Show("没有未处理的记录，请稍后再尝试");
                    return;
                }

                bll = new MainBLL(InitCntCtrl());

                row = dt[0];
                row2 = dt.Count > 1 ? dt[1] : null;

                bll.row1 = row;
                bll.row2 = row2;

                //顶部信息
                lblLoadDis.Text = row.DOCK_STATUS == 0 ? "装船" : "卸船";
                lblLoadPos.Text = ""; //?暂时不填
                lblQC.Text = row.TRVAL_NO;
                lblVessel.Text = row.SHIP_CODE;
                lblVoyage.Text = row.C_VOYAGE;
                lblService.Text = row.SERVICECODE;
                lblShipAgent.Text = row.SHIPAGENT;
                lblBerth.Text = row.BERTH_NUM;

                btnUp.Enabled = true;
                btnDown.Enabled = true;

                //加载图片
                LoadPhotos();

                //填充箱信息
                cntResult = bll.InitCntCtrl(row);

                numMoves.Focus();                          
                
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



        private void ClearImg()
        {
            if (this.pb1.Image != null)
            {
                this.pb1.Image.Dispose();
                this.pb1.Image = null;
            }

            if (this.pb2.Image != null)
            {
                this.pb2.Image.Dispose();
                this.pb2.Image = null;
            }

            if (this.pb3.Image != null)
            {
                this.pb3.Image.Dispose();
                this.pb3.Image = null;
            }

            if (this.pb4.Image != null)
            {
                this.pb4.Image.Dispose();
                this.pb4.Image = null;
            }

            if (this.pb5.Image != null)
            {
                this.pb5.Image.Dispose();
                this.pb5.Image = null;
            }

            if (this.pb6.Image != null)
            {
                this.pb6.Image.Dispose();
                this.pb6.Image = null;
            }

            if (this.pb7.Image != null)
            {
                this.pb7.Image.Dispose();
                this.pb7.Image = null;
            }

            if (this.pb8.Image != null)
            {
                this.pb8.Image.Dispose();
                this.pb8.Image = null;
            }
        }


        //加载图片
        private void LoadPhotos()
        {
            using (OcrPhoto.T_OCR_PHOTODataTable ds = bll.GetPhoto(row.DOCK_ID))
            {
                string url1 = string.Empty;
                string url2 = string.Empty;
                string url3 = string.Empty;
                string url4 = string.Empty;
                string url5 = string.Empty;
                string url6 = string.Empty;
                string url7 = string.Empty;
                string url8 = string.Empty;

                OcrDBPmsServer pms = OcrDBPmsServerList[Convert.ToInt32(row.PMS_ID)];

                if (pms == null)
                {
                    throw new Exception("图片服务器设置异常");
                }

                string path = string.Format(@"http://{0}:{1}", pms.Ip, pms.Port);

                foreach (OcrPhoto.T_OCR_PHOTORow dr in ds)
                {
                    //int photo_pos = Convert.ToInt32(dr["PHOTO_POS"]);
                    string url = string.Format(@"{0}{1}", path, dr.PHOTO_URL);

                    switch (Convert.ToInt32(dr.PHOTO_POS))
                    {
                        case 1:
                            url1 = url;
                            break;
                        case 2:
                            url2 = url;
                            break;
                        case 3:
                            url3 = url;
                            break;
                        case 4:
                            url4 = url;
                            break;
                        case 5:
                            url5 = url;
                            break;
                        case 6:
                            url6 = url;
                            break;
                        case 7:
                            url7 = url;
                            break;
                        case 8:
                            url8 = url;
                            break;

                    }


                }

                tableLayoutPanel1.RowStyles[2] = new RowStyle(SizeType.Absolute, 1F);
                tableLayoutPanel1.RowStyles[3] = new RowStyle(SizeType.Absolute, 1F);
                tableLayoutPanel1.RowStyles[0] = new RowStyle(SizeType.Percent, 50F);
                tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Percent, 50F);

                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb1, url1));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb2, url2));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb3, url3));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb4, url4));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb5, url5));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb6, url6));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb7, url7));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb8, url8));
            }
        }

        private delegate void InvokeCallBack(LoadPhotoState state);

        public void LoadPhotoAsy(object state)
        {
            try
            {
                LoadPhotoState state1 = (LoadPhotoState)state;
                if (state1.Pb.InvokeRequired)
                {
                    InvokeCallBack d = new InvokeCallBack(LoadPhotoAsy);
                    this.Invoke(d, new object[] { state1 });
                }
                else
                {
                    if (state1.Pb.Image != null)
                    {
                        state1.Pb.Image.Dispose();
                        state1.Pb.Image = null;
                    }
                    state1.Pb.ImageLocation = state1.Url;
                }
            }
            catch { }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //加载图片
                tableLayoutPanel1.RowStyles[2] = new RowStyle(SizeType.Absolute, 0F);
                tableLayoutPanel1.RowStyles[3] = new RowStyle(SizeType.Absolute, 0F);
                tableLayoutPanel1.RowStyles[0] = new RowStyle(SizeType.Percent, 50F);
                tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Percent, 50F);


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

        private void btnDown_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //加载图片
                tableLayoutPanel1.RowStyles[0] = new RowStyle(SizeType.Absolute, 0F);
                tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Absolute, 0F);
                tableLayoutPanel1.RowStyles[2] = new RowStyle(SizeType.Percent, 50F);
                tableLayoutPanel1.RowStyles[3] = new RowStyle(SizeType.Percent, 50F);

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

        private void rbnLoad_CheckedChanged(object sender, EventArgs e)
        {
            
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (row == null)
                {
                    return;
                }

                gbDis.Text = rbnLoad.Checked ? "装船确认" : "卸船确认";
                row.DOCK_STATUS = rbnLoad.Checked ? 0 : 1;
                bll.SetLoadDis(rbnLoad.Checked, row);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            //txtContNo_Leave(this, null);
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (row == null)
                {
                    MessageBox.Show("请先获取新记录");
                    return;
                }

                DialogResult drs = MessageBox.Show("确认无需处理？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (drs == DialogResult.No)
                {
                    return;
                }

                bll.MarkSkip();

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

        //箱号失去焦点
        private void txtContNo_Leave(object sender, EventArgs e)
        {
            txtContNoEntered = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (row == null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(txtContNo.Text.Trim()))
                {
                    return;
                }

                //检验箱号规则
                bool valid = ValidateBLL.CheckContNo(txtContNo.Text.Trim());
                if (!valid)
                {
                    DialogResult drs = MessageBox.Show(string.Format("箱号{0}不符合规则，是否强行通过？", txtContNo.Text.Trim()), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (drs == DialogResult.No)
                    {
                        return;
                    }
                }

                if (Convert.ToDecimal(txtMove1.Text) > 1)
                {
                    if (row2.CONTAINER_NO != txtContNo.Text.Trim())
                    {
                        row2.RCONTAINER_NO = txtContNo.Text.Trim();
                        cntResult = bll.InitCntCtrl(row2);
                    }
                }
                else
                {
                    if (row.CONTAINER_NO != txtContNo.Text.Trim())
                    {
                        row.RCONTAINER_NO = txtContNo.Text.Trim();
                        cntResult = bll.InitCntCtrl(row);
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

        private DataSet1.T_OCRX_CNTRow NewRow(DataSet1.T_OCRX_CNTRow oldNew)
        {
            DataSet1.T_OCRX_CNTDataTable dt = new DataSet1.T_OCRX_CNTDataTable();
            DataSet1.T_OCRX_CNTRow rowN = dt.NewT_OCRX_CNTRow();
            rowN.ItemArray = oldNew.ItemArray;
            rowN.CONTAINER_NO = string.Empty;
            rowN.RCONTAINER_NO = string.Empty;
            rowN.DOCK_ID = -1;
            rowN.CSTATUS = 1;
            rowN.CTOSERRORCODE = "0";
            rowN.CONTAINERID = 0;

            return rowN;
        }

        bool txtContNoEntered = false;
        private void txtContNo_Enter(object sender, EventArgs e)
        {
            txtContNoEntered = true;
            txtContNo.SelectAll();
        }

        //下一步确认
        private void btnNextStep_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (row == null)
                {
                    MessageBox.Show("请先获取新记录");
                    return;
                }

                if (txtContNoEntered)
                {
                    txtContNo_Leave(null, null);
                }
                if (txtISOEntered)
                {
                    txtISO_Leave(null, null);
                }


                if (Convert.ToDecimal(txtMove1.Text) > 1)
                {
                    //if (row2.CONTAINER_NO != txtContNo.Text.Trim() && row2.RCONTAINER_NO != txtContNo.Text.Trim())
                    //{
                    //    row2.RCONTAINER_NO = txtContNo.Text.Trim();
                    //    cntResult = bll.InitCntCtrl(row2);
                    //}

                    bll.NextStep(row2);
                }
                else
                {
                    //if (row.CONTAINER_NO != txtContNo.Text.Trim() && row.RCONTAINER_NO != txtContNo.Text.Trim())
                    //{
                    //    row.RCONTAINER_NO = txtContNo.Text.Trim();
                    //    cntResult = bll.InitCntCtrl(row);
                    //}

                    bll.NextStep(row);
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

        private void numBndl_ValueChanged(object sender, EventArgs e)
        {
            if (txtBndl1.Text == "0" && numBndl.Value > 1)
            {
                txtBndl1.Text = "1";
                //row.MAINCONTAINERNO = txtContNo.Text.Trim();
            }
            else if (numBndl.Value == 0)
            {
                txtBndl1.Text = "0";
            }
            else if (numBndl.Value == 1)
            {
                txtBndl1.Text = "0";
            }
        }

        //转异常处理
        private void btnException_Click(object sender, EventArgs e)
        {
            //CtosAPIBLL.SM001001("OCR", "123456", "172.16.1.1");

            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (row == null)
                {
                    MessageBox.Show("请先获取新记录");
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

                bll.ClearData();

                DialogResult drs1 = MessageBox.Show("转异常成功！\r\n是否继续下一组图片？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (drs1 == DialogResult.Yes)
                {
                    btnNext.PerformClick();
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


        private void rbnDis_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtISO_Leave(object sender, EventArgs e)
        {
            txtISOEntered = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (row == null)
                {
                    return;
                }

                //检验ISO
                IsoCode iso = bll.CheckIsoCode(txtISO.Text.Trim());
                //bll.iso = iso;
                if (iso == null)
                {
                    MessageBox.Show("ISOCODE未定义");
                    return;
                }

                if (Convert.ToDecimal(txtMove1.Text) > 1)
                {
                    if (row2.CONTAINER_SHAPE != txtISO.Text.Trim())
                    {
                        row2.RCONTAINER_SHAPE = txtISO.Text.Trim();
                        //cntResult = bll.InitCntCtrl(row);
                    }
                }
                else
                {
                    if (row.CONTAINER_SHAPE != txtISO.Text.Trim())
                    {
                        row.RCONTAINER_SHAPE = txtISO.Text.Trim();
                        //cntResult = bll.InitCntCtrl(row);
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


        bool txtISOEntered = false;
        private void txtISO_Enter(object sender, EventArgs e)
        {
            txtISOEntered = true;
            txtISO.SelectAll();
        }

        //双击放大图片
        private void ShowBigPicture(int index)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                List<string> urls = new List<string>();
                urls.Add(pb1.ImageLocation);
                urls.Add(pb2.ImageLocation);
                urls.Add(pb3.ImageLocation);
                urls.Add(pb4.ImageLocation);
                urls.Add(pb5.ImageLocation);
                urls.Add(pb6.ImageLocation);
                urls.Add(pb7.ImageLocation);
                urls.Add(pb8.ImageLocation);

                OCR.WIN.fmBigPicture fm = new OCR.WIN.fmBigPicture(urls, index);
                fm.ShowDialog();
                fm.BringToFront();

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

        private void pb1_DoubleClick(object sender, EventArgs e)
        {
            ShowBigPicture(0);
        }

        private void pb2_DoubleClick(object sender, EventArgs e)
        {
            ShowBigPicture(1);
        }

        private void pb3_DoubleClick(object sender, EventArgs e)
        {
            ShowBigPicture(2);
        }

        private void pb4_DoubleClick(object sender, EventArgs e)
        {
            ShowBigPicture(3);
        }

        private void pb5_DoubleClick(object sender, EventArgs e)
        {
            ShowBigPicture(4);
        }

        private void pb6_DoubleClick(object sender, EventArgs e)
        {
            ShowBigPicture(5);
        }

        private void pb7_DoubleClick(object sender, EventArgs e)
        {
            ShowBigPicture(6);
        }

        private void pb8_DoubleClick(object sender, EventArgs e)
        {
            ShowBigPicture(7);
        }

        private void numMoves_KeyPress(object sender, KeyPressEventArgs e)
        {
            //B
            if (e.KeyChar == 98 || e.KeyChar == 66)
            {
                btnDown.PerformClick();
            }
            //F
            else if (e.KeyChar == 70 || e.KeyChar == 102)
            {
                btnUp.PerformClick();
            }
            //U
            else if (e.KeyChar == 117 || e.KeyChar == 85)
            {
                this.btnSkip.PerformClick();
            }
            
        }

        private void fmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopThreads();
        }

        private void btnSkip_Click_1(object sender, EventArgs e)
        {
            //DateTime d1 = DateTime.Now;
            //CtosAPIBLL.CM005001("YJTU0000001", @"TuF5efc795YhKbQRvka7nMMrrp9Owb60ouvC6+RziV3ihuf/uVbTcoMXPmfXlJ6KLDOKb9S2wd0Jhe57slC8bhYVyK5SdAibkIA91J7vINkV/LoA3L7dv4J38OZQ870hbAz288tGjzUeM8bkfYKThhNJ8qzEbIKtRvo6v8q3M5fjjCpVvOEd6b+ELMb0+T7qD21hwH5CK0CEjRQlQadHISnuk6VUygKtzGtgiweYk6vUrwN3L3T4i7egZGRXcmRv8ByZUjXzTHQ=");
            ////CtosAPIBLL.CM005001("MEDU6242909", @"TuF5efc795YhKbQRvka7nMMrrp9Owb60ouvC6+RziV3ihuf/uVbTcoMXPmfXlJ6KLDOKb9S2wd0Jhe57slC8bhYVyK5SdAibkIA91J7vINkV/LoA3L7dv4J38OZQ870hbAz288tGjzUeM8bkfYKThhNJ8qzEbIKtRvo6v8q3M5fjjCpVvOEd6b+ELMb0+T7qD21hwH5CK0CEjRQlQadHISnuk6VUygKtzGtgiweYk6vUrwN3L3T4i7egZGRXcmRv8ByZUjXzTHQ=");
            //MessageBox.Show(((TimeSpan)DateTime.Now.Subtract(d1)).Seconds.ToString());

            //return;

            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (row == null)
                {
                    MessageBox.Show("请先获取新记录");
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

                bll.MarkSkip();

                bll.ClearData();

                DialogResult drs1 = MessageBox.Show("无需处理成功！\r\n是否继续下一组图片？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (drs1 == DialogResult.Yes)
                {
                    btnNext.PerformClick();
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


        

        private void WriteLog(string text)
        {
            string path = @"d:\ocrlog.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(text);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.Write(text);
                }
            }
        }

        private void numMoves_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (row != null)
                {
                    row.CTYPE = numMoves.Value == 1 ? 0 : 1;
                }

                if (row2 != null)
                {
                    row2.CTYPE = numMoves.Value == 1 ? 0 : 1;
                }
                else
                {
                    row2 = NewRow(row);
                }

                if (numMoves.Value == 1)
                {
                    txtMove1.Text = "1";
                }
            }
            catch
            {
            }
        }

        private bool ScalerEnable = false;

        //图片放大镜
        private int scale = 1;
        //private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelScaler;
        private System.Windows.Forms.PictureBox pictureBoxScaler;
        private void InitScaler()
        {
            if (ScalerEnable)
            {
                this.panelScaler = new System.Windows.Forms.Panel();
                this.pictureBoxScaler = new System.Windows.Forms.PictureBox();

                this.panelScaler.SuspendLayout();

                this.pictureBoxScaler.Location = new System.Drawing.Point(0, 0);
                this.pictureBoxScaler.Name = "pictureBoxScaler";
                this.pictureBoxScaler.Size = new System.Drawing.Size(80, 80);
                this.pictureBoxScaler.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBoxScaler.TabIndex = 0;
                this.pictureBoxScaler.TabStop = false;

                this.panelScaler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.panelScaler.Controls.Add(this.pictureBoxScaler);
                this.panelScaler.Enabled = false;
                this.panelScaler.Location = new System.Drawing.Point(50, 50);
                this.panelScaler.Name = "panelScaler";
                this.panelScaler.Size = new System.Drawing.Size(80, 80);

                this.panelScaler.ResumeLayout(false);
            }
            //this.panelScaler.BringToFront();
        }
        private void Scaling(Image img, PictureBox pic, System.Windows.Forms.MouseEventArgs e)
        {
            if (ScalerEnable)
            {
                try
                {
                    if (img != null)
                    {
                        if (!pic.Controls.Contains(this.panelScaler))
                        {
                            pic.Controls.Add(this.panelScaler);
                        }

                        if (pictureBoxScaler.Image != null)
                        {
                            pictureBoxScaler.Image.Dispose();
                            pictureBoxScaler.Image = null;
                        }

                        pictureBoxScaler.Width = img.Width * scale;
                        pictureBoxScaler.Height = img.Height * scale;
                        pictureBoxScaler.Image = (Image)img.Clone();

                        int left = (int)(e.X * (1.0 * img.Width / pic.Width) * scale);
                        int top = (int)(e.Y * (1.0 * img.Height / pic.Height) * scale);

                        left -= panelScaler.Width / 2;
                        top -= panelScaler.Height / 2;

                        pictureBoxScaler.Left = -left;
                        pictureBoxScaler.Top = -top;

                        pic.Refresh();
                    }
                    panelScaler.Top = e.Y - panelScaler.Height / 2;
                    panelScaler.Left = e.X - panelScaler.Width / 2;
                }
                catch
                {
                }
            }
        }

        private void pb1_MouseMove(object sender, MouseEventArgs e)
        {
            Scaling(pb1.Image, pb1, e);
        }

        private void pb2_MouseMove(object sender, MouseEventArgs e)
        {
            Scaling(pb2.Image, pb2, e);
        }

        private void pb2_MouseLeave(object sender, EventArgs e)
        {
            if (ScalerEnable)
            {
                try
                {
                    pb2.Controls.Remove(this.panelScaler);
                }
                catch { }
            }
        }

        private void pb1_MouseLeave(object sender, EventArgs e)
        {
            if (ScalerEnable)
            {
                try
                {
                    pb1.Controls.Remove(this.panelScaler);
                }
                catch { }
            }
        }

        private void pb3_MouseMove(object sender, MouseEventArgs e)
        {
            Scaling(pb3.Image, pb3, e);
        }

        private void pb3_MouseLeave(object sender, EventArgs e)
        {
            if (ScalerEnable)
            {
                try
                {
                    pb3.Controls.Remove(this.panelScaler);
                }
                catch { }
            }
        }

        private void pb4_MouseMove(object sender, MouseEventArgs e)
        {
            Scaling(pb4.Image, pb4, e);
        }

        private void pb4_MouseLeave(object sender, EventArgs e)
        {
            if (ScalerEnable)
            {
                try
                {
                    pb4.Controls.Remove(this.panelScaler);
                }
                catch { }
            }
        }

        private void pb5_MouseMove(object sender, MouseEventArgs e)
        {
            Scaling(pb5.Image, pb5, e);
        }

        private void pb5_MouseLeave(object sender, EventArgs e)
        {
            if (ScalerEnable)
            {
                try
                {
                    pb5.Controls.Remove(this.panelScaler);
                }
                catch { }
            }
        }

        private void pb6_MouseMove(object sender, MouseEventArgs e)
        {
            Scaling(pb6.Image, pb6, e);
        }

        private void pb6_MouseLeave(object sender, EventArgs e)
        {
            if (ScalerEnable)
            {
                try
                {
                    pb6.Controls.Remove(this.panelScaler);
                }
                catch { }
            }
        }

        private void pb7_MouseMove(object sender, MouseEventArgs e)
        {
            Scaling(pb7.Image, pb7, e);
        }

        private void pb7_MouseLeave(object sender, EventArgs e)
        {
            if (ScalerEnable)
            {
                try
                {
                    pb7.Controls.Remove(this.panelScaler);
                }
                catch { }
            }
        }

        private void pb8_MouseMove(object sender, MouseEventArgs e)
        {
            Scaling(pb8.Image, pb8, e);
        }

        private void pb8_MouseLeave(object sender, EventArgs e)
        {
            if (ScalerEnable)
            {
                try
                {
                    pb8.Controls.Remove(this.panelScaler);
                }
                catch { }
            }
        }

        private void ckbIsDmg_CheckedChanged(object sender, EventArgs e)
        {
            txtDmg.Enabled = ckbIsDmg.Checked;

            if (!ckbIsDmg.Checked)
            {
                txtDmg.Text = string.Empty;
            }
        }

        private void ckbDanger_CheckedChanged(object sender, EventArgs e)
        {
            txtImdg1.Enabled = ckbDanger.Checked;
            txtImdg2.Enabled = ckbDanger.Checked;
            txtImdg3.Enabled = ckbDanger.Checked;

            if (!ckbDanger.Checked)
            {
                txtImdg1.Text = string.Empty;
                txtImdg2.Text = string.Empty;
                txtImdg3.Text = string.Empty;
            }
        }

        private void ckbOOG_CheckedChanged(object sender, EventArgs e)
        {
            gpOOG.Enabled = ckbOOG.Checked;

            if (!ckbOOG.Checked)
            {
                txtOH.Text = string.Empty;
                txtOL.Text = string.Empty;
                txtOR.Text = string.Empty;
                txtOA.Text = string.Empty;
                txtOF.Text = string.Empty;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (row == null)
                {
                    MessageBox.Show("请先获取新记录");
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

                bll.ClearData();

                DialogResult drs1 = MessageBox.Show("转异常成功！\r\n是否继续下一组图片？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (drs1 == DialogResult.Yes)
                {
                    btnNext.PerformClick();
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