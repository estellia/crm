/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/20 14:14:36
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

namespace JIT.Utility.Pay.UnionPay.Interface.Wap.ValueObject
{
    /// <summary>
    /// Wap支付中的交易类型 
    /// </summary>
    public enum WapTransTypes
    {
        /// <summary>
        /// 消费
        /// </summary>
        Consumption
        ,
        /// <summary>
        /// 消费撤销
        /// </summary>
        ConsumptionRevoked
        ,
        /// <summary>
        /// 预授权
        /// </summary>
        PreAuthorization
        ,
        /// <summary>
        /// 预授权完成
        /// </summary>
        PreAuthorizationCompleted
        ,
        /// <summary>
        /// 预授权撤销
        /// </summary>
        PreAuthorizationRevoked
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
    }

    /// <summary>
    /// TransTypes的扩展方法
    /// </summary>
    public static class TransTypesExtensionMethods
    {
        /// <summary>
        /// 扩展方法：获取交易类型编码
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string GetCode(this WapTransTypes pCaller)
        {
            switch (pCaller)
            {
                case WapTransTypes.Consumption:
                    return "01";
                case WapTransTypes.PreAuthorization:
                    return "02";
                case WapTransTypes.PreAuthorizationCompleted:
                    return "03";
                case WapTransTypes.ReturnOfGoods:
                    return "04";
                case WapTransTypes.ConsumptionRevoked:
                    return "31";
                case WapTransTypes.PreAuthorizationRevoked:
                    return "32";
                case WapTransTypes.PreAuthorizationCompletedRevoked:
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
        public static WapTransTypes? ParseTransTypes(this string pCaller)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            switch (pCaller)
            {
                case "01":
                    return WapTransTypes.Consumption;
                case "02":
                    return WapTransTypes.PreAuthorization;
                case "03":
                    return WapTransTypes.PreAuthorizationCompleted;
                case "04":
                    return WapTransTypes.ReturnOfGoods;
                case "31":
                    return WapTransTypes.ConsumptionRevoked;
                case "32":
                    return WapTransTypes.PreAuthorizationRevoked;
                case "33":
                    return WapTransTypes.PreAuthorizationCompletedRevoked;
                default:
                    throw new ArgumentException(string.Format("找不到编码为[{0}]的交易类型.",pCaller));
            }
        }
    }
}
