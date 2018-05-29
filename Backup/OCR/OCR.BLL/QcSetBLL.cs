using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCR.DAL;
using OCR.Model;


namespace OCR.BLL
{
    /// <summary>
    /// �ŵ���ҵ����
    /// </summary>
    public class QcSetBLL
    {
        /// <summary>
        /// ���ŵ���ҵ����
        /// </summary>
        /// <returns></returns>
        public QcSet.T_OCR_QCSETDataTable SelectQCSet()
        {
            QcSet.T_OCR_QCSETDataTable dt = DAL.cctdbDAL.SelectQCSet();

            return dt;
        }

        /// <summary>
        /// ���ŵ���ҵ����
        /// </summary>
        /// <returns></returns>
        public QcSet.T_OCR_QCSETRow SelectQCSet(string qcno)
        {
            return DAL.cctdbDAL.SelectQCSet(qcno);
        }

        /// <summary>
        /// �����ŵ���ҵ����
        /// </summary>
        /// <param name="row"></param>
        public void UpdateQCSet(QcSet.T_OCR_QCSETRow row)
        {
            //У��
            if (row.WORKTYPE == "����" || row.WORKTYPE == "����")
            {
                if (string.IsNullOrEmpty(row.CONTRACTOR_CODE))
                {
                    throw new Exception("������а��̴���");
                }
                if (string.IsNullOrEmpty(row.COMMEND_ID))
                {
                    throw new Exception("������ָ���ֹ���");
                }
                if (string.IsNullOrEmpty(row.COMMEND_PAW))
                {
                    throw new Exception("������ָ��������");
                }
                if (string.IsNullOrEmpty(row.DRIVER_ID))
                {
                    throw new Exception("�������ŵ�˾������");
                }
                if (string.IsNullOrEmpty(row.SHIP_CODE))
                {
                    throw new Exception("�����봬������");
                }
                if (string.IsNullOrEmpty(row.IN_VOYAGE_CODE))
                {
                    throw new Exception("��������ں���");
                }
                if (string.IsNullOrEmpty(row.OUT_VOYAGE_CODE))
                {
                    throw new Exception("��������ں���");
                }
                if (string.IsNullOrEmpty(row.TERMINAL_NO))
                {
                    throw new Exception("�����������ն˺�");
                }
                if (string.IsNullOrEmpty(row.SHIPMENT_DEAL))
                {
                    throw new Exception("������װ���˹�����");
                }
                if (string.IsNullOrEmpty(row.BERTH_WAY))
                {
                    throw new Exception("�����뿿�ҷ���");
                }
            }
            

            row.DEVICE_TYPE = 0;
            row.IS_CHANGE = 0;
            row.VOYAGE_TYPE = "EX";

            //������ҵģʽ���¼CTOS
            if (row.WORKTYPE == "����" || row.WORKTYPE == "����")
            {
                //����ǰ����
                QcSet.T_OCR_QCSETRow row1 = SelectQCSet(row.TRVALCRANE_NO);
                //��Ҫ���µ�¼���޸���ָ���֣�״̬��Ϊ��ҵ��
                if (row1.COMMEND_ID != row.COMMEND_ID || row1.DRIVER_ID != row.DRIVER_ID || (row1.STATUS != "��ҵ��" && row.STATUS == "��ҵ��"))
                {
                    if ((row1.COMMEND_ID != row.COMMEND_ID || row1.DRIVER_ID != row.DRIVER_ID) && row1.STATUS == "��ҵ��")
                    {
                        //������ʼ���˳�
                        CtosResult op06 = CtosAPIBLL.OP007006(row1.COMMEND_ID, row1.DEVICE_NO, row1.TRVALCRANE_NO, row1.DRIVER_ID, row1.CONTRACTOR_CODE, row1.TICKET_ID);

                    }

                    CtosResult rs = CtosAPIBLL.OP007001(row.COMMEND_ID, row.COMMEND_PAW, row.TERMINAL_NO,
                        MainBLL.Parms.DEVICEIP, MainBLL.Parms.TICKETID);

                    //if (row1.DRIVER_ID != row.DRIVER_ID)
                    //{
                    //������ʼ��
                    CtosResult op07 = CtosAPIBLL.OP007030(row.TRVALCRANE_NO, row.DRIVER_ID, row.CONTRACTOR_CODE, row.COMMEND_ID,
                        row.SHIP_CODE, row.OUT_VOYAGE_CODE, row.DEVICE_NO, "O", row.BERTH_NUM, row.TICKET_ID);

                    //}

                    if (rs.ERRORCODE != CtosAPIBLL.SUCCESSCODE)
                    {
                        if (string.IsNullOrEmpty(rs.ERRORCODE))
                        {
                            rs.ERRORMSG = ctosDAL.SelectErrcode(Convert.ToInt32(rs.ERRORCODE));
                        }
                        throw new Exception(string.Format("����CTOS�ӿ�OP007001����\r\n�������{0}\r\n��������{1}", rs.ERRORCODE, rs.ERRORMSG));

                    }
                    else
                    {
                        row.TICKET_ID = rs.DIC["TICKET_ID"];
                        if (row1.COMMEND_ID != row.COMMEND_ID)
                        {
                            //ԭָ�����˳�
                            CtosResult rs2 = CtosAPIBLL.OP007002(row1.COMMEND_ID, row1.TERMINAL_NO, row1.TICKET_ID);
                        }

                    }
                }
                //ע�� ֹͣ��ҵ
                else if (row1.STATUS != "ֹͣ��ҵ" && row.STATUS == "ֹͣ��ҵ")
                {
                    //������ʼ���˳�
                    CtosResult op06 = CtosAPIBLL.OP007006(row.COMMEND_ID, row.DEVICE_NO, row.TRVALCRANE_NO, row.DRIVER_ID, row.CONTRACTOR_CODE, row1.TICKET_ID);

                    CtosResult rs3 = CtosAPIBLL.OP007002(row.COMMEND_ID, row.TERMINAL_NO, row1.TICKET_ID);

                    if (rs3.ERRORCODE != CtosAPIBLL.SUCCESSCODE)
                    {
                        if (string.IsNullOrEmpty(rs3.ERRORCODE))
                        {
                            rs3.ERRORMSG = ctosDAL.SelectErrcode(Convert.ToInt32(rs3.ERRORCODE));
                        }
                        throw new Exception(string.Format("����CTOS�ӿ�OP007002����\r\n�������{0}\r\n��������{1}", rs3.ERRORCODE, rs3.ERRORMSG));

                    }
                    else
                    {
                        //�������
                        row.CONTRACTOR_CODE = string.Empty;
                        row.COMMEND_ID = string.Empty;
                        row.COMMEND_PAW = string.Empty;
                        row.DRIVER_ID = string.Empty;
                        row.SHIP_CODE = string.Empty;
                        row.IN_VOYAGE_CODE = string.Empty;
                        row.OUT_VOYAGE_CODE = string.Empty;
                        row.BERTH_NUM = string.Empty;
                        row.BERTHPLANNO = 0;
                        row.VESSELALIASE = string.Empty;
                        row.INAGENT = string.Empty;
                        row.OUTAGENT = string.Empty;
                        row.OWNER = string.Empty;
                        row.AVESSELNAME = string.Empty;
                        row.INVESSELLINECODE = string.Empty;
                        row.OUTVESSELLINECODE = string.Empty;
                    }
                }
            }
            else
            {
                //����ǲ���HKS�����ҽ����ڴ���Ϊ�գ�����
                //if (row.INVESSELLINECODE == "HKS" && string.IsNullOrEmpty(row.INAGENT))
                //{
                //    throw new Exception("�ô��Ľ��ڴ���Ϊ�գ����ܱ��棡����ϵ�����ƻ���¼���ڴ���");
                //}
                //if (row.OUTVESSELLINECODE == "HKS" && string.IsNullOrEmpty(row.OUTAGENT))
                //{
                //    throw new Exception("�ô��ĳ��ڴ���Ϊ�գ����ܱ��棡����ϵ�����ƻ���¼���ڴ���");
                //}

                //�˷�ģʽҪ���봬������ 
                //У��
                if (row.WORKTYPE == "�˷�")
                {                    
                    if (string.IsNullOrEmpty(row.SHIP_CODE))
                    {
                        throw new Exception("�����봬������");
                    }
                    if (string.IsNullOrEmpty(row.IN_VOYAGE_CODE))
                    {
                        throw new Exception("��������ں���");
                    }
                    if (string.IsNullOrEmpty(row.OUT_VOYAGE_CODE))
                    {
                        throw new Exception("��������ں���");
                    }
                    if (string.IsNullOrEmpty(row.BERTH_NUM))
                    {
                        throw new Exception("��λ�Ų���Ϊ��");
                    }   
                }


                if (row.STATUS == "ֹͣ��ҵ")
                {
                    //�������
                    row.CONTRACTOR_CODE = string.Empty;
                    row.COMMEND_ID = string.Empty;
                    row.COMMEND_PAW = string.Empty;
                    row.DRIVER_ID = string.Empty;
                    row.SHIP_CODE = string.Empty;
                    row.IN_VOYAGE_CODE = string.Empty;
                    row.OUT_VOYAGE_CODE = string.Empty;
                    row.BERTH_NUM = string.Empty;
                    row.BERTHPLANNO = 0;
                    row.VESSELALIASE = string.Empty;
                    row.WORKTYPE = "����";
                    row.INAGENT = string.Empty;
                    row.OUTAGENT = string.Empty;
                    row.OWNER = string.Empty;
                    row.AVESSELNAME = string.Empty;
                    row.INVESSELLINECODE = string.Empty;
                    row.OUTVESSELLINECODE = string.Empty;
                }
            }

            row.OPERATOR_UID = Config.UserId;

            DAL.cctdbDAL.UpdateQCSet(row);
        }

