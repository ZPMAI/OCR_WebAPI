using System;
using System.Collections;
using System.Configuration;

namespace CCT.Common.Configuration
{
	/// <summary>
	/// ConfigurationHelper 的摘要说明。
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


		/// 构造函数
		public ConfigurationHelper(string sectionName)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
