using System;
using System.Collections;
using System.Configuration;

namespace CCT.Common.Configuration
{
	/// <summary>
	/// ConfigurationHelper ��ժҪ˵����
	/// </summary>
	public class ConfigurationHelper
	{
		private static ConfigurationSection section;

		public static ConfigurationSection Section
		{
			get
			{
				if ( section == null )
				{
					section = new ConfigurationSection();
				}

				return section;
			}
		}


		/// ���캯��
		public ConfigurationHelper(string sectionName)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}
