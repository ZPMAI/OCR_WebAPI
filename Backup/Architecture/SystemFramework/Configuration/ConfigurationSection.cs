using System;
using System.Collections;
using System.Configuration;

using CCT.SystemFramework.Web;

namespace CCT.SystemFramework.Configuration
{
	/// <summary>
	/// ConfigurationSection ��ժҪ˵����
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


		/// ���캯��
		public ConfigurationSection()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}
