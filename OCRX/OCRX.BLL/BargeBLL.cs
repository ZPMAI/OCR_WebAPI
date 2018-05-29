using System;
using System.Collections.Generic;
using System.Text;


using OCRX.DAL;
using OCRX.Model;
using OCR.Model;
using System.Data;


namespace OCRX.BLL
{
    /// <summary>
    /// 驳船分发规则
    /// </summary>
    public class BargeBLL
    {
        /// <summary>
        /// 查所有驳船分发规则
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_BARGEDataTable SelectBarge(string companyCode)
        {
            return DAL.cctdbDAL.SelectBarge(companyCode);
        }

        /// <summary>
        /// 新增驳船分发规则
        /// </summary>
        /// <param name="row"></param>
        public void InsertBarge(string companyCode, string shipCode, string voyageIn, string voyageOut, decimal berthplanno)
        {
            if (string.IsNullOrEmpty(companyCode))
            {
                throw new Exception("请输入外理公司代码");
            }
            if (string.IsNullOrEmpty(shipCode))
            {
                throw new Exception("请输入船名代码");
            }
            if (string.IsNullOrEmpty(voyageIn))
            {
                throw new Exception("请输入进口航次");
            }
            if (string.IsNullOrEmpty(voyageOut))
            {
                throw new Exception("请输入出口航次");
            }

            DataSet1.T_OCRX_BARGERow row = new DataSet1.T_OCRX_BARGEDataTable().NewT_OCRX_BARGERow();
            row.COMPANYCODE = companyCode;
            row.SHIP_CODE = shipCode;
            row.IN_VOYAGE_CODE = voyageIn;
            row.OUT_VOYAGE_CODE = voyageOut;
            row.BERTHPLANNO = berthplanno;
            row.CREATEDBY = Config.UserId;

            DAL.cctdbDAL.InsertBarge(row);
        }

        private string[] SplitLines(string lineCode)
        {
            return lineCode.Split(new char[] { ',' });
        }

        /// <summary>
        /// 校验船东是否有重复
        /// </summary>
        /// <param name="shipOwner"></param>
        /// <param name="shipAgent"></param>
        /// <returns></returns>
        private void CheckOwners(string shipOwner, string shipAgent, string companyCode)
        {
            //校验船东是否有重复
            string[] newLines = SplitLines(shipOwner);
            bool isDuplicated = false;
            DataSet1.T_OCRX_BARGEDataTable dt = DAL.cctdbDAL.SelectBargeByAgent(shipAgent);
            foreach (DataSet1.T_OCRX_BARGERow r1 in dt)
            {
                if (r1.COMPANYCODE != companyCode)
                {
                    string[] lines = SplitLines(r1.SHIPOWNER);
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
                throw new Exception("该船代下船东重复定义。");
            }
        }

        /// <summary>
        /// 修改驳船分发规则
        /// </summary>
        /// <param name="row"></param>
        public void UpdateBarge(string companyCode, string shipAgent, string shipOwner)
        {
            if (string.IsNullOrEmpty(companyCode))
            {
                throw new Exception("请输入外理公司代码");
            }
            if (string.IsNullOrEmpty(shipAgent))
            {
                throw new Exception("请输入船代");
            }
            if (string.IsNullOrEmpty(shipOwner))
            {
                throw new Exception("请输入船东");
            }

            //较验船东
            bool isValid = ValidateBLL.CheckEnglishComma(shipOwner, 1, 10);
            if (!isValid)
            {
                throw new Exception("船东不合法。多个船东请用英文逗号隔开");
            }

            CheckOwners(shipOwner, shipAgent, companyCode);

            DataSet1.T_OCRX_BARGERow row = new DataSet1.T_OCRX_BARGEDataTable().NewT_OCRX_BARGERow();
            row.COMPANYCODE = companyCode;
            row.SHIPOWNER = shipOwner;
            row.SHIPAGENT = shipAgent;
            row.UPDATEDBY = Config.UserId;

            DAL.cctdbDAL.UpdateBarge(row);
        }

        /// <summary>
        /// 删除驳船分发规则
        /// </summary>
        /// <param name="row"></param>
        public void DeleteBarge(string companyCode, string shipCode, string voyageIn, string voyageOut, decimal berthplanno)
        {
            DataSet1.T_OCRX_BARGERow row = new DataSet1.T_OCRX_BARGEDataTable().NewT_OCRX_BARGERow();
            row.COMPANYCODE = companyCode;
            row.BERTHPLANNO = berthplanno;
            row.UPDATEDBY = Config.UserId;

            DAL.cctdbDAL.DeleteBarge(row);
        }

        /// <summary>
        /// 按航线查驳船分发规则
        /// </summary>
        /// <returns></returns>
        public DataSet1.T_OCRX_BARGEDataTable SelectBargeByService(string shipAgent)
        {
            return DAL.cctdbDAL.SelectBargeByAgent(shipAgent);
        }


        /// <summary>
        /// 查驳船船期
        /// </summary>
        /// <returns></returns>
        public DataSet SelectBerthplan()
        {
            return OCR.DAL.ctosDAL.SelectBerthplanBg();
        }
    }
}
