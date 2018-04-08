/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/2 16:25:04
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
using JIT.Utility.Message.Baidu.ValueObject;

namespace JIT.Utility.Message.Baidu
{
    /// <summary>
    /// 推送到Android的消息
    /// </summary>
    public class AndroidMessage:BaseMessage
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AndroidMessage()
        {
            //设备锁定为android
            this.MessageParameters.Add("device_type", ((uint)DeviceTypes.Android).ToString());
            //
            BaiduPushNotification msg = new BaiduPushNotification();
            this.MessageParameters.Add("messages", msg);
        }
        #endregion

        /// <summary>
        /// 推送类型
        /// </summary>
        public PushTypes? PushType
        {
            get
            {
                if (this.MessageParameters.ContainsKey("push_type"))
                {
                    return (PushTypes)this.MessageParameters["push_type"];
                }
                //
                return null;
            }
            set
            {
                if (value == null)
                {
                    this.MessageParameters.Remove("push_type");
                }
                else
                {
                    this.MessageParameters.Add("push_type", value.Value);
                }
            }
        }

        /// <summary>
        /// 推送到的用户在百度下的用户ID
        /// <remarks>
        /// <para>用于推送类型为单播时</para>
        /// </remarks>
        /// </summary>
        public string BaiduUserID
        {
            get
            {
                if (this.MessageParameters.ContainsKey("user_id"))
                {
                    return this.MessageParameters["user_id"] as string;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.MessageParameters.Remove("user_id");
                }
                else
                {
                    this.MessageParameters.Add("user_id", value);
                }
            }
        }

        /// <summary>
        /// 消息的标签
        /// <remarks>
        /// <para>用于推送类型为标签式多播</para>
        /// </remarks>
        /// </summary>
        public string Tag
        {
            get
            {
                if (this.MessageParameters.ContainsKey("tag"))
                {
                    return this.MessageParameters["tag"] as string;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.MessageParameters.Remove("tag");
                }
                else
                {
                    this.MessageParameters.Add("tag", value);
                }
            }
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageTypes? MessageType
        {
            get
            {
                if (this.MessageParameters.ContainsKey("message_type"))
                {
                    return (MessageTypes)this.MessageParameters["message_type"];
                }
                //
                return null;
            }
            set
            {
                if (value == null)
                {
                    this.MessageParameters.Remove("message_type");
                }
                else
                {
                    this.MessageParameters.Add("message_type", value.Value);
                }
            }
        }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string MessageTitle
        {
            get
            {
                var msg = this.MessageParameters["messages"] as BaiduPushNotification;
                return msg.Title;
            }
            set
            {
                var msg = this.MessageParameters["messages"] as BaiduPushNotification;
                msg.Title = value;
            }
        }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string MessageDescription
        {
            get
            {
                var msg = this.MessageParameters["messages"] as BaiduPushNotification;
                return msg.Description;
            }
            set
            {
                var msg = this.MessageParameters["messages"] as BaiduPushNotification;
                msg.Description = value;
            }
        }

        /// <summary>
        /// 消息标识。
        /// <para>
        /// 指定消息标识，必须和messages一一对应。相同消息标识的消息会自动覆盖。特别提醒：该功能只支持android、browser、pc三种设备类型。
        /// </para>
        /// </summary>
        public string MessageKey
        {
            get
            {
                if (this.MessageParameters.ContainsKey("msg_keys"))
                {
                    return this.MessageParameters["msg_keys"] as string;
                }
                return null;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.MessageParameters.Remove("msg_keys");
                }
                else
                {
                    this.MessageParameters.Add("msg_keys", value);
                }
            }
        }

        /// <summary>
        /// 指定消息的过期时间，默认为86400秒。必须和messages一一对应。
        /// </summary>
        public uint? MessageExpires
        {
            get
            {
                if (this.MessageParameters.ContainsKey("message_expires"))
                {
                    return (uint)this.MessageParameters["message_expires"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value == null)
                {
                    this.MessageParameters.Remove("message_expires");
                }
                else
                {
                    this.MessageParameters.Add("message_expires", value.Value);
                }
            }
        }

        /// <summary>
        /// 部署状态。指定应用当前的部署状态
        /// <remarks>
        /// 若不指定，则默认设置为生产状态。特别提醒：该功能只支持ios设备类型。
        /// </remarks>
        /// </summary>
        public DeployStatuses? DeployStatus
        {
            get
            {
                if (this.MessageParameters.ContainsKey("deploy_status"))
                {
                    return (DeployStatuses)this.MessageParameters["deploy_status"];
                }
                else
                {
                    return null;
                }
            }
            set 
            {
                if (value == null)
                {
                    this.MessageParameters.Remove("deploy_status");
                }
                else
                {
                    this.MessageParameters.Add("deploy_status", value.Value);
                }
            }
        }

        
    }
}
