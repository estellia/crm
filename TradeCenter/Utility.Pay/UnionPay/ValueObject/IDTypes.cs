/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/20 14:31:15
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
    /// 证件类型 
    /// </summary>
    public enum IDTypes
    {
        /// <summary>
        /// 身份证
        /// </summary>
        IDCard
        ,
        /// <summary>
        /// 军官证
        /// </summary>
        MilitaryID
        ,
        /// <summary>
        /// 护照
        /// </summary>
        Passport
        ,
        /// <summary>
        /// 回乡证
        /// </summary>
        HVPs
        ,
        /// <summary>
        /// 台胞证
        /// </summary>
        MTPs
        ,
        /// <summary>
        /// 警官证
        /// </summary>
        PoliceOfficerID
        ,
        /// <summary>
        /// 士兵证
        /// </summary>
        CardSoldiers
    }

    /// <summary>
    /// IDTypes的扩展方法
    /// </summary>
    public static class IDTypesExtensionMethods
    {
        /// <summary>
        /// 扩展方法：获得证件类型编码
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string GetCode(this IDTypes pCaller)
        {
            switch (pCaller)
            {
                case IDTypes.IDCard:
                    return "01";
                case IDTypes.MilitaryID:
                    return "02";
                case IDTypes.Passport:
                    return "03";
                case IDTypes.HVPs:
                    return "04";
                case IDTypes.MTPs:
                    return "05";
                case IDTypes.PoliceOfficerID:
                    return "06";
                case IDTypes.CardSoldiers:
                    return "07";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
