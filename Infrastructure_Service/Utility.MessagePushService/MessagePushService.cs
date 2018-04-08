using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Log;
using System.Threading;
using JIT.Utility.Message.Baidu;
using System.Configuration;
using JIT.Utility.Message.WCF.DataAccess;
using JIT.Utility.Message.WCF;
using JIT.Utility.Message.IOS;

namespace JIT.Utility.MessagePushService
{
    public partial class MessagePushService : ServiceBase
    {
        public MessagePushService()
        {
            InitializeComponent();
            bw1 = new BackgroundWorker();
            bwWx = new BackgroundWorker();
            _bwJd = new BackgroundWorker();
            bw1.DoWork += new DoWorkEventHandler(bw_DoWork1);
            bwWx.DoWork += new DoWorkEventHandler(bw_DoWorkWx);
            _bwJd.DoWork += new DoWorkEventHandler(bw_DoWorkJd);
            var interval = ConfigurationManager.AppSettings["Interval"];
            if (!int.TryParse(interval, out Interval))
            {
                Interval = 3;
            }
        }

        BackgroundWorker bw1;
        BackgroundWorker bwWx;//微信
        BackgroundWorker _bwJd;//接待
        int Interval;
        LoggingSessionInfo loggingSessionInfo;
        protected override void OnStart(string[] args)
        {
            bw1.RunWorkerAsync();
            bwWx.RunWorkerAsync();
            _bwJd.RunWorkerAsync();
        }

        protected override void OnStop()
        {
            bw1.Dispose();
            bwWx.Dispose();
            _bwJd.Dispose();
        }

        void bw_DoWork1(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    MessageDAO dao = new MessageDAO(new BasicUserInfo());
                    MessageChannelDAO channelDao = new MessageChannelDAO(new BasicUserInfo());
                    var entitys = dao.GetValidMessage();
                    foreach (var item in entitys)
                    {
                        try
                        {
                            var entity = channelDao.GetByID(item.ChannelID);
                            if (entity == null)
                            {
                                throw new Exception(string.Format("未找到相应的ChannelID为：{0}的Channel信息", item.ChannelID));
                            }
                            switch (entity.MobilePlatform)
                            {
                                case 1:
                                    var request = item.GetRequest();
                                    var baiduResponse = BaiduCloudPush.PushMessage(entity.GetBaiDuChannel(), request);
                                    if (baiduResponse.IsSuccess)
                                    {
                                        item.SendCount = (item.SendCount ?? 0) + 1;
                                        item.Status = 2;
                                        Loggers.Debug(new DebugLogInfo() { Message = "【发送成功】：" + item.MessageContent });
                                    }
                                    else
                                    {
                                        item.SendCount = (item.SendCount ?? 0) + 1;
                                        item.Status = 1;
                                        item.FaultReason = baiduResponse.ErrorMessage;
                                        Loggers.Debug(new DebugLogInfo() { Message = "【发送失败】：" + item.MessageContent });
                                    }
                                    break;
                                case 2:
                                    var IOSChannel = entity.GetIOSChannel();
                                    var notification = item.GetIOSNotification();
                                    var IOSResponse = IOSNotificationService.CreateInstance(IOSChannel.SandBox ?? true, IOSChannel.P12, IOSChannel.P12PWD).SendNotification(notification);
                                    if (IOSResponse)
                                    {
                                        item.SendCount = (item.SendCount ?? 0) + 1;
                                        item.Status = 2;
                                        Loggers.Debug(new DebugLogInfo() { Message = "【发送到推送服务器成功】：" + item.MessageContent });
                                    }
                                    else
                                    {
                                        item.SendCount = (item.SendCount ?? 0) + 1;
                                        item.Status = 1;
                                        item.FaultReason = "发送到推送服务器失败";
                                        Loggers.Debug(new DebugLogInfo() { Message = "【发送到推送服务器成功】：" + item.MessageContent });
                                    }
                                    break;
                                default:
                                    throw new Exception("错误的平台类型");
                            }


                            dao.Update(item);
                        }
                        catch (Exception ee)
                        {
                            item.SendCount = (item.SendCount ?? 0) + 1;
                            item.FaultReason = ee.Message;
                            item.Status = 1;
                            dao.Update(item);
                            Loggers.Exception(new ExceptionLogInfo(ee));
                            throw ee;
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(Interval));
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                }
            }
        }

        /// <summary>
        /// 微信推送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bw_DoWorkWx(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    var time = ConfigurationManager.AppSettings["Timing"];

