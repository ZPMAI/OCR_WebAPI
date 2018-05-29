using System;
using System.Collections.Generic;
using System.Text;

namespace OCRX.Model
{
    /// <summary>
    /// 装卸确认界面元素
    /// </summary>
    public class CntCtrl
    {
        public System.Windows.Forms.TextBox txtISO;
        public System.Windows.Forms.TextBox txtContNo;
        public System.Windows.Forms.TextBox txtFle;
        public System.Windows.Forms.RadioButton rbnDis;
        public System.Windows.Forms.RadioButton rbnLoad;
        public System.Windows.Forms.CheckBox ckbIsDmg;
        public System.Windows.Forms.TextBox txtDmg;
        public System.Windows.Forms.Button btnDmg1;
        public System.Windows.Forms.Button btnDmg2;
        public System.Windows.Forms.Button btnDmg3;
        public System.Windows.Forms.Button btnDmg4;
        public System.Windows.Forms.Button btnDmg5;
        public System.Windows.Forms.TextBox txtImdg1;
        public System.Windows.Forms.TextBox txtImdg3;
        public System.Windows.Forms.TextBox txtImdg2;
        public System.Windows.Forms.TextBox txtBndl1;
        public System.Windows.Forms.NumericUpDown numBndl;
        public System.Windows.Forms.TextBox txtOR;
        public System.Windows.Forms.TextBox txtOL;
        public System.Windows.Forms.TextBox txtOF;
        public System.Windows.Forms.TextBox txtOA;
        public System.Windows.Forms.TextBox txtOH;
        public System.Windows.Forms.NumericUpDown numMoves;
        public System.Windows.Forms.TextBox txtMove1;
        public System.Windows.Forms.CheckBox ckbOOG;
        public System.Windows.Forms.CheckBox ckbDanger;
        public System.Windows.Forms.CheckBox ckbOverDis;
        public System.Windows.Forms.Label lblLoadPos;

        public System.Windows.Forms.Label lblVessel;
        public System.Windows.Forms.Label lblVoyage;
        public System.Windows.Forms.Label lblBerth;
        public System.Windows.Forms.Label lblLinecode;
        public System.Windows.Forms.Label lblService;
        public System.Windows.Forms.Label lblShipAgent;

        public System.Windows.Forms.Button btnNext;
        public System.Windows.Forms.Label lblPicNum;
    }

}