using System;
using System.IO;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Xml;
using System.Xml.Serialization;

namespace CCT.Common.Caching
{
    /// <summary>
    /// CacheHelper 的摘要说明。
    /// </summary>
    public static class CacheHelper
    {
        /// 数据库操作命令参数 Cache
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());


        /// <summary>
        /// 设置 Cache 值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="cacheValue">Cache 值</param>
        /// <param name="time">持续时间（以分钟为单位）</param>
        public static void SetCache(string key, object value, int time)
        {
            CacheDependency cacheDependency = new CacheDependency(Path.GetTempFileName());

            HttpContext.Current.Cache.Insert(key, value, cacheDependency, DateTime.Now.AddMinutes(time), TimeSpan.Zero);
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
            #warning Noticed by RQ 2010-11-29 14:58 指定缓存路径，格式化名称 ~ ! @ # $ % ^ * ( ) _ + 。

            string directoryPath = filePath.Substring(0, filePath.LastIndexOf("\\"));

            if (!File.Exists(filePath))
            {
                try
                {
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (TextWriter textWriter = new StreamWriter(filePath))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
                        xmlSerializer.Serialize(textWriter, data);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        #endregion

        #region 反序列化指定 XmlReader 所包含的 XML 文档

        /// <summary>
        /// 反序列化指定 XmlReader 所包含的 XML 文档
        /// </summary>
        /// <param name="filePath">XML 文档路径</param>
        /// <param name="type">对象类型</param>
        /// <returns>对象</returns>
        public static object XmlDeserialize(string filePath, Type type)
        {
            object obj = null;

            if (File.Exists(filePath))
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Open);

                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(type);

                    obj = xmlSerializer.Deserialize(fileStream);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    fileStream.Close();
                }
            }

            return obj;
        }


        #endregion

        #region 将指定的 Object 放入高速缓存

        /// <summary>
        /// 将对象放入高速缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="data">值</param>
        public static void CacheSerialize(string key, object value)
        {
            paramCache[key] = value;            
        }

        #endregion

        #region 读取高速缓存

        /// <summary>
        /// 读取高速缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static object CacheDeserialize(string key)
        {
            return paramCache[key];
        }

        #endregion
    }
}
