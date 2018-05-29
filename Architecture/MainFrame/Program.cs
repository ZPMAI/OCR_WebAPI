using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CCT.MainFrame
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。

        /// </summary>
        [STAThread]
        static void Main()
        {
            FrmLogin frmLogin = new FrmLogin();

            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
