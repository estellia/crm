/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/29 14:03:57
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

using JIT.Utility.Report.Export.SummaryFormatter;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 摘要接口
    /// </summary>
    public interface ISummary
    {
        /// <summary>
        /// 摘要的文本
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// 摘要显示的顺序
        /// </summary>
        int DisplayOrder { get; set; }

        /// <summary>
        /// 摘要的格式化器
        /// <remarks>
        /// <para>此格式化器用于在将报表数据导出时，服务器端对报表摘要进行格式化,以实现所见即所得的导出</para>
        /// </remarks>
        /// </summary>
        ISummaryFormatter Formatter { get; set; }

        /// <summary>
        /// 判断报表列是否需要进行摘要计算
        /// </summary>
        /// <param name="pColumn">报表列</param>
        /// <returns></returns>
        bool IsNeedCalculate(AnalysisColumn pColumn);

        /// <summary>
        /// 计算摘要
        /// </summary>
        /// <param name="pColumn">报表列</param>
        /// <param name="pDataItems">
        /// <remarks>
        /// <para>列的数据项</para>
        /// <para>1.如果列是维度列,则对象是为KeyValuePair集合,其中Key为维度数据项的ID，Value为维度数据项的文本</para>
        /// <para>2.如果是度量列，则对象为度量值</para>
        /// <para>3.如果是自定义计算列，则对象为计算后的值</para>
        /// </remarks>
        /// </param>
        /// <returns></returns>
        string CalculateSummary(AnalysisColumn pColumn,object[] pDataItems);

        #region 1.01版本扩展
        /// <summary>
        /// 摘要计算方式
        /// </summary>
        SummaryCalculationTypes CalculationType { get; }

        /// <summary>
        /// 计算摘要
        /// </summary>
        /// <param name="pColumn">当前进行计算的报表列</param>
        /// <param name="pGridColumn">当前进行计算的(ExtJS)表格列</param>
        /// <param name="pDataRetriever">报表数据获取器</param>
        /// <returns></returns>
        string CalculateSummary(AnalysisColumn pColumn,Column pGridColumn, ReportDataRetriever pDataRetriever);
        #endregion
    }
}
