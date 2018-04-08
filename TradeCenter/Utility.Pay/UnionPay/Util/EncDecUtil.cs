/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/20 17:18:36
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

using JIT.Utility;

namespace JIT.Utility.Pay.UnionPay.Util
{
    /// <summary>
    /// 加密/解密
    /// </summary>
    public static class EncDecUtil
    {
        /// <summary>
        /// 获取加密密钥
        /// </summary>
        /// <param name="pOriginalKey">原始密钥</param>
        /// <returns></returns>
        public static string GetEncryptKey(string pOriginalKey)
        {
            return MD5Helper.Encryption(pOriginalKey).ToLower().Substring(0, 24);
            //return MD5Helper.Encryption(pOriginalKey).ToLower();
        }

        #region MD5加密
        /// <summary>
        /// 按Java的方式对源字符串进行MD5加密
        /// </summary>
        /// <param name="pSource"></param>
        /// <returns></returns>
        public static string MD5ForJava(string pSource)
        {
            return EncDecUtil.MD5ForJava(Encoding.UTF8.GetBytes(pSource));
        }
        /// <summary>
        /// 按Java的方式对源字符串进行MD5加密
        /// </summary>
        /// <param name="pSource"></param>
        /// <returns></returns>
        public static string MD5ForJava(byte[] pSource)
        {
            StringBuilder result = new StringBuilder();
            using (var md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(pSource);
                for (int i = 0; i < bytes.Length; i++)
                {
                    string hex = bytes[i].ToString("X");
                    if (hex.Length == 1)
                    {
                        result.Append("0");
                    }
                    result.Append(hex);
                }
            }
            return result.ToString();
        }
        #endregion

        #region RSA加密解密
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="pCertFilePath">pfx证书文件路径</param>
        /// <param name="pCertFilePassword">pfx证书文件的密码</param>
        /// <param name="pEncryptContent">需要加密的内容</param>
        /// <returns>密文</returns>
        public static byte[] RSAEncrypt(string pCertFilePath,string pCertFilePassword, string pEncryptContent)
        {
            using (FileStream fs = new FileStream(pCertFilePath, FileMode.Open))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                X509Certificate2 mycert = new X509Certificate2(bytes, pCertFilePassword,
                    X509KeyStorageFlags.MachineKeySet
                    | X509KeyStorageFlags.PersistKeySet
                    | X509KeyStorageFlags.Exportable);
                AsymmetricKeyParameter bouncyCastlePrivateKey = EncDecUtil.TransformRSAPrivateKey(mycert.PrivateKey);
                IBufferedCipher c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");// 参数与JAVA中解密的参数一致
                c.Init(true, bouncyCastlePrivateKey);
                byte[] outBytes = c.DoFinal(Encoding.UTF8.GetBytes(pEncryptContent));
                return outBytes;
            }
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="pCerFilePath">cer证书文件路径</param>
        /// <param name="pDecryptContent">需要解密的内容</param>
        /// <returns></returns>
        public static byte[] RSADecrypt(string pCerFilePath, byte[] pDecryptContent)
        {
            using (FileStream fs = new FileStream(pCerFilePath, FileMode.Open))
            {
                Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] {
            cp.ReadCertificate(fs)};
                IAsymmetricBlockCipher eng = new Pkcs1Encoding(new RsaEngine());
                eng.Init(false, chain[0].GetPublicKey());
                var result = eng.ProcessBlock(pDecryptContent, 0, pDecryptContent.Length);
                //
                return result;
            }
        }

        /// <summary>
        /// 转换RSA密钥
        /// </summary>
        /// <param name="pPrivateKey"></param>
        /// <returns></returns>
        private static AsymmetricKeyParameter TransformRSAPrivateKey(AsymmetricAlgorithm pPrivateKey)
        {
            RSACryptoServiceProvider prov = pPrivateKey as RSACryptoServiceProvider;
            RSAParameters parameters = prov.ExportParameters(true);
            return new RsaPrivateCrtKeyParameters(
                new BigInteger(1, parameters.Modulus),
                new BigInteger(1, parameters.Exponent),
                new BigInteger(1, parameters.D),
                new BigInteger(1, parameters.P),
                new BigInteger(1, parameters.Q),
                new BigInteger(1, parameters.DP),
                new BigInteger(1, parameters.DQ),
                new BigInteger(1, parameters.InverseQ));
        }
        #endregion

