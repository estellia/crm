/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/7 17:59:55
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

using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Locale;
using JIT.Utility.Report.Analysis;
using JIT.Utility.Web.ComponentModel.ExtJS.Data;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;

namespace JIT.Utility.Report
{
    /// <summary>
    /// 在内存中进行分组计算的分析报表的基类
    /// <remarks>
    /// <para>度量计算是放在内存中计算的分析报表。该报表的优点在于，数据一次性加载进来，之后的报表操作全部在内存中运算，无须在访问数据库</para>
    /// </remarks>
    /// </summary>
    /// <typeparam name="T">T是最明细数据的实体类,该类实现接口IFactData</typeparam>
    public abstract class BaseMemoryAnalysisReport<T>:BaseAnalysisReport where T:IFactData
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaseMemoryAnalysisReport()
        {
        }
        #endregion

        #region 属性集

        #region 私有&受保护属性
        /// <summary>
        /// 最底层的数据
        /// </summary>
        protected T[] DetailDataes { get; set; }
        #endregion

        #endregion

        #region BaseAnalysisReport抽象成员的实现

        /// <summary>
        /// 执行分组计算
        /// <remarks>
        /// <para>分组计算的主要工作内容有：</para>
        /// <para>1.读取事实数据.</para>
        /// <para>2.对事实数据进行分组,并依次进行度量的计算</para>
        /// </remarks>
        /// </summary>
        /// <param name="pWheres">报表数据的过滤条件</param>
        /// <param name="pOrderBys">报表数据的排序条件</param>
        /// <returns>分组聚合后的数据集,数据集中的所有列的列名为对应的事实数据列的ColumnID</returns>
        protected override DataTable ProcessGroupingCalculation(IWhereCondition[] pWheres, OrderBy[] pOrderBys)
        {
            //根据缓存策略判断是否读取缓存还是重新查询数据库
            this.DetailDataes = this.GetDatas(pWheres, pOrderBys);
            if(this.DetailDataes ==null)
                this.DetailDataes=new T[0];
            //
            return this.ProcessGroupingInMemory(this.DetailDataes);
        }

        /// <summary>
        /// 执行钻取后的分组计算
        /// </summary>
        /// <param name="pWheres">报表数据的过滤条件</param>
        /// <param name="pOrderBys">报表数据的排序条件</param>
        /// <param name="pDrilledColumn">用户选择钻入的维度列</param>
        /// <param name="pDimValue">用户选择钻入的维度项的值</param>
        /// <returns>分组聚合后的数据</returns>
        protected override DataTable ProcessGroupingCalculationByDrilled(IWhereCondition[] pWheres, OrderBy[] pOrderBys, DimColumn pDrilledColumn, string pDimValue, IWhereCondition[] pBringFromDrilling)
        {
            //根据缓存策略判断是否读取缓存还是重新查询数据库
            var dataes = this.DetailDataes.Where(item =>
            {
                var temp = item.GetData(pDrilledColumn.DataColumn.PropertyName);
                var val = string.Empty;
                if (temp != null)
                    val = temp.ToString();
                return val == pDimValue;
            }).ToArray();
            //
            return this.ProcessGroupingInMemory(dataes);
        }

        /// <summary>
        /// 报表的类型
        /// </summary>
        public override AnalysisReportTypes ReportType 
        { 
            get { return AnalysisReportTypes.MemoryBased; } 
        }
        #endregion

        #region 工具方法

        #region 根据分析列创建分组聚合后的DataTable
        /// <summary>
        /// 根据分析列创建分组聚合后的DataTable
        /// </summary>
        /// <param name="pColumns">所有的列</param>
        private DataTable CreateGroupedResultBy(AnalysisColumnList pColumns)
        {
            DataTable dt = new DataTable();
            if (pColumns != null)
            {
                foreach (var column in pColumns)
                {
                    dt.Columns.Add(column.ColumnID, column.GridColumnType.GetDotNetType());
                    if (column.ColumnType == AnalysisColumnTypes.Dim)    //维度列多增加一列文本
                    {
                        var temp = column as DimColumn;
                        dt.Columns.Add(temp.GetTextColumnID(), typeof(string));
                    }
                }
            }
            //
            return dt;
        }
        #endregion

        #region 设置值
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="pRow">数据行</param>
        /// <param name="pGridColumn">需要设置数据的表格列</param>
        /// <param name="pValue">值</param>
        private void SetDataRowValue(DataRow pRow, AnalysisColumn pGridColumn, object pValue)
        {
            pRow[pGridColumn.ColumnID] = pValue;
        }
        #endregion

        #region 获取值
        /// <summary>
        /// 获取指定表格列的指定行的数据
        /// </summary>
        /// <param name="pRowIndex"></param>
        /// <param name="pGridColumn"></param>
        /// <returns></returns>
        private object GetDataRowValue(DataTable pDataes, int pRowIndex, AnalysisColumn pGridColumn)
        {
            return pDataes.Rows[pRowIndex][pGridColumn.ColumnID];
        }
        #endregion

        #region 执行分组计算
        /// <summary>
        /// 执行分组计算
        /// </summary>
        private DataTable ProcessGroupingInMemory(T[] pDetailDataes)
        {
            var dt = this.CreateGroupedResultBy(this.GetCurrentColumns());
            //获取报表表格定义中的维度列和度量列
            var dims = this.GetCurrentColumns().ActiveDims;
            var measures = this.GetCurrentColumns().MemoryMeasures;
            //组织分组条件
            List<Func<T, object>> groupings = new List<Func<T, object>>();
            foreach (var dim in dims)
            {
                var propertyName = dim.DataColumn.PropertyName;
                groupings.Add(item => item.GetData(propertyName));
            }
            //对数据进行分组并获取分组明细
            var grouped = pDetailDataes.GroupByMany(groupings.ToArray()).ToArray();
            var groupDetails = this.GetGroupingDetails<IFactData>(grouped);
            //根据分组明细进行计算
            if (groupDetails != null)
            {
                foreach (var r in groupDetails)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                    //将维度值写进结果行中
                    for (int i = 0, j = dims.Length - 1; i < dims.Length; i++, j--)
                    {
                        this.SetDataRowValue(dr, dims[i], r.Keys[j]);//获取分组明细是是采用递归的方式，因此键值的顺序正好相反
                    }
                    //将度量值写进结果行中
                    if (measures != null && measures.Length > 0)
                    {
                        foreach (var m in measures)
                        {
                            this.SetDataRowValue(dr, m, m.Calclulator(r.Details == null ? null : r.Details.ToArray()));
                        }
                    }
                }
            }
            //
            return dt;
        }
        #endregion

        #endregion

        #region 抽象成员

        #region 获取最明细的事实数据
        /// <summary>
        /// 获取最明细的事实数据
        /// <param name="pWhereConditions">数据的过滤条件</param>
        /// <param name="pOrders">数据的排序条件</param>
        /// </summary>
        protected abstract T[] GetDatas(IWhereCondition[] pWhereConditions, OrderBy[] pOrders);
        #endregion

        #endregion
    }
}
