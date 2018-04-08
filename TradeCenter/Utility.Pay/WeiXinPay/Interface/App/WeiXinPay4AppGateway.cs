/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/10 10:14:35
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
using System.Net;
using System.Text;
using System.Web;

using JIT.Utility.ExtensionMethod;
using JIT.Utility.Pay.WeiXinPay.ValueObject;
using JIT.Utility.Pay.WeiXinPay.Interface.App.Request;
using JIT.Utility.Pay.WeiXinPay.Interface.App.Response;
using JIT.Utility.Pay.Platform.WeiXinPay;

namespace JIT.Utility.Pay.WeiXinPay.Interface
{
    /// <summary>
    /// 微信支付入口 
    /// </summary>
    public static class WeiXinPay4AppGateway
    {
        #region 工具方法
        /// <summary>
        /// 访问API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pURL">API的URL</param>
        /// <param name="pPostContent">需要Post的数据</param>
        /// <returns></returns>
        private static T CallAPI<T>(string pURL, string pPostContent)
        {
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var rsp = client.UploadData(pURL, "POST", Encoding.UTF8.GetBytes(pPostContent));
            var rspText = Encoding.UTF8.GetString(rsp);
            if (!string.IsNullOrWhiteSpace(rspText))
            {
                return rspText.DeserializeJSONTo<T>();
            }
            else
            {
                return default(T);
            }
        }
        #endregion

        #region 接口地址

        /// <summary>
        /// 获取访问凭证的URL模板
        /// </summary>
        static string API_URL_OF_ACCESS_TOKEN = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential";

        /// <summary>
        /// 预订单接口地址
        /// </summary>
        static string API_URL_OF_PREORDER = "https://api.weixin.qq.com/pay/genprepay?";
        #endregion

        #region 接口

        /// <summary>
        /// 获取第三方访问凭证
        /// </summary>
        /// <param name="pAppID">应用ID</param>
        /// <param name="pAppSecret">凭证密钥</param>
        /// <returns></returns>
        public static AccessTokenInfo GetAccessToken(string pAppID, string pAppSecret)
        {
            string content = string.Format("appid={0}&secret={1}", pAppID, pAppSecret);
            return WeiXinPay4AppGateway.CallAPI<AccessTokenInfo>(API_URL_OF_ACCESS_TOKEN, content);
        }

        /// <summary>
        /// 预订单
        /// </summary>
        /// <param name="pChannel"></param>
        /// <param name="pRequest"></param>
        public static PreOrderResponse PreOrder(WeiXinPayChannel pChannel, PreOrderRequest pRequest)
        {
            //参数检查
            if (pChannel == null)
                throw new ArgumentNullException("pChannel");
            if (pRequest == null)
                throw new ArgumentNullException("pRequest");
            //创建预订单
            var preOrderReq = pRequest.GetContent(pChannel);
            //获取访问授权
            var token = WeiXinPay4AppGateway.GetAccessToken(pChannel.AppID, pChannel.AppSecret);
            if (!token.IsSuccess)
                throw new WeiXinPayException("501", string.Format("获取访问授权失败:[微信接口返回的错误码={0};错误信息={1}].", token.ErrorCode, token.ErrorMessage));
            var url = string.Format("{0}access_token={1}", API_URL_OF_PREORDER, token.Token);
            var rsp = WeiXinPay4AppGateway.CallAPI<PreOrderResponse>(url, preOrderReq.ToJSON());
            if (rsp.IsSuccess)
                return rsp;
            else
                throw new WeiXinPayException("502", string.Format("预订单失败:[微信接口返回的错误码={0};错误信息={1}].", rsp.ErrorCode, rsp.ErrorMessage));
        }

        /// <summary>
        /// 创建预订单
        /// <remarks>
        /// <para>与PreOrder的区别在于,此方法返回的信息包含App客户端调用微信SDK进行支付所必须的所有信息</para>
        /// </remarks>
        /// </summary>
        /// <param name="pChannel"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public static PreOrderInfo CreatePreOrder(WeiXinPayChannel pChannel, PreOrderRequest pRequest)
        {
            //参数检查
            if (pChannel == null)
                throw new ArgumentNullException("pChannel");
            if (pRequest == null)
                throw new ArgumentNullException("pRequest");
            //创建预订单
            var preOrderReq = pRequest.GetContent(pChannel);
            //获取访问授权
            var token = WeiXinPay4AppGateway.GetAccessToken(pChannel.AppID, pChannel.AppSecret);
            if (!token.IsSuccess)
                throw new WeiXinPayException("501", string.Format("获取访问授权失败:[微信接口返回的错误码={0};错误信息={1}].", token.ErrorCode, token.ErrorMessage));
            var url = string.Format("{0}access_token={1}", API_URL_OF_PREORDER, token.Token);
            var rsp = WeiXinPay4AppGateway.CallAPI<PreOrderResponse>(url, preOrderReq.ToJSON());
            if (!rsp.IsSuccess)
                throw new WeiXinPayException("502", string.Format("预订单失败:[微信接口返回的错误码={0};错误信息={1}].", rsp.ErrorCode, rsp.ErrorMessage));
            //
            PreOrderInfo rtn = new PreOrderInfo();
            rtn.AppID = pChannel.AppID;
            rtn.NonceStr = preOrderReq.NonceStr;
            rtn.PartnerID = pChannel.ParnterID;
            rtn.PreOrderID = rsp.PrePayID;
            rtn.TimeStamp = preOrderReq.TimeStamp;
            rtn.OrderSign = preOrderReq.Sign;
            //
            return rtn;
        }

        /// <summary>
        /// 创建预订单
        /// <remarks>
        /// <para>与PreOrder的区别在于,此方法返回的信息包含App客户端调用微信SDK进行支付所必须的所有信息</para>
        /// </remarks>
        /// </summary>
        /// <param name="pChannel"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public static PreOrderInfo CreatePreOrder(WeiXinPayChannel pChannel, WeiXinPayBaseRequest pRequest)
        {
            //参数检查
            if (pChannel == null)
                throw new ArgumentNullException("pChannel");
            if (pRequest == null)
                throw new ArgumentNullException("pRequest");
            pRequest.Channel = pChannel;
            //创建预订单
            var reqstr = pRequest.GetContent();
            //获取访问授权
            var token = WeiXinPay4AppGateway.GetAccessToken(pChannel.AppID, pChannel.AppSecret);
            if (!token.IsSuccess)
                throw new WeiXinPayException("501", string.Format("获取访问授权失败:[微信接口返回的错误码={0};错误信息={1}].", token.ErrorCode, token.ErrorMessage));
            var url = string.Format("{0}access_token={1}", API_URL_OF_PREORDER, token.Token);
            var rsp = WeiXinPay4AppGateway.CallAPI<PreOrderResponse>(url, reqstr);
            if (!rsp.IsSuccess)
                throw new WeiXinPayException("502", string.Format("预订单失败:[微信接口返回的错误码={0};错误信息={1}].", rsp.ErrorCode, rsp.ErrorMessage));
            //
            PreOrderInfo rtn = new PreOrderInfo();
            rtn.AppID = pChannel.AppID;
            rtn.NonceStr = pRequest.NonceStr;
            rtn.PartnerID = pChannel.ParnterID;
            rtn.PreOrderID = rsp.PrePayID;
            rtn.TimeStamp = Convert.ToInt32(pRequest.TimeStamp);
            rtn.OrderSign = pRequest.AppSignature;
            //
            return rtn;
        }
        #endregion
    }
}
