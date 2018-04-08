/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/20 10:40:47
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
    /// 设置对象属性值的委托
    /// </summary>
    /// <param name="pTarget">对象</param>
    /// <param name="pPropertyValue">属性值</param>
    public delegate void SetValueDelegate(object pTarget,object pPropertyValue);
}
