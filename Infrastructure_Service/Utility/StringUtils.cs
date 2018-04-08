/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/9 17:14:28
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

namespace JIT.Utility
{
    /// <summary>
    /// 字符串处理工具类 
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// 防止SQL注入的值字符串处理
        /// </summary>
        /// <param name="pS1"></param>
        /// <returns></returns>
        public static string SqlReplace(string pS1)
        {
            if (string.IsNullOrEmpty(pS1))
                return pS1;
            return pS1.Replace("'", "''");
        }

        /// <summary>
        /// 使用SQL SERVER中的限定符(一对中括号[])包装SQL SERVER对象名称
        /// </summary>
        /// <param name="pObjectName">SQL SERVER对象名称</param>
        /// <returns></returns>
        public static string WrapperSQLServerObject(string pObjectName)
        {
            //1.参数处理
            if (string.IsNullOrEmpty(pObjectName))
                return pObjectName;
            pObjectName = pObjectName.Trim();
            //
            StringBuilder sb = new StringBuilder();
            var segments = pObjectName.Split('.');
            foreach (var segment in segments)
            {
                if (!string.IsNullOrEmpty(segment))
                {
                    if (!segment.StartsWith("["))
                    {
                        sb.Append("[");
                    }
                    sb.Append(segment);
                    if (!segment.EndsWith("]"))
                    {
                        sb.Append("]");
                    }
                    sb.Append(".");
                }
                else
                {
                    sb.Append(".");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
