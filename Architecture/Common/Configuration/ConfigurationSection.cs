using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

using CCT.Common.Caching;

namespace CCT.Common.Configuration
{
    /// <summary>
    /// ConfigurationSection ��ժҪ˵����
    /// </summary>
    public class ConfigurationSection
    {
        public Dictionary<string, string> this[string sectionName]
        {
            get
            {
                string key = string.Format("ConfigurationSection_{0}", sectionName);
                Dictionary<string, string> dictionary = CacheHelper.CacheDeserialize(key) as Dictionary<string, string>;

                if (dictionary == null)
                {
                    try
                    {
                        dictionary = ConfigurationManager.GetSection(sectionName) as Dictionary<string, string>;
                        CacheHelper.CacheSerialize(key, dictionary);
                    }
                    catch
                    {
                        throw;
                    }
                }

                return dictionary;
            }
        }


        /// ���캯��
        public ConfigurationSection()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
    }
}
