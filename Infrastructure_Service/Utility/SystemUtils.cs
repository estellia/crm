/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 14:29:41
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
using System.Diagnostics;
using System.Reflection;
using System.Text;

using JIT.Utility.ExtensionMethod;

namespace JIT.Utility
{
    /// <summary>
    /// 系统的工具类 
    /// </summary>
    public static class SystemUtils
    {
        #region 跟踪堆栈
        /// <summary>
        /// 在获取堆栈信息时，需要剔除的方法的全名
        /// </summary>
        private static readonly string[] SKIPPED_METHOD_FULL_NAMES = new string[] { 
            "JIT.Utility.SystemUtils.GetCurrentStackTraces"
            ,"JIT.Utility.SystemUtils.GetStackTracesFrom"
            ,"JIT.Utility.Log.DatabaseLogInfo..ctor"
            ,"JIT.Utility.Log.DebugLogInfo..ctor"
            ,"JIT.Utility.Log.ExceptionLogInfo..ctor"
        };

        /// <summary>
        /// 从系统的跟踪堆栈中获取堆栈信息
        /// </summary>
        /// <param name="pStackTrace"></param>
        /// <param name="pJumpOutTypes"></param>
        /// <returns></returns>
        private static StackTraceInfo[] createStackTraceInfos(StackTrace pStackTrace)
        {
            List<StackTraceInfo> result = new List<StackTraceInfo>();
            //
            StackTrace st = pStackTrace;
            var frames = st.GetFrames();
            //
            var startIndex = findSelfCodeBoundary(frames);
            for (int i = 0; i <= startIndex; i++)
            {
                var frame = frames[i];
                var method = frame.GetMethod();
                //组织跟踪堆栈信息
                StackTraceInfo info = new StackTraceInfo(method);
                info.LineNumber = frame.GetFileLineNumber();
                info.ClassFileLocation = frame.GetFileName();
                //检查该方法是否需要剔除
                if (isSkip(info))
                    continue;
                //
                result.Add(info);
            }
            //
            return result.ToArray();
        }

        /// <summary>
        /// 找到自有代码的边界
        /// </summary>
        /// <param name="pStackFrames"></param>
        /// <returns></returns>
        private static int findSelfCodeBoundary(StackFrame[] pStackFrames)
        {
            if (pStackFrames != null && pStackFrames.Length > 0)
            {
                for (int i = pStackFrames.Length - 1; i >= 0; i--)
                {
                    var classType = pStackFrames[i].GetMethod().ReflectedType;
                    if (classType != null)
                    {
                        var className = classType.FullName;
                        if (className.StartsWith("JIT."))
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 是否要剔除？
        /// </summary>
        /// <param name="pStackTraceInfo"></param>
        /// <returns></returns>
        private static bool isSkip(StackTraceInfo pStackTraceInfo)
        {
            if (pStackTraceInfo != null)
            {
                var methodFullName  =pStackTraceInfo.GetFullMethodName();
                foreach (var item in SKIPPED_METHOD_FULL_NAMES)
                {
                    if (methodFullName == item)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取当前的堆栈信息
        /// </summary>
        /// <param name="pJumpOutTypes">需要跳出的类型</param>
        /// <returns></returns>
        public static StackTraceInfo[] GetCurrentStackTraces()
        {
            var st = new StackTrace(true);
            return createStackTraceInfos(st);
        }
        /// <summary>
        /// 从异常信息中获取堆栈信息
        /// </summary>
        /// <param name="pException"></param>
        /// <returns></returns>
        public static StackTraceInfo[] GetStackTracesFrom(Exception pException)
        {
            var st = new StackTrace(pException,true);
            return createStackTraceInfos(st);
        }
        #endregion
    }
}
