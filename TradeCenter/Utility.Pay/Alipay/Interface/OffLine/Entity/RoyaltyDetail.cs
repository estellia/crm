using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.Interface.Offline.Entity
{
    /// <summary>
    /// 分润明细信息
    /// </summary>
    public class RoyaltyDetail
    {
        /// <summary>
        /// 分账序列号，表示分账执行的顺序，必须为正整数。
        /// </summary>
        public string serialNo { get; set; }
        /// <summary>
        /// 商户分账的外部关联号，用于关联到每一笔分账信息，商户需保证其唯一性。
        /// 如果为空，该值则默认为“商户网站唯一订单号+分账序列号”。
        /// </summary>
        public string outRelationId { get; set; }
        /// <summary>
        /// 要分账的支付宝账号对应的支付宝唯一用户号。
        /// 以 2088 开头的纯 16 位数字。
        /// </summary>
        public string transOut { get; set; }
        /// <summary>
        /// 接受分账金额的支付宝账号对应的支付宝唯一用户号。
        /// 以 2088 开头的纯 16 位数字
        /// </summary>
        public string transIn { get; set; }
        /// <summary>
        /// 分账的金额。
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 分账描述信息。
        /// </summary>
        public string desc { get; set; }
    }
}
