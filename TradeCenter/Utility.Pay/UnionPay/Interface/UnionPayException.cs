/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/24 17:18:11
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

namespace JIT.Utility.Pay.UnionPay.Interface
{
    /// <summary>
    /// 银联WAP支付的异常 
    /// </summary>
    public class UnionPayException:System.Exception
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public UnionPayException()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pErrorMessage">错误信息</param>
        public UnionPayException(string pErrorMessage)
            : base(pErrorMessage)
        {
        }
        #endregion

        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }
    }
}
