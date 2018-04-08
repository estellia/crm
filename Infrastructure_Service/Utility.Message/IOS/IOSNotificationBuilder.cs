using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JdSoft.Apple.Apns.Notifications;


namespace JIT.Utility.Message.IOS
{
    public class IOSNotificationBuilder
    {
        /// <summary>
        /// 创建一个消息
        /// </summary>
        /// <param name="pDeviceToken">设备令牌</param>
        /// <param name="pBody">消息内容</param>
        /// <returns></returns>
        public static JdSoft.Apple.Apns.Notifications.Notification CreateNotification(string pDeviceToken, string pBody)
        {
            JdSoft.Apple.Apns.Notifications.Notification notification = new JdSoft.Apple.Apns.Notifications.Notification();
            notification.DeviceToken = pDeviceToken;
            notification.Payload.Alert.Body = pBody;
            notification.Payload.Sound = "default";
            notification.Payload.Badge = 1;
            return notification;
        }

        /// <summary>
        /// 创建一个消息
        /// </summary>
        /// <param name="pDeviceToken">设备令牌</param>
        /// <param name="pBody">消息内容</param>
        /// <param name="pCustomers">附带数据</param>
        /// <returns></returns>
        public static JdSoft.Apple.Apns.Notifications.Notification CreateNotification(string pDeviceToken, string pBody, Dictionary<string, object> pCustomers)
        {
            JdSoft.Apple.Apns.Notifications.Notification notification = new JdSoft.Apple.Apns.Notifications.Notification();
            notification.DeviceToken = pDeviceToken;
            notification.Payload.Alert.Body = pBody;
            notification.Payload.Sound = "default";
            notification.Payload.Badge = 1;
            foreach (var item in pCustomers)
            {
                notification.Payload.AddCustom(item.Key, item.Value);
            }
            return notification;
        }
    }
}
