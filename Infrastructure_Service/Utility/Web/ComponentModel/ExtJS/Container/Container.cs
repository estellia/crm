/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/12 10:50:38
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

using JIT.Utility.Web.ComponentModel.ExtJS.Layout.Container;

namespace JIT.Utility.Web.ComponentModel.ExtJS.Container
{
    /// <summary>
    /// Ext.container.Container 
    /// </summary>
    public abstract class Container:ExtJSComponent
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Container()
        {
            this.ClassFullName = "Ext.container.Container";
            this.XType = "container";
            this.DefaultXType = "panel";
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 子元素的默认xtype
        /// </summary>
        public string DefaultXType { get; set; }

        /// <summary>
        /// 子元素的布局
        /// </summary>
        public JIT.Utility.Web.ComponentModel.ExtJS.Layout.Container.Container Layout
        {
            get { return this.GetInitConfigValue<JIT.Utility.Web.ComponentModel.ExtJS.Layout.Container.Container>("layout"); }
            set { this.SetInitConfigValue("layout", value); }
        }

        /// <summary>
        /// 子元素集合
        /// </summary>
        public List<ExtJSComponent> Items
        {
            get { return this.GetInitConfigValue<List<ExtJSComponent>>("items"); }
            set 
            {
                if (value != null)
                {
                    foreach (var item in value)
                    {
                        if (string.IsNullOrEmpty(item.XType))
                        {
                            item.XType = this.DefaultXType;
                        }
                    }
                }
                this.SetInitConfigValue("items", value); 
            }
        }
        #endregion
    }
}
