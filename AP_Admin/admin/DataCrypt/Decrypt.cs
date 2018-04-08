using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace cPos.Admin.DataCrypt
{
    public class DecryptManager
    {
        /// <summary>
        /// 对字符串进行解密
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="providerType">加密算法类型</param>
        /// <returns></returns>
        public static string Decrypt(string input, byte[] key, byte[] iv, CryptProviderType providerType)
        {
            MemoryStream mstream = new MemoryStream();
            CryptoStream cstream = null;

            switch (providerType)
            {
                case CryptProviderType.AES:
                    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                    cstream = new CryptoStream(mstream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Write);
                    break;
                case CryptProviderType.DES:
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    cstream = new CryptoStream(mstream, des.CreateDecryptor(key, iv), CryptoStreamMode.Write);
                    break;
                case CryptProviderType.TripleDES:
                    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                    cstream = new CryptoStream(mstream, tdes.CreateDecryptor(key, iv), CryptoStreamMode.Write);
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
                byte[] ibuffer = Convert.FromBase64String(input);
                cstream.Write(ibuffer, 0, ibuffer.Length);
                cstream.FlushFinalBlock();

                string outptut = Encoding.Default.GetString(mstream.ToArray());
                cstream.Close();
                mstream.Close();
                return outptut;
            }
        }

        
    }
}
