using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

using CCT.Common.Xml;

namespace CCT.Common.Configuration
{
	/// <summary>
	/// ConfigurationSectionHandler ��ժҪ˵����
	/// </summary>
    public class ConfigurationSectionHandler : IConfigurationSectionHandler
    {
        /// ���캯��
        public ConfigurationSectionHandler()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        /// ���������ýڴ������ʵ�֣��Է������ýڵ� XML
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
