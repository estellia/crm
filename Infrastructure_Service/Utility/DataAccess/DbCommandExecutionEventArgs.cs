/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/14 14:52:11
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;

namespace JIT.Utility.DataAccess
{
    /// <summary>
    /// SqlCommand执行的事件参数 
    /// </summary>
    public abstract class DbCommandExecutionEventArgs:System.EventArgs
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DbCommandExecutionEventArgs()
        {
        }
        #endregion

        /// <summary>
        /// 用户信息
        /// </summary>
        public BasicUserInfo UserInfo { get; set; }

        /// <summary>
        /// 被执行的SQL命令
        /// </summary>
        public IDbCommand Command { get; set; }

        /// <summary>
        /// SQl命令的执行时间
        /// </summary>
        public TimeSpan ExecutionTime { get; set; }
    }
}
