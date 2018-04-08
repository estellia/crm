/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/8 18:35:29
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
using System.ComponentModel;
using System.Text;

namespace JIT.Utility.Web.ComponentModel.ExtJS
{
    /// <summary>
    /// Ext JS的组件隐藏的模式 
    /// </summary>
    public enum HideModes
    {
        /// <summary>
        /// 通过设置display: none样式来控制组件的显示与否
        /// </summary>
        [Description("display")]
        Display
        ,
        /// <summary>
        /// 通过设置visibility: hidden 样式来控制组件的显示与否
        /// </summary>
        [Description("visibility")]
        Visibility
        ,
        /// <summary>
        /// 通过元素的绝对定位将元素移除到可视区域来控制组件的显示与否
        /// </summary>
        [Description("offsets")]
        Offset
    }
}