                    //支持配置多客户
                    string[] customerIDs = ConfigurationManager.AppSettings["CustomerIDs"].Split(',');
                    foreach (string customerID in customerIDs)
                    {
                        loggingSessionInfo = GetLoggingSession(customerID, "PushService");


                        //微信推送
                        var vipBll = new VipBLL(loggingSessionInfo);                      //会员BLL实例化
                        var orderBll = new Car_OrderBLL(loggingSessionInfo);                //订单BLL实例化
                        var orderEntity = new Car_OrderEntity();
                        VipEntity vipEntity = null;  
                        

                        //查询参数
                        List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                        complexCondition.Add(new LessThanCondition() { FieldName = "ReserveTime", Value = DateTime.Now.AddMinutes(30), IncludeEquals = true, DateTimeAccuracy = DateTimeAccuracys.DateTime });//
                        complexCondition.Add(new MoreThanCondition() { FieldName = "ReserveTime", Value = DateTime.Now, IncludeEquals = true, DateTimeAccuracy = DateTimeAccuracys.DateTime });//
                        complexCondition.Add(new LessThanCondition() { FieldName = "Status", Value = 3 });//????
                        complexCondition.Add(new DirectCondition(" (PushStatus = '0') "));//未推送
                        //排序参数
                        List<OrderBy> lstOrder = new List<OrderBy> { };
                        lstOrder.Add(new OrderBy() { FieldName = "ReserveTime", Direction = OrderByDirections.Asc });
                        //orderEntity = orderBll.QueryByEntity(new Car_OrderEntity { ReserveTime > DateTime.Now }, null);
                        var tempOrderList = orderBll.Query(complexCondition.ToArray(), lstOrder.ToArray());
                        if (tempOrderList != null && tempOrderList.Length != 0)
                        {
                            vipEntity = vipBll.GetByID(tempOrderList[0].VipID);

                            if (!string.IsNullOrEmpty(vipEntity.WeiXinUserId))
                            {
#if DEBUG
                                //var appService = new WApplicationInterfaceBLL(loggingSessionInfo);
                                //var appList = appService.QueryByEntity(new WApplicationInterfaceEntity { CustomerId = customerID }, null);
                                //if (appList != null && appList.Length > 0)
                                //{
                                //    var app = appList.FirstOrDefault();
                                //    if (app != null)
                                //    {
                                //        var commonBll = new CommonBLL();
                                //        commonBll.SendTemplateMessage(app.WeiXinID, "亲，距离您的爱车洗澡时间只有30分钟了，请尽快到店哦！");//模板消息
                                //    }
                                //}
#endif
                                if (tempOrderList[0].ServiceItemID == 1)//为洗车消息
                                    CommonBLL.SendWeixinMessage("亲，距离您的爱车洗澡时间只有30分钟了，请尽快到店哦！", tempOrderList[0].VipID, loggingSessionInfo, vipEntity);

                                orderEntity = orderBll.GetByID(tempOrderList[0].OrderID);
                                orderEntity.PushStatus = 1;
                                orderBll.Update(orderEntity);
                            }
                        }
                        

                        

                    }
                 
