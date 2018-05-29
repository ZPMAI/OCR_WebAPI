using System;
using System.IO;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Xml;
using System.Xml.Serialization;

namespace CCT.SystemFramework.Web
{
	/// <summary>
	/// CacheHelper ��ժҪ˵����
	/// </summary>
	public sealed class CacheHelper
	{
		/// ���ݿ����������� Cache
		private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

		/// ���캯��
		public CacheHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		
		/// <summary>
		/// ���� Cache ֵ
		/// </summary>
		/// <param name="key">��</param>
		/// <param name="cacheValue">Cache ֵ</param>
		/// <param name="time">����ʱ�䣨�Է���Ϊ��λ��</param>
		public static void SetCache(string key, object cacheValue, int time)
		{
			CacheDependency cacheDependency = new CacheDependency(Path.GetTempFileName());

			HttpContext.Current.Cache.Insert(key, cacheValue, cacheDependency, DateTime.Now.AddMinutes(time), TimeSpan.Zero);
		}


		/// <summary>
		/// ��ȡ Cache ֵ
		/// </summary>
		/// <param name="key">��</param>
		/// <returns>Cache ֵ</returns>
		public static object GetCache(string key)
		{
			return HttpContext.Current.Cache[key];
		}


		#region ʹ��ָ���� XmlWriter ���л�ָ���� Object ���� XML �ĵ�д���ļ�

		/// <summary>
		/// ʹ��ָ���� XmlWriter ���л�ָ���� Object ���� XML �ĵ�д���ļ�
		/// </summary>
		/// <param name="filePath">XML �ĵ�·��</param>
		/// <param name="data">����</param>
		public static void XmlSerialize(string filePath, object data)
		{
			string directoryPath = filePath.Substring(0, filePath.LastIndexOf("\\"));

			if ( ! File.Exists(filePath) )
			{
				try
				{
					if ( ! Directory.Exists(directoryPath) )
					{
						Directory.CreateDirectory(directoryPath);
					}

					using ( TextWriter textWriter = new StreamWriter(filePath) )
					{				
						XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());						
						xmlSerializer.Serialize(textWriter, data);
					}
				}
				catch ( Exception e )
				{
					throw e;
				}
			}
		}


		#endregion

		#region �����л�ָ�� XmlReader ˵������ XML �ĵ�

		/// <summary>
		/// �����л�ָ�� XmlReader ˵������ XML �ĵ�
		/// </summary>
		/// <param name="filePath">XML �ĵ�·��</param>
		/// <param name="type">��������</param>
		/// <returns>����</returns>
		public static object XmlDeserialize(string filePath, Type type)
		{
			object obj = null;

			if ( File.Exists(filePath) )
			{
				FileStream fileStream = new FileStream(filePath, FileMode.Open);

				try
				{	
					XmlSerializer xmlSerializer = new XmlSerializer(type);

					obj = xmlSerializer.Deserialize(fileStream);
				}
				catch ( Exception e )
				{
					throw e;
				}
				finally
				{
					fileStream.Close();
				}
			}

			return obj;
		}


		#endregion

		#region ��ָ���� Object ������ٻ��沢ʹ��ָ���� XmlWriter ���л�ָ���� Object ���� XML �ĵ�д���ļ�

		/// <summary>
		/// �����������ٻ��沢ʹ��ָ���� XmlWriter ���л�ָ���� Object ���� XML �ĵ�д���ļ�
		/// </summary>
		/// <param name="filePath">XML �ĵ�·��</param>
		/// <param name="data">����</param>
		/// <param name="serialize">�Ƿ񱾵����л�</param>
		public static void CacheSerialize(string filePath, object data, bool serialize)
		{
			paramCache[filePath] = data;

			if ( serialize )
			{
				XmlSerialize(filePath, data);
			}
		}

		#endregion
		
		#region ��ȡ���ٻ��沢�����л�ָ�� XmlReader ˵������ XML �ĵ�

		/// <summary>
		/// ��ȡ���ٻ��沢�����л�ָ�� XmlReader ˵������ XML �ĵ�
		/// </summary>
		/// <param name="filePath">�ļ�·��</param>
		/// <param name="type">��������</param>
		/// <param name="serialize">�Ƿ񱾵����л�</param>
		/// <returns>����</returns>
		public static object CacheDeserialize(string filePath, Type type, bool serialize)
		{
			if ( paramCache[filePath] == null )
			{
				if ( serialize )
				{
					paramCache[filePath] = XmlDeserialize(filePath, type);
				}
			}

			return paramCache[filePath];
		}

		#endregion		
	}
}
