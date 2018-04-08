using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.MobileDeviceManagement.Base
{
    public class BaseMobileDeviceManagementDAO : BaseDAO<MobileDeviceManagementUserInfo>
    {
        public BaseMobileDeviceManagementDAO(MobileDeviceManagementUserInfo pUserInfo, bool pIsLoggingTSQL, ISQLHelper pSQLHelper)
            : base(pUserInfo, new DirectConnectionStringManager(null))
        {
            this.SQLHelper = pSQLHelper;
            if (pIsLoggingTSQL)
            {
                //给数据访问助手挂载执行完毕事件，以记录所有执行的SQL
                this.SQLHelper.OnExecuted += new EventHandler<SqlCommandExecutionEventArgs>(SQLHelper_OnExecuted);
            }
        }

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
