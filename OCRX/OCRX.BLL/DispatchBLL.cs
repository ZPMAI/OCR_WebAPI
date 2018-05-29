using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCRX.DAL;
using OCRX.Model;
using OCR.Model;
using OCR.BLL;

namespace OCRX.BLL
{
    /// <summary>
    /// ���ݷַ�����
    /// </summary>
    public class DispatchBLL
    {
        /// <summary>
        /// ��һ����� ������ҵ��������ҵ�� ���뾭��������
        /// </summary>
        public string DealExOnly()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;

            jobBLL bll = new jobBLL();

            //���Ҵ��ַ���ʶ���¼
            //�������ţ���CTOS�ӿڣ��ҳ���Ӧ�Ĵ�������
            //�������ţ����������Ч��ת�����˹�����

            using (OCR.Model.OcrCnt.T_OCR_CNTDataTable data = cctdbDAL.SelectDispatch())
            {
                using (DataSet1.T_OCRX_VESSELDataTable rules = cctdbDAL.SelectVessel())
                {
                    foreach (OCR.Model.OcrCnt.T_OCR_CNTRow row in data)
                    {
                        try
                        {
                            if (row.CONTAINER_NO == "δʶ��" && string.IsNullOrEmpty(row.RCONTAINER_NO))
                            {
                                row.CSTATUS = Convert.ToDecimal(Config.CStatus.WaitHandle);//ת�˹� 
                                row.ISARCHIVED = "N";
                            }
                            else
                            {
                                //������Ϣ
                                QcSet.T_OCR_QCSETRow qc = bll.SelectQCSet(row.TRVAL_NO);
                                CtosResult cntInfo = CtosAPIBLL.CM005001(string.IsNullOrEmpty(row.RCONTAINER_NO) ? row.CONTAINER_NO : row.RCONTAINER_NO, qc.TICKET_ID);
                                if (cntInfo.ERRORCODE != CtosAPIBLL.SUCCESSCODE || cntInfo.DS.Tables[0].Rows.Count == 0)
                                {
                                    //if (string.IsNullOrEmpty(row.RCONTAINER_NO))
                                    //{
                                    row.CSTATUS = Convert.ToDecimal(Config.CStatus.WaitHandle);//ת�˹�     
                                    row.ISARCHIVED = "N";
                                    //}
                                    //else
                                    //{
                                    //    continue;
                                    //}
                                }
                                else
                                {
                                    //��ģʽ�ְ��ֺͲ���
                                    if (row.IsENAMNull() || string.IsNullOrEmpty(row.ENAM))
                                    {
                                        //�ҵ��������Σ��ַ�������
                                        row.ENAM = row.DOCK_STATUS == 0 ? cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OUTVELALIASE"].ToString() : cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["INVELALIASE"].ToString();
                                        //���Ӧ�Ĳ�����˾
                                        OCRX.Model.DataSet1.T_OCRX_BARGERow bg = cctdbDAL.SelectBargeByVelaliase(row.ENAM);
                                        if (bg != null)
                                        {
                                            row.COMPANYCODE = bg.COMPANYCODE;
                                            row.SHIP_CODE = row.DOCK_STATUS == 0 ? cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OUTEVESSELNAME"].ToString() : cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["INEVESSELNAME"].ToString();
                                            row.C_VOYAGE = row.DOCK_STATUS == 0 ? cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OUTBOUNDVOY"].ToString() : cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["INBOUNDVOY"].ToString();

                                            row.LINECODE = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["CONTAINEROWNER"].ToString();
                                            row.ISARCHIVED = "Y";
                                            //row.OPERATORNAME = Config.SysUser;
                                            row.CONTAINERID = 0;
                                            bll.UpdateCntStatus(row);

                                            DataSet1.T_OCRX_CNTRow newRow = CopyRow(row);
                                            //�����·ַ���¼
                                            cctdbDAL.InsertCntx(newRow);
                                            //���·ַ�״̬
                                            cctdbDAL.UpdateDispatched(row.DOCK_ID, string.Empty, row.COMPANYCODE);

                                            continue;

                                        }
                                        else
                                        {
                                            //���������죬�鵵
                                            row.ISARCHIVED = "Y";
                                        }
                                    }
                                    else
                                    {
                                        //���� �Ƿ�������������ȴ�
                                        string lineCode = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["CONTAINEROWNER"].ToString();
                                        if (String.IsNullOrEmpty(lineCode))
                                        {
                                            row.CSTATUS = Convert.ToDecimal(Config.CStatus.WaitHandle);//ת�˹�     
                                            row.ISARCHIVED = "N";

                                        }
                                        else
                                        {

                                            //����ƥ������
                                            string companyCode = GetCompanyCode(row.SERVICECODE, lineCode, rules);
                                            if (!string.IsNullOrEmpty(companyCode))
                                            {
                                                DataSet1.T_OCRX_CNTRow newRow = CopyRow(row);
                                                newRow.COMPANYCODE = companyCode;

                                                //�����·ַ���¼
                                                cctdbDAL.InsertCntx(newRow);
                                                //���·ַ�״̬
                                                cctdbDAL.UpdateDispatched(row.DOCK_ID, row.LINECODE, companyCode);
                                                i++;
                                                continue;
                                                //sb.AppendLine(string.Format("���ݷַ� {0} {1}", row.TRVAL_NO, newRow.CONTAINER_NO));
                                            }
                                            else
                                            {
                                                //���������죬�鵵
                                                row.ISARCHIVED = "Y";
                                            }
                                        }
                                    }


                                }
                            }

                            row.CONTAINERID = 0;
                            bll.UpdateCntStatus(row);

                            //DataSet1.T_OCRX_CNTRow newRow = CopyRow(row);
                            ////newRow.COMPANYCODE = companyCode;

                            ////�����·ַ���¼
                            //cctdbDAL.InsertCntx(newRow);
                            ////���·ַ�״̬
                            //cctdbDAL.UpdateDispatched(row.DOCK_ID, string.Empty, row.COMPANYCODE);
                            //i++;
                            //sb.AppendLine(string.Format("���ݷַ� {0} {1}", row.TRVAL_NO, newRow.CONTAINER_NO));

                        }
                        catch { }
                    }
                }
            }

