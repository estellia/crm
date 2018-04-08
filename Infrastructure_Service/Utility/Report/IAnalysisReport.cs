/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/3 13:32:14
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
using System.Data;
using System.Text;

using JIT.Utility.DataAccess.Query;
using JIT.Utility.Locale;
using JIT.Utility.Report.FactData;
using JIT.Utility.Report.Analysis;
using JIT.Utility.Report.DataFilter;
using JIT.Utility.Web;

namespace JIT.Utility.Report
{
    /// <summary>
    /// 分析报表接口 
    /// </summary>
    public interface IAnalysisReport
    {
        /// <summary>
        /// 分析报表实例的ID，该ID全局唯一
        /// </summary>
        string ReportID { get; }

        /// <summary>
        /// 当前的报表列
        /// </summary>
        AnalysisColumnList CurrentColumns { get; }

        /// <summary>
        /// 分析报表结果数据的过滤器
        /// </summary>
        List<IDataFilter> ResultDataFilters { get; set; }

        /// <summary>
        /// 初始化报表的模型
        /// <remarks>
        /// <para>初始化的报表模型包含：</para>
        /// <para>1.FactDataModel定义了所取的基础数据的模型</para>
        /// <para>2.AnalysisModel定义了报表的分析模型</para>
        /// </remarks>
        /// </summary>
        void InitMode();

        /// <summary>
        /// 执行分析报表的查询
        /// </summary>
        /// <param name="pWheres">报表数据的过滤条件</param>
        /// <param name="pOrderBys">报表数据的排序条件</param>
        /// <param name="pLanguage">用户所选择的语言</param>
        /// <returns>报表的输出</returns>
        AnalysisReportOutput ProcessQuery(IWhereCondition[] pWheres, OrderBy[] pOrderBys,Languages pLanguage);

        /// <summary>
        /// 执行分析报表的钻入
        /// </summary>
        /// <param name="pDrilledColumn">用户选择钻入的维度列的列ID</param>
        /// <param name="pDimValue">用户选择钻入的维度项的ID值</param>
        /// <param name="pDimValue">用户选择钻入的维度项的文本值</param>
        /// <param name="pBringFromDrilling">从用户的钻取带入的其他查询条件</param>
        /// <returns></returns>
        AnalysisReportOutput ProcessDrillIn(string pDrilledColumnID, string pDimValue, string pDimText,IWhereCondition[] pBringFromDrilling);

        /// <summary>
        /// 执行分析报表的跳转
        /// </summary>
        /// <param name="pGotoSectionID">用户选择的跳转到的钻取剖面的ID</param>
        /// <returns>报表的输出</returns>
        AnalysisReportOutput ProcessGoto(string pGotoSectionID);

        /// <summary>
        /// 处理数据透视列变更
        /// </summary>
        /// <param name="pPivotChangedColumnID">被改变数据透视状态的维度列ID</param>
        /// <param name="pIsPivoted">改变后的状态为什么</param>
        /// <returns>报表的输出</returns>
        AnalysisReportOutput ProcessPivotChanged(string pPivotChangedColumnID, bool pIsPivoted);

        /// <summary>
        /// 处理行列转换变更
        /// </summary>
        /// <param name="pCRConversionColumnID">进行行列转换的列</param>
        /// <param name="pIsCRConverted">改变后的状态</param>
        /// <returns>报表的输出</returns>
        AnalysisReportOutput ProcessCRConversionChanged(string pCRConversionColumnID, bool pIsCRConverted);

        /// <summary>
        /// 处理行列互换
        /// </summary>
        /// <returns></returns>
        AnalysisReportOutput ProcessCRExchange();

        /// <summary>
        /// 再次执行,注意，必须要已经执行过查询才能够调用该方法
        /// </summary>
        /// <returns>报表的输出</returns>
        AnalysisReportOutput ProcessQueryAgain();
    }
}
