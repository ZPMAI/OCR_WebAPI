using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace DBFramework
{
    /// <remarks>    
    /// �Ȼ�� Setter ��ί�� Action<object> set = Delegate.CreateDelegate(typeof(Action<object>), entity, Property.GetSetMethod()) as Action<object>;  
    /// Ȼ��ֱ�� set(reader[Property.Name]) ���Դ����������ܣ����һ�����ͨ������ set ���ⷴ����������һ��������ܡ�
    /// ���� Setter �����в�ͨ����Ϊ entity �����Զ�̬�ı�
    /// </remarks>
    public static class DynamicMethodContainer
    {
        private static Dictionary<string, DynamicMethod> dictionary = new Dictionary<string, DynamicMethod>();

        /// ���캯��
        static DynamicMethodContainer()
        {
        }


        public static bool IsExist(string key)
        {
            return dictionary.ContainsKey(key);
        }


        public static void Add(string key, DynamicMethod d)
        {
            if (IsExist(key)) 
            { 
                return; 
            }

            dictionary.Add(key, d);
        }


        public static DynamicMethod Get(string key)
        {
            if (!IsExist(key)) 
            { 
                return null; 
            }

            return dictionary[key];
        }
    }
}
