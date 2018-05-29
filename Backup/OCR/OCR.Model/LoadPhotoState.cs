using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OCR.Model
{
    /// <summary>
    /// Õº∆¨“Ï≤Ωº”‘ÿ
    /// </summary>
    public class LoadPhotoState
    {
        public LoadPhotoState(PictureBox pb, string url)
        {
            _pb = pb;
            _url = url;


        }

        private PictureBox _pb;
        public PictureBox Pb
        {
            get
            {
                return _pb;
            }
        }

        private string _url;

        public string Url
        {
            get
            {
                return _url;
            }
        }

    }
}
