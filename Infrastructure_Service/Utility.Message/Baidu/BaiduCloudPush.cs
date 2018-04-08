/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/3 11:06:30
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
using System.Linq;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Text;

using JIT.Utility.Message.Baidu.ValueObject;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Message.Baidu
{
    /// <summary>
    /// 百度云推送
    /// </summary>
    public static class BaiduCloudPush
    {
        #region 工具方法
        /// <summary>
        /// 调用百度云推送API
        /// </summary>
        /// <param name="pChannel">通道</param>
        /// <param name="pHttpMethod">REST API调用时采用的HTTP Method</param>
        /// <param name="pRequest">请求内容</param>
        /// <returns></returns>
        public static BaiduPushMessageResponse Call(BaiduChannel pChannel, HttpMethods pHttpMethod, BaiduPushMessageRequest pRequest)
        {
            //参数检查
            if (pChannel == null)
                throw new ArgumentNullException("pChannel");
            if (pRequest == null)
                throw new ArgumentNullException("pRequest");
            //先移除签名
            pRequest.InnerDictionary.Remove("sign");
            //设置请求的时间戳
            if(pRequest.Timestamp==null)
                pRequest.SetTimestampToNow();
            //将渠道中的信息设置到请求中
            pRequest.APIKey = pChannel.APIKey;
            pRequest.SecretKey = pChannel.SecretKey;
            //参数按键排序
            var parameters = pRequest.InnerDictionary.OrderBy(item => item.Key).ToList();
            //拼接字符串
            var strParameters = new StringBuilder();
            foreach (var p in parameters)
            {
                if (p.Value != null)
                {
                    if(p.Value is string)
                        strParameters.AppendFormat("{0}={1}", p.Key, p.Value);
                    else
                        strParameters.AppendFormat("{0}={1}", p.Key, p.Value.ToJSON());
                }
            }
            var strRequest = new StringBuilder();
            strRequest.AppendFormat("{0}{1}{2}{3}", pHttpMethod.GetDescription(), pChannel.URL, strParameters, pChannel.SecretKey);
            var request = HttpUtility.UrlEncode(strRequest.ToString());
            //
            //request = "POSThttp%3a%2f%2fchannel.api.duapp.com%2frest%2f2.0%2fchannel%2fchannelapikey%3dIpgepOSN5ciRbZXmaKDylV6rchannel_id%3d4545034400365506418device_type%3d3message_type%3d1messages%3d%7b%22title%22%3a%22%e6%b5%8b%e8%af%953%22%2c%22description%22%3a%22%e6%b5%8b%e8%af%953Foo%22%2c%22notification_builder_id%22%3a0%2c%22notification_basic_style%22%3a0%2c%22open_type%22%3a0%2c%22url%22%3a%22%22%2c%22user_confirm%22%3a0%2c%22pkg_content%22%3a%22%22%2c%22custom_content%22%3a%22%22%7dmethod%3dpush_msgmsg_keys%3dde8a3ba2345141abbd98d0565ddd452dpush_type%3d1timestamp%3d1386055916user_id%3d974907629901019623tWAhw2tIZrUnjynOqgELXX5CUuC18uHs";
            //计算签名
            var signSB = new StringBuilder();
            int perIndex = 0;
            for (int i = 0; i < request.Length; i++)
            {
                var c = request[i].ToString();
                if (request[i] == '%')
                {
                    perIndex = i;
                }
                if (i - perIndex == 1 || i - perIndex == 2)
                {
                    c = c.ToUpper();
                }
                signSB.Append(c);
            }
            var sign = FormsAuthentication.HashPasswordForStoringInConfigFile(signSB.ToString(), "MD5").ToLower();
            pRequest.Sign = sign;
            //组织POST的键值对字符串
            var postSB = new StringBuilder();
            foreach (var p in parameters)
            {
                if (p.Value != null)
                {
                    if(p.Value is string)
                        postSB.AppendFormat("{0}={1}&", p.Key, p.Value);
                    else
                        postSB.AppendFormat("{0}={1}&", p.Key, p.Value.ToJSON());
                }
            }
            postSB.AppendFormat("sign={0}",sign);
            //发送请求
            var postData = Encoding.UTF8.GetBytes(postSB.ToString());
            WebClient wc = new WebClient();
            var strResponse =string.Empty;
            try
            {//200响应
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
                byte[] responseData = wc.UploadData(pChannel.URL, "POST", postData);//得到返回字符流  
                strResponse = Encoding.UTF8.GetString(responseData);
            }
            catch (WebException ex)
            {//非200的响应
                using(var sr =new StreamReader(ex.Response.GetResponseStream()))
                {
                    strResponse = sr.ReadToEnd();
                }
            }
            if (string.IsNullOrWhiteSpace(strResponse))
            {
                return null;
            }
            else
            {
                return strResponse.DeserializeJSONTo<BaiduPushMessageResponse>();
            }
        }
        #endregion

        #region API - 推送消息
        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="pChannel"></param>
        /// <param name="pMessage"></param>
        /// <returns>推送的响应</returns>
        public static BaiduPushMessageResponse PushMessage(BaiduChannel pChannel,PushMsgRequest pRequest)
        {
            return BaiduCloudPush.Call(pChannel, HttpMethods.POST, pRequest);
        }
        #endregion
    }
}
