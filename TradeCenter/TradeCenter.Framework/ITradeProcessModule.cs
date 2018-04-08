/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/1/2 10:39:18
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

using JIT.TradeCenter.Framework.ValueObject;

namespace JIT.TradeCenter.Framework
{
    /// <summary>
    /// 交易处理模块接口 
    /// </summary>
    public interface ITradeProcessModule
    {
        /// <summary>
        /// 交易处理模块的类别
        /// </summary>
        ModuleTypes Type { get; set; }

        /// <summary>
        /// 模块名称,唯一标识管道中的模块
        /// </summary>
        string Name { get; set; }
    }
}
