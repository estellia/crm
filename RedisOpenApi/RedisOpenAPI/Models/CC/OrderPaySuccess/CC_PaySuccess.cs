using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC.OrderPaySuccess
{
    public class CC_PaySuccess
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 配置 数据
        /// </summary>
        public CC_ConfigData ConfigData { get; set; }

        /// <summary>
        /// 支付成功 数据
        /// </summary>
        public CC_PaySuccessData PaySuccessData { get; set; }
    }
}
