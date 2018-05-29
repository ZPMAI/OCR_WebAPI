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
    /// 先获得 Setter 的委托 Action<object> set = Delegate.CreateDelegate(typeof(Action<object>), entity, Property.GetSetMethod()) as Action<object>;  
    /// 然后直接 set(reader[Property.Name]) 可以大幅度提高性能，并且还可以通过缓存 set 避免反复反射来进一步提高性能。
    /// 缓存 Setter 方法行不通，因为 entity 不可以动态改变
    /// </remarks>
    public static class DynamicMethodContainer
    {
        private static Dictionary<string, DynamicMethod> dictionary = new Dictionary<string, DynamicMethod>();

        /// 构造函数
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
