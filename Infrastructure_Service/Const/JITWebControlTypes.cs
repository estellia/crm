/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/1/21 11:54:00
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

namespace JIT.Const
{
    /// <summary>
    /// JIT的页面控件类型 
    /// </summary>
    public enum JITWebControlTypes
    {
        /// <summary>
        /// 整数控件
        /// </summary>
        Int=1
        ,
        /// <summary>
        /// 浮点数控件
        /// </summary>
        Float=2
        ,
        /// <summary>
        /// 文本框控件
        /// </summary>
        Text=3
        ,
        /// <summary>
        /// 多行文本框控件
        /// </summary>
        MultiLineText=4
        ,
        /// <summary>
        /// 日期控件
        /// </summary>
        Date =5
        ,
        /// <summary>
        /// 单选下拉列表控件
        /// </summary>
        SingleSelectionComboBox=6
        ,
        /// <summary>
        /// 多选下拉列表控件
        /// </summary>
        MultiSelectionComboBox=7
    }
}
