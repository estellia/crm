/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/3 11:27:21
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

using JIT.Utility.ExtensionMethod;
using JIT.Utility.Message.Baidu.ValueObject;

namespace JIT.Utility.Message.Baidu
{
    /// <summary>
    /// 百度云推送的push_msg方法的请求 
    /// </summary>
    public class PushMsgRequest : BaiduPushMessageRequest
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PushMsgRequest()
        {
            this.BaiduMessageKey = Guid.NewGuid().ToText();
            base.Method = "push_msg";
        }
        #endregion

        /// <summary>
        /// API的资源操作方法名
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public override string Method
        {
            get
            {
                return base.Method;
            }
            set
            {
                base.Method = value;
            }
        }

        /// <summary>
        /// 用户标识，在Android里，channel_id + userid指定某一个特定client。不超过256字节，如果存在此字段，则只推送给此用户。
        /// <remarks>
        /// <para>非必填参数</para>
        /// </remarks>
        /// </summary>
        public string BaiduUserID
        {
            get { return this.GetParam<string>("user_id"); }
            set { this.SetParam("user_id", value); }
        }

        /// <summary>
        /// 百度云推送的通道标识
        /// <remarks>
        /// <para>非必填参数</para>
        /// </remarks>
        /// </summary>
        public string BaiduChannelID
        {
            get { return this.GetParam<string>("channel_id"); }
            set { this.SetParam("channel_id", value); }
        }

        /// <summary>
        /// 推送类型
        /// <remarks>
        /// <para>推送类型分为：</para>
        /// <para>1.单播</para>
        /// <para>2.标签式多播</para>
        /// <para>3.广播式</para>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public PushTypes? PushType
        {
            get { return this.GetParam<PushTypes?>("push_type"); }
            set { this.SetParam("push_type", value); }
        }

        /// <summary>
        /// 标签名称，不超过128字节
        /// <remarks>
        /// <para>非必填参数</para>
        /// </remarks>
        /// </summary>
        public string Tag
        {
            get { return this.GetParam<string>("tag"); }
            set { this.SetParam("tag", value); }
        }

        /// <summary>
        /// 设备类型
        /// <remarks>
        /// <para>非必填参数</para>
        /// </remarks>
        /// </summary>
        public DeviceTypes? DeviceType
        {
            get
            {
                var strVal = this.GetParam<string>("device_type");
                if (!string.IsNullOrWhiteSpace(strVal))
                {
                    DeviceTypes temp;
                    if (Enum.TryParse<DeviceTypes>(strVal, out temp))
                    {
                        return temp;
                    }
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    string strVal = ((uint)value.Value).ToString();
                    this.SetParam("device_type", strVal);
                }
                else
                {
                    this.SetParam("device_type", null);
                }
            }
        }

        /// <summary>
        /// 消息类型
        /// <remarks>
        /// <para>非必填参数</para>
        /// <para>默认为Message</para>
        /// </remarks>
        /// </summary>
        public MessageTypes? MessageType
        {
            get
            {
                var temp = this.InnerDictionary["message_type"];
                if (temp != null)
                {
                    switch (temp.ToString())
                    {
                        case "Message":
                            return MessageTypes.Message;
                        case "Notification":
                            return MessageTypes.Notification;
                    }
                }
                return MessageTypes.Notification;
            }
            set
            {
                this.SetParam("message_type", value);
            }
        }

        /// <summary>
        /// 指定的推送的通知的内容
        /// <remarks>
        /// <para>此时消息类型必须是通知</para>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public BaiduPushNotification Notification
        {
            get
            {
                switch (MessageType)
                {
                    case MessageTypes.Notification:
                        return this.GetParam<BaiduPushNotification>("messages");
                    default:
                        return null;
                }
            }
            set
            {
                switch (MessageType)
                {
                    case MessageTypes.Notification:
                        this.SetParam("messages", value);
                        break;
                }
            }
        }

        /// <summary>
        /// 推送的消息的内容
        /// </summary>
        public string Message
        {
            get
            {
                switch (MessageType)
                {
                    case MessageTypes.Message:
                        return this.GetParam<string>("messages");
                    case MessageTypes.Notification:
                        return this.GetParam<BaiduPushNotification>("messages").ToJSON();
                    default:
                        return null;
                }
            }
            set
            {
                switch (MessageType)
                {
                    case MessageTypes.Message:
                        this.SetParam("messages", value);
                        break;
                    case MessageTypes.Notification:
                        this.SetParam("messages", value.DeserializeJSONTo<BaiduPushNotification>());
                        break;
                }
            }
        }

        /// <summary>
        /// 消息标识。
        /// 指定消息标识，必须和messages一一对应。相同消息标识的消息会自动覆盖。特别提醒：该功能只支持android、browser、pc三种设备类型。
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string BaiduMessageKey
        {
            get { return this.GetParam<string>("msg_keys"); }
            set { this.SetParam("msg_keys", value); }
        }

        /// <summary>
        /// 指定消息的过期时间，默认为86400秒。必须和messages一一对应。
        /// <remarks>
        /// <para>非必填参数</para>
        /// </remarks>
        /// </summary>
        public uint? MessageExpires
        {
            get { return this.GetParam<uint?>("message_expires"); }
            set { this.SetParam("message_expires", value); }
        }

        /// <summary>
        /// 部署状态。指定应用当前的部署状态
        /// 若不指定，则默认设置为生产状态。特别提醒：该功能只支持ios设备类型。
        /// <remarks>
        /// <para>非必填参数</para>
        /// </remarks>
        /// </summary>
        public DeployStatuses? DeployStatus
        {
            get { return this.GetParam<DeployStatuses?>("deploy_status"); }
            set { this.SetParam("message_expires", value); }
        }
    }
}
