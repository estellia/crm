/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/1/11 11:49:21
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

namespace JIT.Const
{
    /// <summary>
    /// 错误类别
    /// <remarks>
    /// <para>0-99      基础框架类的错误</para>
    /// <para>100-199   认证类的错误</para>
    /// </remarks>
    /// </summary>
    public static class ERROR_TYPES
    {
        /// <summary>
        /// 基础框架类的错误
        /// <remarks>
        /// <para>此类错误的错误码以'01'开头</para>
        /// </remarks>
        /// </summary>
        public readonly static int INFRASTRUCTURE = 1;

        /// <summary>
        /// 认证类的错误
        /// <remarks>
        /// <para>此类错误的错误码以'02'开头</para>
        /// </remarks>
        /// </summary>
        public readonly static int AUTHENTICATE = 2;
    }
}
