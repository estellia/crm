/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/24 16:52:48
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
    /// String的扩展方法
    /// </summary>
    public static class StringExtensionMethods
    {
        public static DateTime ParseAPIDateTime(this string pCaller)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            return DateTime.ParseExact(pCaller, "yyyyMMddHHmmss",null);
        }
    }
}
