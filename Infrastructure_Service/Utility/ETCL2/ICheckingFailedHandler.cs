/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/13 11:27:33
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

using JIT.Utility.ETCL2;
using JIT.Utility.ETCL2.ValueObject;

namespace JIT.Utility.ETCL2
{
    /// <summary>
    /// 检查失败时的处理者 
    /// </summary>
    public interface ICheckingFailedHandler
    {
        /// <summary>
        /// 当检查失败时的处理
        /// </summary>
        /// <param name="pDataSource">数据源</param>
        /// <param name="pDestination">目的地</param>
        /// <param name="pCheckResults">检查结果</param>
        /// <param name="pDataItems">转换后的数据项</param>
        void OnError(IDataSource pDataSource, IDestination pDestination, CheckResult[] pCheckResults,IETCLDataItem[] pDataItems);
    }
}
