using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Log;
using JIT.Utility.Message.Baidu;
using JIT.Utility.Message.WCF.DataAccess;
using JIT.Utility.Message.WCF.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Message.Baidu.ValueObject;
using JdSoft.Apple.Apns.Notifications;
using JIT.Utility.Message.IOS;

namespace JIT.Utility.Message.WCFService
{
    public class AndroidRequestHandler
    {
        public PushResponse Process(PushRequest pRequest)
        {
            PushResponse response = new PushResponse();
            response.ResultCode = 0;
            try
            {
                MessageChannelDAO channelDao = new MessageChannelDAO(new BasicUserInfo());
                MessageDAO dao = new MessageDAO(new BasicUserInfo());
                var entity = channelDao.GetByID(pRequest.ChannelID);
                if (entity == null)
                {
                    throw new Exception(string.Format("未找到相应的ChannelID为：{0}的Channel信息", pRequest.ChannelID));
                }
                MessageEntity mEntity = new MessageEntity();
                mEntity.MessageID = Guid.NewGuid();
                mEntity.ClientID = pRequest.ClientID;
                mEntity.ChannelID = pRequest.ChannelID;
                mEntity.AppCode = pRequest.AppCode;
                mEntity.UserID = pRequest.UserID;
                var request = pRequest.Request.DeserializeJSONTo<PushMsgRequest>();
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
                mEntity.MessageParameters = pRequest.GetJson();
                switch (pRequest.Type)
                {
                    case 1://直接发送
                        var result = BaiduCloudPush.PushMessage(entity.GetBaiDuChannel(), pRequest.Request.DeserializeJSONTo<PushMsgRequest>());
                        if (result.IsSuccess)
                        {
                            mEntity.SendCount = 1;
                            mEntity.Status = 2;
                            dao.Create(mEntity);
                            Loggers.Debug(new DebugLogInfo() { Message = "【发送成功】:" + mEntity.MessageParameters });
                        }
                        else
                        {
                            mEntity.Status = 1;
                            mEntity.SendCount = 1;
                            mEntity.FaultReason = result.ErrorMessage;
                            dao.Create(mEntity);
                            Loggers.Debug(new DebugLogInfo() { Message = "【发送失败】:" + mEntity.MessageParameters + Environment.NewLine + result.ErrorMessage });
                            throw new Exception("【发送失败】：" + result.ErrorMessage + Environment.NewLine + mEntity.MessageParameters);
                        }
                        break;
                    case 2://存放到数据库
                        mEntity.SendCount = 0;
                        mEntity.Status = 0;
                        dao.Create(mEntity);
                        Loggers.Debug(new DebugLogInfo() { Message = "【保存数据库成功】:" + mEntity.MessageParameters });
                        break;
                    default:
                        throw new Exception("未定义的请求类型:" + pRequest.Type);
                }
            }
            catch (Exception ex)
            {
                response.ResultCode = 100;
                response.Message = ex.Message;
                Loggers.Exception(new ExceptionLogInfo(ex));
            }
            return response;
        }
    }

    public class IOSRequestHandler
    {
        public PushResponse Process(PushRequest pRequest)
        {
            PushResponse response = new PushResponse();
            response.ResultCode = 0;
            try
            {
                MessageChannelDAO channelDao = new MessageChannelDAO(new BasicUserInfo());
                MessageDAO dao = new MessageDAO(new BasicUserInfo());
                var entity = channelDao.GetByID(pRequest.ChannelID);
                if (entity == null)
                {
                    throw new Exception(string.Format("未找到相应的ChannelID为：{0}的Channel信息", pRequest.ChannelID));
                }
                MessageEntity mEntity = new MessageEntity();
                mEntity.MessageID = Guid.NewGuid();
                mEntity.ClientID = pRequest.ClientID;
                mEntity.ChannelID = pRequest.ChannelID;
                mEntity.AppCode = pRequest.AppCode;
                mEntity.UserID = pRequest.UserID;
                var notification = pRequest.Request.DeserializeJSONTo<JdSoft.Apple.Apns.Notifications.Notification>();
                mEntity.MessageParameters = pRequest.GetJson();
                switch (pRequest.Type)
                {
                    case 1://直接发送
                        var IOSChannel = entity.GetIOSChannel();
                        var result = IOSNotificationService.CreateInstance(IOSChannel.SandBox ?? true, IOSChannel.P12, IOSChannel.P12PWD).SendNotification(notification);
                        if (result)
                        {
                            mEntity.SendCount = 1;
                            mEntity.Status = 2;
                            dao.Create(mEntity);
                            Loggers.Debug(new DebugLogInfo() { Message = "【发送到推送服务器成功】:" + mEntity.MessageParameters });
                        }
                        else
                        {
                            mEntity.Status = 1;
                            mEntity.SendCount = 1;
                            mEntity.FaultReason = "发送到推送服务器失败";
                            dao.Create(mEntity);
                            Loggers.Debug(new DebugLogInfo() { Message = "【发送到推送服务器失败】:" + mEntity.MessageParameters });
                            throw new Exception("【发送到推送服务器失败】");
                        }
                        break;
                    case 2://存放到数据库
                        mEntity.SendCount = 0;
                        mEntity.Status = 0;
                        dao.Create(mEntity);
                        Loggers.Debug(new DebugLogInfo() { Message = "【保存数据库成功】:" + mEntity.MessageParameters });
                        break;
                    default:
                        throw new Exception("未定义的请求类型:" + pRequest.Type);
                }
            }
            catch (Exception ex)
            {
                response.ResultCode = 100;
                response.Message = ex.Message;
                Loggers.Exception(new ExceptionLogInfo(ex));
            }
            return response;
        }
    }
}