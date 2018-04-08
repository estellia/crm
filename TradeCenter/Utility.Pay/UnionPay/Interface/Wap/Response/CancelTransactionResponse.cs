/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/25 16:44:19
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

using JIT.Utility.Pay.UnionPay.ExtensionMethod;
using JIT.Utility.Pay.UnionPay.Interface.Wap.ValueObject;
using JIT.Utility.Pay.UnionPay.ValueObject;

namespace JIT.Utility.Pay.UnionPay.Interface.Wap.Response
{
    /// <summary>
    /// 交易撤销响应
    /// <remarks>
    /// <para>支付前置 -> 商户平台</para>
    /// </remarks>
    /// </summary>
    public class CancelTransactionResponse:BaseAPIResponse
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CancelTransactionResponse()
        {
        }
        #endregion
    }
}
