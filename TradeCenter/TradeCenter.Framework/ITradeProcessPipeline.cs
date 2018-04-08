/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/1/2 10:38:30
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


namespace JIT.TradeCenter.Framework
{
    /// <summary>
    /// 交易处理模块接口
    /// </summary>
    public interface ITradeProcessPipeline
    {
        /// <summary>
        /// 处理交易请求
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        TradeResponse Process(TradeRequest pRequest);

        /// <summary>
        /// 在处理管道中添加处理模块
        /// </summary>
        /// <param name="pModule"></param>
        void AddModule(ITradeProcessModule pModule);

        /// <summary>
        /// 从处理管道中移除处理模块
        /// </summary>
        /// <param name="pModuleName"></param>
        void RemoveModule(string pModuleName);
    }
}
