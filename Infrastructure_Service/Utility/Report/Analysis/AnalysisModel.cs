/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/29 14:01:11
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
    /// 分析报表表格的模型
    /// </summary>
    public class AnalysisModel
    {
        #region 构造函数
        /// <summary>
        /// 分析报表表格的模型 
        /// </summary>
        public AnalysisModel()
        {
        }
        #endregion

        #region 属性集

        /// <summary>
        /// 钻取的路由
        /// </summary>
        public IDrillRouting DrillRouting { get; set; }

        #endregion
    }
}
