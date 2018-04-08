/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 13:02:39
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
using System.IO;
using System.Text;

using log4net;
using log4net.Config;

using JIT.Utility;

namespace JIT.Utility.Log
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    public static class Loggers
    {
        #region 类构造函数
        static Loggers()
        {
            //初始化日志的配置
            string diskLogConfig = AppDomain.CurrentDomain.BaseDirectory + "\\" + "Config\\loggers.xml";
            if (!System.IO.File.Exists(diskLogConfig))
                diskLogConfig = AppDomain.CurrentDomain.BaseDirectory + "\\" + "Config\\logger.xml";
            if (System.IO.File.Exists(diskLogConfig))
            {//自定义的配置文件                
                XmlConfigurator.Configure(new FileInfo(diskLogConfig)); 
            }
            else
            {//内置的配置文件
                var config = ReflectionUtils.GetEmbeddedResource("JIT.Utility.Log.loggers.xml");
                XmlConfigurator.Configure(config);
            }
            //初始化默认的日志记录器
            DEFAULT = new DefaultLogger();
            //DEFAULT.Debug(new DebugLogInfo() { ClientID = "-1", UserID = "-1", Message = "【diskLogConfig】:[" + diskLogConfig + "]" });
        }
        #endregion

        #region 类属性集
        /// <summary>
        /// 默认的日志记录器
        /// </summary>
        public static IJITLogger DEFAULT { get; private set; }
        #endregion

        #region 工具方法

        #region 记录调试信息
        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="pLogInfo">调试信息</param>
        public static void Debug(DebugLogInfo pLogInfo)
        {
            //暂时去掉Debug日志功能,按需要打开
#if DEBUG
            Loggers.DEFAULT.Debug(pLogInfo);
#endif
        }
        #endregion

        #region 记录数据库执行的信息
        /// <summary>
        /// 记录数据库执行的信息
        /// </summary>
        /// <param name="pLogInfo">数据库执行的信息</param>
        public static void Database(DatabaseLogInfo pLogInfo)
        {
#if DEBUG
            Loggers.DEFAULT.Database(pLogInfo);
#endif
        }
        #endregion

        #region 记录异常信息
        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="pLogInfo">异常信息</param>
        public static void Exception(ExceptionLogInfo pLogInfo)
        {
            Loggers.DEFAULT.Exception(pLogInfo);
        }
        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="pUserInfo">当前的用户信息</param>
        /// <param name="pException">异常</param>
        public static void Exception(BasicUserInfo pUserInfo, Exception pException)
        {
            var info = new ExceptionLogInfo(pUserInfo, pException);
            Loggers.DEFAULT.Exception(info);
        }
        #endregion

        #endregion
    }
}
