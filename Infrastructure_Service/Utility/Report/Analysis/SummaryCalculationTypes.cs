/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/10/17 14:52:35
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
    /// 摘要计算方式 
    /// </summary>
    public enum SummaryCalculationTypes
    {
        /// <summary>
        /// 以自身列的值进行计算
        /// <remarks>
        /// <para>默认方式1.0版本默认的</para>
        /// </remarks>
        /// </summary>
        CalculateSelf
        ,
        /// <summary>
        /// 以任意的列的值进行计算
        /// <remarks>
        /// <para>1.01版本扩展的计算方式</para>
        /// </remarks>
        /// </summary>
        CalculateFromAnyColumn
    }
}
