using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using OCRX.Model;
using OCR.Model;
using OCRX.DAL;
using OCR.BLL;


namespace OCRX.BLL
{
    /// <summary>
    /// 装卸作业
    /// </summary>
    public class MainBLL
    {
        public MainBLL()
        {
        }

        public MainBLL(OCRX.Model.CntCtrl cntCtrl)
        {
            this.cntCtrl = cntCtrl;
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

        /// <summary>
        /// 识别记录
        /// </summary>
        public DataSet1.T_OCRX_CNTRow row1;

        /// <summary>
        /// 识别记录2
        /// </summary>
        public DataSet1.T_OCRX_CNTRow row2;

        /// <summary>
        /// 获取新记录
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_CNTDataTable SelectNextRecord(string userName, string companyCode)
        {
            return OCRX.DAL.cctdbDAL.SelectNextRecord(userName, companyCode);
        }

        /// <summary>
        /// 读取图片数据
        /// </summary>
        /// <param name="dock_id">箱号ID</param>
        public OcrPhoto.T_OCR_PHOTODataTable GetPhoto(decimal dock_id)
        {
            return OCRX.DAL.cctdbDAL.SelectPhotos(dock_id);
        }

        /// <summary>
        /// 图片服务器表
        /// </summary>
        /// <returns></returns>
        public IDictionary<int, OcrDBPmsServer> GetOcrDBPmsServer()
        {
            return OCR.DAL.ocrdbDAL.GetOcrDBPmsServer();
        }

        /// <summary>
        /// 装卸确认UI
        /// </summary>
        public OCRX.Model.CntCtrl cntCtrl;

        /// <summary>
        /// 箱信息
        /// </summary>
        public CtosResult cntInfo;

      
        /// <summary>
        /// 桥吊作业配置
        /// </summary>
        public QcSet.T_OCR_QCSETRow qc;

        /// <summary>
        /// 填充装卸确认UI
        /// </summary>
        /// <param name="row"></param>
        public CtosResult InitCntCtrl(DataSet1.T_OCRX_CNTRow row)
        {
            //if (row == null)
            //{
            //    throw new Exception("缺少第二条识别记录！");
            //}

            ClearSpcData();

            //第二个柜以上的装卸以第一个柜为准
            if (Convert.ToDecimal(cntCtrl.txtMove1.Text) <= 1 && Convert.ToDecimal(cntCtrl.txtBndl1.Text) <= 1)
            {
                cntCtrl.rbnLoad.Checked = row.DOCK_STATUS == 0;
            }
            else
            {
                row.DOCK_STATUS = cntCtrl.rbnLoad.Checked ? 0 : 1;
            }

            cntCtrl.txtContNo.Enabled = true;

            string cntNo = !string.IsNullOrEmpty(row.RCONTAINER_NO) ? row.RCONTAINER_NO : row.CONTAINER_NO;

            cntCtrl.numMoves.Value = (row.IsCTYPENull() ? 0 : row.CTYPE) + 1;
            if (row2 == null)
            {
                cntCtrl.numMoves.Value = 1;
            }
            cntCtrl.txtContNo.Text = cntNo;
            cntCtrl.txtISO.Text = row.CONTAINER_SHAPE;

            cntCtrl.lblPicNum.Text = row.IsPIC_NUMNull() ? "0" : row.PIC_NUM.ToString();

            //QC作业配置
            qc = OCR.DAL.cctdbDAL.SelectQCSet(row.TRVAL_NO);

            //if (qc == null)
            //{
            //    throw new Exception(string.Format("桥吊{0}缺少作业配置", qc.TRVALCRANE_NO));
            //}

            //if (qc.STATUS != "作业中")
            //{
            //    throw new Exception(string.Format("桥吊{0}的作业配置状态有误", qc.TRVALCRANE_NO));
            //}

            cntCtrl.lblBerth.Text = row.BERTH_NUM;
            cntCtrl.lblLinecode.Text = row.IsLINECODENull() ? string.Empty : row.LINECODE;
            cntCtrl.lblService.Text = row.IsSERVICECODENull() ? string.Empty : row.SERVICECODE;
            cntCtrl.lblShipAgent.Text = row.IsSHIPAGENTNull() ? string.Empty : row.SHIPAGENT;
            cntCtrl.lblVessel.Text = row.SHIP_CODE;
            cntCtrl.lblVoyage.Text = row.C_VOYAGE;

            //从CTOS查找该箱其他信息
            if (cntNo != "未识别")
            {
                //查箱信息
                cntInfo = OCR.BLL.CtosAPIBLL.CM005001(cntNo, qc.TICKET_ID);

                //分装卸处理
                if (IsLoad())
                {
                    //装
                    cntCtrl.rbnLoad.Checked = true;
                }
                else
                {
                    //卸
                    cntCtrl.rbnLoad.Checked = false;
                }

                if (cntInfo.ERRORCODE != CtosAPIBLL.SUCCESSCODE || cntInfo.DS.Tables[0].Rows.Count == 0)
                {
                    cntCtrl.txtContNo.ForeColor = Color.Red;
                    return null;
                }

                cntCtrl.txtContNo.ForeColor = Color.Black;

                cntCtrl.txtFle.Text = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["EMPTYFULL"].ToString();
                row.EMPTYFULL = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["EMPTYFULL"].ToString();
                cntCtrl.lblLoadPos.Text = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["PLANVESSELCELL"].ToString();
                

                cntCtrl.ckbDanger.Checked = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["ISIMDG"].ToString() == "Y";
                cntCtrl.ckbIsDmg.Checked = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["ISDAMAGE"].ToString() == "Y";
                cntCtrl.ckbOOG.Checked = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["ISOVERTOP"].ToString() == "Y";
                row.ISREEF = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["ISREEF"].ToString();
                row.ISIMDG = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["ISIMDG"].ToString();
                row.ISDAMAGE = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["ISDAMAGE"].ToString();
                row.ISOVERTOP = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["ISOVERTOP"].ToString();
                row.ISBIND = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["ISBIND"].ToString();

                row.INAIM = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["INAIM"].ToString();
                row.CONTAINERTYPE = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["CONTAINERTYPE"].ToString();
                row.CONTAINER_HEIGHT = Convert.ToDecimal(cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["CONTAINERHEIGHT"]);
                row.CONTAINER_SIZE = Convert.ToDecimal(cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["CONTAINERSIZE"]);
                row.FORMLOCK = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["FORMLOCK"].ToString();
                row.TERMINALLOCK = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["TERMINALLOCK"].ToString();
                row.LOCTYPE = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["LOCTYPE"].ToString();

                if (cntInfo.DS.Tables["CM_CONTAINERS"].Columns.Contains("CONTAINEROWNER"))
                {
                    row.LINECODE = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["CONTAINEROWNER"].ToString();
                }
                if (row.ISOVERTOP == "Y")
                {
                    cntCtrl.txtOF.Text = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OVERFRONT"].ToString();
                    cntCtrl.txtOA.Text = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OVERBEHIND"].ToString();
                    cntCtrl.txtOL.Text = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OVERLEFT"].ToString();
                    cntCtrl.txtOR.Text = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OVERRIGHT"].ToString();
                    cntCtrl.txtOH.Text = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OVERTOP"].ToString();
                }

                row.ISOVERDIS = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["ISOVERFLOW"].ToString();
                if (row.ISOVERDIS == "N" && row.LOCTYPE == "V" && row.INAIM == "S")
                {
                    //如果在船上，且不是溢卸，要增加判断进港类型，如果是S，应判断为溢卸
                    row.ISOVERDIS = "Y";
                }

                //危标
                //危标等级
                if (cntInfo.DS.Tables.Contains("CM_CONTAINERIMDGINFO"))
                {
                    try
                    {
                        int i = 1;
                        foreach (DataRow drDanger in cntInfo.DS.Tables["CM_CONTAINERIMDGINFO"].Rows)
                        {
                            switch (i)
                            {
                                case 1:
                                    cntCtrl.txtImdg1.Text = drDanger["IMDGLEVEL"].ToString();
                                    break;
                                case 2:
                                    cntCtrl.txtImdg2.Text = drDanger["IMDGLEVEL"].ToString();
                                    break;
                                case 3:
                                    cntCtrl.txtImdg3.Text = drDanger["IMDGLEVEL"].ToString();
                                    break;
                            }
                            i++;
                        }
                    }
                    catch { }
                }

                //不自动带出打捆柜数量
                if (cntInfo.DS.Tables[1].Rows.Count > 1 && IsLoad())
                {
                    cntCtrl.numBndl.Value = cntInfo.DS.Tables["CM_CONTAINERS_BINDINFO"].Rows.Count + 1;
                    cntCtrl.txtBndl1.Text = "1";
                    row.MAINCONTAINERNO = cntNo; //主箱号
                }
                else if (IsLoad())
                {
                    cntCtrl.numBndl.Value = 1;
                    cntCtrl.txtBndl1.Text = "0";
                    row.MAINCONTAINERNO = string.Empty;
                }

                return cntInfo;

            }

            return null;
        }

        /// <summary>
        /// 下一步
        /// </summary>
        public void NextStep(DataSet1.T_OCRX_CNTRow row)
        {
            if (row == null)
            {
                throw new Exception("请先获取新记录！");
            }

            string cntNo = GetCntNo(row);

            //如果吊次大于1，或者是打捆柜，进入下一屏
            //if (IsLoad())
            //{
            //装船
            //打捆柜在最后一个柜显示完后，再提交，其他逐柜提交
            if (IsBind())
            {
                //确认
                if (IsLoad())
                {
                    bool rtn = LoadConfirm(row);
                    if (rtn == false)
                    {
                        return;
                    }
                }
                else
                {
                    bool isOK = DisConfirm(row);
                    if (!isOK)
                    {
                        return;
                    }
                }

                if (cntCtrl.numBndl.Value > Convert.ToDecimal(cntCtrl.txtBndl1.Text))
                {
                    if (Convert.ToDecimal(cntCtrl.txtBndl1.Text) == 1)
                    {
                        //记录主箱号
                        row.MAINCONTAINERNO = cntCtrl.txtContNo.Text.Trim();
                        row.DOCK_ID2 = row.DOCK_ID;
                    }

                    row.DOCK_ID = -1;
                    row.ISARCHIVED = "N";
                    row.CONTAINER_NO = string.Empty;
                    row.RCONTAINER_NO = string.Empty;
                    row.CONTAINER_SHAPE = string.Empty;
                    row.RCONTAINER_SHAPE = string.Empty;
                    row.CSTATUS = 1;

                    row.ISOVERTOP = "N";
                    row.OVA = decimal.Zero;
                    row.OVF = decimal.Zero;
                    row.OVH = decimal.Zero;
                    row.OVL = decimal.Zero;
                    row.OVR = decimal.Zero;

                    row.ISOVERDIS = "N";
                    row.ISDAMAGE = "N";
                    row.Dmg = string.Empty;

                    row.ISIMDG = "N";
                    row.Imdg1 = string.Empty;
                    row.Imdg2 = string.Empty;
                    row.Imdg3 = string.Empty;


                    InitCntCtrlBind(row);
                }
                else
                {
                    ClearData();

                    //全部处理确认完
                    DialogResult drs = MessageBox.Show("确认成功！\r\n是否继续下一组图片？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (drs == DialogResult.Yes)
                    {
                        cntCtrl.btnNext.PerformClick();
                    }
                }

                
            }
            else
            {
                //确认
                if (IsLoad())
                {
                    bool rtn = LoadConfirm(row);
                    if (rtn == false)
                    {
                        return;
                    }
                }
                else
                {                   
                    bool isOK = DisConfirm(row);
                    if (!isOK)
                    {
                        return;
                    }
                }
                if (cntCtrl.numMoves.Value > Convert.ToDecimal(cntCtrl.txtMove1.Text))
                {
                    cntCtrl.rbnDis.Enabled = false;
                    cntCtrl.rbnLoad.Enabled = false;

                    cntCtrl.txtMove1.Text = Convert.ToString((Convert.ToDecimal(cntCtrl.txtMove1.Text) + 1));

                    if (row2 == null)
                    {
                        row2 = NewRow(row);
                    }

                    //显示下一个柜
                    InitCntCtrl(row2);
                    cntCtrl.txtContNo.Focus();
                }
                else
                {
                    ClearData();

                    //全部处理确认完
                    DialogResult drs = MessageBox.Show("确认成功！\r\n是否继续下一组图片？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (drs == DialogResult.Yes)
                    {
                        cntCtrl.btnNext.PerformClick();
                    }

                }
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
            rowN.ISARCHIVED = "N";

            rowN.ISOVERTOP = "N";
            rowN.OVA = decimal.Zero;
            rowN.OVF = decimal.Zero;
            rowN.OVH = decimal.Zero;
            rowN.OVL = decimal.Zero;
            rowN.OVR = decimal.Zero;

            rowN.ISOVERDIS = "N";
            rowN.ISDAMAGE = "N";
            rowN.Dmg = string.Empty;

            rowN.ISIMDG = "N";
            rowN.Imdg1 = string.Empty;
            rowN.Imdg2 = string.Empty;
            rowN.Imdg3 = string.Empty;

            rowN.DOCK_ID2 = oldNew.DOCK_ID;

            return rowN;
        }

        public void ClearData()
        {
            cntCtrl.numBndl.Value = 1;
            cntCtrl.txtBndl1.Text = "0";
            cntCtrl.numMoves.Value = 1;
            cntCtrl.txtMove1.Text = "1";


        }

        public void ClearSpcData()
        {
            cntCtrl.ckbDanger.Checked = false;
            cntCtrl.txtImdg1.Text = string.Empty;
            cntCtrl.txtImdg2.Text = string.Empty;
            cntCtrl.txtImdg3.Text = string.Empty;

            cntCtrl.ckbOOG.Checked = false;
            cntCtrl.txtOA.Text = string.Empty;
            cntCtrl.txtOH.Text = string.Empty;
            cntCtrl.txtOR.Text = string.Empty;
            cntCtrl.txtOL.Text = string.Empty;
            cntCtrl.txtOR.Text = string.Empty;

            cntCtrl.ckbIsDmg.Checked = false;
            cntCtrl.txtDmg.Text = string.Empty;

            cntCtrl.ckbOverDis.Checked = false;

        }

        /// <summary>
        /// 装船确认
        /// </summary>
        /// <param name="row"></param>
        private bool LoadConfirm(DataSet1.T_OCRX_CNTRow row)
        {
            bool isOK = DisCheck(row);
            if (!isOK)
            {
                return false;
            }

            iso = CheckIsoCode(cntCtrl.txtISO.Text.Trim());

            if (iso == null)
            {
                throw new Exception("ISOCODE无效！");
            }

            int cc = checkCompanyCode(row);
            if (cc == 1)
            {
                return false;
            }
            if (cc == 0)
            {


                row.CONTAINER_SIZE = iso.CONTAINERSIZE;
                row.CONTAINER_HEIGHT = iso.CONTAINERHEIGHT;
                row.CONTAINERTYPE = iso.CONTAINERTYPE;

                InsertNewRow(row);
                row.CSTATUS = Convert.ToDecimal(Config.CStatus.Success);
                row.OPERATORNAME = Config.UserId;
                row.ISARCHIVED = "Y";
            }

            SaveRowInfo(row);

            //改写识别记录处理结果为已成功处理

            OCRX.DAL.cctdbDAL.UpdateCntStatus(row);

            return true;
        }


        /// <summary>
        /// 卸船确认
        /// </summary>
        /// <param name="row"></param>
        private bool DisConfirm(DataSet1.T_OCRX_CNTRow row)
        {
            bool isOK = DisCheck(row);
            if (!isOK)
            {
                return false;
            }

            //iso校验
            iso = CheckIsoCode(cntCtrl.txtISO.Text.Trim());

            if (iso == null)
            {
                throw new Exception("ISOCODE无效！");
            }

            bool isOverDis = false;

            int cc = checkCompanyCode(row);
            if (cc == 1)
            {
                return false;
            }
            if (cc == 0)
            {



                //溢卸判断
                string overDisMsg = string.Empty;
                isOverDis = DealOverDis(row, iso, ref overDisMsg);
                if (isOverDis)
                {
                    DialogResult drs = MessageBox.Show(string.Format("{0}\r\n确认做溢卸吗？", overDisMsg), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (drs == DialogResult.No)
                    {
                        return false;
                    }
                }

                InsertNewRow(row);                

                row.CSTATUS = Convert.ToDecimal(Config.CStatus.Success);
                row.OPERATORNAME = Config.UserId;
                row.ISARCHIVED = "Y";
            }              

            SaveRowInfo(row);
            row.ISOVERDIS = isOverDis ? "Y" : "N";
            
            //改写识别记录处理结果为已成功处理
            OCRX.DAL.cctdbDAL.UpdateCntStatus(row);

            return true;
        }

        /// <summary>
        /// 新增虚拟识别记录
        /// </summary>
        /// <param name="row"></param>
        private void InsertNewRow(DataSet1.T_OCRX_CNTRow row)
        {
            if (row.DOCK_ID == -1)
            {
                row.DOCK_ID = OCRX.DAL.cctdbDAL.SelectSeqOcrx();

                OCRX.DAL.cctdbDAL.InsertCntx(row);

                //复制图片
                OCRX.DAL.cctdbDAL.CopyPhotos(row.DOCK_ID2, row.DOCK_ID);
            }
        }

        /// <summary>
        /// 处理溢卸
        /// </summary>
        /// <param name="row"></param>
        public bool DealOverDis(DataSet1.T_OCRX_CNTRow row, IsoCode isoCode, ref string msg)
        {
            bool isOverDis = false;
            //箱号不符
            if (cntInfo.DS.Tables[0].Rows.Count == 0)
            {
                isOverDis = true;
                msg = "溢卸，该箱没有卸船计划";
            }
            else
            {
                //ctos有溢卸标识 判断是否是箱号溢卸
                if (row.ISOVERDIS == "Y")
                {
                    try
                    {
                        using (DataTable dt = cctdbDAL.SelectOverDisCnt(Convert.ToDecimal(cntInfo.DS.Tables[0].Rows[0]["CONTAINERID"])))
                        {
                            if (dt.Rows.Count > 0)
                            {
                                isOverDis = true;
                                msg = "溢卸，该箱没有卸船计划";
                            }
                        }
                    }
                    catch { }
                }
                if (isOverDis == false)
                {
                    //iso不符
                    if (isoCode.CONTAINERSIZE != row.CONTAINER_SIZE)
                    {
                        isOverDis = true;
                        msg = "溢卸，尺寸不符";
                    }
                    else if (isoCode.CONTAINERTYPE != row.CONTAINERTYPE)
                    {
                        isOverDis = true;
                        msg = "溢卸，箱型不符";
                    }
                    else if (isoCode.CONTAINERHEIGHT != row.CONTAINER_HEIGHT)
                    {
                        isOverDis = true;
                        msg = "溢卸，高度不符";
                    }

                    //危柜不符
                    else if ((row.ISIMDG == "Y" && (string.IsNullOrEmpty(cntCtrl.txtImdg1.Text.Trim()) && string.IsNullOrEmpty(cntCtrl.txtImdg2.Text.Trim()) && string.IsNullOrEmpty(cntCtrl.txtImdg3.Text.Trim())))
                   || (row.ISIMDG == "N" && (!string.IsNullOrEmpty(cntCtrl.txtImdg1.Text.Trim()) || !string.IsNullOrEmpty(cntCtrl.txtImdg2.Text.Trim()) || !string.IsNullOrEmpty(cntCtrl.txtImdg3.Text.Trim()))))
                    {
                        isOverDis = true;
                        msg = "溢卸，危品标识不符";
                    }
                }
            }

            return isOverDis;

        }

        /// <summary>
        /// 箱号
        /// </summary>
        /// <returns></returns>
        public string GetCntNo(DataSet1.T_OCRX_CNTRow row)
        {
            //return !string.IsNullOrEmpty(row.RCONTAINER_NO) ? row.RCONTAINER_NO : row.CONTAINER_NO;
            return cntCtrl.txtContNo.Text.Trim();
        }

        private void DealDisCommit(DataSet1.T_OCRX_CNTRow row)
        {
            row.CTOSERRORMSG = string.Empty;
            row.CTOSERRORCODE = string.Empty;



            row.RCONTAINER_NO = row.IsRCONTAINER_NONull() ? string.Empty : row.RCONTAINER_NO;
            row.RCONTAINER_SHAPE = row.IsRCONTAINER_SHAPENull() ? string.Empty : row.RCONTAINER_SHAPE;
            //改写识别记录处理结果为已成功处理
            row.CSTATUS = Convert.ToDecimal(Config.CStatus.Success);
            row.OPERATORNAME = Config.UserId;
            row.ISARCHIVED = "Y";
            OCRX.DAL.cctdbDAL.UpdateCntStatus(row);

        }

        /// <summary>
        /// iso
        /// </summary>
        public IsoCode iso;

        public IsoCode CheckIsoCode(string isocode)
        {
            IsoCode isoCode = OCR.DAL.ctosDAL.SelectIsoCode(isocode);

            return isoCode;
        }

        /// <summary>
        /// 卸船确认数据校验
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool DisCheck(DataSet1.T_OCRX_CNTRow row)
        {
            string cntNo = GetCntNo(row);

            if (string.IsNullOrEmpty(cntNo))
            {
                throw new Exception("请输入箱号");
            }
            if (string.IsNullOrEmpty(cntCtrl.txtISO.Text.Trim()))
            {
                throw new Exception("请输入ISOCODE");
            }
            if (string.IsNullOrEmpty(cntCtrl.txtFle.Text.Trim()))
            {
                throw new Exception("请输入空重");
            }

            if (cntCtrl.ckbDanger.Checked && (string.IsNullOrEmpty(cntCtrl.txtImdg1.Text.Trim()) && 
                string.IsNullOrEmpty(cntCtrl.txtImdg1.Text.Trim()) && string.IsNullOrEmpty(cntCtrl.txtImdg1.Text.Trim())))
            {
                throw new Exception("请输入危标");
            }

            if (cntCtrl.ckbOOG.Checked && (string.IsNullOrEmpty(cntCtrl.txtOA.Text.Trim()) &&
                string.IsNullOrEmpty(cntCtrl.txtOF.Text.Trim()) && string.IsNullOrEmpty(cntCtrl.txtOH.Text.Trim()) &&
                string.IsNullOrEmpty(cntCtrl.txtOL.Text.Trim()) && string.IsNullOrEmpty(cntCtrl.txtOR.Text.Trim())))
            {
                throw new Exception("请输入超限值");
            }

            if (!string.IsNullOrEmpty(cntCtrl.txtOA.Text.Trim()) && !ValidateBLL.CheckNumber(cntCtrl.txtOA.Text.Trim(), 1, 999))
            {
                throw new Exception("OA，请输入正确的超限值");
            }
            if (!string.IsNullOrEmpty(cntCtrl.txtOF.Text.Trim()) && !ValidateBLL.CheckNumber(cntCtrl.txtOF.Text.Trim(), 1, 999))
            {
                throw new Exception("OF，请输入正确的超限值");
            }
            if (!string.IsNullOrEmpty(cntCtrl.txtOH.Text.Trim()) && !ValidateBLL.CheckNumber(cntCtrl.txtOH.Text.Trim(), 1, 999))
            {
                throw new Exception("OH，请输入正确的超限值");
            }
            if (!string.IsNullOrEmpty(cntCtrl.txtOL.Text.Trim()) && !ValidateBLL.CheckNumber(cntCtrl.txtOL.Text.Trim(), 1, 999))
            {
                throw new Exception("OL，请输入正确的超限值");
            }
            if (!string.IsNullOrEmpty(cntCtrl.txtOR.Text.Trim()) && !ValidateBLL.CheckNumber(cntCtrl.txtOR.Text.Trim(), 1, 999))
            {
                throw new Exception("OR，请输入正确的超限值");
            }

            //以船为单位，校验是否已经确认过
            using (DataTable dt = DAL.cctdbDAL.SelectCntConfirmed(row.ENAM, cntNo, row.DOCK_STATUS))
            {
                if (dt.Rows.Count > 0)
                {
                    string msg1 = string.Format("箱号{0} 已于 {1} 被 {2} 确认过！\r\n是否继续确认？", cntNo, Convert.ToDateTime(dt.Rows[0]["finishtime"]), dt.Rows[0]["operatorname"].ToString());
                
                    DialogResult drs = MessageBox.Show(msg1, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    return drs == DialogResult.Yes;
                }
            }

            return true;
        }

        /// <summary>
        /// 填充装卸确认UI 打捆柜
        /// </summary>
        /// <param name="row"></param>
        public void InitCntCtrlBind(DataSet1.T_OCRX_CNTRow row)
        {
            int cur = Convert.ToInt32(cntCtrl.txtBndl1.Text);

            if (cur > cntCtrl.numBndl.Value)
            {
                return;
            }

            ClearSpcData();

            cntCtrl.rbnDis.Enabled = false;
            cntCtrl.rbnLoad.Enabled = false;

            //if (IsLoad())
            //{
            //    cntCtrl.txtBndl1.Text = Convert.ToString(cur + 1);

            //    DataRow dr = cntInfo.DS.Tables["CM_CONTAINERS_BINDINFO"].Rows[cur - 1];
            //    //装
            //    cntCtrl.txtContNo.Enabled = false;
            //    cntCtrl.txtContNo.Text = dr["CONTAINERNO"].ToString();
            //    cntCtrl.txtISO.Text = dr["ISOCODE"].ToString();
            //    //cntCtrl.txtTruck.Text = dr["CONTAINERNO"].ToString();


            //    cntCtrl.txtFle.Text = dr["EMPTYFULL"].ToString();

            //    cntCtrl.ckbDanger.Checked = dr["ISIMDG"].ToString() == "Y";
            //    cntCtrl.ckbIsDmg.Checked = dr["ISDAMAGE"].ToString() == "Y";
            //    cntCtrl.ckbOOG.Checked = dr["ISOVERTOP"].ToString() == "Y";               
            //}
            //else
            //{
                //查箱信息
                //cntInfo = CtosAPIBLL.CM005001(cntNo, qc.TICKET_ID);

                cntCtrl.txtContNo.Enabled = true;
                //cntCtrl.txtContNo.Text = dr["CONTAINERNO"].ToString();
                cntCtrl.txtContNo.Text = string.Empty;
                cntCtrl.txtISO.Text = string.Empty;
                cntCtrl.txtBndl1.Text = Convert.ToString(cur + 1);

                cntCtrl.txtContNo.Focus();
            //}

            //cntCtrl.txtContNo.Focus();

        }

        /// <summary>
        /// 是否装船
        /// </summary>
        /// <returns></returns>
        public bool IsLoad()
        {
            return cntCtrl.rbnLoad.Checked;
        }

        /// <summary>
        /// 是否打捆
        /// </summary>
        /// <returns></returns>
        public bool IsBind()
        {
            return cntCtrl.numBndl.Value > 1;
        }

        /// <summary>
        /// 空重
        /// </summary>
        /// <returns></returns>
        public string GetEmptyFull()
        {
            return cntCtrl.txtFle.Text.Trim();
        }

        private void SaveRowInfo(DataSet1.T_OCRX_CNTRow row)
        {
            row.RCONTAINER_NO = row.IsRCONTAINER_NONull() ? string.Empty : row.RCONTAINER_NO;
            row.RCONTAINER_SHAPE = row.IsRCONTAINER_SHAPENull() ? string.Empty : row.RCONTAINER_SHAPE;
            row.LINECODE = row.IsLINECODENull() ? string.Empty : row.LINECODE;

            row.BINDSEQ = Convert.ToDecimal(cntCtrl.txtBndl1.Text);

            row.ISOVERTOP = cntCtrl.ckbOOG.Checked ? "Y" : "N";
            row.OVA = cntCtrl.ckbOOG.Checked && !String.IsNullOrEmpty(cntCtrl.txtOA.Text.Trim()) ? Convert.ToDecimal(cntCtrl.txtOA.Text.Trim()) : decimal.Zero;
            row.OVF = cntCtrl.ckbOOG.Checked && !String.IsNullOrEmpty(cntCtrl.txtOF.Text.Trim()) ? Convert.ToDecimal(cntCtrl.txtOF.Text.Trim()) : decimal.Zero;
            row.OVH = cntCtrl.ckbOOG.Checked && !String.IsNullOrEmpty(cntCtrl.txtOH.Text.Trim()) ? Convert.ToDecimal(cntCtrl.txtOH.Text.Trim()) : decimal.Zero;
            row.OVR = cntCtrl.ckbOOG.Checked && !String.IsNullOrEmpty(cntCtrl.txtOR.Text.Trim()) ? Convert.ToDecimal(cntCtrl.txtOR.Text.Trim()) : decimal.Zero;
            row.OVL = cntCtrl.ckbOOG.Checked && !String.IsNullOrEmpty(cntCtrl.txtOL.Text.Trim()) ? Convert.ToDecimal(cntCtrl.txtOL.Text.Trim()) : decimal.Zero;

            row.ISDAMAGE = cntCtrl.ckbIsDmg.Checked ? "Y" : "N";
            row.Dmg = cntCtrl.ckbIsDmg.Checked ? cntCtrl.txtDmg.Text.Trim() : string.Empty;

            row.ISIMDG = cntCtrl.ckbDanger.Checked ? "Y" : "N";
            row.Imdg1 = cntCtrl.ckbDanger.Checked ? cntCtrl.txtImdg1.Text.Trim() : string.Empty;
            row.Imdg2 = cntCtrl.ckbDanger.Checked ? cntCtrl.txtImdg2.Text.Trim() : string.Empty;
            row.Imdg3 = cntCtrl.ckbDanger.Checked ? cntCtrl.txtImdg3.Text.Trim() : string.Empty;

            row.ISOVERDIS = "N";

            if (iso != null)
            {

                row.CONTAINER_SIZE = iso.CONTAINERSIZE;
                row.CONTAINER_HEIGHT = iso.CONTAINERHEIGHT;
                row.CONTAINERTYPE = iso.CONTAINERTYPE;
            }
            else
            {
                row.CONTAINER_HEIGHT = 0;
                row.CONTAINERTYPE = string.Empty;
            }
        }
        
        /// <summary>
        /// 转异常
        /// </summary>
        public void MarkExpcetion()
        {
            if (row1.CSTATUS != Convert.ToDecimal(Config.CStatus.Fetched))
            {
                throw new Exception("记录已成功处理，不能转异常");
            }

            SaveRowInfo(row1);

            row1.CSTATUS = Convert.ToDecimal(Config.CStatus.WaitExpHandle);
            row1.OPERATORNAME = Config.UserId;
            row1.ISARCHIVED = "N";
            row1.CONTAINERID = 0;
            OCRX.DAL.cctdbDAL.UpdateCntStatus(row1);

            if (row2 != null)
            {
                SaveRowInfo(row2);

                row2.CSTATUS = Convert.ToDecimal(Config.CStatus.WaitExpHandle);
                row2.OPERATORNAME = Config.UserId;
                row2.ISARCHIVED = "N";
                row2.CONTAINERID = 0;
                OCRX.DAL.cctdbDAL.UpdateCntStatus(row2);
            }
        }

        /// <summary>
        /// 无需处理
        /// </summary>
        public void MarkSkip()
        {
            if (row1.CSTATUS != Convert.ToDecimal(Config.CStatus.Fetched))
            {
                throw new Exception("记录已成功处理，不能转无需处理");
            }

            SaveRowInfo(row1);

            row1.CSTATUS = Convert.ToDecimal(Config.CStatus.Skip);
            row1.OPERATORNAME = Config.UserId;
            row1.ISARCHIVED = "Y";
            row1.CONTAINERID = 0;
            OCRX.DAL.cctdbDAL.UpdateCntStatus(row1);

            if (row2 != null)
            {
                SaveRowInfo(row2);

                row2.CSTATUS = Convert.ToDecimal(Config.CStatus.Skip);
                row2.OPERATORNAME = Config.UserId;
                row2.ISARCHIVED = "Y";
                row2.CONTAINERID = 0;
                OCRX.DAL.cctdbDAL.UpdateCntStatus(row2);
            }
        }

        /// <summary>
        /// 切换装卸
        /// </summary>
        /// <param name="isLoad">是否装船</param>
        public void SetLoadDis(bool isLoad, Model.DataSet1.T_OCRX_CNTRow row)
        {
            //第二个箱不能再切换装卸
            if (Convert.ToInt32(cntCtrl.txtMove1.Text) > 1 || Convert.ToInt32(cntCtrl.txtBndl1.Text) > 1)
            {
                throw new Exception("不能切换装卸！");
            }

            cntCtrl.rbnDis.Checked = !cntCtrl.rbnLoad.Checked;
        }

        /// <summary>
        /// 查剩余记录数
        /// </summary>
        /// <returns></returns>
        public int SelectLeft(string companyCode)
        {
            return OCRX.DAL.cctdbDAL.SelectLeft(companyCode);
        }


        /// <summary>
        /// 查所有未处理的异常
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_CNTDataTable SelectExcep(string companyCode)
        {
            DataSet1.T_OCRX_CNTDataTable dt = OCRX.DAL.cctdbDAL.SelectExcep(companyCode);
            foreach (DataSet1.T_OCRX_CNTRow row22 in dt)
            {
                row22.LOADDIS = row22.DOCK_STATUS == 0 ? "装" : "卸";
                row22.TWOCNTS = row22.CTYPE == 1 ? "是" : "否";
            }
            return dt;
        }

        /// <summary>
        /// 更新异常处理结果
        /// </summary>
        /// <param name="row"></param>
        public void UpdateExcepStatus(DataSet1.T_OCRX_CNTRow row)
        {
            //if (row.CSTATUS == Convert.ToDecimal(Config.CStatus.Success))
            //{
            //    throw new Exception("记录已成功提交CTOS，无需点击【处理完成】");
            //}

            //row.ISARCHIVED = "Y";
            //row.EXCEPUSER = Config.UserId;
            //row.CSTATUS = Convert.ToDecimal(Config.CStatus.Skip);

            //DAL.cctdbDAL.UpdateExcepStatus(row);

            if (row1.CSTATUS == Convert.ToDecimal(Config.CStatus.Success))
            {
                throw new Exception("记录已成功确认，无需点击【处理完成】");
            }

            row1.ISARCHIVED = "Y";
            row1.EXCEPUSER = Config.UserId;
            row1.CSTATUS = Convert.ToDecimal(Config.CStatus.Skip);

            DAL.cctdbDAL.UpdateExcepStatus(row1);

            if (row2 != null)
            {
                row2.ISARCHIVED = "Y";
                row2.EXCEPUSER = Config.UserId;
                row2.CSTATUS = Convert.ToDecimal(Config.CStatus.Skip);
                DAL.cctdbDAL.UpdateExcepStatus(row2);
            }
        }

        //检查分发公司是否正确
        public int checkCompanyCode(DataSet1.T_OCRX_CNTRow row)
        {
            try
            {
                if (!row.IsRCONTAINER_NONull() && !string.IsNullOrEmpty(row.RCONTAINER_NO))
                {
                    DispatchBLL bll = new DispatchBLL();
                    if (cntInfo == null)
                    {
                        qc = OCR.DAL.cctdbDAL.SelectQCSet(row.TRVAL_NO);
                        cntInfo = OCR.BLL.CtosAPIBLL.CM005001(GetCntNo(row), qc.TICKET_ID);
                    }
                    if (cntInfo.DS.Tables["CM_CONTAINERS"].Rows.Count > 0)
                    {
                        string velaliase = IsLoad() ? cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OUTVELALIASE"].ToString() : cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["INVELALIASE"].ToString();

                        if (!string.IsNullOrEmpty(velaliase))
                        {
                            using (DataTable dt = DAL.cctdbDAL.SelectService(velaliase))
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    if (row.ENAM != velaliase)
                                    {
                                        row.ENAM = velaliase;
                                        row.SHIP_CODE = IsLoad() ? cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OUTEVESSELNAME"].ToString() : cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["INEVESSELNAME"].ToString();
                                        row.C_VOYAGE = IsLoad() ? cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OUTBOUNDVOY"].ToString() : cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["INBOUNDVOY"].ToString();

                                    }

                                    DataRow dr = dt.Rows[0];
                                    string service = IsLoad() ? dr["outvessellinecode"].ToString() : dr["outvessellinecode"].ToString();
                                    string companyCode = bll.getCompanyCode(dr["vesseltype"].ToString(), velaliase, service, row.LINECODE);

                                    if (companyCode != row.COMPANYCODE)
                                    {
                                        //提示分发错误
                                        StringBuilder sb = new StringBuilder();
                                        sb.AppendLine(string.Format("柜号{0}分发错误！", GetCntNo(row)));
                                        sb.AppendLine("请检查柜号是否正确");
                                        sb.AppendLine("如果柜号正确，请点击【是】，该柜将重新分发给正确的外理公司");
                                        sb.AppendLine("否则，请点击【否】，请重新录入正确柜号");
                                        DialogResult drs = MessageBox.Show(sb.ToString(), "分发错误提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                        if (drs == DialogResult.Yes)
                                        {
                                            //分发错误
                                            row.COMPANYCODE = companyCode;
                                            row.OPERATORNAME = string.IsNullOrEmpty(companyCode) ? row.OPERATORNAME : string.Empty;
                                            row.CSTATUS = Convert.ToDecimal(Config.CStatus.WaitHandle);
                                            row.ISARCHIVED = string.IsNullOrEmpty(companyCode) ? "Y" : "N";
                                            return 2;
                                        }
                                        else
                                        {
                                            return 1;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }

                
            }
            catch
            {
            }

            return 0;
        }
    }
}