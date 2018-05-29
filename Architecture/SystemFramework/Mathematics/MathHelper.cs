using System;

namespace CCT.SystemFramework.Mathematics
{
	/// <summary>
	/// MathHelper 的摘要说明。
	/// </summary>
	public class MathHelper
	{
		public MathHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 四舍五入
		/// </summary>
		/// <param name="p_value">原值</param>
		/// <param name="p_dight">小数位数</param>
		/// <returns></returns>
		public static decimal Round(decimal p_value,int p_dight)
		{			
			decimal l_tmp = decimal.Zero;
			l_tmp = (p_value > decimal.Zero ? (decimal)5 : (decimal)-5) / (decimal)System.Math.Pow(10, p_dight + 1);
			p_value = p_value + l_tmp;
			int l_tmp2 = (int)(p_value * (decimal)System.Math.Pow(10, p_dight));
			return p_value = l_tmp2 / (decimal)System.Math.Pow(10,p_dight);				
		}
	}
}
