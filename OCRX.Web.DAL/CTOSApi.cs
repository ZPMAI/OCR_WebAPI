using OCR.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OCRX.Web.DAL
{
    public class CTOSApi
    {
        public static string SUCCESSCODE = "0";

        public static CtosResult CM005001(string CONTAINERNO,string TICKET_ID)
        {
            CtosWebReference.ServiceSoapClient api = new CtosWebReference.ServiceSoapClient();
            string parms = string.Format("CONTAINERNO:'{0}',TICKET_ID:'{1}'", CONTAINERNO, TICKET_ID);

            string rs = api.CM005001(parms);

            return XMLParse(rs, new string[] { "CM_CONTAINERS", "CM_CONTAINERS_BINDINFO", "CM_CONTAINERIMDGINFO", "CM_CONTAINERDANGERINFO" }, true);
        }

        /// <summary>
        /// 解XML文本
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="methodName"></param>
        /// <param name="isDataset"></param>
        /// <returns></returns>
        public static CtosResult XMLParse(string xml, string[] methodName, bool isDataset)
        {
            CtosResult rs = new CtosResult();

            XmlDocument xx = new XmlDocument();
            xx.LoadXml(xml);
            XmlNode errorCode = xx.SelectSingleNode("/CTOSRESULT/RETURNINFO/ERRORCODE");
            rs.ERRORCODE = errorCode.InnerText;
            XmlNode errorMsg = xx.SelectSingleNode("/CTOSRESULT/RETURNINFO/ERRORMSG");
            rs.ERRORMSG = errorMsg.InnerText;

            //成功
            if (rs.ERRORCODE == SUCCESSCODE)
            {
                foreach (string name1 in methodName)
                {
                    if (isDataset)
                    {
                        XmlNodeList nodes = xx.SelectNodes("/CTOSRESULT/" + name1);
                        DataTable dt = new DataTable(name1);
                        rs.DS.Tables.Add(dt);

                        if (nodes != null)
                        {
                            int i = 0;
                            foreach (XmlNode node1 in nodes)
                            {
                                if (i == 0)
                                {
                                    foreach (XmlNode node2 in node1.ChildNodes)
                                    {   //创建列
                                        rs.DS.Tables[name1].Columns.Add(new DataColumn(node2.Name, typeof(string)));
                                    }
                                }

                                //创建行
                                DataRow dr = rs.DS.Tables[name1].NewRow();
                                foreach (XmlNode node2 in node1.ChildNodes)
                                {
                                    dr[node2.Name] = node2.InnerText;
                                }
                                rs.DS.Tables[name1].Rows.Add(dr);

                                i++;
                            }
                        }
                    }
                    else
                    {
                        XmlNode node1 = xx.SelectSingleNode("/CTOSRESULT/" + name1);
                        if (node1 != null)
                        {
                            foreach (XmlNode node2 in node1.ChildNodes)
                            {
                                rs.DIC.Add(node2.Name, node2.InnerText);
                            }
                        }
                    }
                }
            }

            return rs;
        }
    }
}
