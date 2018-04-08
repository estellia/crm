/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/5/13 15:12:47
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
    /// 分析报表的钻取 
    /// </summary>
    public class AnalysisReportDrilling
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AnalysisReportDrilling()
        {
        }
        #endregion

        /// <summary>
        /// 从哪个钻取剖面钻入
        /// </summary>
        public AnalysisReportDrillSection From { get; set; }

        /// <summary>
        /// 钻入哪个钻取剖面
        /// </summary>
        public AnalysisReportDrillSection To { get; set; }

        /// <summary>
        /// 钻取项(钻取项是一个维度项)的值
        /// </summary>
        public string DrilledDimValue { get; set; }

        /// <summary>
        /// 钻取项(钻取项是一个维度项)的文本
        /// </summary>
        public string DrilledDimText { get; set; }
    }
}
