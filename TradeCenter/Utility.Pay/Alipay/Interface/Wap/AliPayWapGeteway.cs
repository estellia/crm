using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using JIT.Utility.Pay.Alipay.Interface.Wap.Response;
using JIT.Utility.Pay.Alipay.Interface.Wap.Request;
using JIT.Utility.Pay.Alipay.Util;
using System.Web;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.Alipay.Channel;

namespace JIT.Utility.Pay.Alipay.Interface.Wap
{
    public static class AliPayWapGeteway
    {
        static AliPayWapGeteway()
        {
            Geteway = "http://wappaygw.alipay.com/service/rest.htm?";
        }

        public static readonly string Geteway;

        /// <summary>
        /// 获取提交交易请求的URL地址
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private static string CreateUrl(AliPayWapQueryTradeRequest pRequest)
        {
            return Geteway.Trim('?') + "?" + pRequest.GetContent();
        }

        /// <summary>
        /// 获取AliPay的支付跳转URL
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public static AliPayWapQueryOrderResponse GetQueryTradeResponse(AliPayWapTokenRequest pRequest, AliPayChannel pChannel)
        {
            AliPayWapQueryOrderResponse orderresponse = new AliPayWapQueryOrderResponse();
            var str = BaseGeteway.GetResponseStr(pRequest, Geteway);
            if (!string.IsNullOrEmpty(pChannel.RSA_PublicKey))
            {
                var token = Res_dataDecrypt(str, pChannel, pRequest.InputCharset);
            }
            TokenResponse response = new TokenResponse();
            var tempdic = AliPayFunction.ParseResponse(str, pRequest.SecID, pChannel.RSA_PrivateKey, pRequest.InputCharset);
            response.Load(tempdic);
            orderresponse.TokenResponse = response;
            AliPayWapQueryTradeRequest tradeRequeset = new AliPayWapQueryTradeRequest(pChannel) { RequestToken = response.Token };
            orderresponse.RedirectURL = CreateUrl(tradeRequeset);
            orderresponse.IsSucess = !string.IsNullOrEmpty(response.ResData);
            if (!orderresponse.IsSucess)
            {
                orderresponse.Message = response.ResError;
            }
            return orderresponse;
        }

        public static RoyaltyResponse GetRoyaltyResponse(RoyaltyRequest pRequest)
        {
            var str = BaseGeteway.GetResponseStr(pRequest, AliPayConfig.RoyaltyUrl);
            Log.Loggers.Debug(new Log.DebugLogInfo() { Message = "分润调用接口:" + str });
            RoyaltyResponse response = new RoyaltyResponse();
            if (!string.IsNullOrEmpty(str))
            {
                var tempdic = AliPayFunction.ParseResponse(str);
                response.Load(tempdic);
            }
            return response;
        }


        /// <summary>
        /// 验签，返回token字符串
        /// </summary>
        /// <param name="strResult">创建订单返回信息</param>
        /// <param name="sellprivatekey">商户私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>token字符串</returns>
        private static string Res_dataDecrypt(string strResult, AliPayChannel pChannel, string input_charset, bool needCheck = false)
        {
            //分解返回数据 用&拆分赋值给result
            string[] result = strResult.Split('&');

            //提取res_data参数
            string res_data = string.Empty;

            for (int i = 0; i < result.Length; i++)
            {
                string data = result[i];
                if (data.IndexOf("res_data=") >= 0)
                {
                    res_data = data.Replace("res_data=", string.Empty);

                    //解密(用"商户私钥"对"res_data"进行解密)
                    res_data = AliPayFunction.Decrypt(res_data, pChannel.RSA_PrivateKey, input_charset);

                    //res_data 赋值 给 result[0]
                    result[i] = "res_data=" + res_data;
                }
            }

            //创建待签名数组
            SortedDictionary<string, string> sd = new SortedDictionary<string, string>();
            int count = 0;
            string sparam = "";
            string key = "";
            string value = "";
            for (int i = 0; i < result.Length; i++)
            {
                sparam = result[i];
                count = sparam.IndexOf('=');
                key = sparam.Substring(0, count);
                value = sparam.Substring(count + 1, sparam.Length - (count + 1));
                sd.Add(key, value);
            }

            string sign = sd["sign"];

            //配置待签名数据
            Dictionary<string, string> dicData = AliPayFunction.FilterPara(sd);
            string req_Data = AliPayFunction.CreateLinkString(dicData);

            //验签，使用支付宝公钥
            bool vailSign = RSAFromPkcs8.Verify(req_Data, sign, pChannel.RSA_PublicKey, input_charset);
            if (!needCheck)
                vailSign = true;
            if (vailSign)//验签通过
            {
                //得到 request_token 的值
                string token = string.Empty;
                try
                {
                    token = AliPayFunction.GetStrForXmlDoc(res_data, "direct_trade_create_res/request_token");
                }
                catch (Exception ex)
                {
                    throw new Exception("方法：AliPayFunction.GetStrForXmlDoc（）解析数据失败", ex);
                }
                return token;
            }
            else
            {
                throw new Exception("返回的数据未通过验证，验签失败");
            }
        }

    }
}