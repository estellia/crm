/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/29 14:53:17
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

using JIT.Utility.Web.ComponentModel;

namespace JIT.Utility.Report.GridModel
{
    /// <summary>
    /// 分析报表表格的钻取列 
    /// </summary>
    public class AnalysisReportGridDrillColumn:AnalysisReportGridColumn
    {
        #region 构造函数
        /// <summary>
        /// 分析报表表格的钻取列 
        /// </summary>
        public AnalysisReportGridDrillColumn()
        {
        }
        #endregion

        #region 属性集

        #region 钻取的路由
        /// <summary>
        /// 钻取的路由
        /// </summary>
        public IDrillRouting DrillRouting { get; set; }
        #endregion

        #region 列标题
        /// <summary>
        /// 列标题
        /// <remarks>
        /// <para>1.列标题为钻取路由中的当前列的列标题</para>
        /// </remarks>
        /// </summary>
        public override string ColumnTitle
        {
            get { return this.DrillRouting.Current.ColumnTitle; }
            set { this.DrillRouting.Current.ColumnTitle = value; }
        }
        #endregion

        #region 列宽(单位为px)
        /// <summary>
        /// 列宽(单位为px)
        /// <remarks>
        /// <para>1.列宽为钻取路由中的当前列的列宽</para>
        /// </remarks>
        /// </summary>
        public override int ColumnWidth
        {
            get { return this.DrillRouting.Current.ColumnWidth; }
            set { this.DrillRouting.Current.ColumnWidth = value; }
        }
        #endregion

        #region 数据列
        /// <summary>
        /// 数据列
        /// <remarks>
        /// <para>1.数据列为钻取路由中的当前列的数据列</para>
        /// </remarks>
        /// </summary>
        public override DataModelColumn DataColumn
        {
            get { return this.DrillRouting.Current.DataColumn; }
            set { this.DrillRouting.Current.DataColumn = value; }
        }
        #endregion

        #region 表格列类型
        /// <summary>
        /// 表格列类型
        /// <remarks>
        /// <para>1.表格列类型为钻取路由中的当前列的表格列类型</para>
        /// </remarks>
        /// </summary>
        public override JITGridColumnTypes? GridColumnType
        {
            get { return this.DrillRouting.Current.GridColumnType; }
            set { this.DrillRouting.Current.GridColumnType = value; }
        }
        #endregion

        #endregion

        #region 实现AnalysisReportGridColumn的抽象成员
        /// <summary>
        /// 获得分析报表表格列的列类型
        /// </summary>
        public override AnalysisReportGridColumnlTypes ColumnType
        {
            get { return AnalysisReportGridColumnlTypes.Drill; }
        }
        #endregion
    }
}
