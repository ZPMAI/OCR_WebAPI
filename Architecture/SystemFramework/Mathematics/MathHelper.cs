using System;

namespace CCT.SystemFramework.Mathematics
{
	/// <summary>
	/// MathHelper ��ժҪ˵����
	/// </summary>
	public class MathHelper
	{
		public MathHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="p_value">ԭֵ</param>
		/// <param name="p_dight">С��λ��</param>
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
