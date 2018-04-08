/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/1/11 11:34:51
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

namespace JIT.Utility
{
    /// <summary>
    /// 错误信息 
    /// </summary>
    public class ErrorInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ErrorInfo()
        {
        }
        #endregion

        /// <summary>
        /// 错误的类别
        /// </summary>
        public int ErrorCategory { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
