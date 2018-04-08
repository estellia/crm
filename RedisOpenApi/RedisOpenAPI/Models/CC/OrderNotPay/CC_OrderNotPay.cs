using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC.OrderNotPay
{
   public class CC_OrderNotPay
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
        /// 订单未付款-发送微信模板消息 数据
        /// </summary>
        public CC_OrderNotPayData OrderNotPayData { get; set; }

    }
}
