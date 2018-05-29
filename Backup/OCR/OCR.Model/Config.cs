using System;
using System.Collections.Generic;
using System.Configuration;

using CCT.SystemFramework.Data;
using CCT.Common.Configuration;

namespace OCR.Model
{
	/// <summary>
	/// 保存系统参数
	/// </summary>
	public class Config
	{
		public Config()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
        }

        private static string _conn = string.Empty;

        #region 数据库连接字符串
        /// <summary>
		/// 数据库连接字符串
		/// </summary>
        public static string ConnectionString
        {
            get
            {
                //if (_conn == string.Empty)
                //{
                //    //return ConnectionHelper.GetOracleConnectionString("CCTD");
                //    _conn = ConfigurationHelper.Section["OCR"]["IsDebug"].ToString() == "false" ?
                //        ConnectionHelper.GetOracleConnectionString("CCTD") :
                //        ConnectionHelper.GetOracleConnectionString("CTOST");
                //}
                //return _conn;
                return ConnectionStringCTOSP;
            }
        }
        #endregion  

        private static string ocrDbConn = string.Empty;
      
        #region ocr数据库连接字符串
        /// <summary>
        /// ocr数据库连接字符串
        /// </summary>
        public static string OcrDbConn
        {
            get
            {
                if (ocrDbConn == string.Empty)
                {
                    ocrDbConn = ConfigurationHelper.Section["OCR"]["OCRDB"].ToString();
                }
                return ocrDbConn;
            }
        }
        #endregion  

        private static string _connCTOSP = string.Empty;

        #region CTOSP数据库连接字符串
        /// <summary>
        /// CTOSP数据库连接字符串
        /// </summary>
        public static string ConnectionStringCTOSP
        {
            get
            {
                if (_connCTOSP == string.Empty)
                {
                    //return ConnectionHelper.GetOracleConnectionString("CCTD");
                    _connCTOSP = ConfigurationHelper.Section["OCR"]["IsDebug"].ToString() == "false" ?
                        ConnectionHelper.GetOracleConnectionString("CTOSP") :
                        ConnectionHelper.GetOracleConnectionString("CTOSU");
                }
                return _connCTOSP;
            }
        }
        #endregion  


        private static string _connCTOST = string.Empty;

        #region CTOSP数据库连接字符串
        /// <summary>
        /// CTOSP数据库连接字符串
        /// </summary>
        public static string ConnectionStringCTOST
        {
            get
            {
                if (_connCTOST == string.Empty)
                {
                    _connCTOST = ConnectionHelper.GetOracleConnectionString("CTOSU");
                }
                return _connCTOST;
            }
        }
        #endregion 

        #region 系统发件人
        /// <summary>
        /// 系统发件人
        /// </summary>
        public static string MailFrom
        {
            get
            {
                return "ocr@cwcct.com";
            }
        }
        #endregion

        #region reporting service url
        /// <summary>
        /// reporting service url
        /// </summary>
        public static string ReportingServiceUrl
        {
            get
            {
                return ConfigurationHelper.Section["OCR"]["REPORT_URL"].ToString();
            }
        }
        #endregion  

        #region reporting service url backup
        /// <summary>
        /// reporting service url backup
        /// </summary>
        public static string ReportingServiceUrl2
        {
            get
            {
                return ConfigurationHelper.Section["OCR"]["REPORT_URL2"].ToString();
            }
        }
        #endregion  

        #region 默认日期
        /// <summary>
        /// 默认日期
        /// </summary>
        public static DateTime DefaultDate
        {
            get
            {
                return new DateTime(1900, 1, 1);
            }
        }
        #endregion
     
        #region 环境
        /// <summary>
        /// 环境
        /// </summary>
        public static bool IsDebug
        {
            get
            {
                return ConfigurationHelper.Section["OCR"]["IsDebug"].ToString() == "false";
            }
        }
        #endregion  

        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserId
        {
            get
            {
                return CCT.Common.Userinfo.Username == null ? "SYS" : CCT.Common.Userinfo.Username;
            }
        }

        /// <summary>
        /// 系统自动确认用户名
        /// </summary>
        public static string SysUser
        {
            get
            {
                return "AUTO";
            }
        }

