/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/9/10 11:39:58
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

namespace JIT.Utility.Report.DataFilter
{
    /// <summary>
    /// 数据过滤的类型 
    /// </summary>
    public enum DataFilterTypes
    {
        /// <summary>
        /// 过滤行
        /// </summary>
        FilterRow=1
        ,
        /// <summary>
        /// 过滤列
        /// </summary>
        FilterColumn=2
        ,
        /// <summary>
        /// 都过滤
        /// </summary>
        Both
    }
}
