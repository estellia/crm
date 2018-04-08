using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC.OrderPushMessage
{
    /// <summary>
    /// 根据订单状态给出不同的提示信息
    /// </summary>
    public class CC_OrderPushMessage
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 商户链接信息
        /// </summary>
        public string LogSession { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStauts { get; set; }

        /// <summary>
        /// 订单标识
        /// </summary>
        public string OrderID { get; set; }
    }
}
