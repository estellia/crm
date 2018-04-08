/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 18:02:18
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
    /// JIT的自定义异常的基类
    /// <remarks>
    /// <para>1.所有自定义的异常都必须继承自该类</para>
    /// </remarks>
    /// </summary>
    public class JITException:Exception
    {
        #region 构造函数
        /// <summary>
        /// JIT的自定义异常的基类
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pErrorInfo">错误信息</param>
        /// <param name="pInnerException">内部异常</param>
        public JITException(BasicUserInfo pUserInfo,ErrorInfo pErrorInfo,Exception pInnerException)
            :base(pErrorInfo!=null?pErrorInfo.ErrorMessage:string.Empty,pInnerException)
        {
            this.UserInfo = pUserInfo;
            this.ErrorInfo = pErrorInfo;
        }

        /// <summary>
        /// JIT的自定义异常的基类
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pErrorInfo">错误信息</param>
        public JITException(BasicUserInfo pUserInfo, ErrorInfo pErrorInfo) : this(pUserInfo, pErrorInfo, null) { }

        /// <summary>
        /// JIT的自定义异常的基类
        /// </summary>
        /// <param name="pErrorInfo">错误信息</param>
        public JITException(ErrorInfo pErrorInfo) : this(null, pErrorInfo, null) { }

        /// <summary>
        /// JIT的自定义异常的基类
        /// </summary>
        /// <param name="pErrorCategory">错误类别</param>
        /// <param name="pErrorCode">错误码</param>
        /// <param name="pErrorMessage">错误信息</param>
        public JITException(int pErrorCategory, string pErrorCode, string pErrorMessage) : this(null, new ErrorInfo() { ErrorCategory = pErrorCategory, ErrorCode = pErrorCode, ErrorMessage = pErrorMessage }, null) { }

        /// <summary>
        /// JIT的自定义异常的基类
        /// </summary>
        /// <param name="pErrorCategory">错误类别</param>
        /// <param name="pErrorCode">错误码</param>
        /// <param name="pErrorMessageTemplate">错误信息模板</param>
        /// <param name="pMessageParams">错误信息模板的参数</param>
        public JITException(int pErrorCategory, string pErrorCode, string pErrorMessageTemplate, params string[] pMessageParams) : this(null, new ErrorInfo() { ErrorCategory = pErrorCategory, ErrorCode = pErrorCode, ErrorMessage = string.Format(pErrorMessageTemplate, pMessageParams) }, null) { }
        #endregion

        #region 属性
        /// <summary>
        /// 用户信息
        /// </summary>
        public BasicUserInfo UserInfo { get; private set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public ErrorInfo ErrorInfo { get; private set; }
        #endregion
    }
}
