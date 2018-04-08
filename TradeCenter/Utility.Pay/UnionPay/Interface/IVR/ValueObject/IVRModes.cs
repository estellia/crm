/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/26 13:25:23
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

namespace JIT.Utility.Pay.UnionPay.Interface.IVR.ValueObject
{
    /// <summary>
    /// 语音支付的模式 
    /// </summary>
    public enum IVRModes
    {
        /// <summary>
        /// 回呼
        /// </summary>
        Callback
        ,
        /// <summary>
        /// 一线通
        /// </summary>
        CallDirectly
    }

    /// <summary>
    /// IVRModes的扩展方法类
    /// </summary>
    public static class IVRModesExtensionMethods
    {
        /// <summary>
        /// 扩展方法：获取语音支付模式的编码
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string GetCode(this IVRModes pCaller)
        {
            switch (pCaller)
            {
                case IVRModes.Callback:
                    return "02";
                case IVRModes.CallDirectly:
                    return "03";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
