using System;
using System.IO;
using System.Xml;
using System.Net;
using System.Text;
using System.Collections;

namespace CCT.SystemFramework.Xml
{
	/// <summary>
	/// XmlHelper ��ժҪ˵����
	/// </summary>
	public class XmlHelper
	{
		/// ���캯��
		public XmlHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		/// ���� XML �ĵ�
		public static XmlDocument LoadXmlFile(string filePath)
		{			
			XmlDocument xmlDocument = new XmlDocument();
			FileInfo fileInfo = new FileInfo(filePath);

			if ( ! fileInfo.Exists )
			{
				throw new FileNotFoundException(string.Format("�Ҳ���ָ���� XML �ļ���{0}��", filePath));
			}

			Uri uri = new Uri(fileInfo.FullName);

			using ( Stream stream = WebRequest.Create(uri).GetResponse().GetResponseStream() )
			{
				xmlDocument.Load(stream);
				stream.Close();
			}
			
			return xmlDocument;
		}
		

		/// ѡ��ƥ�� XPath ���ʽ�ĵ�һ�� XmlNode
		public static XmlNode GetChildElement(XmlDocument xmlDocument, string xpath)
		{
			return xmlDocument.SelectSingleNode(xpath);
		}


		/// ѡ��ƥ�� XPath ���ʽ�ĵ�һ�� XmlNode
		public static XmlNode GetChildElement(XmlNode element, string xpath)
		{
			return element.SelectSingleNode(xpath);
		}


		/// ѡ��ƥ�� XPath ���ʽ�Ľڵ��б�
		public static IList GetChildElements(XmlNode element)
		{
			IList arrayList = new ArrayList();
			XmlNodeList xmlNodeList = element.ChildNodes;

			foreach ( XmlNode xmlNode in xmlNodeList )
			{
				if ( xmlNode.NodeType == XmlNodeType.Element )
				{
					arrayList.Add(xmlNode);
				}
			}

			return arrayList;
		}


		/// ѡ��ƥ�� XPath ���ʽ�Ľڵ��б�
		public static IList GetChildElements(XmlNode element, string xpath)
		{
			IList arrayList = new ArrayList();
			XmlNodeList xmlNodeList = element.SelectNodes(xpath);

			foreach ( XmlNode xmlNode in xmlNodeList )
			{
				if ( xmlNode.NodeType == XmlNodeType.Element )
				{
					arrayList.Add(xmlNode);
				}
			}

			return arrayList;
		}


		/// ѡ��ƥ�� XPath ���ʽ�ĵ�һ�� XmlNode ��ֵ
		public static string GetChildValue(XmlNode element, string childName)
		{
			XmlNode xmlNode = GetChildElement(element, childName);

			if ( xmlNode == null )
			{
				return null;
			}

			return GetValue(xmlNode);
		}


		/// ��ȡ�ڵ��ֵ
		public static string GetValue(XmlNode element)
		{
			XmlNodeList xmlNodeList = element.ChildNodes;

			foreach ( XmlNode xmlNode in xmlNodeList )
			{
				if ( ( xmlNode.NodeType == XmlNodeType.CDATA ) 
					|| ( xmlNode.NodeType == XmlNodeType.Text ) )
				{
					return xmlNode.Value;
				}
			}

			return String.Empty;
		}


		/// ��ȡ�ڵ㼰�������ӽڵ�Ĵ���ֵ
		public static string GetAttributeText(XmlNode xmlNode, string localName)
		{
			XmlNode xmlAttribute = xmlNode.Attributes.GetNamedItem(localName);

			if ( xmlAttribute == null )
			{
				return String.Empty;
			}

			return xmlAttribute.InnerText;
		}


		public static string Encode(object o)
		{
			if ( o == null )
			{
				return String.Empty;
			}

			char[] chars = o.ToString().ToCharArray();
			StringBuilder stringBuilder = new StringBuilder();

			foreach ( char c in chars )
			{
				switch ( c )
				{
					case '&' :
					{
						stringBuilder.Append("&amp;");
						break;
					}
					case '<' :
					{
						stringBuilder.Append("&lt;");
						break;
					}
					case '>' :
					{
						stringBuilder.Append("&gt;");
						break;
					}
					case '\"' :
					{
						stringBuilder.Append("&quot;");
						break;
					}
					default :
					{
						stringBuilder.Append(c);
						break;
					}
				}
			}

			return stringBuilder.ToString();
		}
	}
}
