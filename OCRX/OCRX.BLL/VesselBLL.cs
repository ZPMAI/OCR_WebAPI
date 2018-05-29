using System;
using System.Collections.Generic;
using System.Text;
using OCRX.DAL;
using OCRX.Model;
using OCR.Model;
using System.Data;
using CCT.Common.Web;

namespace OCRX.BLL
{
    /// <summary>
    /// ���ַַ�����
    /// </summary>
    public class VesselBLL
    {
        /// <summary>
        /// �����а��ַַ�����
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_VESSELDataTable SelectVessel()
        {
            return DAL.cctdbDAL.SelectVessel();
        }

        /// <summary>
        /// �������ַַ�����
        /// </summary>
        /// <param name="row"></param>
        public void InsertVessel(string companyCode, string serviceCode, string lineCode)
        {
            if (string.IsNullOrEmpty(companyCode))
            {
                throw new Exception("����������˾����");
            }
            if (string.IsNullOrEmpty(serviceCode))
            {
                throw new Exception("�����뺽��");
            }
            if (string.IsNullOrEmpty(lineCode))
            {
                throw new Exception("����������");
            }

            //��������
            bool isValid = ValidateBLL.CheckEnglishComma(lineCode, 1, 10);
            if (!isValid)
            {
                throw new Exception("�������Ϸ��������������Ӣ�Ķ��Ÿ���");
            }

            CheckLines(lineCode, serviceCode, companyCode);

            DataSet1.T_OCRX_VESSELRow row = new DataSet1.T_OCRX_VESSELDataTable().NewT_OCRX_VESSELRow();
            row.COMPANYCODE = companyCode;
            row.SERVICECODE = serviceCode;
            row.LINECODE = lineCode;
            row.CREATEDBY = Config.UserId;

            DAL.cctdbDAL.InsertVessel(row);
        }

        private string[] SplitLines(string lineCode)
        {
            return lineCode.Split(new char[] { ',' });
        }

        /// <summary>
        /// У�������Ƿ����ظ�
        /// </summary>
        /// <param name="lineCode"></param>
        /// <param name="serviceCode"></param>
        /// <returns></returns>
        private void CheckLines(string lineCode, string serviceCode, string companyCode)
        {
            //У�������Ƿ����ظ�
            string[] newLines = SplitLines(lineCode);
            bool isDuplicated = false;
            DataSet1.T_OCRX_VESSELDataTable dt = DAL.cctdbDAL.SelectVesselByService(serviceCode);
            foreach (DataSet1.T_OCRX_VESSELRow r1 in dt)
            {
                if (r1.COMPANYCODE != companyCode)
                {
                    string[] lines = SplitLines(r1.LINECODE);
                    foreach (string l1 in lines)
                    {
                        foreach (string l2 in newLines)
                        {
                            if (l2.Equals(l1))
                            {
                                isDuplicated = true;
                                break;
                            }
                        }
                        if (isDuplicated)
                        {
                            break;
                        }
                    }
                    if (isDuplicated)
                    {
                        break;
                    }
                }
            }

            if (isDuplicated)
            {
                throw new Exception("�ú����������ظ����塣");
            }
        }

        /// <summary>
        /// �޸İ��ַַ�����
        /// </summary>
        /// <param name="row"></param>
        public void UpdateVessel(string companyCode, string serviceCode, string lineCode)
        {
            if (string.IsNullOrEmpty(companyCode))
            {
                throw new Exception("����������˾����");
            }
            if (string.IsNullOrEmpty(serviceCode))
            {
                throw new Exception("�����뺽��");
            }
            if (string.IsNullOrEmpty(lineCode))
            {
                throw new Exception("����������");
            }

            //��������
            bool isValid = ValidateBLL.CheckEnglishComma(lineCode, 1, 10);
            if (!isValid)
            {
                throw new Exception("�������Ϸ��������������Ӣ�Ķ��Ÿ���");
            }

            CheckLines(lineCode, serviceCode, companyCode);

            DataSet1.T_OCRX_VESSELRow row = new DataSet1.T_OCRX_VESSELDataTable().NewT_OCRX_VESSELRow();
            row.COMPANYCODE = companyCode;
            row.SERVICECODE = serviceCode;
            row.LINECODE = lineCode;
            row.UPDATEDBY = Config.UserId;

            DAL.cctdbDAL.UpdateVessel(row);
        }

        /// <summary>
        /// ɾ�����ַַ�����
        /// </summary>
        /// <param name="row"></param>
        public void DeleteVessel(string companyCode, string serviceCode)
        {
            DataSet1.T_OCRX_VESSELRow row = new DataSet1.T_OCRX_VESSELDataTable().NewT_OCRX_VESSELRow();
            row.COMPANYCODE = companyCode;
            row.SERVICECODE = serviceCode;
            row.UPDATEDBY = Config.UserId;

            DAL.cctdbDAL.DeleteVessel(row);
        }

        /// <summary>
        /// �����߲���ַַ�����
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_VESSELDataTable SelectVesselByService(string serviceCode)
        {
            return DAL.cctdbDAL.SelectVesselByService(serviceCode);
        }

        /// <summary>
        /// �麽��
        /// </summary>
        /// <returns></returns>
        public List<string> SelectService()
        {
            using (DataTable dt = DAL.cctdbDAL.SelectService())
            {
                List<string> list = new List<string>();

                list.Add("ALL");

                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr["vessellinecode"].ToString());
                }

                return list;
            }
        }

        public List<string> SelectCompanyVesselOwner(string vslaliase, string CompanyCode,bool disc)
        {
            return DAL.cctdbDAL.SelectCompanyVesselOwner(vslaliase,CompanyCode,disc);
        }
    }
}
