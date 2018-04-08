using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Negotiation.Interface
{
    public class NegotiationPayChannel
    {
        /// <summary>
        /// 商户号
        /// </summary>
        public string MerchantID { get; set; }

        /// <summary>
        /// 证书文件路径
        /// </summary>
        public string CertificateFilePath { get; set; }

        /// <summary>
        /// 证书文件密码
        /// </summary>
        public string CertificateFilePassword { get; set; }

        /// <summary>
        /// 解密证书路径
        /// </summary>
        public string DecryptCertificateFilePath { get; set; }
    }
}
