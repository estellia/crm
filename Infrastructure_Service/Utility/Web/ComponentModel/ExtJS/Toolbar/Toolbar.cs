/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/11 17:44:29
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
using JIT.Utility.Web.ComponentModel.ExtJS.Container;

namespace JIT.Utility.Web.ComponentModel.ExtJS.Toolbar
{
    /// <summary>
    /// Ext.toolbar.Toolbar 
    /// </summary>
    public class Toolbar:Container.Container
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Toolbar()
        {
            this.ClassFullName = "Ext.toolbar.Toolbar";
            this.XType = "toolbar";
            this.DefaultXType = "button";
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 子元素
        /// </summary>
        public List<ExtJSComponent> Items
        {
            get { return this.GetInitConfigValue<List<ExtJSComponent>>("items"); }
            set { this.SetInitConfigValue("items", value); }
        }
        #endregion
    }
}
