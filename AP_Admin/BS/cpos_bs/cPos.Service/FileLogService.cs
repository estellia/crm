using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace cPos.Service
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

    public class FileLogService : BaseService
    {
        //根据配置数据,确定是否需要记录日志
        private Boolean needLog = false;
        private String filePath = "";
        private LogLevel configLogLevel = LogLevel.DEBUG;

        public FileLogService()
        {
            //判断是否需要记录日志
            string s = ConfigurationManager.AppSettings["need_log"];
            this.needLog = (!String.IsNullOrEmpty(s) && s.Equals("1"));
            if (this.needLog)
            {
                s = ConfigurationManager.AppSettings["log_level"];
                if (string.IsNullOrEmpty(s))
                {
                    this.needLog = false;
                    return;
                }
                this.configLogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), s.ToUpper());

                this.filePath = ConfigurationManager.AppSettings["log_path"];
                if (string.IsNullOrEmpty(this.filePath))
                {
                    this.needLog = false;
                    return;
                }
                else
                {
                    Directory.CreateDirectory(this.filePath);
                }
            }
        }

        public void Log(LogLevel level, string systemName, String moduleName, String functionName, String messageType, string message)
        {
            if (!this.needLog) return;
            if (level < configLogLevel) return;

            string logFile = string.Format("{0}//{1}.log", this.filePath, DateTime.Today.ToString("yyyyMMdd"));
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(logFile, true, Encoding.GetEncoding("gb2312"));
                sw.WriteLine(string.Format("{0} [{1}] System: {2}, Module: {3}, Function: {4}, Type: {5}, Message:{6}",
                    this.GetDateTime(DateTime.Now), level.ToString(), systemName, moduleName, functionName, messageType, message));
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}
