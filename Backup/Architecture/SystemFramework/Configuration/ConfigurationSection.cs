using System;
using System.Collections;
using System.Configuration;

using CCT.SystemFramework.Web;

namespace CCT.SystemFramework.Configuration
{
	/// <summary>
	/// ConfigurationSection 的摘要说明。
	/// </summary>
	public class ConfigurationSection
	{
		public Hashtable this[string sectionName]
		{
			get
			{
				string key = string.Format("ConfigurationSection_{0}", sectionName);
				Hashtable hashtable = CacheHelper.CacheDeserialize(key, typeof(Hashtable), false) as Hashtable;

				if ( hashtable == null )
				{
					try
					{
						hashtable = ConfigurationSettings.GetConfig(sectionName) as Hashtable;
						CacheHelper.CacheSerialize(key, hashtable, false);
					}
					catch ( ConfigurationException e )
					{
						throw e;
					}					
				}

				return hashtable;
			}
		}


		/// 构造函数
		public ConfigurationSection()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