        #region Base64加密解密
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="pEncryptContent">需要进行Base64加密的字节</param>
        /// <returns></returns>
        public static string Base64Encrypt(byte[] pEncryptContent)
        {
            return Convert.ToBase64String(pEncryptContent);
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="pEncryptContent">需要进行Base64加密的字符串（字符串以UTF-8编码）</param>
        /// <returns></returns>
        public static string Base64Encrypt(string pEncryptContent)
        {
            return EncDecUtil.Base64Encrypt(Encoding.UTF8.GetBytes(pEncryptContent));
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="pDecryptContent">需要进行Base64解密的字符串</param>
        /// <returns></returns>
        public static byte[] Base64Decrypt(string pDecryptContent)
        {
            return Convert.FromBase64String(pDecryptContent);
        }
        #endregion

        #region 3DES加密解密
        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="pKey">加密密钥</param>
        /// <param name="pEncryptContent">需要加密的内容</param>
        /// <returns></returns>
        public static byte[] TripleDESEncrypt(string pKey, string pEncryptContent)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(pKey);
            byte[] contentBytes = Encoding.UTF8.GetBytes(pEncryptContent);
            return EncDecUtil.TripleDESEncrypt(keyBytes, contentBytes);
        }

        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="pKey">加密密钥</param>
        /// <param name="pEncryptContent">需要加密的内容</param>
        /// <returns></returns>
        public static byte[] TripleDESEncrypt(string pKey, byte[] pEncryptContent)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(pKey);
            return EncDecUtil.TripleDESEncrypt(keyBytes, pEncryptContent);
        }

        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="pKey">加密密钥</param>
        /// <param name="pEncryptContent">需要加密的内容</param>
        /// <returns></returns>
        public static byte[] TripleDESEncrypt(byte[] pKey, byte[] pEncryptContent)
        {
            byte[] ivBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };

            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.ECB;
            tdsp.Padding = PaddingMode.PKCS7;

            using (var mStream = new MemoryStream())
            {
                using (var cStream = new CryptoStream(mStream, tdsp.CreateEncryptor(pKey, ivBytes), CryptoStreamMode.Write))
                {
                    cStream.Write(pEncryptContent, 0, pEncryptContent.Length);
                    cStream.FlushFinalBlock();

                    byte[] encryptedBytes = mStream.ToArray();
                    return encryptedBytes;
                }
            }
        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="pKey">解密密钥</param>
        /// <param name="pDecryptContent">需要解密的内容</param>
        /// <returns></returns>
        public static byte[] TripleDESDecrypt(string pKey,string pDecryptContent)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(pKey);
            byte[] contentBytes = Encoding.UTF8.GetBytes(pDecryptContent);

            return EncDecUtil.TripleDESDecrypt(keyBytes, contentBytes);
        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="pKey">解密密钥</param>
        /// <param name="pDecryptContent">需要解密的内容</param>
        /// <returns></returns>
        public static byte[] TripleDESDecrypt(string pKey, byte[] pDecryptContent)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(pKey);

            return EncDecUtil.TripleDESDecrypt(keyBytes, pDecryptContent);
        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="pKey">解密密钥</param>
        /// <param name="pDecryptContent">需要解密的内容</param>
        /// <returns></returns>
        public static byte[] TripleDESDecrypt(byte[] pKey, byte[] pDecryptContent)
        {
            byte[] ivBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };

            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.ECB;
            tdsp.Padding = PaddingMode.PKCS7;

            using (var dStream = new MemoryStream(pDecryptContent))
            {
                using (var cStream = new CryptoStream(dStream, tdsp.CreateDecryptor(pKey, ivBytes), CryptoStreamMode.Read))
                {
                    byte[] result = new byte[pDecryptContent.Length];
                    cStream.Read(result, 0, result.Length);
                    return result;
                }
            }
        }
        #endregion
    }
}
