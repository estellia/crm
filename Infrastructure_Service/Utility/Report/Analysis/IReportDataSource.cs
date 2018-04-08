/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/10/24 9:50:19
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

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 分析报表的数据源接口 
    /// </summary>
    public interface IReportDataSource
    {
        /// <summary>
        /// 执行分组计算
        /// <remarks>
        /// <para>分组计算的主要工作内容有：</para>
        /// <para>1.读取事实数据.</para>
        /// <para>2.对事实数据进行分组,并依次进行度量的计算</para>
        /// </remarks>
        /// </summary>
        /// <param name="pWheres">
        /// 数据源的过滤条件
        /// <para>注意：</para>
        /// <para>1.过滤条件中包含报表初始的条件也包含由钻取带入的过滤条件</para>
        /// </param>
        /// <param name="pActiveDims">使用数据源的当前报表剖面中所有的活跃维度（活跃维度是去除了非数据透视的维度）</param>
        /// <param name="pColumns">使用数据源的当前报表剖面中所有将使用当前数据源的自定义数据源列集合</param>
        /// <returns>分组聚合后的数据集,数据集中的所有列的列名为对应的事实数据列的ColumnID</returns>
        DataTable ProcessGroupingCalculation(IWhereCondition[] pWheres,DimColumn[] pActiveDims,CustomizeDataSourceColumn[] pColumns);
    }
}
