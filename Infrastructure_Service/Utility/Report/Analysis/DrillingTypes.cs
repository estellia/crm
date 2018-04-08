/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/10/23 10:13:51
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

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 钻取的方式 
    /// </summary>
    public enum DrillingTypes
    {
        /// <summary>
        /// 带入自身作为筛选条件
        /// </summary>
        DrillSelf
        ,
        /// <summary>
        /// 带入当前所有可用的维度的值作为筛选条件
        /// </summary>
        DrillAllDim
        ,
        /// <summary>
        /// 自定义,可选择带入作为筛选条件的维度列
        /// </summary>
        Customize
    }
}
