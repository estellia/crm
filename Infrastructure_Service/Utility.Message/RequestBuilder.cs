using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Message.Baidu;
using JIT.Utility.Message.Baidu.ValueObject;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Message.IOS;

namespace JIT.Utility.Message
{
    public static class RequestBuilder
    {
        /// <summary>
        /// 创建单播的消息(消息类型=通知)推送请求
        /// </summary>
        /// <param name="pType">类型：1-直接发送，其它放入数据库轮循</param>
        /// <param name="pChannelID">对应MessageChannel表中的ID</param>
        /// <param name="pBaiduUserID">百度用户ID</param>
        /// <param name="pBaiduChannelID">百度通道ID</param>
        /// <param name="pMessage">百度推送的对象</param>
        /// <returns></returns>
        public static PushRequest CreateAndroidUnicastNotificationRequest(int pType, int pChannelID, string pBaiduUserID
            , string pBaiduChannelID
            , BaiduPushNotification pMessage)
        {
            PushRequest mes = new PushRequest();
            mes.PlatForm = 1;
            mes.Type = pType;
            mes.ChannelID = pChannelID;
            mes.Request = PushMsgRequestBuilder.CreateUnicastNotificationRequest(pBaiduUserID, pBaiduChannelID, pMessage).ToJSON();
            return mes;
        }

        /// <summary>
        /// 创建单播的消息(消息类型=通知)推送请求
        /// </summary>
        /// <param name="pType">类型：1-直接发送，其它放入数据库轮循</param>
        /// <param name="pChannelID">对应MessageChannel表中的ID</param>
        /// <param name="pBaiduUserID">百度用户ID</param>
        /// <param name="pBaiduChannelID">百度通道ID</param>
        /// <param name="pMessageTitle">消息标题</param>
        /// <param name="pMessageDescription">消息内容</param>
        /// <returns></returns>
        public static PushRequest CreateAndroidUnicastNotificationRequest(int pType, int pChannelID, string pBaiduUserID
           , string pBaiduChannelID
           , string pMessageTitle
           , string pMessageDescription
           , PushTypes pushType = PushTypes.Unicast)
        {
            PushRequest mes = new PushRequest();
            mes.PlatForm = 1;
            mes.Type = pType;
            mes.ChannelID = pChannelID;
            mes.Request = PushMsgRequestBuilder.CreateUnicastNotificationRequest(pBaiduUserID, pBaiduChannelID, pMessageTitle, pMessageDescription, pushType).ToJSON();
            return mes;
        }

        /// <summary>
        /// 创建单播的消息(消息类型=消息)推送请求
        /// </summary>
        /// <param name="pType">类型：1-直接发送，其它放入数据库轮循</param>
        /// <param name="pChannelID">对应MessageChannel表中的ID</param>
        /// <param name="pBaiduUserID">百度用户ID</param>
        /// <param name="pBaiduChannelID">百度通道ID</param>
        /// <param name="pDeviceType">设备类型</param>
        /// <param name="pMessage">消息内容</param>
        /// <param name="pMessageKey">消息key</param>
        /// <returns></returns>
        public static PushRequest CreateAndroidUnicastMessageRequest(int pType, int pChannelID, string pBaiduUserID
            , string pBaiduChannelID
            , DeviceTypes? pDeviceType
            , string pMessage
            , string pMessageKey
            , PushTypes pushType = PushTypes.Unicast)
        {
            PushRequest mes = new PushRequest();
            mes.PlatForm = 1;
            mes.Type = pType;
            mes.ChannelID = pChannelID;
            mes.Request = PushMsgRequestBuilder.CreateUnicastMessageRequest(pBaiduUserID, pBaiduChannelID, pDeviceType, pMessage, pMessageKey, pushType).ToJSON();
            return mes;
        }

        /// <summary>
        /// 创建单播的消息(消息类型=消息)推送请求
        /// </summary>
        /// <param name="pType">类型：1-直接发送，其它放入数据库轮循</param>
        /// <param name="pChannelID">对应MessageChannel表中的ID</param>
        /// <param name="pBaiduUserID">百度用户ID</param>
        /// <param name="pBaiduChannelID">百度通道ID</param>
        /// <param name="pMessage">消息内容</param>
        /// <returns></returns>
        public static PushRequest CreateAndroidUnicastMessageRequest(int pType, int pChannelID, string pBaiduUserID
           , string pBaiduChannelID
           , string pMessage
           , PushTypes pushType = PushTypes.Unicast)
        {
            PushRequest mes = new PushRequest();
            mes.PlatForm = 1;
            mes.Type = pType;
            mes.ChannelID = pChannelID;
            mes.Request = PushMsgRequestBuilder.CreateUnicastMessageRequest(pBaiduUserID, pBaiduChannelID, null, pMessage, null, pushType).ToJSON();
            return mes;
        }

        /// <summary>
        /// 生成一个IOS的通知
        /// </summary>
        /// <param name="pType">类型：1-直接发送，其它放入数据库轮循</param>
        /// <param name="pChannelID">对应MessageChannel表中的ID</param>
        /// <param name="pDeviceToken"></param>
        /// <param name="pMessage"></param>
        /// <returns></returns>
        public static PushRequest CreateIOSUnicastNotificationRequest(int pType, int pChannelID, string pDeviceToken, string pMessage)
        {
            PushRequest mes = new PushRequest();
            mes.PlatForm = 2;
            mes.Type = pType;
            mes.ChannelID = pChannelID;
            mes.Request = IOSNotificationBuilder.CreateNotification(pDeviceToken, pMessage).ToJSON();
            return mes;
        }
    }
}