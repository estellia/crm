/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/16 14:31:19
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
using JIT.Utility.Locale;

namespace JIT.Utility.Report
{
    /// <summary>
    /// 分析报表的查询 
    /// </summary>
    public class AnalysisReportQuery
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AnalysisReportQuery()
        {
        }
        #endregion

        /// <summary>
        /// 筛选条件
        /// </summary>
        public IWhereCondition[] WhereCondtions { get; set; }

        /// <summary>
        /// 排序条件
        /// </summary>
        public OrderBy[] OrderBys { get; set; }

        /// <summary>
        /// 用户选择的语言
        /// </summary>
        public Languages? Language { get; set; }
    }
}
