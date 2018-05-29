using OCRX.BLL;
using OCR.Model;
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
    public partial class fmDmgModify : Form
    {

        //残损记录
        DataSet1.T_OCRX_DAMAGERow[] damage;
        bool[] modified;
        SearchBLL bll = new SearchBLL();
        decimal dockid;
        string containerno;
        public fmDmgModify()
        {
            InitializeComponent();
        }

        public fmDmgModify(decimal dockid, string containerno)
        {
            InitializeComponent();
            txtContainerNo.Text = containerno;
            txtDockId.Text = dockid.ToString();
            this.dockid = dockid;
            this.containerno = containerno;
            DataSet1.T_OCRX_DAMAGEDataTable dmginfto = bll.SelectDamageRecord(dockid);
            damage = new DataSet1.T_OCRX_DAMAGERow[5];
            modified = new bool[] { false, false, false, false, false };
            for (int i = 0; i < dmginfto.Count; i++)
            {
                damage[i] = dmginfto[i];
                string dmgStr = string.Format("{0} {1}", dmginfto[i].DMGMEMO, dmginfto[i].DMGSIZE);
                switch (i)
                {
                    case 0:
                        btnDmg1.Text = dmgStr;
                        break;
                    case 1:
                        btnDmg2.Text = dmgStr;
                        break;
                    case 2:
                        btnDmg3.Text = dmgStr;
                        break;
                    case 3:
                        btnDmg4.Text = dmgStr;
                        break;
                    case 4:
                        btnDmg5.Text = dmgStr;
                        break;
                    default:
                        break;


                }
            }
        }



        private void btnDmg1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (dockid == 0)
                {
                    MessageBox.Show("请先获取新记录");
                    return;
                }

                fmDamageInput fm = new fmDamageInput();

                DialogResult drs = fm.ShowDialog();
                //DialogResult drs = MessageBox.Show("确认转异常处理？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (drs == DialogResult.No || drs == DialogResult.Cancel)
                {
                    return;
                }

                if (fm.dmgInfo == null || string.IsNullOrEmpty(fm.dmgInfo.DamagePosition) || string.IsNullOrEmpty(fm.dmgInfo.DamageType) || string.IsNullOrEmpty(fm.dmgInfo.DamageSize))
                {
                    return;
                }
                modified[0] = true;
                string dmgStr = string.Format("{0}{1}{2} {3}", fm.dmgInfo.DamagePart, fm.dmgInfo.DamagePositionDesc, fm.dmgInfo.DamageTypeDesc, fm.dmgInfo.DamageSize);
                btnDmg1.Text = dmgStr;
                DataSet1.T_OCRX_DAMAGERow d = new DataSet1.T_OCRX_DAMAGEDataTable().NewT_OCRX_DAMAGERow();
                d.DMGCODE = fm.dmgInfo.DamagePosition + fm.dmgInfo.DamageType;
                d.DMGMEMO = fm.dmgInfo.DamagePart + fm.dmgInfo.DamagePositionDesc + fm.dmgInfo.DamageTypeDesc;
                d.DMGSIZE = fm.dmgInfo.DamageSize;
                d.CREATEDBY = Config.UserId;

                if (damage == null)
                {
                    damage = new DataSet1.T_OCRX_DAMAGERow[5];
                }

                d.DOCK_ID = dockid;
                if (damage[0] != null && damage[0].ID != 0)
                {
                    damage[0].DMGCODE = fm.dmgInfo.DamagePosition + fm.dmgInfo.DamageType;
                    damage[0].DMGMEMO = fm.dmgInfo.DamagePart + fm.dmgInfo.DamagePositionDesc + fm.dmgInfo.DamageTypeDesc;
                    damage[0].DMGSIZE = fm.dmgInfo.DamageSize;
                    damage[0].UPDATEDBY = Config.UserId;

                    //bll.UpdateDamageInfo(damage[0]);
                }
                else
                {
                    damage[0] = d;
                    //bll.InsertDamageInfo(damage[0]);
                }


                toolTip1.SetToolTip(btnDmg1, dmgStr);
                //bll.MarkExpcetion();

                //bll.ClearData();

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

        private void btnDmg2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (dockid == 0)
                {
                    MessageBox.Show("请先获取新记录");
                    return;
                }

                fmDamageInput fm = new fmDamageInput();

                DialogResult drs = fm.ShowDialog();
                //DialogResult drs = MessageBox.Show("确认转异常处理？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (drs == DialogResult.No || drs == DialogResult.Cancel)
                {
                    return;
                }

                if (fm.dmgInfo == null || string.IsNullOrEmpty(fm.dmgInfo.DamagePosition) || string.IsNullOrEmpty(fm.dmgInfo.DamageType) || string.IsNullOrEmpty(fm.dmgInfo.DamageSize))
                {
                    return;
                }
                modified[1] = true;
                string dmgStr = string.Format("{0}{1}{2} {3}", fm.dmgInfo.DamagePart, fm.dmgInfo.DamagePositionDesc, fm.dmgInfo.DamageTypeDesc, fm.dmgInfo.DamageSize);
                btnDmg2.Text = dmgStr;

                DataSet1.T_OCRX_DAMAGERow d = new DataSet1.T_OCRX_DAMAGEDataTable().NewT_OCRX_DAMAGERow();
                d.DMGCODE = fm.dmgInfo.DamagePosition + fm.dmgInfo.DamageType;
                d.DMGMEMO = fm.dmgInfo.DamagePart + fm.dmgInfo.DamagePositionDesc + fm.dmgInfo.DamageTypeDesc;
                d.DMGSIZE = fm.dmgInfo.DamageSize;
                d.CREATEDBY = Config.UserId;

                if (damage == null)
                {
                    damage = new DataSet1.T_OCRX_DAMAGERow[5];
                }

                d.DOCK_ID = dockid;
                if (damage[1] != null && damage[1].ID != 0)
                {
                    damage[1].DMGCODE = fm.dmgInfo.DamagePosition + fm.dmgInfo.DamageType;
                    damage[1].DMGMEMO = fm.dmgInfo.DamagePart + fm.dmgInfo.DamagePositionDesc + fm.dmgInfo.DamageTypeDesc;
                    damage[1].DMGSIZE = fm.dmgInfo.DamageSize;
                    damage[1].UPDATEDBY = Config.UserId;

                    //bll.UpdateDamageInfo(damage[1]);
                }
                else
                {
                    damage[1] = d;
                    //bll.InsertDamageInfo(damage[1]);
                }

                toolTip1.SetToolTip(btnDmg2, dmgStr);
                //bll.MarkExpcetion();

                //bll.ClearData();
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

        private void btnDmg3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (dockid == 0)
                {
                    MessageBox.Show("请先获取新记录");
                    return;
                }

                fmDamageInput fm = new fmDamageInput();

                DialogResult drs = fm.ShowDialog();
                //DialogResult drs = MessageBox.Show("确认转异常处理？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (drs == DialogResult.No || drs == DialogResult.Cancel)
                {
                    return;
                }

                if (fm.dmgInfo == null || string.IsNullOrEmpty(fm.dmgInfo.DamagePosition) || string.IsNullOrEmpty(fm.dmgInfo.DamageType) || string.IsNullOrEmpty(fm.dmgInfo.DamageSize))
                {
                    return;
                }
                modified[2] = true;
                string dmgStr = string.Format("{0}{1}{2} {3}", fm.dmgInfo.DamagePart, fm.dmgInfo.DamagePositionDesc, fm.dmgInfo.DamageTypeDesc, fm.dmgInfo.DamageSize);
                btnDmg3.Text = dmgStr;

                DataSet1.T_OCRX_DAMAGERow d = new DataSet1.T_OCRX_DAMAGEDataTable().NewT_OCRX_DAMAGERow();
                d.DMGCODE = fm.dmgInfo.DamagePosition + fm.dmgInfo.DamageType;
                d.DMGMEMO = fm.dmgInfo.DamagePart + fm.dmgInfo.DamagePositionDesc + fm.dmgInfo.DamageTypeDesc;
                d.DMGSIZE = fm.dmgInfo.DamageSize;
                d.CREATEDBY = Config.UserId;

                if (damage == null)
                {
                    damage = new DataSet1.T_OCRX_DAMAGERow[5];
                }

                d.DOCK_ID = dockid;
                if (damage[2] != null && damage[2].ID != 0)
                {
                    damage[2].DMGCODE = fm.dmgInfo.DamagePosition + fm.dmgInfo.DamageType;
                    damage[2].DMGMEMO = fm.dmgInfo.DamagePart + fm.dmgInfo.DamagePositionDesc + fm.dmgInfo.DamageTypeDesc;
                    damage[2].DMGSIZE = fm.dmgInfo.DamageSize;
                    damage[2].UPDATEDBY = Config.UserId;

                    //bll.UpdateDamageInfo(damage[2]);
                }
                else
                {
                    damage[2] = d;
                    //bll.InsertDamageInfo(damage[2]);
                }

                toolTip1.SetToolTip(btnDmg3, dmgStr);
                //bll.MarkExpcetion();

                //bll.ClearData();


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

        private void btnDmg4_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (dockid == 0)
                {
                    MessageBox.Show("请先获取新记录");
                    return;
                }

                fmDamageInput fm = new fmDamageInput();

                DialogResult drs = fm.ShowDialog();
                //DialogResult drs = MessageBox.Show("确认转异常处理？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (drs == DialogResult.No || drs == DialogResult.Cancel)
                {
                    return;
                }

                if (fm.dmgInfo == null || string.IsNullOrEmpty(fm.dmgInfo.DamagePosition) || string.IsNullOrEmpty(fm.dmgInfo.DamageType) || string.IsNullOrEmpty(fm.dmgInfo.DamageSize))
                {
                    return;
                }
                modified[3] = true;
                string dmgStr = string.Format("{0}{1}{2} {3}", fm.dmgInfo.DamagePart, fm.dmgInfo.DamagePositionDesc, fm.dmgInfo.DamageTypeDesc, fm.dmgInfo.DamageSize);
                btnDmg4.Text = dmgStr;

                DataSet1.T_OCRX_DAMAGERow d = new DataSet1.T_OCRX_DAMAGEDataTable().NewT_OCRX_DAMAGERow();
                d.DMGCODE = fm.dmgInfo.DamagePosition + fm.dmgInfo.DamageType;
                d.DMGMEMO = fm.dmgInfo.DamagePart + fm.dmgInfo.DamagePositionDesc + fm.dmgInfo.DamageTypeDesc;
                d.DMGSIZE = fm.dmgInfo.DamageSize;
                d.CREATEDBY = Config.UserId;

                if (damage == null)
                {
                    damage = new DataSet1.T_OCRX_DAMAGERow[5];
                }

                d.DOCK_ID = dockid;
                if (damage[3] != null && damage[3].ID != 0)
                {
                    damage[3].DMGCODE = fm.dmgInfo.DamagePosition + fm.dmgInfo.DamageType;
                    damage[3].DMGMEMO = fm.dmgInfo.DamagePart + fm.dmgInfo.DamagePositionDesc + fm.dmgInfo.DamageTypeDesc;
                    damage[3].DMGSIZE = fm.dmgInfo.DamageSize;
                    damage[3].UPDATEDBY = Config.UserId;

                    //bll.UpdateDamageInfo(damage[3]);
                }
                else
                {
                    damage[3] = d;
                    //bll.InsertDamageInfo(damage[3]);
                }

                toolTip1.SetToolTip(btnDmg4, dmgStr);
                //bll.MarkExpcetion();

                //bll.ClearData();


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

        private void btnDmg5_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (dockid == 0)
                {
                    MessageBox.Show("请先获取新记录");
                    return;
                }

                fmDamageInput fm = new fmDamageInput();

                DialogResult drs = fm.ShowDialog();
                //DialogResult drs = MessageBox.Show("确认转异常处理？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (drs == DialogResult.No || drs == DialogResult.Cancel)
                {
                    return;
                }

                if (fm.dmgInfo == null || string.IsNullOrEmpty(fm.dmgInfo.DamagePosition) || string.IsNullOrEmpty(fm.dmgInfo.DamageType) || string.IsNullOrEmpty(fm.dmgInfo.DamageSize))
                {
                    return;
                }
                modified[4] = true;
                string dmgStr = string.Format("{0}{1}{2} {3}", fm.dmgInfo.DamagePart, fm.dmgInfo.DamagePositionDesc, fm.dmgInfo.DamageTypeDesc, fm.dmgInfo.DamageSize);
                btnDmg5.Text = dmgStr;

                DataSet1.T_OCRX_DAMAGERow d = new DataSet1.T_OCRX_DAMAGEDataTable().NewT_OCRX_DAMAGERow();
                d.DMGCODE = fm.dmgInfo.DamagePosition + fm.dmgInfo.DamageType;
                d.DMGMEMO = fm.dmgInfo.DamagePart + fm.dmgInfo.DamagePositionDesc + fm.dmgInfo.DamageTypeDesc;
                d.DMGSIZE = fm.dmgInfo.DamageSize;
                d.CREATEDBY = Config.UserId;

                if (damage == null)
                {
                    damage = new DataSet1.T_OCRX_DAMAGERow[5];
                }

                d.DOCK_ID = dockid;
                if (damage[4] != null && damage[4].ID != 0)
                {
                    damage[4].DMGCODE = fm.dmgInfo.DamagePosition + fm.dmgInfo.DamageType;
                    damage[4].DMGMEMO = fm.dmgInfo.DamagePart + fm.dmgInfo.DamagePositionDesc + fm.dmgInfo.DamageTypeDesc;
                    damage[4].DMGSIZE = fm.dmgInfo.DamageSize;
                    damage[4].UPDATEDBY = Config.UserId;

                    //bll.UpdateDamageInfo(damage[4]);
                }
                else
                {
                    damage[4] = d;
                    //bll.InsertDamageInfo(damage[4]);
                }

                toolTip1.SetToolTip(btnDmg5, dmgStr);
                //bll.MarkExpcetion();

                //bll.ClearData();


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

        private void InputDamage(int i)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
          
            DeleteDamage(1);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DeleteDamage(2);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DeleteDamage(3);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DeleteDamage(4);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DeleteDamage(5);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < damage.Length; i++)
                {
                    if (modified[i])
                    {
                        if (damage[i] != null && damage[i].ID != 0)
                        {
                            bll.UpdateDamageInfo(damage[i]);
                        }
                        else
                        {
                            bll.InsertDamageInfo(damage[i]);
                        }
                    }
                }
                MessageBox.Show("保存成功.");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeleteDamage(int i)
        {
            switch(i){
                case 1:
                    btnDmg1.Text= $"输入烂箱明细({i})";
                    break;
                case 2:
                    btnDmg2.Text = $"输入烂箱明细({i})";
                    break;
                case 3:
                    btnDmg3.Text = $"输入烂箱明细({i})";
                    break;
                case 4:
                    btnDmg4.Text = $"输入烂箱明细({i})";
                    break;
                case 5:
                    btnDmg5.Text = $"输入烂箱明细({i})";
                    break;
                default:
                    return;
            }
            if (damage[i-1].ID != 0)
            {
                damage[i-1].UPDATEDBY = Config.UserId;
                bll.DeleteDamageInfo(damage[i-1]);
            }

            damage[i-1] = new DataSet1.T_OCRX_DAMAGEDataTable().NewT_OCRX_DAMAGERow();
        }
    }
}
