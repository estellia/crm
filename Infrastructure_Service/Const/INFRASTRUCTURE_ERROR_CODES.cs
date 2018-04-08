/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/1/11 11:52:37
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
    /// 基础框架的错误码
    /// <remarks>
    /// <para>错误码的规则为:错误码类别(两位)+'-'+错误码值(三位)</para>
    /// <para>错误码的值范围定义为:</para>
    /// <para>Utility           0 - 99</para>
    /// <para>Cache             100 - 199</para>
    /// <para>DataAccess        200 - 299</para>
    /// <para>Entity            300 - 399</para>
    /// <para>ExtensionMethod   400 - 499</para>
    /// <para>Log               500 - 599</para>
    /// </remarks>
    /// </summary>
    public static class INFRASTRUCTURE_ERROR_CODES
    {
        /// <summary>
        /// 持久化状态错误
        /// </summary>
        public const string PERSISTENCE_STATUS_ERROR = "01300";
    }
}
