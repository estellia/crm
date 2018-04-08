using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cPos.Admin.Service.Interfaces;
using cPos.Admin.Service.Implements;

namespace cPos.Admin.Service
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class LogManager
    {
        private static ILogService logService = null;

        static LogManager()
        {
            logService = new FileLogService();
        }

        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="systemName">系统名称</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="functionName">方法名称</param>
        /// <param name="messageType">信息类型</param>
        /// <param name="message">信息</param>
        public static void Log(LogLevel level, string systemName, String moduleName, String functionName, String messageType, string message)
        {
            logService.Log(level, systemName, moduleName, functionName, messageType, message);
        }
    }
}
