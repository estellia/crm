/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/12 11:53:30
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

namespace JIT.Utility.Web
{
    /// <summary>
    /// JS函数的类别
    /// </summary>
    public enum JSFunctionTypes
    {
        /// <summary>
        /// 普通形式
        /// </summary>
        Common
        ,
        /// <summary>
        /// 函数变量
        /// </summary>
        Variable
        ,
        /// <summary>
        /// 内联函数
        /// </summary>
        Inline
    }
}