            if (i > 0)
            {
                sb.AppendFormat("���ݷַ� {0}����¼", i);
            }
            return sb.ToString();
        }

        /// <summary>
        /// �ڶ������ �ɹ�����������
        /// </summary>
        public string DealSuccessWithLinecode()
        {
            StringBuilder sb = new StringBuilder();
            //�������а��ַַ�����
            int i = 0;
            using (DataSet1.T_OCRX_VESSELDataTable rules = cctdbDAL.SelectVessel())
            {
                if (rules == null || rules.Count == 0)
                {
                    return "";
                }

                //���Ҵ��ַ���ʶ���¼

                using (OCR.Model.OcrCnt.T_OCR_CNTDataTable data = cctdbDAL.SelectDispatch2())
                {
                    foreach (OCR.Model.OcrCnt.T_OCR_CNTRow row in data)
                    {
                        try
                        {
                            //����ƥ������
                            string companyCode = GetCompanyCode(row.SERVICECODE, row.LINECODE, rules);
                            if (!string.IsNullOrEmpty(companyCode))
                            {
                                DataSet1.T_OCRX_CNTRow newRow = CopyRow(row);
                                newRow.COMPANYCODE = companyCode;

                                //�����·ַ���¼
                                cctdbDAL.InsertCntx(newRow);
                                //���·ַ�״̬
                                cctdbDAL.UpdateDispatched(row.DOCK_ID, row.LINECODE, companyCode);
                                i++;
                                //sb.AppendLine(string.Format("���ݷַ� {0} {1}", row.TRVAL_NO, newRow.CONTAINER_NO));
                            }
                        }
                        catch { }
                    }
                }
            }
            if (i > 0)
            {
                sb.AppendFormat("���ݷַ� {0}����¼", i);
            }
            return sb.ToString();
        }

