/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/11 18:20:56
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
    /// Ext.layout.container.HBox
    /// </summary>
    public class HBox:Container
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public HBox()
        {
            this.ClassFullName = "Ext.layout.container.HBox";
            //
            this.SetInitConfigValue("type", "hbox");
        }
        #endregion

        #region 属性集

        /// <summary>
        /// 子元素的对其方式
        /// </summary>
        public ChildrenAligns? Align
        {
            get { return this.GetInitConfigValue<ChildrenAligns?>("align"); }
            set { this.SetInitConfigValue("align", value); }
        }
        #endregion
    }
}
