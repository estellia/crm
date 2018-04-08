/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/20 14:00:05
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

namespace JIT.Utility.Pay.UnionPay.ValueObject
{
    /// <summary>
    /// 币种
    /// </summary>
    public enum Currencys
    {
        /// <summary>
        /// 人民币
        /// </summary>
        RMB=156
    }

    /// <summary>
    /// Currencys的扩展方法
    /// </summary>
    public static class CurrencysExtensionMethods
    {
        /// <summary>
        /// 扩展方法：获取币种编码
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string GetCode(this Currencys pCaller)
        {
            switch (pCaller)
            {
                case Currencys.RMB:
                    return "156";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
