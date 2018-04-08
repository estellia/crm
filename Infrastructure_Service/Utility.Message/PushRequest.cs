using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Message.Baidu;
using JIT.Utility.Message.Baidu.ValueObject;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Message
{
    public class PushRequest
    {
        public PushRequest()
        {

        }
        /// <summary>
        /// 百度推送用PushMsgRequestBuilder创建对象取JSON
        /// IOS推送用IOSNotificationBuilder创建对象取JSON
        /// </summary>
        public string Request { get; set; }
        /// <summary>
        /// 1-立即发送，其它存库轮循
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 平台1-安卓,2-IOS
        /// </summary>
        public int PlatForm { get; set; }

        public int ChannelID { get; set; }

        public string ClientID { get; set; }

        public string AppCode { get; set; }

        public string UserID { get; set; }

        public string GetJson()
        {
            switch (PlatForm)
            {
                case 1:
                    return this.Request.DeserializeJSONTo<PushMsgRequest>().InnerDictionary.ToJSON();
                case 2:
                    return this.Request;
                default:
                    throw new Exception("错误的平台类型");
            }
        }
    }
}