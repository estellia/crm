/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/15 10:33:17
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
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess; 
using JIT.Utility.Log;

namespace JIT.Utility.MSTRIntegration.Base
{
    /// <summary>
    /// 管理平台的数据访问基类 
    /// </summary>
    public abstract class BaseReportDAO : BaseDAO<ReportUserInfo>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="pUserInfo">当前的用户信息</param>
        /// <param name="pIsLoggingTSQL">是否将所执行的TSQL语句以日志的形式记录下来</param>
        public BaseReportDAO(ReportUserInfo pUserInfo, bool pIsLoggingTSQL, ISQLHelper pSQLHelper)
            : base(pUserInfo, new DirectConnectionStringManager(null))
        {
            this.SQLHelper = pSQLHelper;
            if (pIsLoggingTSQL)
            {
                //给数据访问助手挂载执行完毕事件，以记录所有执行的SQL
                this.SQLHelper.OnExecuted += new EventHandler<SqlCommandExecutionEventArgs>(SQLHelper_OnExecuted);
            }
        }
        #endregion

        #region 事件处理
        /// <summary>
        /// SQL助手执行完毕后，记录日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SQLHelper_OnExecuted(object sender, SqlCommandExecutionEventArgs e)
        {
            if (e != null)
            {
                var log = new DatabaseLogInfo();
                //获取用户信息
                if (e.UserInfo != null)
                {
                    log.ClientID = e.UserInfo.ClientID;
                    log.UserID = e.UserInfo.UserID;
                }
                //获取T-SQL相关信息
                if (e.Command != null)
                {
                    TSQL tsql = new TSQL();
                    tsql.CommandText = e.Command.GenerateTSQLText();
                    if (e.Command.Connection != null)
                    {
                        tsql.DatabaseName = e.Command.Connection.Database;
                        tsql.ServerName = e.Command.Connection.DataSource;
                    }
                    tsql.ExecutionTime = e.ExecutionTime;
                    log.TSQL = tsql;
                }
                Loggers.DEFAULT.Database(log);
            }
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 从跟踪堆栈中获取第一个数据访问类的调用
        /// </summary>
        /// <param name="pStackTraces"></param>
        protected StackTraceInfo GetFirstDAClassCallFrom(StackTraceInfo[] pStackTraces)
        {
            if (pStackTraces != null)
            {
                foreach (var item in pStackTraces)
                {
                    if (item.Class != "JIT.Utility.MSTRIntegration.BaseReportDAO")
                        return item;
                }
            }
            return null;
        }
        #endregion
    }
}
