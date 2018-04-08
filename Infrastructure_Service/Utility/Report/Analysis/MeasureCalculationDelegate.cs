/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/1 17:28:03
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
    /// 度量计算
    /// </summary>
    /// <param name="pGroupingItems">分组内的所有数据项</param>
    public delegate double MeasureCalculationDelegate(IFactData[] pGroupingItems);
}
