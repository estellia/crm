/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/18 10:16:39
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

namespace JIT.Utility.Web.ComponentModel.ExtJS.Menu
{
    /// <summary>
    /// Ext.menu.Menu 
    /// </summary>
    public class Menu:Container.Container
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Menu()
        {
            this.ClassFullName = "Ext.menu.Menu";
            this.XType = "menu";
        }
        #endregion
    }
}
