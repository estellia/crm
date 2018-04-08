/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/11 15:06:24
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
    /// 供内部使用的向微信支付平台发送预订单请求的请求信息 
    /// </summary>
    public class InnerPreOrderRequestInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public InnerPreOrderRequestInfo()
        {
            this.SignMethod = "sha1";
        }
        #endregion

        /// <summary>
        /// 签名计算方法
        /// </summary>
        [JsonProperty("sign_method")]
        public string SignMethod { get;private set; }

        /// <summary>
        /// 时间戳,该时间戳为系统当前时间的UTC格式的时间与1970-1-1之间的秒数
        /// </summary>
        [JsonProperty("timestamp")]
        public int TimeStamp { get; set; }

        /// <summary>
        /// 订单包裹信息
        /// </summary>
        [JsonProperty("package")]
        public string Package { get; set; }

        /// <summary>
        /// 第三方用户唯一凭证
        /// </summary>
        [JsonProperty("appid")]
        public string AppID { get; set; }

        /// <summary>
        /// 预订单签名
        /// </summary>
        [JsonProperty("app_signature")]
        public string Sign { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonProperty("traceid")]
        public string UserID { get; set; }

        /// <summary>
        /// 防止重复请求的不重复标识符
        /// </summary>
        [JsonProperty("noncestr")]
        public string NonceStr { get; set; }
    }
}
