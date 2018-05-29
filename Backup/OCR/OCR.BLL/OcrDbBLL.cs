using System;
using System.Collections.Generic;
using System.Text;

using OCR.DAL;
using OCR.Model;

namespace OCR.BLL
{
    public class OcrDbBLL
    {
        private static IDictionary<int, OcrDBPmsServer> _OcrDBPmsServer;
        /// <summary>
        /// Í¼Æ¬·þÎñÆ÷±í
        /// </summary>
        public static IDictionary<int, OcrDBPmsServer> OcrDBPmsServer1
        {
            get
            {
                if (_OcrDBPmsServer == null)
                {
                    _OcrDBPmsServer = ocrdbDAL.GetOcrDBPmsServer();
                }

                return _OcrDBPmsServer;
            }
        }
    }
}
