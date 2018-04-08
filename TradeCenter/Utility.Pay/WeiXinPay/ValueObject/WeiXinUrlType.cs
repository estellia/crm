using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.WeiXinPay.ValueObject
{
    public enum WeiXinUrlType
    {
        /// <summary>
        /// 凭证
        /// </summary>
        Token,
        /// <summary>
        /// APP预订单
        /// </summary>
        AppPreOrder,
        /// <summary>
        /// 标记客户的投诉处理状态
        /// </summary>
        UpdateFeedback,
        /// <summary>
        /// 客服接口
        /// </summary>
        CustomerService
    }
}
