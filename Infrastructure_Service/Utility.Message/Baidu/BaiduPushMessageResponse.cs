/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/3 10:06:45
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

namespace JIT.Utility.Message.Baidu
{
    /// <summary>
    /// Baidu消息推送的响应
    /// </summary>
    public class BaiduPushMessageResponse
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaiduPushMessageResponse()
        {
        }
        #endregion

        /// <summary>
        /// 请求ID
        /// </summary>
        [JsonProperty(PropertyName="request_id")]
        public string RequestID { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        [JsonProperty(PropertyName = "response_params")]
        public Dictionary<string, object> ResponseParams { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty(PropertyName = "error_code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty(PropertyName = "error_msg")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonIgnore]
        public bool IsSuccess
        {
            get 
            {
                if (string.IsNullOrWhiteSpace(this.ErrorCode))
                    return true;
                else
                    return false;
            }
        }
    }
}
