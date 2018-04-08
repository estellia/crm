using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC.OrderSend
{
    public class CC_OrderSend
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
        /// APP/后台订单发货-发送微信模板消息 数据
        /// </summary>
        public CC_OrderSendData OrderSendData { get; set; }
    }
}
