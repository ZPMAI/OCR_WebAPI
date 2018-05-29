using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

using CCT.Common.Xml;

namespace CCT.Common.Configuration
{
	/// <summary>
	/// ConfigurationSectionHandler 的摘要说明。
	/// </summary>
    public class ConfigurationSectionHandler : IConfigurationSectionHandler
    {
        /// 构造函数
        public ConfigurationSectionHandler()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        /// 由所有配置节处理程序实现，以分析配置节的 XML
        public object Create(object parent, object configContext, XmlNode section)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            List<XmlNode> xmlNodeList = XmlHelper.GetChildElements(section, "add");

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                string key = XmlHelper.GetAttributeText(xmlNode, "key");
                string value = XmlHelper.GetAttributeText(xmlNode, "value");

                dictionary.Add(key, value);
            }

            return dictionary;
        }
    }
}
