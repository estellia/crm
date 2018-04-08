/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/29 15:11:07
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

using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 自定义计算列
    /// </summary>
    public class CustomizeCalculateColumn:AnalysisColumn
    {
        #region 构造函数
        /// <summary>
        /// 分析报表表格的自定义计算列 
        /// </summary>
        public CustomizeCalculateColumn()
        {
        }
        #endregion

        #region 属性集

        #region 自定义列项的计算器
        /// <summary>
        /// 自定义列项的计算器
        /// <remarks>
        /// <para>计算器的方法声明：</para>
        /// <para>第一个参数：获取需要进行计算的数据的数据获取器.</para>
        /// <para>第二个参数：当前需要进行计算的数据行的行索引，索引以0开始.</para>
        /// <para>返回值：计算后的结果.</para>
        /// </remarks>
        /// </summary>
        public Func<CustomizeCalculationDataRetriever,int, string> Calculator;
        #endregion

        #region 进行自定义列项的计算的计算次序
        /// <summary>
        /// 进行自定义列项的计算的计算次序
        /// </summary>
        public int CalculationOrder { get; set; }
        #endregion

        #endregion

        #region 实现AnalysisColumn的抽象成员
        /// <summary>
        /// 获得分析列的列类型
        /// </summary>
        public override AnalysisColumnTypes ColumnType
        {
            get { return AnalysisColumnTypes.CustomizeCalcaulate; }
        }
        #endregion
    }
}
