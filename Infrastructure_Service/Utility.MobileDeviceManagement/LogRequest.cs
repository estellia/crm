using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MobileDeviceManagement
{
    public class LogRequest
    {
        /// <summary>
        /// 应用编码，唯一标识公司一个应用
        /// </summary>
        public string AppCode { get; set; }
        /// <summary>
        /// 手机版本号
        /// </summary>
        public string AppVersion { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 日志类型:0-异常  1-crash
        /// </summary>
        public int LogType { get; set; }
        /// <summary>
        /// 日志的Tag
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 日志产生时间
        /// </summary>
        public string LogTime { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Message { get; set; }

        public DateTime GetLogTime()
        {
            return DateTime.ParseExact(this.LogTime, "yyyy-MM-dd HH:mm:ss fff", null);
        }
    }
}
