using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using cPos.Admin.Component;
using cPos.Admin.Model.Dex;

namespace cPos.Admin.Service.Interfaces
{
    public interface IDexLogService
    { 
        /// <summary>
        /// 获取平台列表
        /// </summary>
        /// <returns></returns>
        IList<string> GetAppList();
        
        /// <summary>
        /// 获取日志类型列表
        /// </summary>
        /// <returns></returns>
        IList<string> GetLogTypes();
        
        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="maxRowCount">返回最大行数</param>
        /// <param name="startRowIndex">起始行索引</param>
        /// <returns></returns>
        IList<LogInfo> GetLogs(LoggingSessionInfo loggingSession,
            Hashtable condition, int maxRowCount, int startRowIndex);
        
        /// <summary>
        /// 获取日志列表数量
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        int GetLogsCount(LoggingSessionInfo loggingSession, Hashtable condition);

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>日志对象</returns>
        LogInfo GetLog(LoggingSessionInfo loggingSession, string logId);

    }
}
