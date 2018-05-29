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
    /// 装卸作业
    /// </summary>
    public class MainBLL
    {
        public MainBLL()
        {
        }



        private static SysParms parms;
        /// <summary>
        /// 系统参数
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
