/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/9/10 12:55:46
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

using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;
using JIT.Utility.Report.Analysis;

namespace JIT.Utility.Report.DataFilter
{
    /// <summary>
    /// 需要进行筛选的列 
    /// </summary>
    public class FilteringColumn
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public FilteringColumn()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 表格列
        /// </summary>
        public Column GridColumn { get; set; }

        /// <summary>
        /// 分析列
        /// <remarks>
        /// <para>报表列所对应的分析列</para>
        /// </remarks>
        /// </summary>
        public AnalysisColumn AnalysisColumn { get; set; }
        #endregion
    }
}
