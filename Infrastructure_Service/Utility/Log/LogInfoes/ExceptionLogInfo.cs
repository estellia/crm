/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 16:48:32
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
using System.Linq;
using System.Text;

using JIT.Utility;

namespace JIT.Utility.Log
{
    /// <summary>
    /// 异常日志信息 
    /// </summary>
    [Serializable]
    public class ExceptionLogInfo:BaseLogInfo
    {
        #region 构造函数
        /// <summary>
        /// 异常日志信息 
        /// </summary>
        /// <param name="pClientID">客户ID</param>
        /// <param name="pUserID">用户ID</param>
        /// <param name="pException">异常信息</param>
        public ExceptionLogInfo(string pClientID, string pUserID, Exception pException)
        {
            this.ClientID = pClientID;
            this.UserID = pUserID;
            this.Init(pException);
        }
        /// <summary>
        /// 异常日志信息
        /// </summary>
        /// <param name="pUserInfo">当前用户信息</param>
        /// <param name="pException">异常信息</param>
        public ExceptionLogInfo(BasicUserInfo pUserInfo, Exception pException)
        {
            if (pUserInfo != null)
            {
                this.ClientID = pUserInfo.ClientID;
                this.UserID = pUserInfo.UserID;
            }
            this.Init(pException);
        }
        /// <summary>
        /// 异常日志信息
        /// </summary>
        /// <param name="pException">异常信息</param>
        public ExceptionLogInfo(Exception pException)
        {
            this.Init(pException);
        }

        public ExceptionLogInfo()
        { 

        }
        #endregion

        #region 属性集
        /// <summary>
        /// 是否JIT自定义的异常
        /// </summary>
        public bool IsJITException { get; set; }

        /// <summary>
        /// 异常是否被最外层的框架代码捕获
        /// </summary>
        public bool IsCatchedByOuterFrameworkCode { get; set; }

        /// <summary>
        /// 最近20条执行的T-SQL语句
        /// </summary>
        public TSQL[] Last20ExecutedTSQLs { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="pException"></param>
        protected void Init(Exception pException)
        {
            if (pException != null)
            {
                //自定义异常
                if (pException is JITException)
                {
                    this.IsJITException = true;
                    var temp = pException as JITException;
                }
                else
                {
                    this.IsJITException = false;
                }
                this.ErrorMessage = pException.Message;
                //跟踪堆栈
                this.StackTrances = SystemUtils.GetStackTracesFrom(pException);
                if (this.StackTrances != null && this.StackTrances.Length > 0)
                {
                    var thrownLocation = this.StackTrances[this.StackTrances.Length - 1];
                    this.IsCatchedByOuterFrameworkCode = thrownLocation.IsOuterFrameworkCode;
                    this.Location = thrownLocation.GetFullMethodName();
                    //
                    var tSQLs = SystemRuntimeInfo.GetInstace().GetTSQLBy(this.ClientID, this.UserID);
                    if (tSQLs != null && tSQLs.Length > 20)
                    {
                        tSQLs = tSQLs.Take(20).ToArray();
                    }
                    this.Last20ExecutedTSQLs = tSQLs;
                }
            }
        }
        #endregion
    }
}
