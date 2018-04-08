/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/29 11:17:26
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
    /// 分析列的类型 
    /// </summary>
    public enum AnalysisColumnTypes
    {
        /// <summary>
        /// 维度列
        /// </summary>
        Dim
        ,
        /// <summary>
        /// 度量列
        /// </summary>
        MemoryMeasure
        ,
        /// <summary>
        /// SQL度量列
        /// </summary>
        SQLMeasure
        ,
        /// <summary>
        /// 自定义计算列
        /// </summary>
        CustomizeCalcaulate
        ,
        /// <summary>
        /// 自定义数据源列
        /// </summary>
        CustomizeDataSource
    }
}
