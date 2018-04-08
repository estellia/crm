/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/7 17:29:26
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

namespace JIT.Utility.Pay.WeiXinPay.Interface
{
    /// <summary>
    /// 微信支付通道 
    /// </summary>
    public class WeiXinPayChannel
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WeiXinPayChannel()
        {
        }
        #endregion

        /// <summary>
        /// 第三方用户唯一凭证
        /// </summary>
        public string AppID { get; set; }

        /// <summary>
        /// 第三方用户唯一凭证密钥
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string ParnterID { get; set; }

        /// <summary>
        /// 商户号的键
        /// </summary>
        public string ParnterKey { get; set; }

        /// <summary>
        /// 支付签名的密钥
        /// </summary>
        public string PaySignKey { get; set; }

        /// <summary>
        /// 通知到交易中心的URL，长度不超过255个字符
        /// </summary>
        public string NotifyToTradeCenterURL { get; set; }

        /// <summary>
        /// 通知到业务系统的URL
        /// </summary>
        public string NotifyToBussinessSystemURL { get; set; }
    }
}
