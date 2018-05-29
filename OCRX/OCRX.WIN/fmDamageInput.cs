using OCR.BLL;
using OCR.Model;
using OCRX.BLL;
using OCRX.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OCRX.WIN
{
    public partial class fmDamageInput : Form
    {
        OCRX.BLL.MainBLL bll;

        public fmDamageInput()
        {
            InitializeComponent();
            bll = new OCRX.BLL.MainBLL();
        }

        public DamageInfo dmgInfo;
        

        private void fmDamageInput_Load(object sender, EventArgs e)
        {
            SetDamageCode();
            cmbDmgPosition.SelectedText = dmgInfo==null?"":dmgInfo.DamagePosition;
            cmbDmgType.SelectedText = dmgInfo == null ? "" : dmgInfo.DamageType;
            txtDmgSize.Text = dmgInfo == null ? "" : dmgInfo.DamageSize;
        }

        private void SetDamageCode()
        {
            try
            {
                DataTable dataPosition = bll.SelectDamagePositionCode();
                //cmbDmgPosition.DataSource = dataPosition;

                int maxSize = 0;
                System.Drawing.Graphics g = CreateGraphics();
                foreach (DataRow dr in dataPosition.Rows)
                {
                    string itemText = string.Format("{0}_{1}_{2}", dr["DAMAGECODE"].ToString(), dr["DAMAGECMEMO"].ToString(), dr["DAMAGEEMEMO"].ToString());
                    SizeF size = g.MeasureString(itemText, cmbDmgPosition.Font);
                    if (maxSize < ((int)size.Width) + 10)
                    {
                        maxSize = (int)size.Width+10;
                    }
                    cmbDmgPosition.Items.Add(itemText);
                }

                cmbDmgPosition.DropDownWidth = maxSize;

                maxSize = 0;
                DataTable dataType = bll.SelectDamageTypeCode();

                foreach (DataRow dr in dataType.Rows)
                {
                    string itemText = string.Format("{0}_{1}_{2}", dr["DAMAGECODE"].ToString(), dr["DAMAGECMEMO"].ToString(), dr["DAMAGEEMEMO"].ToString());
                    SizeF size = g.MeasureString(itemText, cmbDmgType.Font);
                    if (maxSize < ((int)size.Width)+10)
                    {
                        maxSize = (int)size.Width+10;
                    }
                    cmbDmgType.Items.Add(itemText);
                }

                cmbDmgType.DropDownWidth = maxSize;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }
        }

        private void cmbDmgPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDmgType.Focus();
        }

        private void cmbDmgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDmgSize.Focus();
        }

        private void btnAddDmg_Click(object sender, EventArgs e)
        {
            //DamageInfo dmg = new DamageInfo();
            if (dmgInfo == null)
            {
                dmgInfo = new DamageInfo();
            }
            if (string.IsNullOrEmpty(cmbDmgPosition.Text)) {
                MessageBox.Show("请选择残损位置");
                dmgInfo = new DamageInfo();
                return;
            }
            if (string.IsNullOrEmpty(cmbDmgType.Text)) {
                MessageBox.Show("请选择残损类型");
                dmgInfo = new DamageInfo();
                return;
            }
            if(string.IsNullOrEmpty(txtDmgSize.Text))
            {
                MessageBox.Show("请填写残损尺寸");
                dmgInfo = new DamageInfo();
                return;
            }
            string[] positions = cmbDmgPosition.Text.Split('_');
            dmgInfo.DamagePosition = positions[0];
            dmgInfo.DamagePositionDesc = positions[1];
            string[] types = cmbDmgType.Text.Split('_');
            dmgInfo.DamageType = types[0];
            dmgInfo.DamageTypeDesc = types[1];
            dmgInfo.DamageSize = txtDmgSize.Text;
            dmgInfo.DamagePart = cmbDmgPart.Text;
            //CallDamageInfoAPI();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CallDamageInfoAPI()
        {
            //CtosResult result = CtosAPIBLL.SM001001("OCRTEST", "123456", "172.19.1.40");
            CtosResult result = CtosAPIBLL.OP007095("5525422011679", "04HL", "TOP RAIL HOLE", "kNCUj6D3+QwmRjohRS+bSvn9HEFpiht9zjSA4lL/2xu5S2mr0XRHH3z3peTnFoZqfoGcW2wIc+OXZce8zIfYWzT9sYGY/KSUn+SxhOGF0nDtLKskCCxSiZAFznC/MVkJL2MydABYRUazVg3n9MxaVqdqN1oxGWIPJPSUZqjMvsUoGvpPel31be/FgQcsMWuiwVt+PxOe3fChHK5uG1Z3OMTNIXNj9qdc1YOSRmxFVfJZak1Y8++3GqY70W6EeptFtWQYp/8r5dE=");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
