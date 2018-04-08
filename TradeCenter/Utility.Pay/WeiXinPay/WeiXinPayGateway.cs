using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Pay.WeiXinPay.ValueObject;
using JIT.Utility.Pay.WeiXinPay.Interface;
using JIT.Utility.Pay.WeiXinPay.Interface.App.Response;
using JIT.Utility.Pay.Platform.WeiXinPay.Interface.App.Request;

namespace JIT.Utility.Pay.WeiXinPay
{
    public static class WeiXinPayGateway
    {
        public static T CallAPI<T>(string pUrl, string pContent)
        {
            var str = JIT.Utility.Web.HttpClient.PostQueryString(pUrl, pContent);
            if (string.IsNullOrEmpty(str))
                return default(T);
            try
            {
                return str.DeserializeJSONTo<T>();
            }
            catch (Exception ex)
            {
                Log.Loggers.Exception(new Log.ExceptionLogInfo(ex));
                return default(T);
            }
        }

        private static string GetUrl(WeiXinUrlType pType)
        {
            switch (pType)
            {
                case WeiXinUrlType.Token:
                    return "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential";
                case WeiXinUrlType.AppPreOrder:
                    return "https://api.weixin.qq.com/pay/genprepay";
                case WeiXinUrlType.UpdateFeedback:
                    return "https://api.weixin.qq.com/payfeedback/update";
                case WeiXinUrlType.CustomerService:
                    return "https://api.weixin.qq.com/cgi-bin/message/custom/send";
                default:
                    throw new Exception("错误的类型");
            }
        }

        public static AccessTokenInfo GetToken(WeiXinPayChannel pChannel)
        {
            string url = GetUrl(WeiXinUrlType.Token);
            string content = string.Format("appid={0}&secret={1}", pChannel.AppID, pChannel.AppSecret);
            return CallAPI<AccessTokenInfo>(url, content);
        }

        public static PreOrderResponse PreOrder(WeiXinAppOrderRequest pRequest, WeiXinPayChannel pChannel)
        {
            var tokenInfo = GetToken(pChannel);
            if (tokenInfo == null)
                throw new Exception("获取Token失败");
            if (!tokenInfo.IsSuccess)
                throw new Exception(string.Format("获取Token失败:{0} {1}", tokenInfo.ErrorCode, tokenInfo.ErrorMessage));
            var token = tokenInfo.Token;
            var url = string.Format("{0}?access_token={1}", GetUrl(WeiXinUrlType.AppPreOrder), token);
            return CallAPI<PreOrderResponse>(url, pRequest.GetContent());
        }

        public static bool UpdateFeedBack(WeiXinPayChannel pChannel, string pFeedBackID, string pOpenID, out string msg)
        {
            var tokenInfo = GetToken(pChannel);
            if (tokenInfo == null)
                throw new Exception("获取Token失败");
            if (!tokenInfo.IsSuccess)
                throw new Exception(string.Format("获取Token失败:{0} {1}", tokenInfo.ErrorCode, tokenInfo.ErrorMessage));
            var token = tokenInfo.Token;
            var url = string.Format("{0}?access_token={1}&openid={2}&feedbackid={3}", GetUrl(WeiXinUrlType.UpdateFeedback), token, pOpenID, pFeedBackID);
            var dic = JIT.Utility.Web.HttpClient.GetQueryString(url, "").DeserializeJSONTo<Dictionary<string, string>>();
            msg = dic["errmsg"].ToString();
            return Convert.ToInt32(dic["errcode"]) == 0;
        }

        public static string GetUpdateFeedBackUrl(WeiXinPayChannel pChannel, string pFeedBackID, string pOpenID)
        {
            var tokenInfo = GetToken(pChannel);
            if (tokenInfo == null)
                throw new Exception("获取Token失败");
            if (!tokenInfo.IsSuccess)
                throw new Exception(string.Format("获取Token失败:{0} {1}", tokenInfo.ErrorCode, tokenInfo.ErrorMessage));
            var token = tokenInfo.Token;
            var url = string.Format("{0}?access_token={1}&openid={2}&feedbackid={3}", GetUrl(WeiXinUrlType.UpdateFeedback), token, pOpenID, pFeedBackID);
            return url;
        }
    }
}
