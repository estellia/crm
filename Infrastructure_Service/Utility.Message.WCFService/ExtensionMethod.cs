using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using JIT.Utility.Message.Baidu;
using JIT.Utility.Message.ValueObject;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Message;
using JIT.Utility.Message.WCF.Entity;
using JIT.Utility.Message.IOS.ValueObject;

namespace JIT.Utility.Message.WCFService
{
    public static class ExtensionMethod
    {
        public static BaiduChannel GetBaiDuChannel(this MessageChannelEntity pChannel)
        {
            if (pChannel.MobilePlatform == 2)
                throw new Exception("不能创建IOSChannel,方法选择错误");
            BaiduChannel channel = new Message.Baidu.BaiduChannel();
            var settings = pChannel.Settings.DeserializeJSONTo<Dictionary<string, object>>();
            ParameterDictionary dictionary = new ParameterDictionary();
            dictionary.InnerDictionary = settings;
            channel.Settings = dictionary;
            channel.URL = ConfigurationManager.AppSettings["URL"];
            return channel;
        }

        public static IOSChannel GetIOSChannel(this MessageChannelEntity pChannel)
        {
            if (pChannel.MobilePlatform == 1)
                throw new Exception("不能创建BaiduChannel,方法选择错误");
            IOSChannel channel = pChannel.Settings.DeserializeJSONTo<IOSChannel>();
            ParameterDictionary dictionary = new ParameterDictionary();
            return channel;
        }

        public static PushMsgRequest GetRequest(this MessageEntity pMessage)
        {
            PushMsgRequest request = new Message.Baidu.PushMsgRequest();
            var args = pMessage.MessageParameters.DeserializeJSONTo<Dictionary<string, object>>();
            request.InnerDictionary = args;
            return request;
        }

        public static JdSoft.Apple.Apns.Notifications.Notification GetIOSNotification(this MessageEntity pMessage)
        {
            var notification = pMessage.MessageParameters.DeserializeJSONTo<JdSoft.Apple.Apns.Notifications.Notification>();
            return notification;
        }

    }
}