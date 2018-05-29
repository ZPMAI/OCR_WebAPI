using System;
using System.Collections.Generic;
using System.Text;


using OCRX.DAL;
using OCRX.Model;
using OCR.Model;

namespace OCRX.BLL
{
    /// <summary>
    /// �û�����
    /// </summary>
    public class UsersBLL
    {

        /// <summary>
        /// ����������˾����
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_CODataTable SelectCompany()
        {
            return DAL.cctdbDAL.SelectCompany();
        }

        /// <summary>
        /// ����������˾����
        /// </summary>
        /// <returns></returns>
        public List<string> SelectCompanyList()
        {
            List<string> list = new List<string>();

            DataSet1.T_OCRX_CODataTable dt = DAL.cctdbDAL.SelectCompany();
            foreach (DataSet1.T_OCRX_CORow row in dt)
            {
                list.Add(row.COMPANYCODE);
            }

            return list;
        }

        /// <summary>
        /// ��������˾
        /// </summary>
        /// <param name="row"></param>
        public void InsertCompany(string companyCode, string companyName)
        {
            if (string.IsNullOrEmpty(companyCode))
            {
                throw new Exception("����������˾����");
            }
            if (string.IsNullOrEmpty(companyName))
            {
                throw new Exception("����������˾����");
            }

            DataSet1.T_OCRX_CORow row = new DataSet1.T_OCRX_CODataTable().NewT_OCRX_CORow();
            row.COMPANYCODE = companyCode;
            row.COMPANYNAME = companyName;
            row.CREATEDBY = Config.UserId;

            DAL.cctdbDAL.InsertCompany(row);
        }

        /// <summary>
        /// ɾ������˾
        /// </summary>
        /// <param name="row"></param>
        public void DeleteCompany(string companyCode)
        {
            if (string.IsNullOrEmpty(companyCode))
            {
                throw new Exception("��ѡ������˾����");
            }

            DataSet1.T_OCRX_CORow row = new DataSet1.T_OCRX_CODataTable().NewT_OCRX_CORow();
            row.COMPANYCODE = companyCode;
            row.UPDATEDBY = Config.UserId;

            DAL.cctdbDAL.DeleteCompany(row);
        }


        /// <summary>
        /// ����������˾�û�
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_USERSDataTable SelectUsers()
        {
            return DAL.cctdbDAL.SelectUsers();
        }

        /// <summary>
        /// ���������û�
        /// </summary>
        /// <param name="row"></param>
        public void InsertUsers(string companyCode, string userId)
        {
            if (string.IsNullOrEmpty(companyCode))
            {
                throw new Exception("��ѡ������˾����");
            }
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("�������û�ID");
            }

            DataSet1.T_OCRX_USERSRow row = new DataSet1.T_OCRX_USERSDataTable().NewT_OCRX_USERSRow();
            row.COMPANYCODE = companyCode;
            row.USERID = userId;
            row.CREATEDBY = Config.UserId;

            DAL.cctdbDAL.InsertUsers(row);
        }

        /// <summary>
        /// ɾ�������û�
        /// </summary>
        /// <param name="row"></param>
        public void DeleteUsers(string companyCode, string userId)
        {
            if (string.IsNullOrEmpty(companyCode))
            {
                throw new Exception("��ѡ������˾����");
            }
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("�������û�ID");
            }

            DataSet1.T_OCRX_USERSRow row = new DataSet1.T_OCRX_USERSDataTable().NewT_OCRX_USERSRow();
            row.COMPANYCODE = companyCode;
            row.USERID = userId;
            row.UPDATEDBY = Config.UserId;

            DAL.cctdbDAL.DeleteUsers(row);
        }


        /// <summary>
        /// ���û�������
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_USERSRow SelectUser(string userId)
        {
            return DAL.cctdbDAL.SelectUser(userId);
        }


        /// <summary>
        /// ������˾�������
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_CORow SelectCompany(string companyCode)
        {
            return DAL.cctdbDAL.SelectCompany(companyCode);
        }

    }
}
