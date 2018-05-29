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
    /// CacheHelper ��ժҪ˵����
    /// </summary>
    public static class CacheHelper
    {
        /// ���ݿ����������� Cache
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());


        /// <summary>
        /// ���� Cache ֵ
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="cacheValue">Cache ֵ</param>
        /// <param name="time">����ʱ�䣨�Է���Ϊ��λ��</param>
        public static void SetCache(string key, object value, int time)
        {
            CacheDependency cacheDependency = new CacheDependency(Path.GetTempFileName());

            HttpContext.Current.Cache.Insert(key, value, cacheDependency, DateTime.Now.AddMinutes(time), TimeSpan.Zero);
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
            #warning Noticed by RQ 2010-11-29 14:58 ָ������·������ʽ������ ~ ! @ # $ % ^ * ( ) _ + ��

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

        #region �����л�ָ�� XmlReader �������� XML �ĵ�

        /// <summary>
        /// �����л�ָ�� XmlReader �������� XML �ĵ�
        /// </summary>
        /// <param name="filePath">XML �ĵ�·��</param>
        /// <param name="type">��������</param>
        /// <returns>����</returns>
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

        #region ��ָ���� Object ������ٻ���

        /// <summary>
        /// �����������ٻ���
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="data">ֵ</param>
        public static void CacheSerialize(string key, object value)
        {
            paramCache[key] = value;            
        }

        #endregion

        #region ��ȡ���ٻ���

        /// <summary>
        /// ��ȡ���ٻ���
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>ֵ</returns>
        public static object CacheDeserialize(string key)
        {
            return paramCache[key];
        }

        #endregion
    }
}
