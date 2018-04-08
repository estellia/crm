using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    public enum EnumOrderPaymentType
    {
        /// <summary>
        /// 微信支付
        /// </summary>
        WeixinPay = 1,
        /// <summary>
        /// 线下支付
        /// </summary>
        OfflinePay = 2,
        /// <summary>
        /// 积分支付
        /// </summary>
        IntegralPay = 3,
        /// <summary>
        /// 余额支付
        /// </summary>
        AmountPay = 4,
        /// <summary>
        /// 优惠券支付
        /// </summary>
        CounponPay = 5
    }
}