        /// <summary>
        /// ��������� �ɹ�����������
        /// </summary>
        public string DealSuccessWithoutLinecode()
        {
            StringBuilder sb = new StringBuilder();
            //�������а��ַַ�����
            int i = 0;
            using (DataSet1.T_OCRX_VESSELDataTable rules = cctdbDAL.SelectVessel())
            {
                if (rules == null || rules.Count == 0)
                {
                    return "";
                }

                //���Ҵ��ַ���ʶ���¼

                using (OCR.Model.OcrCnt.T_OCR_CNTDataTable data = cctdbDAL.SelectDispatch3())
                {
                    foreach (OCR.Model.OcrCnt.T_OCR_CNTRow row in data)
                    {
                        try
                        {
                            //����ƥ������
                            string companyCode = GetCompanyCode(row.SERVICECODE, row.containerowner, rules);
                            if (!string.IsNullOrEmpty(companyCode))
                            {
                                DataSet1.T_OCRX_CNTRow newRow = CopyRow(row);
                                newRow.COMPANYCODE = companyCode;

                                //�����·ַ���¼
                                cctdbDAL.InsertCntx(newRow);
                                //���·ַ�״̬
                                cctdbDAL.UpdateDispatched(row.DOCK_ID, row.containerowner, companyCode);
                                i++;
                                //sb.AppendLine(string.Format("���ݷַ� {0} {1}", row.TRVAL_NO, newRow.CONTAINER_NO));
                            }
                        }
                        catch { }
                    }
                }
            }
            if (i > 0)
            {
                sb.AppendFormat("���ݷַ� {0}����¼", i);
            }
            return sb.ToString();
        }

        /// <summary>
        /// ���Ҳ�������
        /// </summary>
        /// <param name="shipAgent"></param>
        /// <param name="shipOwner"></param>
        /// <param name="barges"></param>
        /// <returns></returns>
        private string GetCompanyCode(string shipAgent, string shipOwner, DataSet1.T_OCRX_BARGEDataTable barges)
        {
            string companyCode = string.Empty;

            //��������ƥ�䣬����ƥ��������
            foreach (DataSet1.T_OCRX_BARGERow row in barges)
            {
                if (row.SHIPAGENT == shipAgent)
                {
                    if (row.SHIPOWNER == "ALL")
                    {
                        companyCode = row.COMPANYCODE;
                    }
                    else
                    {
                        row.SHIPOWNER = string.Format(",{0},", row.SHIPOWNER);
                        string shipOwner1 = string.Format(",{0},", shipOwner);
                        if (row.SHIPOWNER.Contains(shipOwner1))
                        {
                            companyCode = row.COMPANYCODE;
                            break;
                        }
                    }
                }
            }

            return companyCode;
        }

        /// <summary>
        /// ���Ұ�������
        /// </summary>
        /// <param name="serviceCode"></param>
        /// <param name="lineCode"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        private string GetCompanyCode(string serviceCode, string lineCode, DataSet1.T_OCRX_VESSELDataTable rules)
        {
            string companyCode = string.Empty;

            //��������ƥ�䣬��������
            foreach (DataSet1.T_OCRX_VESSELRow row in rules)
            {
                row.LINECODE = string.Format(",{0},", row.LINECODE);
                string lineCode1 = string.Format(",{0},", lineCode);

                if (row.SERVICECODE == serviceCode && row.LINECODE.Contains(lineCode1))
                {
                    companyCode = row.COMPANYCODE;
                    break;
                }

                if (row.SERVICECODE == "ALL" && row.LINECODE.Contains(lineCode1))
                {
                    companyCode = row.COMPANYCODE;
                }
            }

            return companyCode;
        }

