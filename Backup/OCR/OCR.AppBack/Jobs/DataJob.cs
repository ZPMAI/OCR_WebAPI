using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCR.BLL;
using OCR.Model;

namespace OCR.AppBack.Jobs
{
    /// <summary>
    /// ͬ������
    /// </summary>
    public class DataJob : Job
    {
        public DataJob(UIDelegate delegate1)
            : base(delegate1)
        {
        }

        public override string MailTo
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        private static int maxId;

        /// <summary>
        /// ʶ���¼���ID
        /// </summary>
        private static int MaxId
        {
            get
            {
                if (maxId == 0)
                {
                    //jobBLL bll = new jobBLL();
                    //��ͬ�����ID
                    maxId = jobBLL.SelectMaxId1();
                }

                return maxId;
            }
            set
            {
                if (value > maxId)
                {
                    maxId = value;
                }
            }
        }

        private static string lastCntNo;

        public override string Deal()
        {
            StringBuilder msg = new StringBuilder();

            try
            {
                jobBLL bll = new jobBLL();

                //��ȡ��ͬ������
                using (DataSet ds = bll.GetOcrDBCnt(MaxId))
                {
                    //���ŵ���ҵ����
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        QcSet.T_OCR_QCSETDataTable qc = bll.SelectQCSet();

                        //����д����ο������ݿ�
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            try
                            {
                                MaxId = Convert.ToInt32(dr["dock_id"]);

                                bool flag = false;
                                dr["isarchived"] = "N";
                                foreach (QcSet.T_OCR_QCSETRow row in qc)
                                {
                                    if (row.TRVALCRANE_NO == dr["trval_no"].ToString() && row.STATUS == "��ҵ��")
                                    {
                                        int dock_status = Convert.ToInt32(dr["dock_status"]);
                                        dr["driver_no"] = row.DRIVER_ID;
                                        dr["ship_code"] = row.SHIP_CODE;
                                        dr["c_voyage"] = dock_status == 0 ? row.OUT_VOYAGE_CODE : row.IN_VOYAGE_CODE;
                                        dr["COMMEND_ID"] = row.COMMEND_ID;
                                        dr["BERTH_NUM"] = row.BERTH_NUM;
                                        dr["ENAM"] = row.VESSELALIASE;
                                        //dr["Cstatus"] = dock_status == 0 && row.SHIPMENT_DEAL == "��" ? Convert.ToInt32(Config.CStatus.LoadWaitAuto) : Convert.ToInt32(Config.CStatus.WaitHandle);
                                        //2017-9-29 Ҷ���� ����ж���Զ�ȷ��
                                        dr["Cstatus"] = row.SHIPMENT_DEAL == "��" ? Convert.ToInt32(Config.CStatus.LoadWaitAuto) : Convert.ToInt32(Config.CStatus.WaitHandle);
                                        dr["SERVICECODE"] = dock_status == 0 ? row.OUTVESSELLINECODE : row.INVESSELLINECODE;


                                        if (row.WORKTYPE == "����" || row.WORKTYPE == "�ֳ�" || row.WORKTYPE == "�˷�")
                                        {
                                            //������ҵģʽ
                                            dr["Cstatus"] = Convert.ToInt32(Config.CStatus.ExOnly);
                                        }

                                        flag = true;
                                        break;
                                    }

                                }

                                if (!flag)
                                {
                                    dr["Cstatus"] = Convert.ToInt32(Config.CStatus.NoQcSet);
                                    dr["isarchived"] = "A";
                                    AddResult(string.Format("��{0} {1}", dr["trval_no"].ToString(), "�ŵ���ҵ�����쳣"));
                                }

                                bll.InsertCnt(dr);

                                AddResult(string.Format("��{0} {1} {2}", dr["dock_id"].ToString(), dr["container_no"].ToString(), dr["trval_no"].ToString()));
                            }
                            catch (Exception ex)
                            {
                                AddResult(string.Format("��{0} {1}", dr["dock_id"].ToString(), ex.Message));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddResult(string.Format("�� {0}", ex.Message));
            }

            return msg.ToString();
        }

        
    }
}
