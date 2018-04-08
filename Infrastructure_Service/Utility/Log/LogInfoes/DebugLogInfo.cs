/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 16:18:50
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
    /// 调试日志
    /// </summary>
    [Serializable]
    public class DebugLogInfo:BaseLogInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DebugLogInfo()
        {
            this.StackTrances = SystemUtils.GetCurrentStackTraces();
            this.Location = this.TryGetLocationFromStackTrace();
        }
        #endregion

        /// <summary>
        /// 调试信息
        /// </summary>
        public string Message { get; set; }
    }
}
