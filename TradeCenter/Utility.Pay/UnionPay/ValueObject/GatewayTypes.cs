/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/20 14:18:42
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
    /// 支付网关类型 
    /// </summary>
    public enum GatewayTypes
    {
        /// <summary>
        /// WAP支付
        /// </summary>
        WAP
        ,
        /// <summary>
        /// 语音支付
        /// </summary>
        IVR
    }

    /// <summary>
    /// GatewayTypes的扩展方法
    /// </summary>
    public static class GatewayTypesExtensionMethods
    {
        /// <summary>
        /// 扩展方法：获取支付网关类型编码
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string GetCode(this GatewayTypes pCaller)
        {
            switch (pCaller)
            {
                case GatewayTypes.WAP:
                    return "01";
                case GatewayTypes.IVR:
                    return "03";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
