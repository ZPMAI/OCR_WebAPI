using System;
using System.Collections.Generic;
using System.Text;


using OCRX.DAL;
using OCRX.Model;
using OCR.Model;

namespace OCRX.BLL
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UsersBLL
    {

        /// <summary>
        /// 查所有外理公司代码
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_CODataTable SelectCompany()
        {
            return DAL.cctdbDAL.SelectCompany();
        }

        /// <summary>
        /// 查所有外理公司代码
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
        /// 新增外理公司
        /// </summary>
        /// <param name="row"></param>
        public void InsertCompany(string companyCode, string companyName)
        {
            if (string.IsNullOrEmpty(companyCode))
            {
                throw new Exception("请输入外理公司代码");
            }
            if (string.IsNullOrEmpty(companyName))
            {
                throw new Exception("请输入外理公司名称");
            }

            DataSet1.T_OCRX_CORow row = new DataSet1.T_OCRX_CODataTable().NewT_OCRX_CORow();
            row.COMPANYCODE = companyCode;
            row.COMPANYNAME = companyName;
            row.CREATEDBY = Config.UserId;

            DAL.cctdbDAL.InsertCompany(row);
        }

        /// <summary>
        /// 删除外理公司
        /// </summary>
        /// <param name="row"></param>
        public void DeleteCompany(string companyCode)
        {
            if (string.IsNullOrEmpty(companyCode))
            {
                throw new Exception("请选择外理公司代码");
            }

            DataSet1.T_OCRX_CORow row = new DataSet1.T_OCRX_CODataTable().NewT_OCRX_CORow();
            row.COMPANYCODE = companyCode;
            row.UPDATEDBY = Config.UserId;

            DAL.cctdbDAL.DeleteCompany(row);
        }


        /// <summary>
        /// 查所有外理公司用户
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_USERSDataTable SelectUsers()
        {
            return DAL.cctdbDAL.SelectUsers();
        }

        /// <summary>
        /// 新增外理用户
        /// </summary>
        /// <param name="row"></param>
        public void InsertUsers(string companyCode, string userId)
        {
            if (string.IsNullOrEmpty(companyCode))
            {
                throw new Exception("请选择外理公司代码");
            }
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("请输入用户ID");
            }

            DataSet1.T_OCRX_USERSRow row = new DataSet1.T_OCRX_USERSDataTable().NewT_OCRX_USERSRow();
            row.COMPANYCODE = companyCode;
            row.USERID = userId;
            row.CREATEDBY = Config.UserId;

            DAL.cctdbDAL.InsertUsers(row);
        }

        /// <summary>
        /// 删除外理用户
        /// </summary>
        /// <param name="row"></param>
        public void DeleteUsers(string companyCode, string userId)
        {
            if (string.IsNullOrEmpty(companyCode))
            {
                throw new Exception("请选择外理公司代码");
            }
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("请输入用户ID");
            }

            DataSet1.T_OCRX_USERSRow row = new DataSet1.T_OCRX_USERSDataTable().NewT_OCRX_USERSRow();
            row.COMPANYCODE = companyCode;
            row.USERID = userId;
            row.UPDATEDBY = Config.UserId;

            DAL.cctdbDAL.DeleteUsers(row);
        }


        /// <summary>
        /// 按用户名查找
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_USERSRow SelectUser(string userId)
        {
            return DAL.cctdbDAL.SelectUser(userId);
        }


        /// <summary>
        /// 按外理公司代码查找
        /// </summary>
        /// <returns></returns>
        public static DataSet1.T_OCRX_CORow SelectCompany(string companyCode)
        {
            return DAL.cctdbDAL.SelectCompany(companyCode);
        }

    }
}
