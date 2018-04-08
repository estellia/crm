using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Security.Cryptography;

namespace cPos.Admin.DataCrypt
{
    /// <summary>
    /// 数据加密
    /// </summary>
    public class EncryptManager
    {
        /// <summary>
        /// 对字符串进行加密
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="providerType">加密算法类型</param>
        /// <returns></returns>
        public static string Encrypt(string input, byte[] key, byte[] iv, CryptProviderType providerType)
        {
            MemoryStream mstream = new MemoryStream();
            CryptoStream cstream = null;

            switch (providerType)
            {
                case CryptProviderType.AES:
                    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                    cstream = new CryptoStream(mstream, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                    break;
                case CryptProviderType.DES:
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    cstream = new CryptoStream(mstream, des.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                    break;
                case CryptProviderType.TripleDES:
                    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                    cstream = new CryptoStream(mstream, tdes.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                    break;
                default:
                    break;
            }

            if (cstream == null)
            {
                mstream.Close();
                return input;
            }
            else
            {
                byte[] ibuffer = Encoding.Default.GetBytes(input);
                cstream.Write(ibuffer, 0, ibuffer.Length);
                cstream.FlushFinalBlock();

                string output = Convert.ToBase64String(mstream.ToArray());
                cstream.Close();
                mstream.Close();
                return output;
            }
        }
    }
}
