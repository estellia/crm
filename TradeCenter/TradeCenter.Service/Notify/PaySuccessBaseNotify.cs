using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.TradeCenter.Entity;
using JIT.TradeCenter.BLL;
using System.Threading.Tasks;
using PayCenterNotifyService;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using System.Threading;
using JIT.Utility.Web;

namespace JIT.TradeCenter.Service.Notify
{
    public abstract class PaySuccessBaseNotify : IHttpHandler
    {
        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var channelid = Convert.ToInt32(context.Request["ChannelID"]);
                Loggers.Debug(new DebugLogInfo() { Message = "ChannelID:" + channelid });
                var paychannelbll = new PayChannelBLL(new Utility.BasicUserInfo());
                var paychannel = paychannelbll.GetByID(channelid);

                AppOrderEntity entity;
                AppOrderBLL bll = new AppOrderBLL(new Utility.BasicUserInfo());
                if (Process(paychannel, context, out entity))
                {
                    Loggers.Debug(new DebugLogInfo() { Message = "entity:" + entity.ToJSON()});
                    if (!entity.IsNotified.HasValue) entity.IsNotified = false;
                    if (!entity.IsNotified.Value)
                    {
                        Loggers.Debug(new DebugLogInfo() { Message = "IsNotified is false" });
                        //var t = Task.Factory.StartNew(() =>
                        //  {//起一个新线程通知业务系统处理订单
                            try
                            {
                                string msg;
                                if (Notify(entity, out msg))
                                {
                                    entity.IsNotified = true;
                                }
                                else
                                {
                                    entity.NextNotifyTime = DateTime.Now.AddMinutes(1);
                                }
                                //通知完成,通知次数+1
                                entity.NotifyCount = (entity.NotifyCount ?? 0) + 1;
                                bll.Update(entity);
                            }
                            catch (Exception ex)
                            {
                                Loggers.Exception(new ExceptionLogInfo(ex));
                            }
                          //});
                        //t.Wait();
                        Loggers.Debug(new DebugLogInfo() { Message = "task is end" });
                    }
                    else
                    {
                        Loggers.Debug(new DebugLogInfo() { Message = "已处理过的订单" });
                    };

                }
                else
                {
                    if (entity != null)
                        Loggers.Debug(new DebugLogInfo() { Message = "交易失败:" + entity.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo() { Message = "error:" + ex.Message });
                context.Response.Write(ex);
            }
        }



        private bool Notify(string pUrl, string content, out string message)
        {
            try
            {
                var str = HttpClient.GetQueryString(pUrl, content);
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("{0}?{1}", pUrl, content) });
                if (str.Contains("SUCCESS"))
                {
                    message = "通知成功";
                    return true;
                }
                else
                {
                    message = "通知失败:" + str;
                    Loggers.Debug(new DebugLogInfo() { Message = message });
                    return false;
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                message = "通知失败:" + ex.Message;
                return false;
            }
        }

        private bool Notify(AppOrderEntity pEntity, out string msg, bool isTonyCard = false)
        {
            string content = string.Format("OrderID={0}&OrderStatus={1}&CustomerID={2}&UserID={3}&ChannelID={4}&SerialPay={5}&isTonyCard={6}", pEntity.AppOrderID, pEntity.Status, pEntity.AppClientID, pEntity.AppUserID, pEntity.PayChannelID, pEntity.OrderID, isTonyCard ? 1 : 0);
            var channelbll = new PayChannelBLL(new JIT.Utility.BasicUserInfo());
            var channel = channelbll.GetByID(pEntity.PayChannelID);
            string notifyUrl = channel.NotifyUrl;
            Loggers.Debug(new DebugLogInfo() { Message = "wx pay Notify 开始调用通知接口:" + pEntity.ToJSON() });
            var i = Notify(channel.NotifyUrl, content, out msg);
            string message = i ? " wx pay Notify 调用通知接口成功" : " wx pay Notify 调用通知接口失败";
            message += ("：" + msg + "::" + notifyUrl + "?" + content);
            Loggers.Debug(new DebugLogInfo() { Message = message });
            return i;
        }



        public abstract bool Process(PayChannelEntity pChannel, HttpContext pContext, out AppOrderEntity pEntity);
    }
}