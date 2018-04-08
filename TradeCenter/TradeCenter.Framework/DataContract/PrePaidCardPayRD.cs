using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.TradeCenter.Framework.DataContract
{
    public class PrePaidCardPayRD
    {
        /// <summary>
        /// 订单支付结果：200-支付成功，其他失败
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string errmsg { get; set; }
    }
}
