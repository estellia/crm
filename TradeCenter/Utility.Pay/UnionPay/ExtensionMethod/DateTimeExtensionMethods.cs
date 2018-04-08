/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/20 15:18:43
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

namespace JIT.Utility.Pay.UnionPay.ExtensionMethod
{
    /// <summary>
    /// DateTime的扩展方法
    /// </summary>
    public static class DateTimeExtensionMethods
    {
        /// <summary>
        /// 扩展方法：将日期值的转换为满足API格式要求的字符串
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string ToAPIFormatString(this DateTime pCaller)
        {
            return pCaller.ToString("yyyyMMddHHmmss");
        }
    }
}
