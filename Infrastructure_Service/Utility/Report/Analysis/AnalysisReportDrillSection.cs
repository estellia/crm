/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/1 11:20:18
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

using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 分析报表的钻取剖面 
    /// </summary>
    public class AnalysisReportDrillSection
    {
        #region 构造函数
        /// <summary>
        /// 分析报表的钻取剖面 
        /// </summary>
        public AnalysisReportDrillSection()
        {
            this.SectionID = Guid.NewGuid().ToText();
        }
        #endregion

        #region 属性集

        /// <summary>
        /// 剖面的ID，在同一个报表中的所有钻取剖面的ID不能相同
        /// </summary>
        public string SectionID { get; set; }

        /// <summary>
        /// 剖面的名称
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// 报表的表格列
        /// </summary>
        public AnalysisColumnList Columns { get; set; }

        /// <summary>
        /// 报表的摘要
        /// </summary>
        public List<ISummary> Summeries { get; set; }

        #endregion
    }
}
