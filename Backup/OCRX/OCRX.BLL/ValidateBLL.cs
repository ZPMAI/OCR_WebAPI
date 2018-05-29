using System;
using System.Collections.Generic;
using System.Text;

namespace OCRX.BLL
{
    public class ValidateBLL
    {
        //校验英文字符串，中间用逗号隔开
        public static bool CheckEnglishComma(string str, int min, int max)
        {
            string regEx = @"^[a-zA-Z]{" + min.ToString() + "," + max.ToString() + "}(,[a-zA-Z]{" + min.ToString() + "," + max.ToString() + "})*$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }

        #region 箱号检验
        /// <summary>
        /// 箱号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckContNo(string str)
        {
            if (str.Length < 11) return false;
            long TT = 0;
            int L11 = Convert.ToInt16(str.Substring(10, 1));
            for (int i = 0; i < 10; i++)
                TT = TT + Convert.ToInt64(ContCharInt(str.Substring(i, 1)) * Math.Pow(2, i));
            long YS = (TT % 11);
            if (L11 == YS || 10L + L11 == YS)
                return true;
            else return false;
        }

        private static int ContCharInt(string c)
        {
            int i = 0;
            switch (c)
            {
                case "A": i = 10;
                    break;
                case "B": i = 12;
                    break;
                case "C": i = 13;
                    break;
                case "D": i = 14;
                    break;
                case "E": i = 15;
                    break;
                case "F": i = 16;
                    break;
                case "G": i = 17;
                    break;
                case "H": i = 18;
                    break;
                case "I": i = 19;
                    break;
                case "J": i = 20;
                    break;
                case "K": i = 21;
                    break;
                case "L": i = 23;
                    break;
                case "M": i = 24;
                    break;
                case "N": i = 25;
                    break;
                case "O": i = 26;
                    break;
                case "P": i = 27;
                    break;
                case "Q": i = 28;
                    break;
                case "R": i = 29;
                    break;
                case "S": i = 30;
                    break;
                case "T": i = 31;
                    break;
                case "U": i = 32;
                    break;
                case "V": i = 34;
                    break;
                case "W": i = 35;
                    break;
                case "X": i = 36;
                    break;
                case "Y": i = 37;
                    break;
                case "Z": i = 38;
                    break;
                case "1": i = 1;
                    break;
                case "2": i = 2;
                    break;
                case "3": i = 3;
                    break;
                case "4": i = 4;
                    break;
                case "5": i = 5;
                    break;
                case "6": i = 6;
                    break;
                case "7": i = 7;
                    break;
                case "8": i = 8;
                    break;
                case "9": i = 9;
                    break;
                case "0": i = 0;
                    break;

            }
            return i;
        }
        #endregion

        #region 校验数字
        /// <summary>
        /// 校验数字
        /// </summary>
        /// <param name="str"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool CheckNumber(string str, int min, int max)
        {
            string regEx = @"^[0-9]{" + min.ToString() + "," + max.ToString() + "}$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, regEx);
        }
        #endregion
    }
}
