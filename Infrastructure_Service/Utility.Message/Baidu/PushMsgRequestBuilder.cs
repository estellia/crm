/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/3 15:48:21
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
using System.Data;

using JIT.Utility.Message.Baidu.ValueObject;
using Microsoft.SqlServer.Server;

namespace JIT.Utility.Message.Baidu
{
    /// <summary>
    /// PushMsgRequest的构建器 
    /// </summary>
    public static class PushMsgRequestBuilder
    {
        #region 创建单播的消息(消息类型=通知)推送请求
        /// <summary>
        /// 创建单播的消息(消息类型=通知)推送请求
        /// </summary>
        /// <param name="pBaiduUserID"></param>
        /// <param name="pBaiduChannelID"></param>
        /// <param name="pDeviceType"></param>
        /// <param name="pMessage"></param>
        /// <param name="pMessageKey"></param>
        /// <returns></returns>
        public static PushMsgRequest CreateUnicastNotificationRequest(string pBaiduUserID
            , string pBaiduChannelID
            , DeviceTypes? pDeviceType
            , BaiduPushNotification pMessage
            , string pMessageKey
            , PushTypes pushType = PushTypes.Unicast)
        {
            PushMsgRequest req = new PushMsgRequest();
            req.MessageType = MessageTypes.Notification;
            req.PushType = pushType;
            if (!string.IsNullOrEmpty(pBaiduUserID))
                req.BaiduUserID = pBaiduUserID;
            req.BaiduChannelID = pBaiduChannelID;
            req.DeviceType = pDeviceType;
            req.Notification = pMessage;
            if (!string.IsNullOrWhiteSpace(pMessageKey))
                req.BaiduMessageKey = pMessageKey;
            return req;
        }

        /// <summary>
        /// 创建单播的消息(消息类型=通知)推送请求
        /// </summary>
        /// <param name="pBaiduUserID"></param>
        /// <param name="pBaiduChannelID"></param>
        /// <param name="pMessage"></param>
        /// <returns></returns>
        public static PushMsgRequest CreateUnicastNotificationRequest(string pBaiduUserID
            , string pBaiduChannelID
            , BaiduPushNotification pMessage)
        {
            return PushMsgRequestBuilder.CreateUnicastNotificationRequest(pBaiduUserID, pBaiduChannelID, null, pMessage, null);
        }

        /// <summary>
        /// 创建单播的消息(消息类型=通知)推送请求
        /// </summary>
        /// <param name="pBaiduUserID"></param>
        /// <param name="pBaiduChannelID"></param>
        /// <param name="pMessageTitle"></param>
        /// <param name="pMessageDescription"></param>
        /// <returns></returns>
        public static PushMsgRequest CreateUnicastNotificationRequest(string pBaiduUserID
            , string pBaiduChannelID
            , string pMessageTitle
            , string pMessageDescription
            , PushTypes pushType = PushTypes.Unicast)
        {
            BaiduPushNotification msg = new BaiduPushNotification();
            msg.Title = pMessageTitle;
            msg.Description = pMessageDescription;
            msg.OpenType = 2;
            msg.NotificationBuilderID = 0;
            msg.NotificationBasicStyle = 4;
            return PushMsgRequestBuilder.CreateUnicastNotificationRequest(pBaiduUserID, pBaiduChannelID, null, msg, null, pushType);
        }
        #endregion

        #region 创建单播的消息(消息类型=消息)推送请求
        /// <summary>
        /// 创建单播的消息(消息类型=消息)推送请求
        /// </summary>
        /// <param name="pBaiduUserID"></param>
        /// <param name="pBaiduChannelID"></param>
        /// <param name="pDeviceType"></param>
        /// <param name="pMessage"></param>
        /// <param name="pMessageKey"></param>
        /// <returns></returns>
        public static PushMsgRequest CreateUnicastMessageRequest(string pBaiduUserID
            , string pBaiduChannelID
            , DeviceTypes? pDeviceType
            , string pMessage
            , string pMessageKey
            , PushTypes pushType = PushTypes.Unicast)
        {
            PushMsgRequest req = new PushMsgRequest();
            req.PushType = pushType;
            if (!string.IsNullOrEmpty(pBaiduUserID))
                req.BaiduUserID = pBaiduUserID;
            req.BaiduChannelID = pBaiduChannelID;
            req.DeviceType = pDeviceType;
            req.MessageType = MessageTypes.Message;
            req.Message = pMessage;
            if (!string.IsNullOrWhiteSpace(pMessageKey))
                req.BaiduMessageKey = pMessageKey;
            //
            return req;
        }

        /// <summary>
        /// 创建单播的消息(消息类型=消息)推送请求
        /// </summary>
        /// <param name="pBaiduUserID"></param>
        /// <param name="pBaiduChannelID"></param>
        /// <param name="pMessage"></param>
        /// <returns></returns>
        public static PushMsgRequest CreateUnicastMessageRequest(string pBaiduUserID
            , string pBaiduChannelID
            , string pMessage)
        {
            return PushMsgRequestBuilder.CreateUnicastMessageRequest(pBaiduUserID, pBaiduChannelID, null, pMessage, null);
        }

        #endregion
    }
}
