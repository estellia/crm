using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using JIT.Utility.Message.WCF.DataAccess;
using JIT.Utility;
using JIT.Utility.Message.WCF;
using JIT.Utility.Message.Baidu;
using JIT.Utility.Log;
using System.Threading;
using JIT.Utility.Message.Baidu.ValueObject;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Message.WCF.Entity;

namespace Utility.MessageHandleService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            bw1 = new BackgroundWorker();
            bw1.DoWork += new DoWorkEventHandler(bw_DoWork1);
            bw2 = new BackgroundWorker();
            bw2.DoWork += new DoWorkEventHandler(bw2_DoWork);
            var interval = ConfigurationManager.AppSettings["Interval"];
            if (!int.TryParse(interval, out Interval))
            {
                Interval = 3;
            }
        }



        BackgroundWorker bw1;
        BackgroundWorker bw2;
        int Interval;

        protected override void OnStart(string[] args)
        {
            bw1.RunWorkerAsync();
            bw2.RunWorkerAsync();
        }

        protected override void OnStop()
        {
            bw1.Dispose();
            bw2.Dispose();
        }

        void bw_DoWork1(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    Loggers.Debug(new DebugLogInfo() { Message = "审批订单-查询数据库" });
                    MessageDAO mdao = new MessageDAO(new BasicUserInfo());
                    OrdersCheckingDAO dao = new OrdersCheckingDAO(new BasicUserInfo());
                    ClientUserDAO userDao = new ClientUserDAO(new BasicUserInfo());
                    MessageChannelDAO channelDao = new MessageChannelDAO(new BasicUserInfo());
                    var entitys = dao.GetValidOrdersChecking();
                    Loggers.Debug(new DebugLogInfo() { Message = "审批订单-找到" + entitys.Count() + "条数据" });
                    foreach (var item in entitys)
                    {
                        Dictionary<ClientUserEntity, string> messages;
                        var userEntitys = userDao.GetValidUserByStatus0(item, out messages);
                        foreach (var it in userEntitys)
                        {
                            if (!string.IsNullOrEmpty(it.PushChannel))
                            {
                                PushChannel info = it.PushChannel.DeserializeJSONTo<PushChannel>();
                                var pRequest = PushMsgRequestBuilder.CreateUnicastNotificationRequest(info.BaiduUserID, info.BaiduChannelID, "订单审批通知", messages[it]);
                                MessageEntity mEntity = new MessageEntity();
                                mEntity.MessageID = Guid.NewGuid();
                                mEntity.ChannelID = 1;
                                mEntity.ClientID = it.ClientID.Value.ToString();
                                mEntity.UserID = it.ClientUserID.Value.ToString();
                                var request = pRequest;
                                var messagetype = request.InnerDictionary["message_type"].ToString();
                                //判断消息类型0-消息，1-通知
                                switch (messagetype)
                                {
                                    case "0":
                                        mEntity.MessageContent = request.InnerDictionary["messages"].ToString();
                                        break;
                                    case "1":
                                        mEntity.MessageContent = request.InnerDictionary["messages"].ToJSON().DeserializeJSONTo<BaiduPushNotification>().Description;
                                        break;
                                }
                                mEntity.MessageParameters = pRequest.InnerDictionary.ToJSON();
                                mEntity.SendCount = 0;
                                mEntity.Status = 0;
                                mdao.Create(mEntity);
                                Loggers.Debug(new DebugLogInfo() { Message = "【保存数据库成功】:" + mEntity.MessageParameters });
                                item.SendStatus = 1;
                                dao.Update(item);
                            }
                            else
                                Loggers.Debug(new DebugLogInfo() { Message = string.Format("用户{0}-{1}的PushChanel为空", it.ClientUserID, it.Username) });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                }
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("审批订单-轮循结束,等待{0}秒", Interval) });
                Thread.Sleep(TimeSpan.FromSeconds(Interval));
            }
        }

        void bw2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    Loggers.Debug(new DebugLogInfo() { Message = "确认订单-查询数据库" });
                    MessageDAO mdao = new MessageDAO(new BasicUserInfo());
                    OrdersCheckingDAO dao = new OrdersCheckingDAO(new BasicUserInfo());
                    ClientUserDAO userDao = new ClientUserDAO(new BasicUserInfo());
                    MessageChannelDAO channelDao = new MessageChannelDAO(new BasicUserInfo());
                    var entitys = dao.GetValidOrdersChecking(1);
                    Loggers.Debug(new DebugLogInfo() { Message = "确认订单-找到" + entitys.Count() + "条数据" });
                    foreach (var item in entitys)
                    {
                        Dictionary<ClientUserEntity, string> messages;
                        var userEntitys = userDao.GetValidUserByStatus1(item, out messages);
                        foreach (var it in userEntitys)
                        {
                            if (!string.IsNullOrEmpty(it.PushChannel))
                            {
                                PushChannel info = it.PushChannel.DeserializeJSONTo<PushChannel>();
                                var pRequest = PushMsgRequestBuilder.CreateUnicastNotificationRequest(info.BaiduUserID, info.BaiduChannelID, "订单审批通知", messages[it]);
                                MessageEntity mEntity = new MessageEntity();
                                mEntity.MessageID = Guid.NewGuid();
                                mEntity.ChannelID = 1;
                                mEntity.ClientID = it.ClientID.Value.ToString();
                                mEntity.UserID = it.ClientUserID.Value.ToString();
                                var request = pRequest;
                                var messagetype = request.InnerDictionary["message_type"].ToString();
                                //判断消息类型0-消息，1-通知
                                switch (messagetype)
                                {
                                    case "0":
                                        mEntity.MessageContent = request.InnerDictionary["messages"].ToString();
                                        break;
                                    case "1":
                                        mEntity.MessageContent = request.InnerDictionary["messages"].ToJSON().DeserializeJSONTo<BaiduPushNotification>().Description;
                                        break;
                                }
                                mEntity.MessageParameters = pRequest.InnerDictionary.ToJSON();
                                mEntity.SendCount = 0;
                                mEntity.Status = 0;
                                mdao.Create(mEntity);
                                Loggers.Debug(new DebugLogInfo() { Message = "【保存数据库成功】:" + mEntity.MessageParameters });
                                item.SendStatus = 2;
                                dao.Update(item);
                            }
                            else
                                Loggers.Debug(new DebugLogInfo() { Message = string.Format("用户{0}-{1}的PushChanel为空", it.ClientUserID, it.Username) });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                }
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("确认订单-轮循结束,等待{0}秒", Interval) });
                Thread.Sleep(TimeSpan.FromSeconds(Interval));
            }
        }
    }
}
