using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;

namespace CCT.SystemFramework.Text
{
	/// <summary>
	/// TextHelper ��ժҪ˵����
	/// </summary>
	public sealed class TextHelper
	{
		/// ˽�й��캯��
		private TextHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		#region ��������ת��

		/// <summary>
		/// �ַ�������ת��
		/// </summary>
		/// <param name="inputValue">����ֵ</param>
		/// <returns>���ֵ</returns>
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
		/// ��������ת��
		/// </summary>
		/// <param name="inputValue">����ֵ</param>
		/// <returns>���ֵ</returns>
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
		/// 16λ�з�����������ת��
		/// </summary>
		/// <param name="inputValue">����ֵ</param>
		/// <returns>���ֵ</returns>
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
		/// 16λ�з�����������ת��
		/// </summary>
		/// <param name="inputValue">����ֵ</param>
		/// <returns>���ֵ</returns>
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
		/// ��������ת��
		/// </summary>
		/// <param name="inputValue">����ֵ</param>
		/// <returns>���ֵ</returns>
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
		/// 64λ�з�����������ת��
		/// </summary>
		/// <param name="inputValue">����ֵ</param>
		/// <returns>���ֵ</returns>
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
		/// Decimal ����ת��
		/// </summary>
		/// <param name="inputValue">����ֵ</param>
		/// <returns>���ֵ</returns>
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
		/// ˫��������ת��
		/// </summary>
		/// <param name="inputValue">����ֵ</param>
		/// <returns>���ֵ</returns>
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
		/// ����������ת��
		/// </summary>
		/// <param name="inputValue">����ֵ</param>
		/// <returns>���ֵ</returns>
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
		/// ��������ת��
		/// </summary>
		/// <param name="inputValue">����ֵ</param>
		/// <returns>���ֵ</returns>
		public static DateTime ConvertToDateTime(object inputValue)
		{
			try
			{
				// 1/1/1753 12:00:00 AM �� 12/31/9999 11:59:59 PM ֮��
				DateTime outputValue = Convert.ToDateTime(inputValue);

				if ( ( outputValue > DateTime.Parse("1753-01-01 12:00:00") )
					&& ( outputValue < DateTime.Parse("9999-01-01 23:59:59") ) )
				{
					return outputValue;
				}
			}
			catch
			{}

			return DateTime.Parse("1900-01-01 00:00:00");
		}


