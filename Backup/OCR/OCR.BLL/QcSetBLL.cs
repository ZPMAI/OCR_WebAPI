using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCR.DAL;
using OCR.Model;


namespace OCR.BLL
{
    /// <summary>
    /// 桥吊作业配置
    /// </summary>
    public class QcSetBLL
    {
        /// <summary>
        /// 查桥吊作业配置
        /// </summary>
        /// <returns></returns>
        public QcSet.T_OCR_QCSETDataTable SelectQCSet()
        {
            QcSet.T_OCR_QCSETDataTable dt = DAL.cctdbDAL.SelectQCSet();

            return dt;
        }

        /// <summary>
        /// 查桥吊作业配置
        /// </summary>
        /// <returns></returns>
        public QcSet.T_OCR_QCSETRow SelectQCSet(string qcno)
        {
            return DAL.cctdbDAL.SelectQCSet(qcno);
        }

        /// <summary>
        /// 更新桥吊作业配置
        /// </summary>
        /// <param name="row"></param>
        public void UpdateQCSet(QcSet.T_OCR_QCSETRow row)
        {
            //校验
            if (row.WORKTYPE == "智能" || row.WORKTYPE == "内理")
            {
                if (string.IsNullOrEmpty(row.CONTRACTOR_CODE))
                {
                    throw new Exception("请输入承包商代码");
                }
                if (string.IsNullOrEmpty(row.COMMEND_ID))
                {
                    throw new Exception("请输入指挥手工号");
                }
                if (string.IsNullOrEmpty(row.COMMEND_PAW))
                {
                    throw new Exception("请输入指挥手密码");
                }
                if (string.IsNullOrEmpty(row.DRIVER_ID))
                {
                    throw new Exception("请输入桥吊司机工号");
                }
                if (string.IsNullOrEmpty(row.SHIP_CODE))
                {
                    throw new Exception("请输入船名代码");
                }
                if (string.IsNullOrEmpty(row.IN_VOYAGE_CODE))
                {
                    throw new Exception("请输入进口航次");
                }
                if (string.IsNullOrEmpty(row.OUT_VOYAGE_CODE))
                {
                    throw new Exception("请输入出口航次");
                }
                if (string.IsNullOrEmpty(row.TERMINAL_NO))
                {
                    throw new Exception("请输入无线终端号");
                }
                if (string.IsNullOrEmpty(row.SHIPMENT_DEAL))
                {
                    throw new Exception("请输入装船人工处理");
                }
                if (string.IsNullOrEmpty(row.BERTH_WAY))
                {
                    throw new Exception("请输入靠弦方向");
                }
            }
            

            row.DEVICE_TYPE = 0;
            row.IS_CHANGE = 0;
            row.VOYAGE_TYPE = "EX";

            //智能作业模式需登录CTOS
            if (row.WORKTYPE == "智能" || row.WORKTYPE == "内理")
            {
                //读当前设置
                QcSet.T_OCR_QCSETRow row1 = SelectQCSet(row.TRVALCRANE_NO);
                //需要重新登录，修改了指挥手，状态改为作业中
                if (row1.COMMEND_ID != row.COMMEND_ID || row1.DRIVER_ID != row.DRIVER_ID || (row1.STATUS != "作业中" && row.STATUS == "作业中"))
                {
                    if ((row1.COMMEND_ID != row.COMMEND_ID || row1.DRIVER_ID != row.DRIVER_ID) && row1.STATUS == "作业中")
                    {
                        //船舶初始化退出
                        CtosResult op06 = CtosAPIBLL.OP007006(row1.COMMEND_ID, row1.DEVICE_NO, row1.TRVALCRANE_NO, row1.DRIVER_ID, row1.CONTRACTOR_CODE, row1.TICKET_ID);

                    }

                    CtosResult rs = CtosAPIBLL.OP007001(row.COMMEND_ID, row.COMMEND_PAW, row.TERMINAL_NO,
                        MainBLL.Parms.DEVICEIP, MainBLL.Parms.TICKETID);

                    //if (row1.DRIVER_ID != row.DRIVER_ID)
                    //{
                    //船舶初始化
                    CtosResult op07 = CtosAPIBLL.OP007030(row.TRVALCRANE_NO, row.DRIVER_ID, row.CONTRACTOR_CODE, row.COMMEND_ID,
                        row.SHIP_CODE, row.OUT_VOYAGE_CODE, row.DEVICE_NO, "O", row.BERTH_NUM, row.TICKET_ID);

                    //}

                    if (rs.ERRORCODE != CtosAPIBLL.SUCCESSCODE)
                    {
                        if (string.IsNullOrEmpty(rs.ERRORCODE))
                        {
                            rs.ERRORMSG = ctosDAL.SelectErrcode(Convert.ToInt32(rs.ERRORCODE));
                        }
                        throw new Exception(string.Format("调用CTOS接口OP007001出错。\r\n错误代码{0}\r\n错误描述{1}", rs.ERRORCODE, rs.ERRORMSG));

                    }
                    else
                    {
                        row.TICKET_ID = rs.DIC["TICKET_ID"];
                        if (row1.COMMEND_ID != row.COMMEND_ID)
                        {
                            //原指挥手退出
                            CtosResult rs2 = CtosAPIBLL.OP007002(row1.COMMEND_ID, row1.TERMINAL_NO, row1.TICKET_ID);
                        }

                    }
                }
                //注销 停止作业
                else if (row1.STATUS != "停止作业" && row.STATUS == "停止作业")
                {
                    //船舶初始化退出
                    CtosResult op06 = CtosAPIBLL.OP007006(row.COMMEND_ID, row.DEVICE_NO, row.TRVALCRANE_NO, row.DRIVER_ID, row.CONTRACTOR_CODE, row1.TICKET_ID);

                    CtosResult rs3 = CtosAPIBLL.OP007002(row.COMMEND_ID, row.TERMINAL_NO, row1.TICKET_ID);

                    if (rs3.ERRORCODE != CtosAPIBLL.SUCCESSCODE)
                    {
                        if (string.IsNullOrEmpty(rs3.ERRORCODE))
                        {
                            rs3.ERRORMSG = ctosDAL.SelectErrcode(Convert.ToInt32(rs3.ERRORCODE));
                        }
                        throw new Exception(string.Format("调用CTOS接口OP007002出错。\r\n错误代码{0}\r\n错误描述{1}", rs3.ERRORCODE, rs3.ERRORMSG));

                    }
                    else
                    {
                        //清空数据
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
                //如果是驳船HKS航线且进出口船代为空，报错
                //if (row.INVESSELLINECODE == "HKS" && string.IsNullOrEmpty(row.INAGENT))
                //{
                //    throw new Exception("该船的进口船代为空，不能保存！请联系驳船计划补录进口船代");
                //}
                //if (row.OUTVESSELLINECODE == "HKS" && string.IsNullOrEmpty(row.OUTAGENT))
                //{
                //    throw new Exception("该船的出口船代为空，不能保存！请联系驳船计划补录出口船代");
                //}

                //核封模式要输入船名航次 
                //校验
                if (row.WORKTYPE == "核封")
                {                    
                    if (string.IsNullOrEmpty(row.SHIP_CODE))
                    {
                        throw new Exception("请输入船名代码");
                    }
                    if (string.IsNullOrEmpty(row.IN_VOYAGE_CODE))
                    {
                        throw new Exception("请输入进口航次");
                    }
                    if (string.IsNullOrEmpty(row.OUT_VOYAGE_CODE))
                    {
                        throw new Exception("请输入出口航次");
                    }
                    if (string.IsNullOrEmpty(row.BERTH_NUM))
                    {
                        throw new Exception("泊位号不能为空");
                    }   
                }


                if (row.STATUS == "停止作业")
                {
                    //清空数据
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
                    row.WORKTYPE = "智能";
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
        /// 查承包商
        /// </summary>
        /// <returns></returns>
        public List<string> SelectContractor()
        {
            return DAL.ctosDAL.SelectContractor();
        }

        /// <summary>
        /// 按承包商查指挥手
        /// </summary>
        /// <returns></returns>
        public List<string> SelectCommend(string contractor)
        {
            return DAL.ctosDAL.SelectCommend(contractor);
        }

        /// <summary>
        /// 查承包商
        /// </summary>
        /// <returns></returns>
        public DataSet SelectBerthplan()
        {
            return DAL.ctosDAL.SelectBerthplan();
        }

        /// <summary>
        /// 查虚拟手持终端号
        /// </summary>
        /// <returns></returns>
        public string SelectRemote()
        {
            Dictionary<string, string> list = DAL.ctosDAL.SelectRemote();

            //去掉已使用的终端号
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
                throw new Exception("虚拟无线终端号已用完！");
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
        /// 查操作日志
        /// </summary>
        /// <returns></returns>
        public OpLog.T_OCR_LOGDataTable SelectLogs(string tablename, string colname)
        {
            return DAL.cctdbDAL.SelectLogs(tablename, colname);
        }


       
    }
}
