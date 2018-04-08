/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/11 15:01:45
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

using Newtonsoft.Json;

namespace JIT.Utility.Pay.WeiXinPay.ValueObject
{
    /// <summary>
    /// 预订单信息 
    /// </summary>
    public class PreOrderInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PreOrderInfo()
        {
        }
        #endregion

        /// <summary>
        /// 第三方用户唯一凭证
        /// </summary>
        public string AppID { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string PartnerID { get; set; }

        /// <summary>
        /// 预订单号
        /// </summary>
        public string PreOrderID { get; set; }

        /// <summary>
        /// 防止重复请求的不重复标识符
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public int TimeStamp { get; set; }

        /// <summary>
        /// 订单的加密签名
        /// </summary>
        public string OrderSign { get; set; }
    }
}
