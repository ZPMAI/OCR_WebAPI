using System;
using System.Collections.Generic;
using System.Text;

namespace CCT.SystemFramework.Validation
{
    /// <summary>
    /// ͨ������У��
    /// </summary>
    /// <remarks>2013-01-21 Ҷ���� ����</remarks>
    public class ValidationHelper
    {        
        /// <summary>
        /// ������ĸ������
        /// </summary>        
        public static bool CheckLetterAndNumber(string str, int min, int max)
        {
            string regEx = @"^[a-zA-Z0-9/_-]{" + min.ToString() + "," + max.ToString() + "}$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }

        /// <summary>
        /// �����ȼ���Ƿ��ַ�
        /// </summary>        
        public static bool CheckForbiddenCharacters(string str, int min, int max)
        {
            string regEx = @"^[\w]{" + min.ToString() + "," + max.ToString() + "}$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }

        /// <summary>
        /// �����ȼ�����ĸ
        /// </summary>
        public static bool CheckLetters(string str, int min, int max)
        {
            string regEx = @"^[a-zA-Z]{" + min.ToString() + "," + max.ToString() + "}$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }

        /// <summary>
        /// �����ʼ���ַ
        /// </summary>
        public static bool CheckEmail(string email)
        {
            string regEx = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+$)*";
            return System.Text.RegularExpressions.Regex.IsMatch(email, regEx);
        }

        /// <summary>
        /// ��Ӣ����֤�����ģ��ո�Ӣ�ģ�
        /// </summary>
        public static bool CheckChineseEnglish(string str, int min, int max)
        {
            string regEx = @"^[\w\W\u4e00-\u9fa5]{" + min.ToString() + "," + (max / 2).ToString() + @"}$|^[\w\W]{" + min.ToString() + "," + max.ToString() + "}$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }

        /// <summary>
        /// У��Ӣ���ַ������м��ö��Ÿ���
        /// </summary>
        /// <returns></returns>
        public static bool CheckEnglishComma(string str, int min, int max)
        {
            string regEx = @"^[a-zA-Z]{" + min.ToString() + "," + max.ToString() + "}(,[a-zA-Z]{" + min.ToString() + "," + max.ToString() + "})*$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }

        /// <summary>
        /// У����ĸ���ֿո�
        /// </summary>
        public static bool CheckLetterNumberBlank(string str, int min, int max)
        {
            string regEx = @"^[a-zA-Z0-9\040]{" + min.ToString() + "," + max.ToString() + "}$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }

        /// <summary>
        /// У������
        /// </summary>
        public static bool CheckNumber(string str, int min, int max)
        {
            string regEx = @"^[0-9]{" + min.ToString() + "," + max.ToString() + "}$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }
        
    }
}
