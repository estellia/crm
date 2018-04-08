using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JdSoft.Apple.Apns.Notifications;
using JIT.Utility.Log;
using System.Configuration;

namespace JIT.Utility.Message.IOS
{
    public class IOSNotificationService
    {
        private IOSNotificationService()
        {
            string filepath = ConfigurationManager.AppSettings["P12File"];
            string pwd = ConfigurationManager.AppSettings["P12FilePWD"];
            service = new JdSoft.Apple.Apns.Notifications.NotificationService(false, filepath, pwd, 1);
            service.SendRetries = 5; //5 retries before generating notificationfailed event
            service.ReconnectDelay = 5000; //5 seconds
            AddEvents(service);
        }

        private IOSNotificationService(bool pSandbox, string pP12File, string pP12PWD)
        {
            service = new JdSoft.Apple.Apns.Notifications.NotificationService(pSandbox, pP12File, pP12PWD, 1);
            service.SendRetries = 5; //5 retries before generating notificationfailed event
            service.ReconnectDelay = 5000; //5 seconds
            AddEvents(service);
        }

        private IOSNotificationService(byte[] pP12Data, string pP12PWD)
        {
            service = new JdSoft.Apple.Apns.Notifications.NotificationService(false, pP12Data, pP12PWD, 1);
            service.SendRetries = 5; //5 retries before generating notificationfailed event
            service.ReconnectDelay = 5000; //5 seconds
            AddEvents(service);
        }

        private NotificationService service;

        private static IOSNotificationService _default;

        public static IOSNotificationService Default
        {
            get
            {
                if (_default != null)
                    return _default;
                else
                    return _default = new IOSNotificationService();
            }

        }

        public static IOSNotificationService CreateInstance()
        {
            return new IOSNotificationService();
        }

        public static IOSNotificationService CreateInstance(bool pSandbox, string pP12File, string pP12PWD)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + pP12File;
            return new IOSNotificationService(pSandbox, path, pP12PWD);
        }

        private void AddEvents(NotificationService pService)
        {
            pService.Error += (s, e) =>
            {
                Loggers.Exception(new ExceptionLogInfo() { ErrorMessage = e.Message + e.InnerException });
            };
            pService.NotificationTooLong += (s, e) => { Loggers.Exception(new ExceptionLogInfo() { ErrorMessage = e.Message }); };
            pService.BadDeviceToken += (s, e) => { Loggers.Exception(new ExceptionLogInfo() { ErrorMessage = e.Message }); };
            pService.NotificationFailed += (s, e) => { Loggers.Exception(new ExceptionLogInfo() { ErrorMessage = "发送消息失败:" + e.ToString() }); };
            pService.NotificationSuccess += (s, e) => { Loggers.Debug(new DebugLogInfo() { Message = "发送成功:" + e.ToString() }); };
            pService.Connecting += (e) => { Loggers.Debug(new DebugLogInfo() { Message = "正在连接......" }); };
            pService.Connected += (e) => { Loggers.Debug(new DebugLogInfo() { Message = "连接成功!" }); };
            pService.Disconnected += (e) => { Loggers.Debug(new DebugLogInfo() { Message = "连接关闭!" }); };
        }

        public bool SendNotification(JdSoft.Apple.Apns.Notifications.Notification pNotification)
        {
            if (service.QueueNotification(pNotification))
            {
                Loggers.Debug(new DebugLogInfo() { Message = "成功发送至推送服务器:" + pNotification.ToString() });
                return true;
            }
            else
            {
                Loggers.Debug(new DebugLogInfo() { Message = "发送到服务失败:" + pNotification.ToString() });
                return false;
            }
        }

    }
}
