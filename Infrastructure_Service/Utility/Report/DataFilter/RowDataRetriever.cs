/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/9/10 16:31:47
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
using System.Linq;
using System.Data;
using System.Text;

using JIT.Utility.Report.Analysis;

namespace JIT.Utility.Report.DataFilter
{
    /// <summary>
    /// 行数据获取器 
    /// </summary>
    public class RowDataRetriever
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public RowDataRetriever(DataRow pDataRow, Dictionary<string, AnalysisColumn> pColumnMappings)
        {
            this.Dataes = pDataRow;
            this.ColumnMappings = pColumnMappings;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 数据
        /// </summary>
        protected DataRow Dataes { get; set; }

        /// <summary>
        /// 报表列与数据集中列的映射关系
        /// </summary>
        protected Dictionary<string, AnalysisColumn> ColumnMappings { get; set; }
        #endregion

        #region 获取指定列的值
        /// <summary>
        /// 获取指定列的值
        /// </summary>
        /// <param name="pTargetColumn">指定的列</param>
        /// <returns>数据项值</returns>
        public T[] GetData<T>(AnalysisColumn pTargetColumn)
        {
            if (this.Dataes != null)
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
                        foreach (var col in columns)
                        {
                            if (this.Dataes[col] != DBNull.Value)
                            {
                                var temp = Convert.ChangeType(this.Dataes[col], typeof(T));
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
