using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace JIT.Utility
{
    /// <summary>
    /// MD5加密 
    /// </summary>
    public static class MD5Helper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pInput">输入的字符串</param>
        /// <returns>加密后的结果</returns>
        public static string Encryption(string pInput)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] val, hash;
            val = Encoding.UTF8.GetBytes(pInput);
            hash = md5.ComputeHash(val);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
    }
}
