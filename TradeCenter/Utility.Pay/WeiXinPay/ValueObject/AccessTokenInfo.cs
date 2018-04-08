/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/7 16:42:39
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
    /// 访问凭证信息 
    /// </summary>
    public class AccessTokenInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AccessTokenInfo()
        {
        }
        #endregion

        /// <summary>
        /// 凭证
        /// </summary>
        [JsonProperty(PropertyName = "access_token")]
        public string Token { get; set; }

        /// <summary>
        /// 过期
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public int Expires { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty(PropertyName = "errmsg")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get 
            {
                return string.IsNullOrWhiteSpace(this.ErrorCode) && (!string.IsNullOrWhiteSpace(this.Token));
            }
        }
    }
}
