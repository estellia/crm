/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/11 18:34:27
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

namespace JIT.Utility.Web.ComponentModel.ExtJS.Layout.Container
{
    /// <summary>
    /// Ext.layout.container.Table 
    /// </summary>
    public class Table : Container
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Table()
        {
            this.ClassFullName = "Ext.layout.container.Table";
            //
            this.SetInitConfigValue("type", "table");
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 表格布局的列数
        /// </summary>
        public int? Columns
        {
            get { return this.GetInitConfigValue<int?>("columns"); }
            set { this.SetInitConfigValue("columns", value); }
        }
        #endregion
    }
}
