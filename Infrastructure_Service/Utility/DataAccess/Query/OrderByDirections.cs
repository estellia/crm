/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/9 17:36:53
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
using System.ComponentModel;
using System.Text;

namespace JIT.Utility.DataAccess.Query
{
    /// <summary>
    /// 排序方向 
    /// </summary>
    public enum OrderByDirections
    {
        /// <summary>
        /// 倒序
        /// </summary>
        [Description("desc")]
        Desc
        ,
        /// <summary>
        /// 顺序
        /// </summary>
        [Description("asc")]
        Asc
    }
}
