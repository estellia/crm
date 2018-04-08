using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.TradeCenter.Framework.DataContract
{
    /// <summary>
    /// 储值卡支付请求参数
    /// </summary>
    public class PrePaidCardPayRP
    {
        /// <summary>
        /// 支付订单订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 汇付储值卡卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 回复储值卡密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayAmount { get; set; }
    }
}
