using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using cPos.Admin.DataCrypt;

namespace cPos.Admin.Component
{
    public class CryptManager
    {
        private static string GetKeyFileFullPath(string keyFile)
        {
            //取客户密钥文件
            string fileKey = "";
            if (HttpContext.Current != null)
            {
                fileKey = HttpContext.Current.Server.MapPath("~\\keys\\" + keyFile);
            }
            else
            {
                string exe_path = System.Configuration.ConfigurationManager.AppSettings["app_exe_path"];
                fileKey = exe_path + "\\keys\\" + keyFile;
            }

            return fileKey;
        }
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="customerID">密钥文件</param>
        /// <param name="input">被加密的明文</param>
        /// <returns></returns>
        public static string EncryptString(string keyFile, string input)
        {
            //取客户密钥文件
            string fileKey = GetKeyFileFullPath(keyFile);
            if (!System.IO.File.Exists(fileKey))
            {
                throw new System.IO.FileNotFoundException("未找到密钥文件！");
            }
            byte[] keys, iv;
            if (!CryptKeyManager.GetCryptKeyAndIV(fileKey, out keys, out iv))
            {
                throw new System.IO.FileNotFoundException("密钥文件错误！");
            }
            string output = cPos.Admin.DataCrypt.EncryptManager.Encrypt(input, keys, iv, CryptProviderType.TripleDES);
            return output;
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="customerID">密钥文件</param>
        /// <param name="input">被加密的明文</param>
        /// <returns></returns>
        public static string DecryptString(string keyFile, string input)
        {
            //取客户密钥文件
            string fileKey = GetKeyFileFullPath(keyFile);
            if (!System.IO.File.Exists(fileKey))
            {
                throw new System.IO.FileNotFoundException("未找到密钥文件！");
            }
            byte[] keys, iv;
            if (!CryptKeyManager.GetCryptKeyAndIV(fileKey, out keys, out iv))
            {
                throw new System.IO.FileNotFoundException("密钥文件错误！");
            }
            string output = cPos.Admin.DataCrypt.DecryptManager.Decrypt(input, keys, iv, CryptProviderType.TripleDES);
            return output;
        }
    }
}
