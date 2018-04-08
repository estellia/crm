/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/9/10 15:14:58
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
    /// 通用的数据筛选器
    /// <remarks>
    /// <para>通过定义相关委托实现数据筛选,避免过多的创建数据筛选类</para>
    /// </remarks>
    /// </summary>
    public class CommonDataFilter:IDataFilter
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CommonDataFilter()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 是否显示列的策略
        /// </summary>
        public Func<FilteringColumn, ColumnDataRetriever, bool> ShowColumnStrategy { get; set; }

        /// <summary>
        /// 是否显示行的策略
        /// </summary>
        public Func<int, RowDataRetriever, bool> ShowRowStrategy { get; set; }
        #endregion

        #region IDataFilter 成员
        /// <summary>
        /// 过滤的方式
        /// </summary>
        public DataFilterTypes FilterType { get; set; }

        /// <summary>
        /// 是否显示该列
        /// </summary>
        /// <param name="pColumn">进行筛选的列</param>
        /// <param name="pDataRetriever">列数据获取器</param>
        /// <returns></returns>
        public bool IsShowColumn(FilteringColumn pColumn, ColumnDataRetriever pDataRetriever)
        {
            if (this.ShowColumnStrategy != null)
            {
                return this.ShowColumnStrategy(pColumn, pDataRetriever);
            }
            //默认为显示
            return true;
        }

        /// <summary>
        /// 是否显示该行
        /// </summary>
        /// <param name="pRowIndex">行索引,索引以0开始</param>
        /// <param name="pDataRetriever">行数据获取器</param>
        /// <returns></returns>
        public bool IsShowRow(int pRowIndex, RowDataRetriever pDataRetriever)
        {
            if (this.ShowRowStrategy != null)
            {
                return this.IsShowRow(pRowIndex, pDataRetriever);
            }
            //默认为显示
            return true;
        }
        #endregion
    }
}