        public enum CStatus
        {
            /// <summary>
            /// 装船等待自动处理
            /// </summary>
            LoadWaitAuto = -1,
            /// <summary>
            /// 等待人工处理
            /// </summary>
            WaitHandle = 0,
            /// <summary>
            /// 已人工获取
            /// </summary>
            Fetched = 1,
            /// <summary>
            /// 已成功处理
            /// </summary>
            Success = 2,
            /// <summary>
            /// 等待人工异常处理
            /// </summary>
            WaitExpHandle = 3,
            /// <summary>
            /// 无需处理
            /// </summary>
            Skip = 4,
            /// <summary>
            /// 异常处理完成
            /// </summary>
            ExpDone = 5,
            /// <summary>
            /// 无QC作业设置
            /// </summary>
            NoQcSet = -2,
            /// <summary>
            /// 重复记录
            /// </summary>
            Duplicated = -3,
            /// <summary>
            /// 仅外理作业
            /// </summary>
            ExOnly = -4
        }

        /// <summary>
        /// 随手拍服务器URL
        /// </summary>
        //public static string PhotoReportUrl = @"http://172.16.1.4/photoreport/default.aspx";
        public static string PhotoReportUrl = @"http://serweb01.cwcct.com/photoreport/default.aspx";


        /// <summary>
        /// 随手拍用户名
        /// </summary>
        public static string PhotoReportUserName = UserId;

        /// <summary>
        /// 随手拍部门
        /// </summary>
        public static string PhotoReportDept = @"OCR";

        /// <summary>
        /// 随手拍手机
        /// </summary>
        public static string PhotoReportPhone = @"12345678912";

        private static Dictionary<string, string> cameras;
        /// <summary>
        /// 监控摄像头
        /// </summary>
        public static Dictionary<string, string> Cameras
        {
            get
            {
                if (cameras == null)
                {
                    cameras = new Dictionary<string, string>();
                    cameras.Add("C01", "10000000001130000009");
                    cameras.Add("C02", "10000000001130000010");
                    cameras.Add("C03", "10000000001130000011");
                    cameras.Add("C04", "10000000001130000012");
                    cameras.Add("C05", "10000000001130000013");
                    cameras.Add("C06", "10000000001130000014");
                    cameras.Add("C07", "10000000001130000015");
                    cameras.Add("C08", "10000000001130000016");
                    cameras.Add("C09", "10000000001130000017");
                    cameras.Add("C10", "10000000001130000018");
                    cameras.Add("C11", "10000000001130000019");
                    cameras.Add("C12", "10000000001130000020");
                    cameras.Add("C13", "10000000001130000021");
                    cameras.Add("C14", "10000000001130000022");
                    cameras.Add("C15", "10000000001130000023");
                    cameras.Add("C16", "10000000001130000024");
                    cameras.Add("C17", "10000000001310000033");
                    cameras.Add("C18", "10000000001310000036");
                    cameras.Add("C19", "10000000001310000035");
                    cameras.Add("C20", "10000000001310000034");
                    cameras.Add("C21", "10000000001130000025");
                    cameras.Add("C22", "10000000001130000026");
                    cameras.Add("C23", "10000000001130000027");
                    cameras.Add("C24", "10000000001130000028");
                    cameras.Add("C25", "10000000001130000029");

                    cameras.Add("Q01", "10000000001130000030");
                    cameras.Add("Q02", "10000000001130000031");
                    cameras.Add("Q03", "10000000001130000032");

                    cameras.Add("M01", "10000000001130000033");
                    cameras.Add("M02", "10000000001130000034");
                    cameras.Add("M03", "10000000001130000035");
                    cameras.Add("M04", "10000000001130000036");
                    cameras.Add("M05", "10000000001130000037");
                    cameras.Add("M06", "10000000001130000038");
                    cameras.Add("M07", "10000000001130000039");
                    cameras.Add("M08", "10000000001130000040");
                    cameras.Add("M09", "10000000001130000041");
                    cameras.Add("M10", "10000000001130000042");
                    cameras.Add("M11", "10000000001130000043");
                    cameras.Add("M12", "10000000001130000044");
                }
                return cameras;
            }
        }


