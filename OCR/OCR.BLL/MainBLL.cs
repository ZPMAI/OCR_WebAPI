using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;

using OCR.DAL;
using OCR.Model;
using System.Windows.Forms;

namespace OCR.BLL
{
    /// <summary>
    /// װж��ҵ
    /// </summary>
    public class MainBLL
    {
        public MainBLL()
        {
        }



        private static SysParms parms;
        /// <summary>
        /// ϵͳ����
        /// </summary>
        public static SysParms Parms
        {
            get
            {
                if (parms == null)
                {
                    parms = cctdbDAL.SelectParams();
                }

                return parms;
            }
        }

    }
}
