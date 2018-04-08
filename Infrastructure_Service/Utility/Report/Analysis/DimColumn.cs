/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/29 11:41:14
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

using JIT.Utility.ExtensionMethod;
using JIT.Utility.Report.FactData;
using JIT.Utility.Web.ComponentModel.ExtJS;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 维度列
    /// </summary>
    public class DimColumn:AnalysisColumn
    {
        #region 构造函数
        /// <summary>
        /// 分析报表表格的维度列 
        /// </summary>
        public DimColumn()
        {
            this.IsDrillable = true;
            this.IsPivotable = true;
            this.IsPivoted = true;
            this.DrillingType = DrillingTypes.DrillSelf;
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
                    if (value.ColumnType == DataModelColumnTypes.Dim)
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

        #region 行列转换
        /// <summary>
        /// 是否支持行列转换（也就是打横）
        /// <remarks>
        /// <para>1.C=Column,R=Row,CR=行列</para>
        /// </remarks>
        /// </summary>
        public bool IsCRConversionable { get; set; }

        /// <summary>
        /// 是否已经行列转换
        /// </summary>
        public bool IsCRConverted { get; set; }
        #endregion

        #region 表格列
        /// <summary>
        /// 表格列
        /// <remarks>
        /// <para>维度列的不可以设置表格列的列类型，默认该列的列类型为自定义</para>
        /// </remarks>
        /// </summary>
        public override ColumnTypes GridColumnType
        {
            get
            {
                return ColumnTypes.Customize;
            }
            set
            {
                throw new NotSupportedException();
            }
        }
        #endregion

        #region 数据透视
        /// <summary>
        /// 是否支持数据透视
        /// </summary>
        public bool IsPivotable { get; set; }

        /// <summary>
        /// 是否已经在数据透视内了
        /// </summary>
        public bool IsPivoted { get; set; }
        #endregion

        #region 列是否可以钻取
        /// <summary>
        /// 列是否可以钻取
        /// </summary>
        public bool IsDrillable { get; set; }
        #endregion

        #region 1.01版本新增
        /// <summary>
        /// 钻取的方式
        /// <remarks>
        /// <para>默认为带入自身</para>
        /// </remarks>
        /// </summary>
        public DrillingTypes DrillingType { get; set; }

        /// <summary>
        /// 当钻取的方式为自定义时,此属性生效,此属性用于指定钻取时需要带入的维度
        /// <remarks>
        /// <para>注意：如果指定的维度在当前钻取剖面不可用于带入作为筛选条件,则会忽略此维度列</para>
        /// </remarks>
        /// </summary>
        public List<DimColumn> BringDimsToFilter { get; set; }
        #endregion

        #endregion

        #region 获取维度列的文本列的ID
        private string _textColumnID = string.Empty;
        /// <summary>
        /// 获取维度列的文本列的ID
        /// </summary>
        /// <returns></returns>
        public string GetTextColumnID()
        {
            if (string.IsNullOrEmpty(this._textColumnID))
            {
                this._textColumnID = Guid.NewGuid().ToText();
            }
            return this._textColumnID;
        }
        #endregion

        #region 实现AnalysisColumn的抽象成员
        /// <summary>
        /// 获得分析列的列类型
        /// </summary>
        public override AnalysisColumnTypes ColumnType
        {
            get { return AnalysisColumnTypes.Dim; }
        }
        #endregion
    }
}
