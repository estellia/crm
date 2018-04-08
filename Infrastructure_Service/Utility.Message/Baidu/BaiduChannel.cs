/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/2 17:49:21
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

namespace JIT.Utility.Message.Baidu
{
    /// <summary>
    /// Baidu的消息推送渠道
    /// </summary>
    public class BaiduChannel:BaseChannel
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaiduChannel()
        {
        }
        #endregion

        /// <summary>
        /// 百度云推送的接口URL
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 百度云推送的访问令牌
        /// </summary>
        public string APIKey
        {
            get { return this.Settings.GetParam<string>("apikey"); }
            set { this.Settings.SetParam("apikey",value);}
        }
        /// <summary>
        /// 百度云推送的密钥
        /// </summary>
        public string SecretKey
        {
            get { return this.Settings.GetParam<string>("secret_key"); }
            set { this.Settings.SetParam("secret_key", value); }
        }

    }
}
