using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.ValueObject
{
    public enum TradeStatus
    {
        /// <summary>
        /// 交易成功且结束，即不可再做任何操作
        /// </summary>
        TRADE_FINISHED,
        /// <summary>
        /// 交易成功，且可对该交易做操作，如：多级分润、退款等。
        /// </summary>
        TRADE_SUCCESS,
        /// <summary>
        /// 交易创建，等待买家付款。
        /// </summary>
        WAIT_BUYER_PAY,
        /// <summary>
        ///  在指定时间段内未支付时关闭的交易； 在交易完成全额退款成功时关闭的交易。
        /// </summary>
        TRADE_CLOSED,
        /// <summary>
        /// 等待卖家收款（买家付款后，如果卖家账号被冻结）。
        /// </summary>
        TRADE_PENDING
    }
}