                    Thread.Sleep(TimeSpan.FromSeconds(Interval));
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                }
            }
        }
        /// <summary>
        /// 接待推送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bw_DoWorkJd(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    var time = ConfigurationManager.AppSettings["Timing"];

                    //支持配置多客户
                    string[] customerIDs = ConfigurationManager.AppSettings["CustomerIDs"].Split(',');
                    foreach (string customerID in customerIDs)
                    {
                        loggingSessionInfo = GetLoggingSession(customerID, "PushService");

                        var vipBll = new VipBLL(loggingSessionInfo); //会员BLL实例化
                        var orderBll = new Car_OrderBLL(loggingSessionInfo); //订单BLL实例化
                        var orderEntity = new Car_OrderEntity();
                        VipEntity vipEntity = null;

                        //查询参数
                        List<IWhereCondition> complexCondition = new List<IWhereCondition> {};
                        complexCondition.Add(new LessThanCondition()
                        {
                            FieldName = "ReserveTime",
                            Value = DateTime.Now.AddMinutes(15),
                            IncludeEquals = true,
                            DateTimeAccuracy = DateTimeAccuracys.DateTime
                        }); //
                        complexCondition.Add(new MoreThanCondition()
                        {
                            FieldName = "ReserveTime",
                            Value = DateTime.Now,
                            IncludeEquals = true,
                            DateTimeAccuracy = DateTimeAccuracys.DateTime
                        }); //
                        complexCondition.Add(new LessThanCondition() {FieldName = "Status", Value = 3}); //????
                        complexCondition.Add(new DirectCondition(" ((PushStatus = '1' or (VipID is null or VipID = '')) and PushStatus != '15') ")); //未推送
                        //排序参数
                        List<OrderBy> lstOrder = new List<OrderBy> {};
                        lstOrder.Add(new OrderBy() {FieldName = "ReserveTime", Direction = OrderByDirections.Asc});
                        //orderEntity = orderBll.QueryByEntity(new Car_OrderEntity { ReserveTime > DateTime.Now }, null);
                        var tempOrderList = orderBll.Query(complexCondition.ToArray(), lstOrder.ToArray());
                        if (tempOrderList != null && tempOrderList.Length != 0)
                        {
                            //接待提前15分钟的订单消息推送
                            var orderPush = new InoutService(loggingSessionInfo);
                            orderPush.OrderPushMessage(tempOrderList[0].OrderID.ToString(), "15");

                            orderEntity = orderBll.GetByID(tempOrderList[0].OrderID);
                            orderEntity.PushStatus = 15;
                            orderBll.Update(orderEntity);
                        }
                    }


                    //MessageDAO dao = new MessageDAO(new BasicUserInfo());
                    //MessageChannelDAO channelDao = new MessageChannelDAO(new BasicUserInfo());
                    //var entitys = dao.GetValidMessage();
                    //foreach (var item in entitys)
                    //{
                    //    try
                    //    {
                    //        var entity = channelDao.GetByID(item.ChannelID);
                    //        if (entity == null)
                    //        {
                    //            throw new Exception(string.Format("未找到相应的ChannelID为：{0}的Channel信息", item.ChannelID));
                    //        }
                    //        switch (entity.MobilePlatform)
                    //        {
                    //            case 1:
                    //                var request = item.GetRequest();
                    //                var baiduResponse = BaiduCloudPush.PushMessage(entity.GetBaiDuChannel(), request);
                    //                if (baiduResponse.IsSuccess)
                    //                {
                    //                    item.SendCount = (item.SendCount ?? 0) + 1;
                    //                    item.Status = 2;
                    //                    Loggers.Debug(new DebugLogInfo() { Message = "【发送成功】：" + item.MessageContent });
                    //                }
                    //                else
                    //                {
                    //                    item.SendCount = (item.SendCount ?? 0) + 1;
                    //                    item.Status = 1;
                    //                    item.FaultReason = baiduResponse.ErrorMessage;
                    //                    Loggers.Debug(new DebugLogInfo() { Message = "【发送失败】：" + item.MessageContent });
                    //                }
                    //                break;
                    //            case 2:
                    //                var IOSChannel = entity.GetIOSChannel();
                    //                var notification = item.GetIOSNotification();
                    //                var IOSResponse = IOSNotificationService.CreateInstance(IOSChannel.SandBox ?? true, IOSChannel.P12, IOSChannel.P12PWD).SendNotification(notification);
                    //                if (IOSResponse)
                    //                {
                    //                    item.SendCount = (item.SendCount ?? 0) + 1;
                    //                    item.Status = 2;
                    //                    Loggers.Debug(new DebugLogInfo() { Message = "【发送到推送服务器成功】：" + item.MessageContent });
                    //                }
                    //                else
                    //                {
                    //                    item.SendCount = (item.SendCount ?? 0) + 1;
                    //                    item.Status = 1;
                    //                    item.FaultReason = "发送到推送服务器失败";
                    //                    Loggers.Debug(new DebugLogInfo() { Message = "【发送到推送服务器成功】：" + item.MessageContent });
                    //                }
                    //                break;
                    //            default:
                    //                throw new Exception("错误的平台类型");
                    //        }


                    //        dao.Update(item);
                    //    }
                    //    catch (Exception ee)
                    //    {
                    //        item.SendCount = (item.SendCount ?? 0) + 1;
                    //        item.FaultReason = ee.Message;
                    //        item.Status = 1;
                    //        dao.Update(item);
                    //        Loggers.Exception(new ExceptionLogInfo(ee));
                    //        throw ee;
                    //    }
                    //}
                    Thread.Sleep(TimeSpan.FromSeconds(Interval));
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                }
            }
        }

        public static LoggingSessionInfo GetLoggingSession(string customerId, string userId)
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            //loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, "7d4cda48970b4ed0aa697d8c2c2e4af3");
            loggingSessionInfo.CurrentUser = new JIT.CPOS.BS.Entity.User.UserInfo();
            loggingSessionInfo.CurrentUser.User_Id = userId;
            loggingSessionInfo.CurrentUser.customer_id = customerId;

            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = customerId;
            loggingSessionInfo.Conn = GetCustomerConn(customerId);

            loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Name = "";
            loggingSessionInfo.CurrentLoggingManager.User_Name = "";
            return loggingSessionInfo;
        }
        public static string GetCustomerConn(string customerId)
        {
            string sql = string.Format(
                "select 'user id='+a.db_user+';password='+a.db_pwd+';data source='+a.db_server+';database='+a.db_name+';' conn " +
                " from t_customer_connect a where a.customer_id='{0}' ",
                customerId);
            string conn = ConfigurationManager.AppSettings["APConn"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
    }
}