		/// <summary>
		/// ��ʽ�������ַ������
		/// </summary>
		/// <param name="inputValue">����ֵ</param>
		/// <param name="format">��ʽ���ַ���</param>
		/// <returns>���ֵ</returns>
		public static string ToDateTimeString(object inputValue, string format)
		{
			string outputValue = string.Empty;

			try
			{
				DateTime formatValue = Convert.ToDateTime(inputValue);

				if ( ( formatValue > DateTime.Parse("1753-01-01 12:00:00") )
					&& ( formatValue < DateTime.Parse("9999-01-01 23:59:59") ) )
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

		/// <summary>
		/// Removes the local servername from A and IMG tags
		/// </summary>
		public string RemoveServerNameFromUrls(string input, string servername) 
		{
			// HttpContext.Current.Trace.Write("ServerName", servername);

			input = Regex.Replace(input, "HREF=http://" + servername, "HREF=", RegexOptions.IgnoreCase);
			input = Regex.Replace(input, "HREF=\"http://" + servername, "HREF=\"", RegexOptions.IgnoreCase);
			input = Regex.Replace(input, "src=http://" + servername, "src=", RegexOptions.IgnoreCase);
			input = Regex.Replace(input, "src=\"http://" + servername, "src=\"", RegexOptions.IgnoreCase);
			
			return input;
		}


		/// <summary>
		/// Removes the scriptname from bookmarks (#mark).
		/// </summary>
		public string RemoveScriptNameFromBookmarks(string input, string serverName, string url, string qString) 
		{
			// HttpContext.Current.Trace.Write("Bookmark", serverName + url + "?" + qString);

			input = input.Replace("href=\"http://" + serverName + url + "?" + qString, "href=\"");
			input = input.Replace("href=http://" + serverName + url + "?" + qString, "href=");
			
			return input;
		}

		//	/// <summary>
		//	/// �ж��Ƿ���������
		//	/// </summary>
		//	/// <param name="inputValue">����ֵ</param>
		//	/// <returns>��/��</returns>
		//	public static bool IsNumeric(object inputValue)
		//	{
		//		bool isNumeric = false;
		//		
		//		try
		//		{
		//			Convert.ToDecimal(inputValue);
		//			
		//			isNumeric = true;
		//		}
		//		catch
		//		{}
		//
		//		return isNumeric;
		//	}


		/// <summary>
		/// ��ʽ�� Script ������
		/// </summary>
		/// <param name="input">����ֵ</param>
		/// <returns>���ֵ</returns>
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
		/// ��ʽ�� HTML ������
		/// </summary>
		/// <param name="input">����ֵ</param>
		/// <returns>���ֵ</returns>
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
		/// ��ʽ�� XML ������
		/// </summary>
		/// <param name="input">����ֵ</param>
		/// <returns>���ֵ</returns>
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


		/// <summary>
		/// �� PL/SQL �ַ���ת��Ϊ��ȫ�� PL/SQL ���
		/// </summary>
		/// <param name="input">����ֵ</param>
		/// <returns>ת�����SQL���</returns>
		public static string FormatSQL(string input)
		{
			StringBuilder stringBuilder = new StringBuilder(input); 
			stringBuilder.Replace("'", "''");
			
			return stringBuilder.ToString();
		}


		/// <summary>
		/// ��DataTable�����л�ȡ PL/SQL ��ѯ���
		/// </summary>
		/// <param name="dataTable">DataTable ����</param>
		/// <param name="extraFieldNames">׷�Ӳ�ѯ�ֶ�</param>
		/// <returns>PL/SQL ��ѯ���</returns>
		public static string GeneralSQL(DataTable dataTable, params string[] extraFieldNames)
		{
			if ( dataTable.Columns.Count == 0 )
			{
				return String.Empty;
			}

			StringBuilder sql = new StringBuilder("SELECT ");

			for ( int i = 0; i < dataTable.Columns.Count; i++ )
			{	
				sql.AppendFormat("{0}.{1}", dataTable.TableName, dataTable.Columns[i].ColumnName);

				if ( i != dataTable.Columns.Count - 1 )
				{
					sql.Append(", ");
				}
			}

			for ( int i = 0; i < extraFieldNames.Length; i++ )
			{
				sql.AppendFormat(", {0}", extraFieldNames[i]);
			}

			sql.Append(String.Format(" FROM {0} ", dataTable.TableName));

			return sql.ToString();
		}

		public static DataSet ListToDataSet(IList ResList)
		{
			DataSet RDS=new DataSet();
			DataTable TempDT = new DataTable();
			//�˴�����IList�Ľṹ������ͬ����DataTable
			System.Reflection.PropertyInfo[] p = ResList[0].GetType().GetProperties();
			foreach (System.Reflection.PropertyInfo pi in p)
			{
				TempDT.Columns.Add(pi.Name,System.Type.GetType(pi.PropertyType.ToString()));
			}
			for (int i = 0; i < ResList.Count; i++)
			{
				IList TempList = new ArrayList();
				//��IList�е�һ����¼д��ArrayList
				foreach (System.Reflection.PropertyInfo pi in p)
				{
					object oo = pi.GetValue(ResList[i], null);
					TempList.Add(oo);
				}
                
				object[] itm=new object[p.Length];
				//����ArrayList��object[]�������
				for (int j = 0; j < TempList.Count; j++)
				{
					itm.SetValue(TempList[j], j);
				}
				//��object[]�����ݷ���DataTable
				TempDT.LoadDataRow(itm, true);
			}
			//��DateTable����DataSet
			RDS.Tables.Add(TempDT);
			//����DataSet
			return RDS;
		}
	}
}
