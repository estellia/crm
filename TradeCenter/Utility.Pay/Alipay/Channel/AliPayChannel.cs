using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.Channel
{
    public class AliPayChannel
    {
        public string Partner { get; set; }
        /// <summary>
        /// 卖家支付宝用户ID。 如果该值为空，则默认为商户签约账号对应的支付宝用户ID
        /// 对应支付宝文档seller_id  考虑兼容老版本配置信息
        /// </summary>
        public string SellerAccountName { get; set; }
        public string SellerEmail { get; set; }
        public string MD5Key { get; set; }
        public string AgentID { get; set; }
        public string RSA_PublicKey { get; set; }
        public string RSA_PrivateKey { get; set; }

        /// <summary>
        /// 支付宝分配给开发者的应用ID 
        /// 新版当面付接口使用
        /// </summary>
        public string SCAN_AppID { get; set; }

    }
}
