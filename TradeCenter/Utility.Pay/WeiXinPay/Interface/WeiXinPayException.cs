/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/10 11:11:50
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

namespace JIT.Utility.Pay.WeiXinPay.Interface
{
    /// <summary>
    /// WeiXinPayException 
    /// </summary>
    public class WeiXinPayException:System.Exception
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WeiXinPayException()
        {
            this.Code = "500";
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pErrorMessage">错误信息</param>
        public WeiXinPayException(string pErrorMessage)
            : base(pErrorMessage)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pErrorCode">错误码</param>
        /// <param name="pErrorMessage">错误信息</param>
        public WeiXinPayException(string pErrorCode, string pErrorMessage)
            : base(pErrorMessage)
        {
            this.Code = pErrorCode;
        }
        #endregion

        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }
    }
}
