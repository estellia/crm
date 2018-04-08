using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC.OrderReward
{
    /// <summary>
    ///  确认收货时处理积分、返现、佣金
    /// </summary>
    public class CC_OrderReward
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
        /// 订单信息
        /// </summary>
        public string OrderInfo { get; set; }
        /// <summary>
        ///订单标识
        /// </summary>
        public string OrderID { get; set; }
    }
}
