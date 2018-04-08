/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/12 9:55:47
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
    /// Ext.layout.container.Container 
    /// </summary>
    public class Container:ExtJSClass
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Container()
        {
            this.ClassFullName = "Ext.layout.container.Container";
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 布局的类型
        /// </summary>
        public string Type
        {
            get { return this.GetInitConfigValue<string>("type"); }
        }
        #endregion
    }
}
