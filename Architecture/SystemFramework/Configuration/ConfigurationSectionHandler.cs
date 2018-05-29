using System;
using System.Xml;
using System.Collections;
using System.Configuration;

using CCT.SystemFramework.Xml;

namespace CCT.SystemFramework.Configuration
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
