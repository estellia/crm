using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC.Order
{
    public class CC_Order
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 商户链接信息
        /// </summary>
        public string LogSession { get; set; }

        /// <summary>
        /// 商户链接信息
        /// </summary>
        public string OrderInfo { get; set; } 
    }
}
