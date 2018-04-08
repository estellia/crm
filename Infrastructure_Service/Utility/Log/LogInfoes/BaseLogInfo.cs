/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 16:19:40
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

using JIT.Utility;

namespace JIT.Utility.Log
{
    /// <summary>
    /// 日志信息基类 
    /// </summary>
    [Serializable]
    public abstract class BaseLogInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaseLogInfo()
        {
        }
        #endregion

        /// <summary>
        /// 客户ID
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 日志代码触发的代码位置
        /// </summary>
        public string Location { get; protected set; }

        /// <summary>
        /// 堆栈信息
        /// </summary>
        public StackTraceInfo[] StackTrances { get; protected set; }

        /// <summary>
        /// 从跟踪堆栈中获取触发代码位置
        /// </summary>
        /// <returns></returns>
        protected virtual string TryGetLocationFromStackTrace()
        {
            if (this.StackTrances != null && this.StackTrances.Length > 0)
            {
                var location = this.StackTrances[this.StackTrances.Length - 1];
                return location.GetFullMethodName();
            }
            return string.Empty;
        }
    }
}
