/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/18 10:18:42
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

namespace JIT.Utility.Web.ComponentModel.ExtJS.Menu
{
    /// <summary>
    /// Ext.menu.Item 
    /// </summary>
    public class Item:ExtJSComponent
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Item()
        {
            this.ClassFullName = "Ext.menu.Item";
            this.XType = "menuitem";
        }
        #endregion

        #region 属性集

        /// <summary>
        /// 图标的url
        /// </summary>
        public string IconUrl
        {
            get { return this.GetInitConfigValue<string>("icon"); }
            set { this.SetInitConfigValue("icon", value); }
        }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return this.GetInitConfigValue<string>("text"); }
            set { this.SetInitConfigValue("text", value); }
        }
        /// <summary>
        /// 菜单项的点击事件
        /// </summary>
        public JSFunction Handler
        {
            get { return this.GetInitConfigValue<JSFunction>("handler"); }
            set { this.SetInitConfigValue("handler", value); }
        }

        /// <summary>
        /// 菜单(菜单项中如果包含菜单,则他是一个子菜单)
        /// </summary>
        public Menu Menu
        {
            get { return this.GetInitConfigValue<Menu>("menu"); }
            set { this.SetInitConfigValue("menu", value); }
        }
        #endregion
    }
}
