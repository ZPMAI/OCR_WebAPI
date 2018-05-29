using System;
using System.Collections.Generic;
using System.Text;

namespace CCT.SystemFramework.Validation
{
    /// <summary>
    /// 通用输入校验
    /// </summary>
    /// <remarks>2013-01-21 叶君腾 创建</remarks>
    public class ValidationHelper
    {        
        /// <summary>
        /// 检验字母与数字
        /// </summary>        
        public static bool CheckLetterAndNumber(string str, int min, int max)
        {
            string regEx = @"^[a-zA-Z0-9/_-]{" + min.ToString() + "," + max.ToString() + "}$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }

        /// <summary>
        /// 按长度检验非法字符
        /// </summary>        
        public static bool CheckForbiddenCharacters(string str, int min, int max)
        {
            string regEx = @"^[\w]{" + min.ToString() + "," + max.ToString() + "}$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }

        /// <summary>
        /// 按长度检验字母
        /// </summary>
        public static bool CheckLetters(string str, int min, int max)
        {
            string regEx = @"^[a-zA-Z]{" + min.ToString() + "," + max.ToString() + "}$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }

        /// <summary>
        /// 检验邮件地址
        /// </summary>
        public static bool CheckEmail(string email)
        {
            string regEx = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+$)*";
            return System.Text.RegularExpressions.Regex.IsMatch(email, regEx);
        }

        /// <summary>
        /// 中英文验证（中文，空格，英文）
        /// </summary>
        public static bool CheckChineseEnglish(string str, int min, int max)
        {
            string regEx = @"^[\w\W\u4e00-\u9fa5]{" + min.ToString() + "," + (max / 2).ToString() + @"}$|^[\w\W]{" + min.ToString() + "," + max.ToString() + "}$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }

        /// <summary>
        /// 校验英文字符串，中间用逗号隔开
        /// </summary>
        /// <returns></returns>
        public static bool CheckEnglishComma(string str, int min, int max)
        {
            string regEx = @"^[a-zA-Z]{" + min.ToString() + "," + max.ToString() + "}(,[a-zA-Z]{" + min.ToString() + "," + max.ToString() + "})*$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }

        /// <summary>
        /// 校验字母数字空格
        /// </summary>
        public static bool CheckLetterNumberBlank(string str, int min, int max)
        {
            string regEx = @"^[a-zA-Z0-9\040]{" + min.ToString() + "," + max.ToString() + "}$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }

        /// <summary>
        /// 校验数字
        /// </summary>
        public static bool CheckNumber(string str, int min, int max)
        {
            string regEx = @"^[0-9]{" + min.ToString() + "," + max.ToString() + "}$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }
        
    }
}
