/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/12 11:31:31
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

namespace JIT.Utility.Web.ComponentModel.ExtJS.Form
{
    /// <summary>
    /// Ext.form.Label 
    /// </summary>
    public class Label:ExtJSComponent
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Label()
        {
            this.ClassFullName = "Ext.form.Label";
            this.XType = "label";
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
