/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/7 12:56:06
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
using System.Text;

using Newtonsoft.Json;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column
{
    /// <summary>
    /// Jit.grid.column.Column 
    /// </summary>
    public class Column:ExtJSComponent
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Column()
        {
            this.ClassFullName = "Jit.grid.column.Column";
            this.XType = "jitcolumn";
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 列的类型
        /// </summary>
        public ColumnTypes ColumnType
        {
            get { return this.GetInitConfigValue<ColumnTypes>("jitDataType"); }
            set { this.SetInitConfigValue("jitDataType", value); }
        }

        /// <summary>
        /// 如果列类型为百分比,则表示百分比值的小数位
        /// </summary>
        public int? Accuracy
        {
            get { return this.GetInitConfigValue<int?>("accuracy"); }
            set { this.SetInitConfigValue("accuracy", value); }
        }

        /// <summary>
        /// 列标题
        /// </summary>
        public string ColumnTitle
        {
            get { return this.GetInitConfigValue<string>("text"); }
            set { this.SetInitConfigValue("text", value); }
        }

        /// <summary>
        /// 是否为自动伸缩
        /// </summary>
        public bool? IsFlex
        {
            get { return this.GetInitConfigValue<bool?>("flex"); }
            set { this.SetInitConfigValue("flex", value); }
        }

        /// <summary>
        /// 是否可排序
        /// </summary>
        public bool? IsSortable
        {
            get { return this.GetInitConfigValue<bool?>("sortable"); }
            set { this.SetInitConfigValue("sortable", value); }
        }

        /// <summary>
        /// 列所关联的数据项
        /// </summary>
        public string DataIndex
        {
            get { return this.GetInitConfigValue<string>("dataIndex"); }
            set { this.SetInitConfigValue("dataIndex", value); }
        }

        /// <summary>
        /// 显示顺序
        /// <remarks>
        /// <para>1.非Ext JS配置项</para>
        /// </remarks>
        /// </summary>
        public int? DisplayOrder { get; set; }

        /// <summary>
        /// 子列
        /// </summary>
        public List<Column> Children
        {
            get { return this.GetInitConfigValue<List<Column>>("columns"); }
            set { this.SetInitConfigValue("columns", value); }
        }

        /// <summary>
        /// 摘要呈现器
        /// </summary>
        public IJavascriptObject SummaryRenderer
        {
            get { return this.GetInitConfigValue<IJavascriptObject>("summaryRenderer"); }
            set { this.SetInitConfigValue("summaryRenderer", value); }
        }

        /// <summary>
        /// 列呈现器
        /// </summary>
        public IJavascriptObject Renderer
        {
            get { return this.GetInitConfigValue<IJavascriptObject>("renderer"); }
            set 
            {
                if (value != null)
                {
                    if (value is JSFunction)
                    {
                        ((JSFunction)value).Type = JSFunctionTypes.Variable;
                    }
                }
                this.SetInitConfigValue("renderer", value); 
            }
        }
        #endregion

        #region 根据ID查找相应的列（递归查找子列）
        /// <summary>
        /// 根据ID查找相应的列（递归查找子列）
        /// </summary>
        /// <param name="pID">列ID</param>
        /// <returns></returns>
        public Column FindByID(string pID)
        {
            //如果id为空或null，直接返回null
            if (string.IsNullOrEmpty(pID))
                return null;
            //查找自身
            if (this.ID == pID)
                return this;
            //递归查找子列
            if (this.Children != null)
            {
                foreach (var child in this.Children)
                {
                    var found= child.FindByID(pID);
                    if (found != null)
                        return found;
                }
            }
            //如果都未找到，则是没有，返回null
            return null;
        }
        #endregion

        #region 类成员

        #region 查找列集合中的第一列（递归查找子列）
        /// <summary>
        /// 查找列集合中的第一列（递归查找子列）
        /// </summary>
        /// <param name="pColumns">列集合</param>
        /// <returns></returns>
        public static Column FindFirstVisiableColumn(IEnumerable<Column> pColumns)
        {
            if (pColumns == null)
                return null;
            var firstCol = pColumns.Where(item=>item.IsHidden.HasValue ==false|| item.IsHidden.Value == false).OrderBy(item => item.DisplayOrder).FirstOrDefault();
            if (firstCol == null)
                return null;
            if (firstCol.Children != null)
                return Column.FindFirstVisiableColumn(firstCol.Children);
            else
                return firstCol;
        }
        #endregion

        #endregion
    }
}