        /// <summary>
        /// ����ʶ����Ϣ
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private DataSet1.T_OCRX_CNTRow CopyRow(OCR.Model.OcrCnt.T_OCR_CNTRow row)
        {
            DataSet1.T_OCRX_CNTRow r = new DataSet1.T_OCRX_CNTDataTable().NewT_OCRX_CNTRow();

            r.DOCK_ID = row.DOCK_ID;
            r.CONTAINER_NO = row.CONTAINER_NO;
            r.CONTAINER_SHAPE = row.CONTAINER_SHAPE;
            r.CONTAINER_SIZE = row.CONTAINER_SIZE;
            r.CTYPE = row.CTYPE;
            r.DOCK_STATUS = row.DOCK_STATUS;
            r.CSTATUS = Convert.ToDecimal(Config.CStatus.WaitHandle);
            r.TRVALCRANE_ID = row.TRVALCRANE_ID;
            r.TRVAL_NO = row.TRVAL_NO;
            r.CNAM = row.CNAM;
            r.ENAM = row.ENAM;
            r.SHIP_CODE = row.SHIP_CODE;
            r.C_VOYAGE = row.C_VOYAGE;
            r.PIC_NUM = row.PIC_NUM;
            r.MSG_INDEX = row.MSG_INDEX;
            r.CTIME = row.COPYTIME;
            r.PMS_ID = row.PMS_ID;
            r.BERTH_NUM = row.BERTH_NUM;
            r.ISARCHIVED = "N";
            r.CONTAINERID = row.IsCONTAINERIDNull() ? 0 : row.CONTAINERID;
            r.LINECODE = row.IsLINECODENull() ? string.Empty : row.LINECODE;
            r.SERVICECODE = row.IsSERVICECODENull() ? string.Empty : row.SERVICECODE;
            r.SHIPAGENT = row.IsSHIPAGENTNull() ? string.Empty : row.SHIPAGENT;
            r.SHIPOWNER = row.IsSHIPOWNERNull() ? string.Empty : row.SHIPOWNER;
            r.COMPANYCODE = row.IsCOMPANYCODENull() ? string.Empty : row.COMPANYCODE;

            return r;
        }

        /// <summary>
        /// �ж�����������˾
        /// </summary>
        /// <param name="vesselType"></param>
        /// <param name="velaliase"></param>
        /// <param name="service"></param>
        /// <param name="linecode"></param>
        /// <returns></returns>
        public string getCompanyCode(string vesselType, string velaliase, string service, string linecode)
        {
            string companyCode = string.Empty;

            if (vesselType == "B")
            {
                //����
                using (DataSet1.T_OCRX_VESSELDataTable rules = cctdbDAL.SelectVessel())
                {
                    if (rules == null || rules.Count == 0)
                    {
                        return "";
                    }

                    try
                    {
                        //����ƥ������
                        companyCode = GetCompanyCode(service, linecode, rules);
                    }
                    catch { }
                }

            }
            else
            {
                //����
                //���Ӧ�Ĳ�����˾
                OCRX.Model.DataSet1.T_OCRX_BARGERow bg = cctdbDAL.SelectBargeByVelaliase(velaliase);
                if (bg != null)
                {
                    companyCode = bg.COMPANYCODE;
                }
            }

            return companyCode;
        }


        /// <summary>
        /// ��һ����� ������ҵ��������ҵ SCT����
        /// </summary>
        public string DealExOnlySCT()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;

            jobBLL bll = new jobBLL();

            //���Ҵ��ַ���ʶ���¼
            //�������ţ���CTOS�ӿڣ��ҳ���Ӧ�Ĵ�������
            //�������ţ����������Ч������QC���ñ�Ĵ���Ӧ�Ĵ����ַ�

