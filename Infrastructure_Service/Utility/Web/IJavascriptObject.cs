/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/8 11:42:56
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

using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Web
{
    /// <summary>
    /// Javascript对象的接口
    /// <remarks>
    /// <para>Javascript对象可以是一个函数,一个对象申明,一个方法调用等等,这些对象最终都被转换为脚本块输出到前台.</para>
    /// </remarks>
    /// </summary>
    public interface IJavascriptObject
    {
        /// <summary>
        /// Javascript对象转换成对象的脚本块
        /// </summary>
        /// <param name="pPrevTabs">脚本所有内容都包含的前导tab键数,用于排版</param>
        /// <returns>Javascript脚本块</returns>
        string ToScriptBlock(int pPrevTabs);
    }
}
