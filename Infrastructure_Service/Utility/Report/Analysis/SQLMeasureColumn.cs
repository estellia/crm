/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/17 10:49:18
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
using JIT.Utility.Report.FactData;
using JIT.Utility.Web;

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 基于SQL语句的度量列 
    /// </summary>
    public class SQLMeasureColumn:AnalysisColumn
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SQLMeasureColumn()
        {
        }
        #endregion

        #region 属性集

        #region 数据列
        private DataModelColumn _dataColumn = null;
        /// <summary>
        /// 数据列
        /// </summary>
        public virtual DataModelColumn DataColumn
        {
            get { return this._dataColumn; }
            set
            {
                this._dataColumn = value;
            }
        }
        #endregion

        #region 表达式模板
        /// <summary>
        /// 表达式模板
        /// <remarks>
        /// <para>1.模板中会将度量的数据列的列名替换进去,占位符为：$column$</para>
        /// </remarks>
        /// </summary>
        public string ExpressionTemplate { get; set; }
        #endregion

        #region 数据筛选
        /// <summary>
        /// 数据筛选
        /// <remarks>
        /// <para>1.生成的语句也会使用占位符($column$)将内容替换掉</para>
        /// </remarks>
        /// </summary>
        public IWhereCondition[] Wheres { get; set; }
        #endregion

        #endregion

        #region 实现AnalysisColumn的抽象成员
        /// <summary>
        /// 获得分析列的列类型
        /// </summary>
        public override AnalysisColumnTypes ColumnType
        {
            get { return AnalysisColumnTypes.SQLMeasure; }
        }
        #endregion
    }
}
