/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/2 17:31:18
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

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 常见的度量计算器
    /// </summary>
    public enum MeasureCalculatorTypes
    {
        /// <summary>
        /// 求合计
        /// </summary>
        Sum
        ,
        /// <summary>
        /// 求平均值
        /// </summary>
        Avg
        ,
        /// <summary>
        /// 求最大值
        /// </summary>
        Max
        ,
        /// <summary>
        /// 求最小值
        /// </summary>
        Min
    }
}
