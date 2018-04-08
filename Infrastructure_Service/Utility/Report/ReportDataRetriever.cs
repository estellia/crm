/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/10/17 11:03:25
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
using System.Linq;
using System.Text;

using JIT.Utility.Report.Analysis;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;

namespace JIT.Utility.Report
{
    /// <summary>
    /// 报表数据获取器
    /// <remarks>
    /// <para>作用：</para>
    /// <para>1.隐藏报表数据与分析列之间的复杂的映射关系.</para>
    /// <para>2.对外提供简易的接口,获取指定的分析列在报表中指定行与列的数据.</para>
    /// <para>3.对外提供简易的接口,获取报表的列摘要的值</para>
    /// </remarks>
    /// </summary>
    public class ReportDataRetriever
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ReportDataRetriever()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 数据
        /// </summary>
        internal DataTable Dataes { get; set; }

        /// <summary>
        /// 表格列与分析列的映射关系
        /// </summary>
        internal Dictionary<Column, AnalysisColumn> GridColumnToAnalysisColumnMappings { get; set; }

        /// <summary>
        /// 列的摘要的值
        /// </summary>
        internal Dictionary<Column, Dictionary<ISummary, string>> ColumnSummaryValues { get; set; }
        #endregion

        #region 获取指定分析列的报表数据
        /// <summary>
        /// 获取指定分析列的报表数据
        /// </summary>
        /// <typeparam name="T">报表列数据的类型</typeparam>
        /// <param name="pTargetColumn">分析列</param>
        /// <returns>
        /// <para>如果列为度量列,则因为度量列会因为维度列的打横而拆分为多个,因此会返回多列数据,其中</para>
        /// <para>1.键为列对应的Ext的表格列</para>
        /// <para>2.值为该列下的所有数据</para>
        /// </returns>
        public Dictionary<Column, T[]> GetColumnDataBy<T>(AnalysisColumn pTargetColumn)
        {
            //获取分析列所对应的表格列
            Column[] gridColumns = this.GetGridColumnBy(pTargetColumn);
            if (gridColumns!=null && gridColumns.Length>0)
            {
                //获取数据
                Dictionary<Column, T[]> result = new Dictionary<Column, T[]>();
                foreach (var col in gridColumns)
                {
                    List<T> dataes = new List<T>();
                    foreach (DataRow dr in this.Dataes.Rows)
                    {
                        if (dr[col.DataIndex] != DBNull.Value)
                        {
                            var temp = Convert.ChangeType(dr[col.DataIndex], typeof(T));
                            if (temp == null)
                                dataes.Add(default(T));
                            else
                                dataes.Add((T)temp);
                        }
                        else
                        {
                            dataes.Add(default(T));
                        }
                    }
                    //
                    result.Add(col, dataes.ToArray());
                }
                //返回结果
                return result;
            }
            return null;
        }
        #endregion

        #region 获取指定分析列中某一行的数据
        /// <summary>
        /// 获取指定分析列中某一行的数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="pTargetColumn">分析列</param>
        /// <param name="pRowIndex">行号,以0开始</param>
        /// <returns></returns>
        public T[] GetRowDataBy<T>(AnalysisColumn pTargetColumn, int pRowIndex)
        {
            var gridColumns = this.GetGridColumnBy(pTargetColumn);
            if (gridColumns != null && gridColumns.Length > 0
                && this.Dataes!=null && this.Dataes.Rows.Count>pRowIndex)
            {
                List<T> result = new List<T>();
                //
                var dr = this.Dataes.Rows[pRowIndex];
                foreach (var col in gridColumns)
                {
                    if (dr[col.DataIndex] != DBNull.Value)
                    {
                        var temp = Convert.ChangeType(dr[col.DataIndex], typeof(T));
                        if (temp == null)
                            result.Add(default(T));
                        else
                            result.Add((T)temp);
                    }
                    else
                    {
                        result.Add(default(T));
                    }
                }
                //
                return result.ToArray();
            }
            //
            return null;
        }
        #endregion

        #region 获取指定分析列的报表摘要
        /// <summary>
        /// 获取指定分析列的报表摘要
        /// </summary>
        /// <param name="pTargetColumn">分析列</param>
        /// <returns></returns>
        public Dictionary<Column, Dictionary<ISummary, string>> GetSummaryBy(AnalysisColumn pTargetColumn)
        {
            var gridColumns = this.GetGridColumnBy(pTargetColumn);
            if (gridColumns != null && gridColumns.Length > 0
                && this.ColumnSummaryValues!=null)
            {
                var items =this.ColumnSummaryValues.Where(item => gridColumns.Contains(item.Key)).ToArray();
                var result = new Dictionary<Column, Dictionary<ISummary, string>>();
                foreach (var item in items)
                {
                    result.Add(item.Key, item.Value);
                }
                //
                return result;
            }
            return null;
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 根据分析列找出所对应的表格列
        /// </summary>
        /// <param name="pTargetColumn"></param>
        /// <returns></returns>
        private Column[] GetGridColumnBy(AnalysisColumn pTargetColumn)
        {
            if (pTargetColumn != null
                   && this.GridColumnToAnalysisColumnMappings != null
                   && this.GridColumnToAnalysisColumnMappings.ContainsValue(pTargetColumn))
            {
                List<Column> gridColumns = new List<Column>();
                foreach (var item in this.GridColumnToAnalysisColumnMappings)
                {
                    if (item.Value == pTargetColumn)
                        gridColumns.Add(item.Key);
                }
                return gridColumns.ToArray();
            }
            //
            return null;
        }
        #endregion
    }
}
