using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JIT.TradeCenter.Framework.DataContract
{
    public class SetPayChannel
    {
        public SetPayChannelList[] AddPayChannelData { get; set; }

    }



    public class SetPayChannelList
    {
        public int ChannelId { get; set; }

        public int PayType { get; set; }

        public string NotifyUrl { get; set; }

        public WapInfo WapData { get; set; }

        public WxPayInfo WxPayData { get; set; }

        public UnionPayInfo UnionPayData { get; set; }


    }

    public class WapInfo
    {
        /// <summary>
        /// 帐号（线上，线下）
        /// </summary>
        public string Partner { get; set; }
        /// <summary>
        /// 卖家淘宝帐号（线上）
        /// </summary>
        public string SellerAccountName { get; set; }
        /// <summary>
        /// 公钥（线上）
        /// </summary>
        public string RSA_PublicKey { get; set; }
        /// <summary>
        /// 私钥（线上）
        /// </summary>
        public string RSA_PrivateKey { get; set; }
        /// <summary>
        /// 密钥（线下）
        /// </summary>
        public string MD5Key { get; set; }

        public string AgentID { get; set; }

        /// <summary>
        /// 扫码支付appId 对应数据库字段 EncryptionCertificate
        /// </summary>
        public string SCAN_AppID { get; set; }
        /// <summary>
        /// 支付模式：1：网页支付;2：扫码支付;3：APP支付
        /// </summary>
        public string PayEncryptedPwd { get; set; }
    }

    public class WxPayInfo
    {
        /// <summary>
        /// 微信公众号
        /// </summary>
        public string AppID { get; set; }
        ///// <summary>
        ///// 权限获取所需密钥
        ///// </summary>
        //public string AppSecret { get; set; }
        ///// <summary>
        ///// 财付通商户身份标识
        ///// </summary>
        //public string ParnterID { get; set; }
        ///// <summary>
        ///// 财付通商户权限密钥
        ///// </summary>
        //public string ParnterKey { get; set; }
        ///// <summary>
        ///// 加密的密钥
        ///// </summary>
        //public string PaySignKey { get; set; }
        //public string NotifyToTradeCenterURL { get; set; }
        //public string NotifyToBussinessSystemURL { get; set; }
        /// <summary>
        /// 微信支付商户号
        /// </summary>
        public string Mch_ID { get; set; }
        /// <summary>
        /// API操作密钥
        /// </summary>
        public string SignKey { get; set; }
        /// <summary>
        /// 微信支付类型=JSAPI
        /// </summary>
        public string Trade_Type { get; set; }

    }

    public class UnionPayInfo
    {
        /// <summary>
        ///  账户ID
        /// </summary>
        public string MerchantID { get; set; }
        /// <summary>
        /// 加密证书路径  
        /// </summary>
        public string CertificateFilePath { get; set; }
        /// <summary>
        /// 加密证书密码
        /// </summary>
        public string CertificateFilePassword { get; set; }
        /// <summary>
        /// 解密证书路径
        /// </summary>
        public string DecryptCertificateFilePath { get; set; }
        /// <summary>
        /// 解密证书密码
        /// </summary>
        public string PacketEncryptKey { get; set; }


    }


    public class PayChannelResponse
    {
        public int ChannelId { get; set; }
    }

    public class PayChannelResponsList
    {
        public List<PayChannelResponse> PayChannelIdList { get; set; }
    }
}
