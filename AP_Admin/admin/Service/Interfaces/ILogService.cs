using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Service.Interfaces
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// DEBUG信息
        /// </summary>
        DEBUG,
        /// <summary>
        /// 显示信息
        /// </summary>
        INFO,
        /// <summary>
        /// 警告信息
        /// </summary>
        WARNING,
        /// <summary>
        /// 错误信息
        /// </summary>
        ERROR,
        /// <summary>
        /// 严重错误信息
        /// </summary>
        FATAL
    }

    public interface ILogService
    {
        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="systemName">系统名称</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="functionName">方法名称</param>
        /// <param name="messageType">信息类型</param>
        /// <param name="message">信息</param>
        void Log(LogLevel level, string systemName, String moduleName, String functionName, String messageType, string message);
    }
}
