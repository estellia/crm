/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/25 12:32:33
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace JIT.Utility.Pay.UnionPay.Interface.Wap.ValueObject
{
    /// <summary>
    /// 订单查询结果 
    /// </summary>
    public enum OrderQueryResults
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success=0
        ,
        /// <summary>
        /// 失败
        /// </summary>
        Failed=1
        ,
        /// <summary>
        /// 处理中
        /// </summary>
        Processing=2
        ,
        /// <summary>
        /// 无此交易
        /// </summary>
        NoSuchOrder=3
    }
}
