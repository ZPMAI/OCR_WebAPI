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
	/// CacheHelper 的摘要说明。
	/// </summary>
	public sealed class CacheHelper
	{
		/// 数据库操作命令参数 Cache
		private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

		/// 构造函数
		public CacheHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		
		/// <summary>
		/// 设置 Cache 值
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="cacheValue">Cache 值</param>
		/// <param name="time">持续时间（以分钟为单位）</param>
		public static void SetCache(string key, object cacheValue, int time)
		{
			CacheDependency cacheDependency = new CacheDependency(Path.GetTempFileName());

			HttpContext.Current.Cache.Insert(key, cacheValue, cacheDependency, DateTime.Now.AddMinutes(time), TimeSpan.Zero);
		}


		/// <summary>
		/// 获取 Cache 值
		/// </summary>
		/// <param name="key">键</param>
		/// <returns>Cache 值</returns>
		public static object GetCache(string key)
		{
			return HttpContext.Current.Cache[key];
		}


		#region 使用指定的 XmlWriter 序列化指定的 Object 并将 XML 文档写入文件

		/// <summary>
		/// 使用指定的 XmlWriter 序列化指定的 Object 并将 XML 文档写入文件
		/// </summary>
		/// <param name="filePath">XML 文档路径</param>
		/// <param name="data">对象</param>
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

		#region 反序列化指定 XmlReader 说包含的 XML 文档

		/// <summary>
		/// 反序列化指定 XmlReader 说包含的 XML 文档
		/// </summary>
		/// <param name="filePath">XML 文档路径</param>
		/// <param name="type">对象类型</param>
		/// <returns>对象</returns>
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

		#region 将指定的 Object 放入高速缓存并使用指定的 XmlWriter 序列化指定的 Object 并将 XML 文档写入文件

		/// <summary>
		/// 将对象放入高速缓存并使用指定的 XmlWriter 序列化指定的 Object 并将 XML 文档写入文件
		/// </summary>
		/// <param name="filePath">XML 文档路径</param>
		/// <param name="data">对象</param>
		/// <param name="serialize">是否本地序列化</param>
		public static void CacheSerialize(string filePath, object data, bool serialize)
		{
			paramCache[filePath] = data;

			if ( serialize )
			{
				XmlSerialize(filePath, data);
			}
		}

		#endregion
		
		#region 读取高速缓存并反序列化指定 XmlReader 说包含的 XML 文档

		/// <summary>
		/// 读取高速缓存并反序列化指定 XmlReader 说包含的 XML 文档
		/// </summary>
		/// <param name="filePath">文件路径</param>
		/// <param name="type">对象类型</param>
		/// <param name="serialize">是否本地序列化</param>
		/// <returns>对象</returns>
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
