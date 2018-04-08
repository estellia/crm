/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/26 13:17:09
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
    /// 语音支付中的交易类型 
    /// </summary>
    public enum IVRTransTypes
    {
        /// <summary>
        /// 快捷消费
        /// </summary>
        ExpressConsumption
        ,
        /// <summary>
        /// 消费撤销
        /// </summary>
        ExpressConsumptionRevoked
        ,
        /// <summary>
        /// 预授权
        /// </summary>
        PreAuthorization
        ,
        /// <summary>
        /// 预授权撤销
        /// </summary>
        PreAuthorizationRevoked
        ,
        /// <summary>
        /// 预授权完成
        /// </summary>
        PreAuthorizationCompleted
        ,
        /// <summary>
        /// 预授权完成撤销
        /// </summary>
        PreAuthorizationCompletedRevoked
        ,
        /// <summary>
        /// 退货
        /// </summary>
        ReturnOfGoods
        ,
        /// <summary>
        /// 鉴权
        /// </summary>
        Authentication
    }

    /// <summary>
    /// IVRTransTypes的扩展方法
    /// </summary>
    public static class IVRTransTypesExtensionMethods
    {
        /// <summary>
        /// 扩展方法：获取交易类型的编码
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string GetCode(this IVRTransTypes pCaller)
        {
            switch (pCaller)
            {
                case IVRTransTypes.ExpressConsumption:
                    return "01";
                case IVRTransTypes.PreAuthorization:
                    return "02";
                case IVRTransTypes.PreAuthorizationCompleted:
                    return "03";
                case IVRTransTypes.ReturnOfGoods:
                    return "04";
                case IVRTransTypes.Authentication:
                    return "05";
                case IVRTransTypes.ExpressConsumptionRevoked:
                    return "31";
                case IVRTransTypes.PreAuthorizationRevoked:
                    return "32";
                case IVRTransTypes.PreAuthorizationCompletedRevoked:
                    return "33";
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 扩展方法：根据交易类型编码解析出对应的交易类型
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static IVRTransTypes? ParseTransTypes(this string pCaller)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            switch (pCaller)
            {
                case "01":
                    return IVRTransTypes.ExpressConsumption;
                case "02":
                    return IVRTransTypes.PreAuthorization;
                case "03":
                    return IVRTransTypes.PreAuthorizationCompleted;
                case "04":
                    return IVRTransTypes.ReturnOfGoods;
                case "05":
                    return IVRTransTypes.Authentication;
                case "31":
                    return IVRTransTypes.ExpressConsumptionRevoked;
                case "32":
                    return IVRTransTypes.PreAuthorizationRevoked;
                case "33":
                    return IVRTransTypes.PreAuthorizationCompletedRevoked;
                default:
                    throw new ArgumentException(string.Format("找不到编码为[{0}]的交易类型.", pCaller));
            }
        }
    }
}
