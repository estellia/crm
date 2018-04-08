/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/29 14:56:51
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

using JIT.Utility.DataAccess.Query;
using JIT.Utility.Web;
using JIT.Utility.Web.ComponentModel;

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 表格列的钻取路由接口 
    /// </summary>
    public interface IDrillRouting
    {
        /// <summary>
        /// 是否可以钻入
        /// </summary>
        /// <param name="pTargetColumn">用户选择钻入的报表列</param>
        /// <returns>是否可以钻入</returns>
        bool CanDrillIn(AnalysisColumn pTargetColumn);
        
        /// <summary>
        /// 当前的表格剖面
        /// </summary>
        AnalysisReportDrillSection CurrentSection { get; }

        /// <summary>
        /// 钻入
        /// </summary>
        /// <param name="pTargetColumn">用户选择钻入的报表列</param>
        /// <param name="pDrilledDimValue">钻入项(是一个维度项)的值</param>
        /// <param name="pDrilledDimText">钻入项(是一个维度项)的文本</param>
        /// <returns>钻入后的表格剖面</returns>
        AnalysisReportDrillSection DrillIn(AnalysisColumn pTargetColumn,string pDrilledDimValue,string pDrilledDimText);
        
        /// <summary>
        /// 跳转到指定钻取剖面
        /// </summary>
        /// <param name="pSectionName">指定钻取剖面的ID</param>
        /// <param name="pConditionsByDrilling">由钻入带来的筛选条件,字典的KEY为钻取剖面的ID</param>
        /// <returns>如果跳转成功，则返回相应的钻取剖面</returns>
        AnalysisReportDrillSection Goto(string pSectionID,Dictionary<string, IWhereCondition[]> pConditionsByDrilling);

        /// <summary>
        /// 获得钻取的路线
        /// </summary>
        /// <returns></returns>
        AnalysisReportDrilling[] GetDrillingRoute();
    }
}
