using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.TradeCenter.Framework.DataContract
{
    public class QueryOrderResponse
    {
        /// <summary>
        /// 订单状态0-初始,1-已提交,2-已支付
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }
    }
}
