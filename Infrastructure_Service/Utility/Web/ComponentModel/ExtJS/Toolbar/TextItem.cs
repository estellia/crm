/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/12 10:46:06
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

namespace JIT.Utility.Web.ComponentModel.ExtJS.Toolbar
{
    /// <summary>
    /// Ext.toolbar.TextItem 
    /// </summary>
    public class TextItem:ExtJSComponent
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TextItem()
        {
            this.ClassFullName = "Ext.toolbar.TextItem";
            this.XType = "tbtext";
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return this.GetInitConfigValue<string>("text"); }
            set { this.SetInitConfigValue("text", value); }
        }
        #endregion
    }
}
