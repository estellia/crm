using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.ValueObject
{
    /// <summary>
    /// 业务响应码
    /// </summary>
    public enum ResultCodes
    {
        /// <summary>
        /// 下单失败
        /// </summary>
        ORDER_FAIL,
        /// <summary>
        /// 下单成功并支付成功
        /// </summary>
        ORDER_SUCCESS_PAY_SUCCESS,
        /// <summary>
        /// 下单成功,支付失败
        /// </summary>
        ORDER_SUCCESS_PAY_FAIL,
        /// <summary>
        /// 下单成功,支付处理中
        /// </summary>
        ORDER_SUCCESS_PAY_INPROCESS,
        /// <summary>
        /// 处理结果未知
        /// </summary>
        UNKNOWN
    }
}
