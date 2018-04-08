/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/3 12:49:23
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
    /// 百度云推送的通知对象结构
    /// </summary>
    public class BaiduPushNotification
    {
        /// <summary>
        /// 通知标题，可以为空；如果为空则设为appid对应的应用名;
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// 通知文本内容，不能为空;
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// android客户端自定义通知样式，如果没有设置默认为0;
        /// </summary>
        [JsonProperty(PropertyName = "notification_builder_id")]
        public int NotificationBuilderID { get; set; }

        /// <summary>
        /// 只有notification_builder_id为0时才有效，才需要设置，
        /// 如果notification_builder_id为0则可以设置通知的基本样式包括(响铃：0x04;振动：0x02;可清除：0x01;)
        /// ,这是一个flag整形，每一位代表一种样式;
        /// </summary>
        [JsonProperty(PropertyName = "notification_basic_style")]
        public int NotificationBasicStyle { get; set; }

        /// <summary>
        /// 点击通知后的行为(打开Url：1; 自定义行为：2：其它值则默认打开应用;);
        /// </summary>
        [JsonProperty(PropertyName = "open_type")]
        public int OpenType { get; set; }

        /// <summary>
        /// 只有open_type为1时才有效，才需要设置，如果open_type为1则可以设置需要打开的Url地址;
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string URL { get; set; }

        /// <summary>
        /// 只有open_type为1时才有效，才需要设置,(需要请求用户授权：1；默认直接打开：0), 如果open_type为1则可以设置打开的Url地址时是否请求用户授权;
        /// </summary>
        [JsonProperty(PropertyName = "user_confirm")]
        public int UserConfirm { get; set; }

        /// <summary>
        /// 只有open_type为2时才有效，才需要设置, 如果open_type为2则可以设置自定义打开行为(具体参考管理控制台文档);
        /// </summary>
        [JsonProperty(PropertyName = "pkg_content")]
        public string PackageContent { get; set; }

        /// <summary>
        /// 自定义内容，键值对，Json对象形式(可选)；在android客户端，这些键值对将以Intent中的extra进行传递。
        /// </summary>
        [JsonProperty(PropertyName = "custom_content")]
        public string CustomContent { get; set; }
    }
}