        #region H3cUrl
        private static string h3cUrl = string.Empty;
        /// <summary>
        /// H3cUrl
        /// </summary>
        public static string H3cUrl
        {
            get
            {
                if (h3cUrl == string.Empty)
                {
                    h3cUrl = ConfigurationHelper.Section["OCR"]["H3C_URL"].ToString();
                }
                return h3cUrl;
            }
        }
        #endregion 

        #region H3C_PWD
        private static string h3cPwd = string.Empty;
        /// <summary>
        /// H3C_PWD
        /// </summary>
        public static string H3cPwd
        {
            get
            {
                if (h3cPwd == string.Empty)
                {
                    h3cPwd = ConfigurationHelper.Section["OCR"]["H3C_PWD"].ToString();
                }
                return h3cPwd;
            }
        }
        #endregion 

        #region H3C_USERNAME
        private static string h3cUserName = string.Empty;
        /// <summary>
        /// H3cUserName
        /// </summary>
        public static string H3cUserName
        {
            get
            {
                if (h3cUserName == string.Empty)
                {
                    h3cUserName = ConfigurationHelper.Section["OCR"]["H3C_USERNAME"].ToString();
                }
                return h3cUserName;
            }
        }
        #endregion 

        #region IPADDRESS
        private static string ipAddress = string.Empty;
        /// <summary>
        /// IPADDRESS
        /// </summary>
        public static string IPADDRESS
        {
            get
            {
                if (ipAddress == string.Empty)
                {
                    ipAddress = ConfigurationHelper.Section["OCR"]["MY_IPADDRESS"].ToString();
                }
                return ipAddress;
            }
        }
        #endregion 

        #region ERROR_COUNT
        private static int eRROR_COUNT = 0;
        /// <summary>
        /// ERROR_COUNT
        /// </summary>
        public static int ERROR_COUNT
        {
            get
            {
                if (eRROR_COUNT == 0)
                {
                    eRROR_COUNT = Convert.ToInt32(ConfigurationHelper.Section["OCR"]["ERROR_COUNT"].ToString());
                }
                return eRROR_COUNT;
            }
        }
        #endregion 
        
        #region PLC_DELAY
        private static int pLC_DELAY = 0;
        /// <summary>
        /// PLC_DELAY
        /// </summary>
        public static int PLC_DELAY
        {
            get
            {
                if (pLC_DELAY == 0)
                {
                    pLC_DELAY = Convert.ToInt32(ConfigurationHelper.Section["OCR"]["PLC_DELAY"].ToString());
                }
                return pLC_DELAY;
            }
        }
        #endregion 

        #region PLC_OFFSET
        private static int pLC_OFFSET = 0;
        /// <summary>
        /// ERROR_COUNT
        /// </summary>
        public static int PLC_OFFSET
        {
            get
            {
                if (pLC_OFFSET == 0)
                {
                    pLC_OFFSET = Convert.ToInt32(ConfigurationHelper.Section["OCR"]["PLC_OFFSET"].ToString());
                }
                return pLC_OFFSET;
            }
        }
        #endregion 

        #region CNT_RATE
        private static int cNT_RATE = 0;
        /// <summary>
        /// ERROR_COUNT
        /// </summary>
        public static int CNT_RATE
        {
            get
            {
                if (cNT_RATE == 0)
                {
                    cNT_RATE = Convert.ToInt32(ConfigurationHelper.Section["OCR"]["CNT_RATE"].ToString());
                }
                return cNT_RATE;
            }
        }
        #endregion 

        #region CNT_RATE_2
        private static int cNT_RATE_2 = 0;
        /// <summary>
        /// ERROR_COUNT
        /// </summary>
        public static int CNT_RATE_2
        {
            get
            {
                if (cNT_RATE_2 == 0)
                {
                    cNT_RATE_2 = Convert.ToInt32(ConfigurationHelper.Section["OCR"]["CNT_RATE_2"].ToString());
                }
                return cNT_RATE_2;
            }
        }
        #endregion 

        #region AP_DELAY
        private static int aP_DELAY = 0;
        /// <summary>
        /// ERROR_COUNT
        /// </summary>
        public static int AP_DELAY
        {
            get
            {
                if (aP_DELAY == 0)
                {
                    aP_DELAY = Convert.ToInt32(ConfigurationHelper.Section["OCR"]["AP_DELAY"].ToString());
                }
                return aP_DELAY;
            }
        }
        #endregion 
        
    }
}
