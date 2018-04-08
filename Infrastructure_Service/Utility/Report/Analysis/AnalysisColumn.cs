/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/29 11:31:27
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
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Report.FactData;
using JIT.Utility.Report.Export.DataFormatter;
using JIT.Utility.Report.Export.DataFormatter.Excel;
using JIT.Utility.Web;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 分析列基类
    /// </summary>
    public abstract class AnalysisColumn
    {
        #region 构造函数
        /// <summary>
        /// 分析列基类 
        /// </summary>
        public AnalysisColumn()
        {
            this.ColumnID = Guid.NewGuid().ToText();
            this.DataFormatter = new CommonExcelDataFormatter();
        }
        #endregion

        #region 属性集

        #region 列标识符
        /// <summary>
        /// 列标识符,该标识符为唯一键
        /// </summary>
        public string ColumnID { get; set; }
        #endregion

        #region 列标题
        /// <summary>
        /// 列标题
        /// </summary>
        public virtual string ColumnTitle { get; set; }
        #endregion

        #region 列宽(单位为px)
        /// <summary>
        /// 列宽(单位为px)
        /// </summary>
        public virtual int ColumnWidth { get; set; }
        #endregion

        #region 列显示的顺序
        /// <summary>
        /// 列显示的顺序
        /// </summary>
        public virtual int DisplayOrder { get; set; }
        #endregion

        #region 列数据的排序
        /// <summary>
        /// 列数据的排序
        /// </summary>
        public OrderByDirections? DataOrderBy { get; set; }
        #endregion

        #region 表格列类型
        /// <summary>
        /// 表格列类型
        /// </summary>
        public virtual ColumnTypes GridColumnType { get; set; }
        #endregion

        #region 表格列值的精度
        /// <summary>
        /// 当表格列为百分比列时,用于指定百分比值所要保留的小数位
        /// </summary>
        public virtual int GridColumnValueAccuracy { get; set; }
        #endregion

        #region 表格列的数据格式化器
        /// <summary>
        /// 表格列的数据格式化器
        /// <remarks>
        /// <para>此格式化器用于在将报表数据导出时，服务器端对数据进行格式化,以实现所见即所得的导出</para>
        /// </remarks>
        /// </summary>
        public IDataFormatter DataFormatter { get; set; }
        #endregion

        #region 表格列(Ext JS Grid Column)的呈现器
        /// <summary>
        /// 表格列(Ext JS Grid Column)的呈现器
        /// <remarks>
        /// <para>作用：</para>
        /// <para>1.当指定了表格列的呈现器时,则分析报表中间件则按给定的呈现器呈现表格列,否则则按表格列数据类型的方式进行呈现</para>
        /// <para>2.注意：此属性只对于非维度列才有效，维度列因为要创建钻取连接，采用自身的Renderer.</para>
        /// </remarks>
        /// </summary>
        public IJavascriptObject Renderer { get; set; }
        #endregion

        #region 是否隐藏
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public virtual bool IsHidden { get; set; }
        #endregion

        #endregion

        #region 抽象成员

        /// <summary>
        /// 获得分析列的列类型
        /// </summary>
        public abstract AnalysisColumnTypes ColumnType { get; }
        #endregion
    }
}
