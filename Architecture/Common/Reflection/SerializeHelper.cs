using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace CCT.Common.Reflection
{
	/// <summary>
    /// SerializeHelper ��ժҪ˵����
	/// </summary>
    public static class SerializeHelper
    {
        /// ����������ָ�������������Ķ���ͼ�����л�Ϊ�������ṩ����ĸ�����
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


        /// ��ָ�����������л�Ϊ����ͼ��
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
