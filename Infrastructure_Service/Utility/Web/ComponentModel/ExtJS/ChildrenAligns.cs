/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/11 18:23:42
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
    /// 子元素的对齐方式 
    /// </summary>
    public enum ChildrenAligns
    {
        /// <summary>
        /// 子元素纵向从上到下排列
        /// </summary>
        [Description("top")]
        Top
        ,
        /// <summary>
        /// 子元素纵向居中排列
        /// </summary>
        [Description("middle")]
        Middle
        ,
        /// <summary>
        /// 子元素纵向拉伸填充父容器
        /// </summary>
        [Description("stretch")]
        Stretch
        ,
        [Description("stretchmax")]
        StretchMax
    }
}
