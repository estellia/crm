using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.TradeCenter.BLL.TonysFarmRecharge
{
    public class PrePaidCardUtil
    {
        #region 获取多利充值储值卡地址URL
        /// <summary>
        /// 获取多利充值储值卡地址URL
        /// </summary>
        /// <returns></returns>
        public static string GetTonyRechargeUrl()
        {
            return "https://web.pnrtec.com/portalService/json.action?requestDto=";
        }
        #endregion

        #region 获取商户merchantcode
        /// <summary>
        /// 获取商户merchantcode
        /// </summary>
        /// <returns></returns>
        public static string GetMerchantCode()
        {
            return "900010000000168";
        }
        #endregion

        #region 获取加密key
        /// <summary>
        /// 获取加密key
        /// </summary>
        /// <returns></returns>
        public static string GetEncodingKey()
        {
            return "Dw&7#~d2";
        }
        #endregion
    }
}
