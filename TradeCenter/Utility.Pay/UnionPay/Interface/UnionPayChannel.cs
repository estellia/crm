/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/24 14:40:41
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
using System.Text;

namespace JIT.Utility.Pay.UnionPay.Interface
{
    /// <summary>
    /// 银联支付的通道
    /// </summary>
    public class UnionPayChannel
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public UnionPayChannel()
        {
        }
        #endregion

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

        /// <summary>
        /// 数据报文加密的密钥
        /// </summary>
        public string PacketEncryptKey { get; set; }
    }
}
