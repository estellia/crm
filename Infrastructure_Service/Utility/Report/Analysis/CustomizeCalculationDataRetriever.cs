/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/10 13:09:02
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

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 计算自定义计算列时的报表数据的获取器 
    /// </summary>
    public class CustomizeCalculationDataRetriever
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CustomizeCalculationDataRetriever(DataTable pDataes,Dictionary<string,AnalysisColumn> pColumnMappings)
        {
            this.Dataes = pDataes;
            this.ColumnMappings = pColumnMappings;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 数据
        /// </summary>
        protected DataTable Dataes { get; set; }

        /// <summary>
        /// 报表列与数据集中列的映射关系
        /// </summary>
        protected Dictionary<string, AnalysisColumn> ColumnMappings { get; set; }
        #endregion

        #region 获取指定列的值
        /// <summary>
        /// 获取指定列的值
        /// <remarks>
        /// <para>1.如果列是度量列而且进行了行列转换,则返回的值会有多个.</para>
        /// <para>2.如果列是自定义计算列，则为该列值，但如果该列还未计算，则会为null</para>
        /// <para>3.如果列是维度列，则会返回维度列的ID和文本，结果数组中的第一个</para>
        /// <para>4.返回的结果中包含null值项</para>
        /// </remarks>
        /// </summary>
        /// <param name="pRowIndex">行号,以0开始</param>
        /// <param name="pTargetColumn">指定的列</param>
        /// <returns>数据项值</returns>
        public T[] GetData<T>(int pRowIndex, AnalysisColumn pTargetColumn)
        {
            if (this.Dataes != null && this.Dataes.Rows.Count > pRowIndex)
            {
                if (this.ColumnMappings != null && this.ColumnMappings.ContainsValue(pTargetColumn))
                {
                    var q = from m in this.ColumnMappings
                            where m.Value == pTargetColumn
                            select m.Key;
                    var columns = q.ToArray();
                    if (columns != null && columns.Length > 0)
                    {
                        List<T> results = new List<T>();
                        var dr = this.Dataes.Rows[pRowIndex];
                        foreach (var col in columns)
                        {
                            if (dr[col] != DBNull.Value)
                            {
                                var temp = Convert.ChangeType(dr[col], typeof(T));
                                if (temp == null)
                                    results.Add(default(T));
                                else
                                    results.Add((T)temp);
                            }
                            else
                            {
                                results.Add(default(T));
                            }
                        }
                        return results.ToArray();
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
