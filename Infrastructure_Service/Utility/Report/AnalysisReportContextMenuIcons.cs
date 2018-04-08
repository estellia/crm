/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/22 13:37:42
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
using System.ComponentModel;
using System.Text;

namespace JIT.Utility.Report
{
    /// <summary>
    /// 分析报表上下文菜单的图标 
    /// </summary>
    public enum AnalysisReportContextMenuIcons
    {
        /// <summary>
        /// 导出
        /// </summary>
        Export
        ,
        /// <summary>
        /// 数据透视
        /// </summary>
        Pivot
        ,
        /// <summary>
        /// 行转列
        /// </summary>
        RowToColumn
        ,
        /// <summary>
        /// 行列互换
        /// </summary>
        CRExchange
        ,
        /// <summary>
        /// 查看明细
        /// </summary>
        ViewDetail
    }

    /// <summary>
    /// AnalysisReportContextMenuIcons枚举的扩展方法
    /// </summary>
    public static class AnalysisReportContextMenuIconsExtensionMethods
    {
        /// <summary>
        /// 扩展方法：获取图标的Url
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns>图标的Url</returns>
        public static string GetIconURL(this AnalysisReportContextMenuIcons pCaller)
        {
            switch (pCaller)
            {
                case AnalysisReportContextMenuIcons.Export:
                    return "/Lib/Image/export.png";
                case AnalysisReportContextMenuIcons.CRExchange:
                    return "/Lib/Image/crExchange.png";
                case AnalysisReportContextMenuIcons.Pivot:
                    return "/Lib/Image/pivot.png";
                case AnalysisReportContextMenuIcons.RowToColumn:
                    return "/Lib/Image/rowToColumn.png";
                case AnalysisReportContextMenuIcons.ViewDetail:
                    return "/Lib/Image/viewDetail.png";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
