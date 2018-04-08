using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace MVS.MPos.Component
{
    /// <summary>
    /// hash的算法类型
    /// </summary>
    public enum HashProviderType
    {
        SHA1,
        MD5,
        LMD5
    }

    public class HashManager
    {
        /// <summary>
        /// 获取字符串的hash值
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="providerType">hash算法类型</param>
        /// <returns></returns>
        public static string Hash(string input, HashProviderType providerType)
        {
            string ret;
            switch (providerType)
            {
                case HashProviderType.SHA1:
                    SHA1 sha1 = new SHA1CryptoServiceProvider();
                    byte[] sha1_in = UTF8Encoding.Default.GetBytes(input);
                    byte[] sha1_out = sha1.ComputeHash(sha1_in);
                    ret = BitConverter.ToString(sha1_out).Replace("-", "").ToLower();
                    break;
                case HashProviderType.LMD5:
                    MD5 lmd5 = new MD5CryptoServiceProvider();
                    byte[] lmd5_in = UTF8Encoding.Default.GetBytes(input);
                    byte[] lmd5_out = lmd5.ComputeHash(lmd5_in);
                    ret = BitConverter.ToString(lmd5_out).Replace("-", "").ToLower();
                    ret = ret.Substring(8, 16);
                    break;
                case HashProviderType.MD5:
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] md5_in = UTF8Encoding.Default.GetBytes(input);
                    byte[] md5_out = md5.ComputeHash(md5_in);
                    ret = BitConverter.ToString(md5_out).Replace("-", "").ToLower();
                    return ret;
                default:
                    ret = input;
                    break;
            }
            return ret;
        }        
    }

    /// <summary>
    /// 加密的算法类型
    /// </summary>
    public enum EncryptProviderType
    {
        AES,
        DES,
        TripleDES
    }

    public class DecryptManager
    {
        /// <summary>
        /// 对字符串进行解密
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="providerType">加密算法类型</param>
        /// <returns></returns>
        public static string Decrypt(string input, byte[] key, byte[] iv, EncryptProviderType providerType)
        {
            MemoryStream mstream = new MemoryStream();
            CryptoStream cstream = null;

            switch (providerType)
            {
                case EncryptProviderType.AES:
                    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                    cstream = new CryptoStream(mstream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Write);
                    break;
                case EncryptProviderType.DES:
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    cstream = new CryptoStream(mstream, des.CreateDecryptor(key, iv), CryptoStreamMode.Write);
                    break;
                case EncryptProviderType.TripleDES:
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

        /// <summary>
        /// 从文件中获取解密用的key和iv
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="key">解密用的key</param>
        /// <param name="iv">解密用的iv</param>
        /// <returns></returns>
        public static bool GetDecryptKeyAndIV(string fileName, out byte[] key, out byte[] iv)
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
