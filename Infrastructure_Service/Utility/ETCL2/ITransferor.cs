/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/13 14:24:17
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

namespace JIT.Utility.ETCL2
{
    /// <summary>
    /// 转换器接口 
    /// </summary>
    public interface ITransferor
    {
        /// <summary>
        /// 将源数据转换成目标数据
        /// </summary>
        /// <param name="pSource">源数据</param>
        /// <returns>目标数据</returns>
        object Transfer(object pSource);
    }
}
