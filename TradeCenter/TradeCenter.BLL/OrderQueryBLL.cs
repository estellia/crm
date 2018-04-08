using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.TradeCenter.Entity;
using JIT.Utility.Pay.WeiXinPay.Interface;
using JIT.Utility.Log;
using JIT.Utility.Web;


namespace JIT.TradeCenter.BLL
{
    public partial class OrderQueryBLL
    {
        /// <summary>
        /// 第三方支付通知失败处理
        /// </summary>
        public void SetNotificationFailed()
        {
            AppOrderBLL appOrderBLL = new AppOrderBLL(new BasicUserInfo());
            PayChannelBLL payChannelBLL = new PayChannelBLL(new BasicUserInfo());

            WeiXinOrderQuery orderQuery = new WeiXinOrderQuery();
            PayChannelEntity payChannelEntiy = null;
            WeiXinOrderQuery.OrderQueryPara para = null;

            try
            {
                //获取当天未支付成功的订单
                AppOrderEntity[] appOrderList = appOrderBLL.GetUnpaidOrder();
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("找到{0}条通知失败或未支付成功数据", appOrderList.Count()) });
                foreach (var order in appOrderList)
                {
                    //读取微信微信配置信息
                    payChannelEntiy = payChannelBLL.GetByID(order.PayChannelID);
                    if (payChannelEntiy != null)
                    {
                        if (payChannelEntiy.PayType == 6)//微信公众号支付
                        {
                            para = new WeiXinOrderQuery.OrderQueryPara();
                            para = payChannelEntiy.ChannelParameters.DeserializeJSONTo<WeiXinOrderQuery.OrderQueryPara>();
                            para.out_trade_no = order.OrderID.ToString();
                            WeiXinOrderQuery.OrderQueryInfo result = orderQuery.OrderQuery(para);

                            if (result.return_code == "SUCCESS" && result.result_code == "SUCCESS")
                            {
                                if (result.trade_state == "SUCCESS")//交易状态=支付成功
                                {
                                    order.Status = 2;       //付款成功
                                    appOrderBLL.Update(order);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
            }
        }
        /// <summary>
        /// 支付中心通知失败重新通知到业务平台
        /// </summary>
        public void PayCenterNotify()
        {
            try
            {
                var bll = new AppOrderBLL(new JIT.Utility.BasicUserInfo());
                //获取未通知的订单信息
                var entitys = bll.GetNotNodify();
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("找到{0}条待通知记录", entitys.Length) });
                foreach (var item in entitys)
                {
                    string msg;
                    if (Notify(item, out msg))
                    {
                        item.IsNotified = true;
                    }
                    else
                    {
                        //设定下次通知时间
                        item.NextNotifyTime = GetNextNotifyTime(item.NotifyCount ?? 0);
                    }
                    //NotifyCount++
                    item.NotifyCount++;
                    //更新数据
                    bll.Update(item);
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
            }
        }

        private DateTime GetNextNotifyTime(int NotifyCount)
        {
            switch (NotifyCount)
            {
                case 0:
                    return DateTime.Now.AddSeconds(30);
                case 1:
                    return DateTime.Now.AddMinutes(3);
                case 2:
                    return DateTime.Now.AddMinutes(10);
                case 3:
                    return DateTime.Now.AddMinutes(30);
                case 4:
                    return DateTime.Now.AddHours(1);
                case 5:
                    return DateTime.Now.AddHours(6);
                default:
                    return DateTime.Now.AddDays(1);
            }
        }
        public static bool Notify(string pUrl, string content, out string message)
        {
            try
            {
                var str = HttpClient.GetQueryString(pUrl, content);
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("{0}?{1}", pUrl, content) });
                if (str == "SUCCESS")
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

        public static bool Notify(AppOrderEntity pEntity, out string msg)
        {
            string content = string.Format("OrderID={0}&OrderStatus={1}&CustomerID={2}&UserID={3}&ChannelID={4}&SerialPay={5}", pEntity.AppOrderID, pEntity.Status, pEntity.AppClientID, pEntity.AppUserID, pEntity.PayChannelID, pEntity.OrderID);
            var channelbll = new PayChannelBLL(new JIT.Utility.BasicUserInfo());
            var channel = channelbll.GetByID(pEntity.PayChannelID);
            Loggers.Debug(new DebugLogInfo() { Message = "开始调用通知接口:" + pEntity.ToJSON() });
            var i =Notify(channel.NotifyUrl, content, out msg);
            Loggers.Debug(new DebugLogInfo() { Message = i ? "调用通知接口成功" : "调用通知接口失败" });
            return i;
        }
    }
}
