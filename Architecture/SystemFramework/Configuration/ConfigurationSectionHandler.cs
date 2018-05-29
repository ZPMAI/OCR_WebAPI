using System;
using System.Xml;
using System.Collections;
using System.Configuration;

using CCT.SystemFramework.Xml;

namespace CCT.SystemFramework.Configuration
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
			IDictionary hashtable = new Hashtable();
			IList xmlNodeList = XmlHelper.GetChildElements(section, "add");

			foreach ( XmlNode xmlNode in xmlNodeList )
			{
				string key = XmlHelper.GetAttributeText(xmlNode, "key");
				string _value = XmlHelper.GetAttributeText(xmlNode, "value");

				hashtable.Add(key, _value);
			}

			return hashtable;
		}
	}
}
