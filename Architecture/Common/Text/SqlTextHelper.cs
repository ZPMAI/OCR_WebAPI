using System;
using System.Collections.Generic;
using System.Text;

namespace CCT.Common.Text
{
    public static class SqlTextHelper
    {
        public static string Split2QuoteString(string s, char c)
        {
            string[] ss = s.Split(c);

            return Array2QuoteString(ss);
        }

        public static string Split2String(string s, char c)
        {
            string[] ss = s.Split(c);

            return Array2String(ss);
        }

        public static string Array2QuoteString<T>(T[] array)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.AppendFormat("'{0}'", array[i]);

                if (i != array.Length - 1)
                {
                    stringBuilder.Append(",");
                }
            }

            return stringBuilder.ToString();
        }

        public static string Array2String<T>(T[] array)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.AppendFormat("{0}", array[i]);

                if (i != array.Length - 1)
                {
                    stringBuilder.Append(",");
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 将 PL/SQL 字符串转换为安全的 PL/SQL 语句
        /// </summary>
        /// <param name="input">输入值</param>
        /// <returns>转换后的SQL语句</returns>
        public static string FormatSQL(string input)
        {
            return input.Replace("'", "''");
        }	
    }
}
