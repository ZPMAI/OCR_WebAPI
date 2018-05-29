using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.IO;
using System.Reflection;

namespace CCT.Common.Reflection
{
    /// <summary>
    /// ObjectUtil ��ժҪ˵����
    /// </summary>
    public static class ObjectHelper
    {
        /// ���������ƴ��������ʵ��
        public static object CreateInstance(string name)
        {
            string[] ss = name.Split(',');
            string className = ss[0].Trim();
            string assemblyString = ss[1].Trim();

            if (File.Exists(assemblyString))
            {
                throw new FileNotFoundException(string.Format("�Ҳ���ָ���ĳ����ļ���{0}", assemblyString));
            }
            
            Assembly assembly = Assembly.Load(assemblyString);
            return assembly.CreateInstance(className);

            #region ����һ�ַ���

            //AppDomainSetup appDomainSetup = new AppDomainSetup();
            //appDomainSetup.ApplicationBase = string.Format("file:///{0}", Environment.CurrentDirectory);
            //AppDomain appDomain = AppDomain.CreateDomain("RemoteDomain", null, appDomainSetup);

            //try
            //{
            //Assembly assembly = Assembly.Load(assemblyString);

            //Type type = Type.GetType(name);
            //ConstructorInfo[] constructors = type.GetConstructors();

            //if (constructors.Length == 0)
            //{
            //    return null;
            //}

            //ParameterInfo[] parameterInfos = constructors[0].GetParameters();
            //int length = parameterInfos.Length;
            //Type[] constructor = new Type[length];

            //for (int i = 0; i < length; i++)
            //{
            //    constructor[i] = parameterInfos[i].ParameterType;
            //}

            //ConstructorInfo constructorInfo = type.GetConstructor(constructor);
            //object obj = constructorInfo.Invoke(new object[] { });

            //if (obj == null)
            //{
            //    throw new Exception(string.Format("������ʵ�� {0} ʧ�ܣ�", className));
            //}

            //return obj;
            //}
            //finally
            //{
            //    AppDomain.Unload(appDomain);
            //}

            #endregion
        }
    }
}
