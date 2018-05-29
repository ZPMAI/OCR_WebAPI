using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CCT.Common.Encrypt
{
    public class EncryptHelper
    {
        public static string database = string.Empty;
        public static string user_id = string.Empty; //用户ID
        public static ArrayList Role_gkey; //角色ID
        public static string deptname = string.Empty; //部门ID
        public static string encryptKey = "SCTITDEP";
        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch //(Exception ex)
            {
                // MessageBox.Show("Encrypt.EncryptDES(): " + ex.Message, "error information");
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("Encrypt.DecryptDES(): " + ex.Message, "error information");
                return decryptString;
            }
        }

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="d">d表示要四舍五入的数</param>
        /// <param name="i">i表示要保留的小数点后为数</param>
        /// <returns></returns>
        public static double Round(double d, int i)
        {
            if (d >= 0)
            {
                d += 5 * Math.Pow(10, -(i + 1));
            }
            else
            {
                d += -5 * Math.Pow(10, -(i + 1));
            }
            string str = d.ToString();
            string[] strs = str.Split('.');
            int idot = str.IndexOf('.');
            string prestr = strs[0];
            string poststr = strs[1];
            if (poststr.Length > i)
            {
                poststr = str.Substring(idot + 1, i);
            }
            string strd = prestr + "." + poststr;
            d = Double.Parse(strd);
            return d;
        }

        private string RoundFileLen(long iLen)
        {
            string a = Convert.ToString(iLen / 1024.00);
            if (a.IndexOf('.') != -1)
            {
                string b = a.Split('.')[1].Substring(0, 1);
                if (Convert.ToInt16(b) >= 5)
                {
                    return Convert.ToString(Convert.ToInt32(a.Split('.')[0]) + 1);
                }
                else
                {
                    return Convert.ToString(Convert.ToInt32(a.Split('.')[0]));
                }
            }
            else
            {
                return a;
            }
        }
    }
}
