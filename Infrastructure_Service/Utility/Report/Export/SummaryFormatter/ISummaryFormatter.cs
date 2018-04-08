/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/9/9 19:42:31
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

namespace JIT.Utility.Report.Export.SummaryFormatter
{
    /// <summary>
    /// 报表摘要的格式化器 
    /// </summary>
    public interface ISummaryFormatter
    {
        /// <summary>
        /// 往容器中写摘要值
        /// </summary>
        /// <param name="pContainer">导出后的容器,如果导出为Excel,则为WookSheet</param>
        /// <param name="pSummary">报表摘要</param>
        /// <param name="pSummaryValue">摘要值</param>
        /// <param name="pRowIndex">行</param>
        /// <param name="pColumnIndex">列</param>
        /// <param name="pIsIncludeSummaryTitle">是否在写数据的时候包含摘要标题(主要是当摘要列是第一列时,将标题和数据放在一起写)</param>
        /// <param name="pColumnType">摘要所属的表格列的列类型</param>
        void WriteSummary(object pContainer,ISummary pSummary, object pSummaryValue, int pRowIndex, int pColumnIndex,bool pIsIncludeSummaryTitle, ColumnTypes pColumnType);

        /// <summary>
        /// 往容器中写摘要标题
        /// </summary>
        /// <param name="pContainer"></param>
        /// <param name="pSummary"></param>
        /// <param name="pRowIndex"></param>
        /// <param name="pColumnIndex"></param>
        void WriteSummaryTitle(object pContainer, ISummary pSummary, int pRowIndex, int pColumnIndex);
    }
}
