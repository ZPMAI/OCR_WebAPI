using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;

namespace CCT.Common.Text
{
	/// <summary>
	/// TextHelper 的摘要说明。
	/// </summary>
	public static class TextHelper
	{
		#region 数据类型转换

		/// <summary>
		/// 字符串类型转换
		/// </summary>
		/// <param name="inputValue">输入值</param>
		/// <returns>输出值</returns>
		public static string ConvertToString(object inputValue)
		{
			string outputValue = String.Empty;

			try
			{
				outputValue = Convert.ToString(inputValue);
			}
			catch
			{}

			return outputValue;
		}


		/// <summary>
		/// 布尔类型转换
		/// </summary>
		/// <param name="inputValue">输入值</param>
		/// <returns>输出值</returns>
		public static bool ConvertToBool(object inputValue)
		{
			bool outputValue = false;

			try
			{
				outputValue = Convert.ToBoolean(inputValue);
			}
			catch
			{}

			return outputValue;
		}


		/// <summary>
		/// 16位有符合整型类型转换
		/// </summary>
		/// <param name="inputValue">输入值</param>
		/// <returns>输出值</returns>
		public static byte ConvertToByte(object inputValue)
		{
			byte outputValue = 0;

			try
			{
				outputValue = Convert.ToByte(inputValue);
			}
			catch
			{}

			return outputValue;
		}


		/// <summary>
		/// 16位有符合整型类型转换
		/// </summary>
		/// <param name="inputValue">输入值</param>
		/// <returns>输出值</returns>
		public static Int16 ConvertToInt16(object inputValue)
		{
			Int16 outputValue = 0;

			try
			{
				outputValue = Convert.ToInt16(inputValue);
			}
			catch
			{}

			return outputValue;
		}


		/// <summary>
		/// 整型类型转换
		/// </summary>
		/// <param name="inputValue">输入值</param>
		/// <returns>输出值</returns>
		public static int ConvertToInt(object inputValue)
		{
			int outputValue = 0;

			try
			{
				outputValue = Convert.ToInt32(inputValue);
			}
			catch
			{}

			return outputValue;
		}


		/// <summary>
		/// 64位有符合整型类型转换
		/// </summary>
		/// <param name="inputValue">输入值</param>
		/// <returns>输出值</returns>
		public static Int64 ConvertToInt64(object inputValue)
		{
			Int64 outputValue = 0;

			try
			{
				outputValue = Convert.ToInt64(inputValue);
			}
			catch
			{}

			return outputValue;
		}


		/// <summary>
		/// Decimal 类型转换
		/// </summary>
		/// <param name="inputValue">输入值</param>
		/// <returns>输出值</returns>
		public static decimal ConvertToDecimal(object inputValue)
		{
			decimal outputValue = 0;

			try
			{
				outputValue = Convert.ToDecimal(inputValue);
			}
			catch
			{}

			return outputValue;
		}


		/// <summary>
		/// 双精度类型转换
		/// </summary>
		/// <param name="inputValue">输入值</param>
		/// <returns>输出值</returns>
		public static double ConvertToDouble(object inputValue)
		{
			double outputValue = 0;

			try
			{
				outputValue = Convert.ToDouble(inputValue);
			}
			catch
			{}

			return outputValue;
		}


		/// <summary>
		/// 浮点型类型转换
		/// </summary>
		/// <param name="inputValue">输入值</param>
		/// <returns>输出值</returns>
		public static float ConvertToFloat(object inputValue)
		{
			float outputValue = 0;

			try
			{
				outputValue = float.Parse(inputValue.ToString());
			}
			catch
			{}

			return outputValue;
		}


		/// <summary>
		/// 日期类型转换
		/// </summary>
		/// <param name="inputValue">输入值</param>
		/// <returns>输出值</returns>
		public static DateTime? ConvertToDateTime(object inputValue)
		{
			try
			{
				// 1/1/1753 12:00:00 AM 和 12/31/9999 11:59:59 PM 之间
				DateTime outputValue = Convert.ToDateTime(inputValue);

				if ( ( outputValue > DateTime.Parse("1753-01-01 12:00:00") )
					&& ( outputValue < DateTime.Parse("9999-01-01 23:59:59") ) )
				{
					return outputValue;
				}
			}
			catch
			{}

			return null;
		}

