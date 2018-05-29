using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace CCT.SystemFramework.Data
{
    public class Encryption
    {
        /// 密钥
        const string KEY = "WANGXIAOJIEHUGUANGQINGHUXIZHOUZHENYAOHONGYUGANDONGWANGLIANPENGCUILANXIAOCHUANEHUANGYUAOLINGSHIGUANG";
        static readonly byte[] rgbKey = { 37, 16, 93, 156, 78, 30, 218, 32 };
        static readonly byte[] rgbIV = { 55, 103, 246, 79, 36, 99, 167, 57 };

        /// DES 加密
        public static string DesEncrypt(string input)
        {
            string output = string.Empty;
            MemoryStream memoryStream = new MemoryStream();

            try
            {
                DESCryptoServiceProvider desCryptoServiceProvider = new DESCryptoServiceProvider();
                ICryptoTransform cryptoTransform = desCryptoServiceProvider.CreateEncryptor(rgbKey, rgbIV);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
                StreamWriter streamWriter = new StreamWriter(cryptoStream);
                streamWriter.Write(input);
                streamWriter.Flush();
                cryptoStream.FlushFinalBlock();
                memoryStream.Flush();

                output = Convert.ToBase64String(memoryStream.GetBuffer(), 0, Convert.ToInt32(memoryStream.Length));
            }
            finally
            {
                memoryStream.Close();
            }

            return output;
        }

        /// DES 解密
        public static string DesDecrypt(string encryptedString)
        {
            string output = string.Empty;
            byte[] buffer = Convert.FromBase64String(encryptedString);
            MemoryStream memoryStream = new MemoryStream(buffer);

            try
            {    
                DESCryptoServiceProvider desCryptoServiceProvider = new DESCryptoServiceProvider();
                ICryptoTransform cryptoTransform = desCryptoServiceProvider.CreateDecryptor(rgbKey, rgbIV);                
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read);
                StreamReader streamReader = new StreamReader(cryptoStream);

                output = streamReader.ReadToEnd();
            }
            finally
            {
                memoryStream.Close();
            }

            return output;
        }

        /// 加密
        public static string Encrypt(string input)
        {
            char CharMsgEncrypt; //加密字符串的一个字符

            char CharSecretKey;  //密钥字符串的一字符

            long CharXOR;

            int i;

            string strSecretkey; //重新调整过的密钥

            string strEncrypt = string.Empty;   //已被加密或解密的字符串

            try
            {

                Random random = new Random();

                i = System.Convert.ToInt32(KEY.Length * random.NextDouble()) + 1;

                strSecretkey = KEY.Remove(0, i) +

                    KEY.Remove(i, KEY.Length - i);

                strEncrypt += System.Convert.ToChar(i);

                for (int j = 0; j < input.Length; j++)
                {

                    CharMsgEncrypt = input[j];

                    CharSecretKey = strSecretkey[(j % input.Length) + 1];

                    CharXOR = CharMsgEncrypt ^ CharSecretKey;

                    strEncrypt = strEncrypt + System.Convert.ToChar(CharXOR);

                }

                return strEncrypt;

            }

            catch
            {

                return null;

            }

        }


        /// 解密
        public static string Decrypt(string encryptedString)
        {
            if (encryptedString.Length < 2) return string.Empty;

            StringBuilder output = new StringBuilder();

            try
            {
                int index0 = Convert.ToInt32(encryptedString[0]);

                if (index0 > 99) return string.Empty;

                string secretkey = KEY.Remove(0, index0) + KEY.Remove(index0, KEY.Length - index0);

                for (int i = 1; i < encryptedString.Length; i++)
                {
                    char c = secretkey[((i - 1) % encryptedString.Length) + 1];
                    long l = encryptedString[i] ^ c;

                    output.Append(Convert.ToChar(l));
                }

                return output.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}