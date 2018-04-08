/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/18 11:46:10
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
    /// Ext.menu.CheckItem
    /// </summary>
    public class CheckItem:Item
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CheckItem()
        {
            this.XType = "menucheckitem";
            this.ClassFullName = "Ext.menu.CheckItem";
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 是否已经勾选上
        /// </summary>
        public bool? IsChecked
        {
            get { return this.GetInitConfigValue<bool?>("checked"); }
            set { this.SetInitConfigValue("checked", value); }
        }

        /// <summary>
        /// 勾选变化事件的处理者
        /// </summary>
        public JSFunction CheckHandler
        {
            get { return this.GetInitConfigValue<JSFunction>("checkHandler"); }
            set { this.SetInitConfigValue("checkHandler", value); }
        }
        #endregion
    }
}
