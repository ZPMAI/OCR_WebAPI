using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using OCR.Model;
using OCR.BLL;

namespace OCR.WIN
{
    public partial class fmShowPics : Form
    {
        public fmShowPics()
        {
            InitializeComponent();
        }

        public fmShowPics(decimal dock_id, decimal pms_id)
        {
            InitializeComponent();
            this.dock_id = dock_id;
            this.pms_id = pms_id;
        }

        decimal dock_id = 0;
        decimal pms_id = 0;

        //服务器设置
        IDictionary<int, OcrDBPmsServer> OcrDBPmsServerList;

        private void fmShowPics_Load(object sender, EventArgs e)
        {
            OcrDBPmsServerList = SearchBLL.GetOcrDBPmsServer();

            LoadPhotos();
        }

        //加载图片
        private void LoadPhotos()
        {
            using (OcrPhoto.T_OCR_PHOTODataTable ds = SearchBLL.GetPhoto(dock_id))
            {
                string url1 = string.Empty;
                string url2 = string.Empty;
                string url3 = string.Empty;
                string url4 = string.Empty;
                string url5 = string.Empty;
                string url6 = string.Empty;
                string url7 = string.Empty;
                string url8 = string.Empty;

                OcrDBPmsServer pms = OcrDBPmsServerList[Convert.ToInt32(pms_id)];

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

                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb1, url1));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb2, url2));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb3, url4));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb4, url3));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb5, url5));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb6, url6));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb7, url7));
                ThreadPool.QueueUserWorkItem(new WaitCallback(LoadPhotoAsy), new LoadPhotoState(pb8, url8));
            }
        }

        private delegate void InvokeCallBack(LoadPhotoState state);

        public void LoadPhotoAsy(object state)
        {
            LoadPhotoState state1 = (LoadPhotoState)state;
            if (state1.Pb.InvokeRequired)
            {
                InvokeCallBack d = new InvokeCallBack(LoadPhotoAsy);
                this.Invoke(d, new object[] { state1 });
            }
            else
            {
                state1.Pb.ImageLocation = state1.Url;
            }
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

                fmBigPicture fm = new fmBigPicture(urls, index);
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
    }
}