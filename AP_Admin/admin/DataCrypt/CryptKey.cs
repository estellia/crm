using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace cPos.Admin.DataCrypt
{
    public class CryptKeyManager
    {
        /// <summary>
        /// 获取加密和解密时用的key和iv
        /// </summary>
        /// <param name="providerType">算法类型</param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        public static void GenerateCryptKeyAndIV(CryptProviderType providerType, out byte[] key, out byte[] iv)
        {
            key = null;
            iv = null;
            switch (providerType)
            {
                case CryptProviderType.AES:
                    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                    aes.GenerateKey();
                    aes.GenerateIV();
                    key = aes.Key;
                    iv = aes.IV;
                    return;
                case CryptProviderType.DES:
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    des.GenerateKey();
                    des.GenerateIV();
                    key = des.Key;
                    iv = des.IV;
                    return;
                case CryptProviderType.TripleDES:
                    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                    tdes.GenerateKey();
                    tdes.GenerateIV();
                    key = tdes.Key;
                    iv = tdes.IV;
                    return;
                default:
                    return;
            }
        }

        /// <summary>
        /// 从文件中获取解密用的key和iv
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="key">解密用的key</param>
        /// <param name="iv">解密用的iv</param>
        /// <returns></returns>
        public static bool GetCryptKeyAndIV(string fileName, out byte[] key, out byte[] iv)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            key = null;
            iv = null;

            if (!File.Exists(fileName))
            {
                return false;
            }

            FileStream fs = new FileStream(fileName, FileMode.Open);
            try
            {
                if (fs.Length > 2)
                {
                    int key_len = fs.ReadByte();
                    int iv_len = fs.ReadByte();
                    key = new byte[key_len];
                    iv = new byte[iv_len];
                    int m = fs.Read(key, 0, key_len);
                    int n = fs.Read(iv, 0, iv_len);
                    if ((m != key_len) || (n != iv_len))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
