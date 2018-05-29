using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace CCT.Common.Reflection
{
	/// <summary>
    /// SerializeHelper 的摘要说明。
	/// </summary>
    public static class SerializeHelper
    {
        /// 将对象或具有指定顶级（根）的对象图形序列化为附加所提供标题的给定流
        public static byte[] Serialize<T>(T obj)
        {
            byte[] content;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.TypeFormat = FormatterTypeStyle.TypesWhenNeeded;

                    binaryFormatter.Serialize(memoryStream, obj);
                    content = memoryStream.ToArray();
                }
                finally
                {
                    memoryStream.Close();
                }
            }

            return content;
        }


        /// 将指定的流反序列化为对象图形
        public static T Deserialize<T>(byte[] content)
        {
            using (Stream stream = new MemoryStream(content))
            {
                return Deserialize<T>(stream);
            }
        }


        public static T Deserialize<T>(Stream stream)
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.TypeFormat = FormatterTypeStyle.TypesWhenNeeded;

                return (T)binaryFormatter.Deserialize(stream);
            }
            catch
            {
                return default(T);
            }
            finally
            {
                stream.Close();
            }
        }
    }
}
