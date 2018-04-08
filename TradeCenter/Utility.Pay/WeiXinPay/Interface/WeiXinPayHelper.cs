using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.WeiXinPay.Util;
using JIT.Utility.Pay.WeiXinPay.Interface;
using System.Web.Security;
using Newtonsoft.Json;
using JIT.Utility.ExtensionMethod;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using JIT.TradeCenter.Entity;
using System.Configuration;
using JIT.Utility.Log;

namespace JIT.Utility.Pay.WeiXinPay.Interface
{
    public class WeiXinPayHelper
    {
        public Channel channel;
        public Dictionary<string, string> prePayParameters;
        public string prePayRequest;
        public string prePayResponse;

        public WeiXinPayHelper(Channel channel)
        {
            this.channel = channel;
        }

        /// <summary>
        /// 普通商户支付
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public PrePayResult prePay(AppOrderEntity entity)
        {
            this.prePayParameters = new Dictionary<string, string>();
            this.prePayParameters.Add("appid", this.channel.appid);
            this.prePayParameters.Add("mch_id", this.channel.mch_id);
            //this.prePayParameters.Add("device_info", "");
            this.prePayParameters.Add("nonce_str", Guid.NewGuid().ToString("N"));
            this.prePayParameters.Add("body", entity.AppOrderDesc);
            //this.prePayParameters.Add("attach", "");
            this.prePayParameters.Add("out_trade_no", entity.OrderID.ToString());//在支付中心生成的订单号****
            this.prePayParameters.Add("total_fee", entity.AppOrderAmount.Value.ToString());
            this.prePayParameters.Add("spbill_create_ip", entity.ClientIP);
            //this.prePayParameters.Add("spbill_create_ip","192.168.1.120");  //操作者ip测试 add by Henry
            //this.prePayParameters.Add("time_start", "");
            //this.prePayParameters.Add("time_expire", "");
            //this.prePayParameters.Add("goods_tag", "");
            this.prePayParameters.Add("notify_url", entity.NotifyUrl);
            this.prePayParameters.Add("trade_type", this.channel.trade_type);
            if (!string.IsNullOrEmpty(entity.OpenId))//trade_type=JSAPI，此参数必传，app是不需要填写，而如果值为空时，就不要做为键值对放到sign里***
            {
                this.prePayParameters.Add("openid", entity.OpenId);
            }
            //this.prePayParameters.Add("openid", "on9oCuAzFW9nbK8yC62LwaHnYWf0"); //openId测试 add by Henry
            //this.prePayParameters.Add("product_id", "");

            KeyValuePair<string, string>[] sortParameters = this.prePayParameters.OrderBy(item => item.Key).ToArray();
            List<string> paramater = new List<string>();
            foreach (KeyValuePair<string, string> item in sortParameters)
            {
                paramater.Add(string.Format("<{0}>{1}</{0}>", item.Key, item.Value));
            }
            paramater.Add(string.Format("<sign>{0}</sign>", this.getSign(this.prePayParameters, this.channel.signKey)));

            //下面向微信支付发起了支付
            this.prePayResponse = WeiXinPayHelper.getResponse(entity.PayUrl, this.prePayRequest = string.Format("<xml>{0}</xml>", string.Join("", paramater.ToArray())));
            return new XmlSerializer(typeof(PrePayResult)).Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(this.prePayResponse.Replace("xml>", "PrePayResult>")))) as PrePayResult;
        }

        /// <summary>
        /// 服务商支付
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public PrePayResult serPrePay(AppOrderEntity entity)
        {
            string appid = ConfigurationManager.AppSettings["appid"];
            string mch_id = ConfigurationManager.AppSettings["mch_id"];
            string signKey = ConfigurationManager.AppSettings["signKey"];

            Loggers.Debug(new DebugLogInfo() { Message = "entity:" + entity .ToJSON()});
            Loggers.Debug(new DebugLogInfo() { Message = "appid:" + appid + "mch_id:"
                + mch_id + "sub_appid:" + this.channel.appid + "sub_mch_id:" + this.channel.mch_id
            });
            this.prePayParameters = new Dictionary<string, string>();
            this.prePayParameters.Add("appid", appid);
            this.prePayParameters.Add("mch_id", mch_id);
            this.prePayParameters.Add("sub_appid", this.channel.appid);//子商户公众账号ID
            this.prePayParameters.Add("sub_mch_id", this.channel.mch_id); //子商户号
            this.prePayParameters.Add("nonce_str", Guid.NewGuid().ToString("N"));
            this.prePayParameters.Add("body", entity.AppOrderDesc);
            this.prePayParameters.Add("out_trade_no", entity.OrderID.ToString());//在支付中心生成的订单号****
            this.prePayParameters.Add("total_fee", entity.AppOrderAmount.Value.ToString());
            this.prePayParameters.Add("spbill_create_ip", entity.ClientIP);
            this.prePayParameters.Add("notify_url", entity.NotifyUrl);
            this.prePayParameters.Add("trade_type", this.channel.trade_type);
            this.prePayParameters.Add("sub_openid", entity.OpenId);

            KeyValuePair<string, string>[] sortParameters = this.prePayParameters.OrderBy(item => item.Key).ToArray();
            List<string> paramater = new List<string>();
            foreach (KeyValuePair<string, string> item in sortParameters)
            {
                paramater.Add(string.Format("<{0}>{1}</{0}>", item.Key, item.Value));
            }
            paramater.Add(string.Format("<sign>{0}</sign>", this.getSign(this.prePayParameters, signKey)));

            //下面向微信支付发起了支付
            this.prePayResponse = WeiXinPayHelper.getResponse(entity.PayUrl, this.prePayRequest = string.Format("<xml>{0}</xml>", string.Join("", paramater.ToArray())));
            return new XmlSerializer(typeof(PrePayResult)).Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(this.prePayResponse.Replace("xml>", "PrePayResult>")))) as PrePayResult;
        }

        public string getJsParamater(PrePayResult result)
        {
            Dictionary<string, string> preJsParameters = new Dictionary<string, string>();
            preJsParameters.Add("appId", result.appid);
            preJsParameters.Add("timeStamp", ((long)((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds)).ToString());
            preJsParameters.Add("nonceStr", Guid.NewGuid().ToString("N"));
            preJsParameters.Add("package", string.Format("prepay_id={0}", result.prepay_id));//这里的package就是prepay_id的值
            preJsParameters.Add("signType", "MD5");

            KeyValuePair<string, string>[] sortParameters = preJsParameters.OrderBy(item => item.Key).ToArray();
            List<string> paramater = new List<string>();
            foreach (KeyValuePair<string, string> item in sortParameters)
            {
                paramater.Add(string.Format("\"{0}\":\"{1}\"", item.Key, item.Value));
            }
            paramater.Add(string.Format("\"paySign\":\"{0}\"", this.getSign(preJsParameters, this.channel.signKey)));//根据商户的signKey获取支付时的paySign

            return string.Format("{{{0}}}", string.Join(",", paramater.ToArray()));
        }

        public string getAppParamater(PrePayResult result)
        {
            Dictionary<string, string> preJsParameters = new Dictionary<string, string>();
            preJsParameters.Add("appid", result.appid);
            preJsParameters.Add("partnerid", this.channel.mch_id);//商户号
            preJsParameters.Add("prepayid", result.prepay_id);//另外定义了一个prepayid
            preJsParameters.Add("package", "Sign=WXPay");//这里的package是固定的
            preJsParameters.Add("noncestr", Guid.NewGuid().ToString("N"));
            preJsParameters.Add("timestamp", ((long)((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds)).ToString());
            //  preJsParameters.Add("signType", "MD5");

            KeyValuePair<string, string>[] sortParameters = preJsParameters.OrderBy(item => item.Key).ToArray();//排序
            List<string> paramater = new List<string>();
            foreach (KeyValuePair<string, string> item in sortParameters)
            {
                paramater.Add(string.Format("\"{0}\":\"{1}\"", item.Key, item.Value));
            }
            paramater.Add(string.Format("\"sign\":\"{0}\"", this.getSign(preJsParameters, this.channel.signKey)));//在加上sign，注意这里的sign名字和js支付的不一样
            return string.Format("{{{0}}}", string.Join(",", paramater.ToArray()));
        }
        private string getSign(Dictionary<string, string> parameters, string signKey)
        {
            KeyValuePair<string, string>[] sortParameters = parameters.OrderBy(item => item.Key).ToArray();
            List<string> paramater = new List<string>();
            foreach (KeyValuePair<string, string> item in sortParameters)
            {
                paramater.Add(string.Format("{0}={1}", item.Key, item.Value));
            }
            paramater.Add(string.Format("key={0}", signKey));
            return JIT.Utility.MD5Helper.Encryption(string.Join("&", paramater.ToArray())).ToUpper();
        }

        private static string getResponse(string url, string paramater)
        {
            byte[] requestParamater = Encoding.UTF8.GetBytes(paramater);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestParamater.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(requestParamater, 0, requestParamater.Length);
            stream.Close();
            stream = request.GetResponse().GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
            string result = streamReader.ReadToEnd();
            streamReader.Close();
            stream.Close();
            return result;
        }

        public class Channel
        {
            public string appid { get; set; }
            public string mch_id { get; set; }
            public string signKey { get; set; }
            public string trade_type { get; set; }
            public string isSpPay { get; set; }
        }

        public class PrePayResult
        {
            public string return_code { get; set; }
            public string return_msg { get; set; }
            public string appid { get; set; }
            public string mch_id { get; set; }
            public string nonce_str { get; set; }
            public string sign { get; set; }
            public string result_code { get; set; }
            public string err_code { get; set; }
            public string err_code_des { get; set; }
            public string trade_type { get; set; }
            public string prepay_id { get; set; }
            public string code_url { get; set; }
        }

        public class NotifyResult
        {
            public string return_code { get; set; }
            public string return_msg { get; set; }
            public string appid { get; set; }
            public string mch_id { get; set; }
            public string device_info { get; set; }
            public string nonce_str { get; set; }
            public string sign { get; set; }
            public string result_code { get; set; }
            public string err_code { get; set; }
            public string err_code_des { get; set; }
            public string openid { get; set; }
            public string is_subscribe { get; set; }
            public string trade_type { get; set; }
            public string bank_type { get; set; }
            public int total_fee { get; set; }
            public int coupon_fee { get; set; }
            public string fee_type { get; set; }
            public string transaction_id { get; set; }
            public string out_trade_no { get; set; }
            public string attach { get; set; }
            public string time_end { get; set; }
        }
    }
}
