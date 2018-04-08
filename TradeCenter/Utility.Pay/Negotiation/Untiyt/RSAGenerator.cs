using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Negotiation.Util
{
    /// <summary>
    /// 获取公钥和私钥
    /// </summary>
    public  class RSAGenerator
    {
        private RSACryptoServiceProvider rsa;
        public RSAGenerator()
        {
            rsa = new RSACryptoServiceProvider();
        }
        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <returns></returns>
        public string getPublicKey()
        {
            return rsa.ToXmlString(false);
        }
        /// <summary>
        /// 获取私钥
        /// </summary>
        /// <returns></returns>
        public string getPrivateKey()
        {
            return rsa.ToXmlString(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RSACryptoServiceProvider getRSACryptoServiceProvider()
        {
            return rsa;
        }
    }
}