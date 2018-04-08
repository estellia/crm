/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/10/24 11:07:44
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
    /// 自定义数据源列 
    /// </summary>
    public class CustomizeDataSourceColumn:SQLMeasureColumn
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CustomizeDataSourceColumn()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 自定义数据源
        /// </summary>
        public IReportDataSource DataSource { get; set; }
        #endregion

        #region 实现AnalysisColumn的抽象成员
        /// <summary>
        /// 获得分析列的列类型
        /// </summary>
        public override AnalysisColumnTypes ColumnType
        {
            get { return AnalysisColumnTypes.CustomizeDataSource; }
        }
        #endregion
    }
}