            using (OCR.Model.OcrCnt.T_OCR_CNTDataTable data = cctdbDAL.SelectDispatch())
            {
                using (DataSet1.T_OCRX_VESSELDataTable rules = cctdbDAL.SelectVessel())
                {
                    foreach (OCR.Model.OcrCnt.T_OCR_CNTRow row in data)
                    {
                        try
                        {
                            QcSet.T_OCR_QCSETRow qc = bll.SelectQCSet(row.TRVAL_NO);
                            if (row.CONTAINER_NO == "δʶ��" && string.IsNullOrEmpty(row.RCONTAINER_NO))
                            {
                                //��ģʽ�ְ��ֺͲ���
                                if (row.IsENAMNull() || string.IsNullOrEmpty(row.ENAM) || row.ENAM.StartsWith("8"))
                                {
                                    

                                    //�ҵ��������Σ��ַ�������
                                    row.ENAM = qc.VESSELALIASE;
                                    //���Ӧ�Ĳ�����˾
                                    OCRX.Model.DataSet1.T_OCRX_BARGERow bg = cctdbDAL.SelectBargeByVelaliase(row.ENAM);
                                    if (row.SERVICECODE == "DOM")
                                    {
                                        row.ISARCHIVED = "Y";
                                    }
                                    else if (bg != null)
                                    {
                                        row.COMPANYCODE = bg.COMPANYCODE;
                                        row.SHIP_CODE = qc.SHIP_CODE;
                                        row.C_VOYAGE = row.DOCK_STATUS == 0 ? qc.OUT_VOYAGE_CODE : qc.IN_VOYAGE_CODE;

                                        //row.LINECODE = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["CONTAINEROWNER"].ToString();
                                        row.ISARCHIVED = "Y";
                                        //row.OPERATORNAME = Config.SysUser;
                                        row.CONTAINERID = 0;
                                        bll.UpdateCntStatus(row);

                                        DataSet1.T_OCRX_CNTRow newRow = CopyRow(row);
                                        //�����·ַ���¼
                                        cctdbDAL.InsertCntx(newRow);
                                        //���·ַ�״̬
                                        cctdbDAL.UpdateDispatched(row.DOCK_ID, string.Empty, row.COMPANYCODE);

                                        continue;

                                    }
                                    else
                                    {
                                        //���������죬�鵵
                                        row.ISARCHIVED = "Y";
                                    }
                                }
                                else
                                {
                                    //���� �Ƿ�������������ȴ�
                                    string lineCode = qc.OWNER;
                                    if (String.IsNullOrEmpty(lineCode))
                                    {
                                        //row.CSTATUS = Convert.ToDecimal(Config.CStatus.WaitHandle);//ת�˹�     
                                        row.ISARCHIVED = "N";

                                    }
                                    else
                                    {

                                        //����ƥ������
                                        string companyCode = GetCompanyCode(row.SERVICECODE, lineCode, rules);
                                        if (!string.IsNullOrEmpty(companyCode))
                                        {
                                            DataSet1.T_OCRX_CNTRow newRow = CopyRow(row);
                                            newRow.COMPANYCODE = companyCode;

                                            //�����·ַ���¼
                                            cctdbDAL.InsertCntx(newRow);
                                            //���·ַ�״̬
                                            cctdbDAL.UpdateDispatched(row.DOCK_ID, row.LINECODE, companyCode);
                                            i++;
                                            continue;
                                            //sb.AppendLine(string.Format("���ݷַ� {0} {1}", row.TRVAL_NO, newRow.CONTAINER_NO));
                                        }
                                        else
                                        {
                                            //���������죬�鵵
                                            row.ISARCHIVED = "Y";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //������Ϣ
                                CtosResult cntInfo = CtosAPIBLL.CM005001(string.IsNullOrEmpty(row.RCONTAINER_NO) ? row.CONTAINER_NO : row.RCONTAINER_NO, qc.TICKET_ID);
                                if (cntInfo.ERRORCODE != CtosAPIBLL.SUCCESSCODE || cntInfo.DS.Tables[0].Rows.Count == 0)
                                {
                                    //���� �Ƿ�������������ȴ�
                                    string lineCode = qc.OWNER;
                                    //����ƥ������
                                    string companyCode = GetCompanyCode(row.SERVICECODE, lineCode, rules);
                                    row.LINECODE = lineCode;
                                    if (!string.IsNullOrEmpty(companyCode))
                                    {
                                        DataSet1.T_OCRX_CNTRow newRow = CopyRow(row);
                                        newRow.COMPANYCODE = companyCode;

                                        //�����·ַ���¼
                                        cctdbDAL.InsertCntx(newRow);
                                        //���·ַ�״̬
                                        cctdbDAL.UpdateDispatched(row.DOCK_ID, row.LINECODE, companyCode);
                                        i++;
                                        continue;
                                        //sb.AppendLine(string.Format("���ݷַ� {0} {1}", row.TRVAL_NO, newRow.CONTAINER_NO));
                                    }
                                    else
                                    {
                                        //���������죬�鵵
                                        row.ISARCHIVED = "Y";
                                    }
                                }
                                else
                                {
                                    //��ģʽ�ְ��ֺͲ���
                                    if (row.IsENAMNull() || string.IsNullOrEmpty(row.ENAM)||row.ENAM.StartsWith("8"))
                                    {
                                        //�ҵ��������Σ��ַ�������
                                        row.ENAM = row.DOCK_STATUS == 0 ? cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OUTVELALIASE"].ToString() : cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["INVELALIASE"].ToString();
                                        //���Ӧ�Ĳ�����˾
                                        OCRX.Model.DataSet1.T_OCRX_BARGERow bg = cctdbDAL.SelectBargeByVelaliase(row.ENAM);
                                        if (row.SERVICECODE == "DOM")
                                        {
                                            row.ISARCHIVED = "Y";
                                        }
                                        else if (bg != null)
                                        {
                                            row.COMPANYCODE = bg.COMPANYCODE;
                                            row.SHIP_CODE = row.DOCK_STATUS == 0 ? cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OUTEVESSELNAME"].ToString() : cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["INEVESSELNAME"].ToString();
                                            row.C_VOYAGE = row.DOCK_STATUS == 0 ? cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["OUTBOUNDVOY"].ToString() : cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["INBOUNDVOY"].ToString();

                                            row.LINECODE = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["CONTAINEROWNER"].ToString();
                                            row.ISARCHIVED = "Y";
                                            //row.OPERATORNAME = Config.SysUser;
                                            row.CONTAINERID = 0;
                                            bll.UpdateCntStatus(row);

                                            DataSet1.T_OCRX_CNTRow newRow = CopyRow(row);
                                            //�����·ַ���¼
                                            cctdbDAL.InsertCntx(newRow);
                                            //���·ַ�״̬
                                            cctdbDAL.UpdateDispatched(row.DOCK_ID, string.Empty, row.COMPANYCODE);

                                            continue;

                                        }
                                        else
                                        {
                                            //���������죬�鵵
                                            row.ISARCHIVED = "Y";
                                        }
                                    }
                                    else
                                    {
                                        //���� �Ƿ�������������ȴ�
                                        string lineCode = cntInfo.DS.Tables["CM_CONTAINERS"].Rows[0]["CONTAINEROWNER"].ToString();
                                        if (String.IsNullOrEmpty(lineCode))
                                        {
                                            //row.CSTATUS = Convert.ToDecimal(Config.CStatus.WaitHandle);//ת�˹�     
                                            //row.ISARCHIVED = "N";
                                            lineCode = qc.OWNER;
                                        }
                                        //else
                                        //{

                                        //����ƥ������
                                        string companyCode = GetCompanyCode(row.SERVICECODE, lineCode, rules);
                                        row.LINECODE = lineCode;
                                        if (!string.IsNullOrEmpty(companyCode))
                                        {
                                            DataSet1.T_OCRX_CNTRow newRow = CopyRow(row);
                                            newRow.COMPANYCODE = companyCode;

                                            //�����·ַ���¼
                                            cctdbDAL.InsertCntx(newRow);
                                            //���·ַ�״̬
                                            cctdbDAL.UpdateDispatched(row.DOCK_ID, row.LINECODE, companyCode);
                                            i++;
                                            continue;
                                            //sb.AppendLine(string.Format("���ݷַ� {0} {1}", row.TRVAL_NO, newRow.CONTAINER_NO));
                                        }
                                        else
                                        {
                                            //���������죬�鵵
                                            row.ISARCHIVED = "Y";
                                        }
                                        //}
                                    }


                                }
                            }

                            row.CONTAINERID = 0;
                            bll.UpdateCntStatus(row);

                            //DataSet1.T_OCRX_CNTRow newRow = CopyRow(row);
                            ////newRow.COMPANYCODE = companyCode;

                            ////�����·ַ���¼
                            //cctdbDAL.InsertCntx(newRow);
                            ////���·ַ�״̬
                            //cctdbDAL.UpdateDispatched(row.DOCK_ID, string.Empty, row.COMPANYCODE);
                            //i++;
                            //sb.AppendLine(string.Format("���ݷַ� {0} {1}", row.TRVAL_NO, newRow.CONTAINER_NO));

                        }
                        catch (Exception ex) { sb.Append(ex.StackTrace); }
                    }
                }
            }

            if (i > 0)
            {
                sb.AppendFormat("���ݷַ� {0}����¼", i);
            }
            return sb.ToString();
        }

    }
}
