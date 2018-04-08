/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/9/10 11:36:06
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

namespace JIT.Utility.Report.DataFilter
{
    /// <summary>
    /// 报表数据的筛选器接口 
    /// </summary>
    public interface IDataFilter
    {
        /// <summary>
        /// 过滤的方式
        /// </summary>
        DataFilterTypes FilterType { get; }

        /// <summary>
        /// 是否显示该列
        /// </summary>
        /// <param name="pColumn">进行筛选的列</param>
        /// <param name="pDataRetriever">列数据获取器</param>
        /// <returns></returns>
        bool IsShowColumn(FilteringColumn pColumn,ColumnDataRetriever pDataRetriever);

        /// <summary>
        /// 是否显示该行
        /// </summary>
        /// <param name="pRowIndex">行索引,索引以0开始</param>
        /// <param name="pDataRetriever">行数据获取器</param>
        /// <returns></returns>
        bool IsShowRow(int pRowIndex, RowDataRetriever pDataRetriever);
    }
}
