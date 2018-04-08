/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/1/2 11:01:54
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

namespace JIT.TradeCenter.Framework.ValueObject
{
    /// <summary>
    /// 交易处理模块类别 
    /// </summary>
    public enum ModuleTypes
    {
        /// <summary>
        /// 交易处理执行之前
        /// </summary>
        BeforeProcessTrade=0
        ,
        /// <summary>
        /// 交易处理执行之后
        /// </summary>
        AfterProcessTrade
    }
}
