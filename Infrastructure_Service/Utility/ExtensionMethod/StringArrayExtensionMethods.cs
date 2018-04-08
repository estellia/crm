/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/4 11:16:23
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

namespace JIT.Utility.ExtensionMethod
{
    /// <summary>
    /// string[]的扩展方法 
    /// </summary>
    public static class StringArrayExtensionMethods
    {
        /// <summary>
        /// 扩展方法：将数组中的字符串通过分隔符串联起来
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <param name="pSpliter">分隔符</param>
        /// <param name="pWrapper">包装符,如果不为null,则数组中每个元素都用包装符在前后包装下.</param>
        /// <returns></returns>
        public static string ToJoinString(this string[] pCaller, char pSpliter, char? pWrapper = null)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            //
            StringBuilder result = new StringBuilder();
            if (pCaller.Length > 0)
            {
                for (var i = 0; i < pCaller.Length; i++)
                {
                    if (i != 0)
                        result.Append(pSpliter);
                    if (pWrapper.HasValue)
                        result.AppendFormat("{1}{0}{1}", pCaller[i], pWrapper.Value);
                    else
                        result.Append(pCaller[i]);
                }
            }
            //
            return result.ToString();
        }

        /// <summary>
        /// 扩展方法：将数组中的字符串通过分隔符串联起来
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <param name="pSpliter">分隔符</param>
        /// <param name="pWrapper">包装符,如果不为null,则数组中每个元素都用包装符在前后包装下.</param>
        /// <returns></returns>
        public static string ToJoinString(this string[] pCaller, string pSpliter, string pWrapper = null)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            //
            StringBuilder result = new StringBuilder();
            var hasWrapper = (!string.IsNullOrEmpty(pWrapper));
            if (pCaller.Length > 0)
            {
                for (var i = 0; i < pCaller.Length; i++)
                {
                    if (i != 0)
                        result.Append(pSpliter);
                    if (hasWrapper)
                        result.AppendFormat("{1}{0}{1}", pCaller[i], pWrapper);
                    else
                        result.Append(pCaller[i]);
                }
            }
            //
            return result.ToString();
        }

        /// <summary>
        /// 将字符串数组转换成In后面的条件
        /// </summary>
        /// <param name="pCaller">字符串数组</param>
        /// <returns>不带括号的In查询条件</returns>
        public static string ToSqlInString(this string[] pCaller)
        {
            string conditionIds = string.Empty;
            foreach (var conditionIdItem in pCaller)
            {
                conditionIds += "'" + conditionIdItem.Replace("'", "''") +"',";
            }
            if (conditionIds.Length > 0)
                conditionIds = conditionIds.Substring(0, conditionIds.Length - 1);
            return conditionIds;
        }
    }
}