        /// <summary>
        /// ��а���
        /// </summary>
        /// <returns></returns>
        public List<string> SelectContractor()
        {
            return DAL.ctosDAL.SelectContractor();
        }

        /// <summary>
        /// ���а��̲�ָ����
        /// </summary>
        /// <returns></returns>
        public List<string> SelectCommend(string contractor)
        {
            return DAL.ctosDAL.SelectCommend(contractor);
        }

        /// <summary>
        /// ��а���
        /// </summary>
        /// <returns></returns>
        public DataSet SelectBerthplan()
        {
            return DAL.ctosDAL.SelectBerthplan();
        }

        /// <summary>
        /// �������ֳ��ն˺�
        /// </summary>
        /// <returns></returns>
        public string SelectRemote()
        {
            Dictionary<string, string> list = DAL.ctosDAL.SelectRemote();

            //ȥ����ʹ�õ��ն˺�
            QcSet.T_OCR_QCSETDataTable dt = DAL.cctdbDAL.SelectQCSet();
            foreach (QcSet.T_OCR_QCSETRow row in dt)
            {
                if (list.ContainsKey(row.DEVICE_NO))
                {
                    list.Remove(row.DEVICE_NO);
                }
            }

            if (list.Count == 0)
            {
                throw new Exception("���������ն˺������꣡");
            }

            string str = string.Empty;
            foreach (string key in list.Keys)
            {
                str = string.Format("{0},{1}", key, list[key]);
                break;
            }

            return str;
        }

        /// <summary>
        /// �������־
        /// </summary>
        /// <returns></returns>
        public OpLog.T_OCR_LOGDataTable SelectLogs(string tablename, string colname)
        {
            return DAL.cctdbDAL.SelectLogs(tablename, colname);
        }


       
    }
}