		/// <summary>
		/// 格式化日期字符串输出
		/// </summary>
		/// <param name="inputValue">输入值</param>
		/// <param name="format">格式化字符串</param>
		/// <returns>输出值</returns>
		public static string ToDateTimeString(object inputValue, string format)
		{
			string outputValue = string.Empty;

			try
			{
				DateTime formatValue = Convert.ToDateTime(inputValue);

				if ( ( formatValue > DateTime.Parse("1753-01-01 12:00:00") )
					&& ( formatValue < DateTime.Parse("9999-01-01 23:59:59") ) 
					&& ( formatValue.Year != 1900 ) )
				{
					outputValue =  formatValue.ToString(format);
				}
			}
			catch
			{
				outputValue = string.Empty;
			}

			return outputValue;
		}


		#endregion

        #region 四舍五入

        /// <summary>
		/// 实现数据的四舍五入法
		/// </summary>
		/// <param name="d">要进行处理的数据</param>
		/// <param name="decimals">保留的小数位数</param>
		/// <returns>四舍五入后的结果</returns>
		public static double Round(double d, int decimals)
		{
			double output = d;
			bool isNegative = false;

			// 如果是负数
			if ( d < 0 )
			{
				isNegative = true;
				output = -output;
			}

			int decuple = 1;

			for ( int i = 1; i <= decimals; i++ )
			{
				decuple *= 10;
			}

			double Int = Math.Round(output * decuple + 0.5, 0);
			output = Int / decuple;
		        
			if ( isNegative )
			{
				output = -output;
			}

			return output;
		}


		/// <summary>
		/// 实现数据的四舍五入法
		/// </summary>
		/// <param name="d">要进行处理的数据</param>
		/// <param name="decimals">保留的小数位数</param>
		/// <returns>四舍五入后的结果</returns>
		public static double Round(decimal d, int decimals)
		{
			double output = (double)d;
			bool isNegative = false;

			// 如果是负数
			if ( d < 0 )
			{
				isNegative = true;
				output = -output;
			}

			int decuple = 1;

			for ( int i = 1; i <= decimals; i++ )
			{
				decuple *= 10;
			}

			double Int = Math.Round(output * decuple + 0.5, 0);
			output = Int / decuple;
		        
			if ( isNegative )
			{
				output = -output;
			}

			return output;
        }

        #endregion

        /// <summary>
		/// 格式化 Script 语句输出
		/// </summary>
		/// <param name="input">输入值</param>
		/// <returns>输出值</returns>
		public static string FormatScript(string input)
		{
			StringBuilder stringBuilder = new StringBuilder(input);			
			stringBuilder.Replace("\"", "\\\"");
			stringBuilder.Replace("\'", "\\'");
			stringBuilder.Replace("\t", "\\t");
			stringBuilder.Replace("\r", "\\r");
			stringBuilder.Replace("\n", "\\n");
			stringBuilder.Replace("\\u", "\\\\u");

			return stringBuilder.ToString();
		}


		/// <summary>
		/// 格式化 HTML 语句输出
		/// </summary>
		/// <param name="input">输入值</param>
		/// <returns>输出值</returns>
		public static string FormatHTML(string input)
		{
			StringBuilder stringBuilder = new StringBuilder(input);

			stringBuilder.Replace("&", "&amp;");
			stringBuilder.Replace("<", "&lt;");
			stringBuilder.Replace(">", "&gt;");
			stringBuilder.Replace("\"", "&quot;");
			stringBuilder.Replace("'", "&#39;");
			stringBuilder.Replace(" ", "&nbsp;");
			stringBuilder.Replace("\r\n", "<BR>");
			stringBuilder.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");

			return stringBuilder.ToString();
		}


		/// <summary>
		/// 格式化 XML 语句输出
		/// </summary>
		/// <param name="input">输入值</param>
		/// <returns>输出值</returns>
		public static string FormatXML(string input)
		{
			StringBuilder stringBuilder = new StringBuilder(input);

			stringBuilder.Replace("&", "&amp;");
			stringBuilder.Replace("<", "&lt;");
			stringBuilder.Replace(">", "&gt;");
			stringBuilder.Replace("\"", "&quot;");
			stringBuilder.Replace("'", "&apos;");

			return stringBuilder.ToString();
		}


        ///// <summary>
        ///// 将 PL/SQL 字符串转换为安全的 PL/SQL 语句
        ///// </summary>
        ///// <param name="input">输入值</param>
        ///// <returns>转换后的SQL语句</returns>
        //public static string FormatSQL(string input)
        //{
        //    StringBuilder stringBuilder = new StringBuilder(input); 
        //    stringBuilder.Replace("'", "''");
			
        //    return stringBuilder.ToString();
        //}				
	}
}
