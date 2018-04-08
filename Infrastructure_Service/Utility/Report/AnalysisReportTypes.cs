/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/17 10:07:03
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

namespace JIT.Utility.Report
{
    /// <summary>
    /// 分析报表的类别 
    /// </summary>
    public enum AnalysisReportTypes
    {
        /// <summary>
        /// 基于内存的
        /// </summary>
        MemoryBased
        ,
        /// <summary>
        /// 基于SQL语句的
        /// </summary>
        SQLBased
    }
}
