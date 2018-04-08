/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 13:57:28
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

namespace JIT.Utility.Log
{
    /// <summary>
    /// JIT日志记录器接口 
    /// </summary>
    public interface IJITLogger
    {
        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="pLogInfo">日志信息</param>
        void Debug(DebugLogInfo pLogInfo);

        /// <summary>
        /// 记录对数据库的操作信息
        /// </summary>
        /// <param name="pLogInfo">日志信息</param>
        void Database(DatabaseLogInfo pLogInfo);

        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="pLogInfo">日志信息</param>
        void Exception(ExceptionLogInfo pLogInfo);
    }
}
