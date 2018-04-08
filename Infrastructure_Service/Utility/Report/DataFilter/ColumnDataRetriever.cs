/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/9/10 13:01:02
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

using JIT.Utility.Report.Analysis;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;

namespace JIT.Utility.Report.DataFilter
{
    /// <summary>
    /// 列数据的获取器 
    /// </summary>
    public class ColumnDataRetriever
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ColumnDataRetriever(DataTable pDataes, Dictionary<Column, Dictionary<ISummary, string>> pColumnSummaryValues)
        {
            this.Dataes = pDataes;
            this.ColumnSummaryValues = pColumnSummaryValues;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 数据
        /// </summary>
        protected DataTable Dataes { get; set; }

        /// <summary>
        /// 表格列与摘要的映射
        /// </summary>
        protected Dictionary<Column, Dictionary<ISummary, string>> ColumnSummaryValues { get; set; }
        #endregion

        /// <summary>
        /// 获取列数据
        /// </summary>
        /// <param name="pColumn">需要进行过滤的列</param>
        /// <returns></returns>
        public object[] GetDataes(FilteringColumn pColumn)
        {
            List<object> dataes = new List<object>();
            //
            if (this.Dataes != null && this.Dataes.Rows.Count > 0
                && pColumn!=null
                && pColumn.GridColumn!=null
                && (!string.IsNullOrEmpty(pColumn.GridColumn.DataIndex))
                )
            {
                var colID = pColumn.GridColumn.DataIndex;
                if (this.Dataes.Columns.Contains(colID))
                {
                    foreach (DataRow dr in this.Dataes.Rows)
                    {
                        if (dr[colID] == DBNull.Value)
                            dataes.Add(null);
                        else
                            dataes.Add(dr[colID]);
                    }
                }
            }
            //
            return dataes.ToArray();
        }

        /// <summary>
        /// 获取摘要值
        /// </summary>
        /// <param name="pColumn">需要进行过滤的列</param>
        /// <param name="pSummary">摘要</param>
        /// <returns></returns>
        public string GetSummary(FilteringColumn pColumn,ISummary pSummary)
        {
            if (pColumn != null && pColumn.GridColumn != null && pSummary != null && this.ColumnSummaryValues!=null)
            {
                if (this.ColumnSummaryValues.ContainsKey(pColumn.GridColumn))
                {
                    var vals = this.ColumnSummaryValues[pColumn.GridColumn];
                    if (vals != null && vals.Count > 0)
                    {
                        foreach (var val in vals)
                        {
                            if (val.Key == pSummary)
                                return val.Value;
                        }
                    }
                }
            }
            return null;
        }
    }
}
