using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Web;
using JIT.Utility.Log;
using JIT.TradeCenter.Entity;
using JIT.TradeCenter.BLL;
using JIT.Utility.ExtensionMethod;
using System.Configuration;
using System.Text;
using System.Collections;

namespace PayCenterNotifyService
{
    public static class NotifyHandler
    {
        public static bool Notify(string pUrl, string content, out string message)
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

        public static bool Notify(AppOrderEntity pEntity, out string msg, bool isTonyCard = false)
        {
            string content = string.Format("OrderID={0}&OrderStatus={1}&CustomerID={2}&UserID={3}&ChannelID={4}&SerialPay={5}&isTonyCard={6}", pEntity.AppOrderID, pEntity.Status, pEntity.AppClientID, pEntity.AppUserID, pEntity.PayChannelID, pEntity.OrderID, isTonyCard ? 1 : 0);
            var channelbll = new PayChannelBLL(new JIT.Utility.BasicUserInfo());
            var channel = channelbll.GetByID(pEntity.PayChannelID);
            string notifyUrl = channel.NotifyUrl;
            Loggers.Debug(new DebugLogInfo() { Message = "wx pay Notify 开始调用通知接口:" + pEntity.ToJSON() });
            var i = NotifyHandler.Notify(channel.NotifyUrl, content, out msg);
            string message = i ? " wx pay Notify 调用通知接口成功" : " wx pay Notify 调用通知接口失败";
            message += ("：" + msg + "::" + notifyUrl + "?" + content);
            Loggers.Debug(new DebugLogInfo() { Message = message });
            return i;
        }

        /// <summary>
        /// 通知cpos
        /// </summary>
        /// <param name="pEntity"></param>
        /// <param name="msg"></param>
        /// <param name="ht">附加参数</param>
        /// <param name="isTonyCard"></param>
        /// <returns></returns>
        public static bool Notify(AppOrderEntity pEntity, out string msg, Hashtable ht, bool isTonyCard = false)
        {
            string content = string.Format("OrderID={0}&OrderStatus={1}&CustomerID={2}&UserID={3}&ChannelID={4}&SerialPay={5}&isTonyCard={6}", pEntity.AppOrderID, pEntity.Status, pEntity.AppClientID, pEntity.AppUserID, pEntity.PayChannelID, pEntity.OrderID, isTonyCard ? 1 : 0);
            // 附加参数
            if (ht.Count > 0)
            {
                foreach (DictionaryEntry item in ht)
                {
                    content += "&" + item.Key + "=" + item.Value;
                }
            }
            var channelbll = new PayChannelBLL(new JIT.Utility.BasicUserInfo());
            var channel = channelbll.GetByID(pEntity.PayChannelID);
            string notifyUrl = channel.NotifyUrl;
            Loggers.Debug(new DebugLogInfo() { Message = "wx pay Notify 开始调用通知接口:" + pEntity.ToJSON() });
            var i = NotifyHandler.Notify(channel.NotifyUrl, content, out msg);
            string message = i ? " wx pay Notify 调用通知接口成功" : " wx pay Notify 调用通知接口失败";
            message += ("：" + msg + "::" + notifyUrl + "?" + content);
            Loggers.Debug(new DebugLogInfo() { Message = message });
            return i;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="customerId"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static bool NotifyTFSaveWxVipInfo(string orderId, string customerId, string openId)
        {
            try
            {
                var notifyUrl = ConfigurationManager.AppSettings["ApiHost"];
                string param = "{\"common\": { \"isALD\": \"0\", \"customerId\": \"" + customerId + "\", \"locale\": \"zh\",\"openid\":\"" + openId + "\" }, \"special\": { \"orderId\": \"" + orderId + "\"} }";
                string fulParam = string.Format("OnlineShopping/data/Data.aspx?action=getWxPersonInfoByOpenId&ReqContent={0}", param);
                Loggers.Debug(new DebugLogInfo() { Message = "NotifyTFSaveWxVipInfo 开始调用通知接口:" + notifyUrl + "?" + fulParam });
                string msg = string.Empty;
                var i = NotifyHandler.Notify(notifyUrl, fulParam, out msg);
                string message = i ? " NotifyTFSaveWxVipInfo 调用通知接口成功" : " NotifyTFSaveWxVipInfo 调用通知接口失败";
                message += ("：" + notifyUrl + "?" + fulParam + ":" + msg);
                Loggers.Debug(new DebugLogInfo() { Message = message });
                return i;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="customerId"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static bool NotifyLifePayInfo(string orderId, string customerId, string openId)
        {
            try
            {
                var notifyUrl = ConfigurationManager.AppSettings["ApiHost"];
                string param = "{\"common\": { \"isALD\": \"0\", \"customerId\": \"" + customerId + "\", \"locale\": \"zh\",\"openid\":\"" + openId + "\" }, \"special\": { \"orderId\": \"" + orderId + "\"} }";
                string fulParam = string.Format("OnlineShopping/data/Data.aspx?action=getWxPersonInfoByOpenId&ReqContent={0}", param);
                Loggers.Debug(new DebugLogInfo() { Message = "NotifyLifePayInfo 开始调用通知接口:" + notifyUrl + "?" + fulParam });
                string msg = string.Empty;
                var i = NotifyHandler.Notify(notifyUrl, fulParam, out msg);
                string message = i ? " NotifyLifePayInfo 调用通知接口成功" : " NotifyLifePayInfo 调用通知接口失败";
                message += ("：" + notifyUrl + "?" + fulParam + ":" + msg);
                Loggers.Debug(new DebugLogInfo() { Message = message });
                return i;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}