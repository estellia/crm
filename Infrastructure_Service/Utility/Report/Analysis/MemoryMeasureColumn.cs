/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/29 14:13:46
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

using JIT.Utility.Report.FactData;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 内存度量列,度量的计算是放在内存中计算的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MemoryMeasureColumn:AnalysisColumn
    {
        #region 构造函数
        /// <summary>
        /// 分析报表表格的度量列 
        /// </summary>
        public MemoryMeasureColumn()
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
                if (value != null)
                {
                    if (value.ColumnType == DataModelColumnTypes.Fact)
                    {
                        this._dataColumn = value;
                    }
                    else
                    {
                        throw new ArgumentException("数据列的列类型与报表表格列不匹配.数据列为维度列,则报表表格列必须是维度列;数据列为事实列,则报表表格列必须为度量列.", "DataColumn");
                    }
                }
                else
                {
                    this._dataColumn = null;
                }
            }
        }
        #endregion

        #region 度量值计算器
        /// <summary>
        /// 度量值计算器
        /// <remarks>
        /// <para>计算器的方法声明：</para>
        /// <para>第一个参数：需要进行度量值计算的分组数据数组.</para>
        /// <para>返回值：计算后的结果,计算的结果必须是double型.</para>
        /// </remarks>
        /// </summary>
        public Func<IFactData[],double> Calclulator ;
        #endregion

        #endregion

        #region 实现AnalysisColumn的抽象成员
        /// <summary>
        /// 获得分析列的列类型
        /// </summary>
        public override AnalysisColumnTypes ColumnType
        {
            get { return AnalysisColumnTypes.MemoryMeasure; }
        }
        #endregion
    }
}
