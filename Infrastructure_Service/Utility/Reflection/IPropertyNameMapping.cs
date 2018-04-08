/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/20 14:43:59
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

namespace JIT.Utility.Reflection
{
    /// <summary>
    /// 属性名映射 
    /// </summary>
    public interface IPropertyNameMapping
    {
        /// <summary>
        /// 根据名称找到相应的属性名
        /// </summary>
        /// <param name="pSourceName">源名称</param>
        /// <returns></returns>
        string GetPropertyNameBy(string pSourceName);
    }
}
