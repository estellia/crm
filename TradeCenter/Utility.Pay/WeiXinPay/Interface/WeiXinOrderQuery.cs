using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace JIT.Utility.Pay.WeiXinPay.Interface
{
    public class WeiXinOrderQuery
    {
        public Dictionary<string, string> orderQueryParameters;
        public string orderQueryRequest;
        public string orderQueryResponse;
        public OrderQueryInfo OrderQuery(OrderQueryPara para)
        {
            this.orderQueryParameters = new Dictionary<string, string>();
            //this.orderQueryParameters.Add("appid", "wx73194d1c7fb63529");
            //this.orderQueryParameters.Add("mch_id", "1231431502");
            ////this.orderQueryParameters.Add("out_trade_no", "88672");
            //this.orderQueryParameters.Add("out_trade_no", "88669");
            //this.orderQueryParameters.Add("nonce_str", Guid.NewGuid().ToString("N"));

            this.orderQueryParameters.Add("appid", para.appid);
            this.orderQueryParameters.Add("mch_id", para.mch_id);
            //this.orderQueryParameters.Add("out_trade_no", "88672");
            this.orderQueryParameters.Add("out_trade_no",para.out_trade_no);
            this.orderQueryParameters.Add("nonce_str", Guid.NewGuid().ToString("N"));



            KeyValuePair<string, string>[] sortParameters = this.orderQueryParameters.OrderBy(item => item.Key).ToArray();
            List<string> paramater = new List<string>();
            foreach (KeyValuePair<string, string> item in sortParameters)
            {
                paramater.Add(string.Format("<{0}>{1}</{0}>", item.Key, item.Value));
            }
            paramater.Add(string.Format("<sign>{0}</sign>", this.getSign(this.orderQueryParameters,para.signKey)));//signKey
            this.orderQueryResponse = WeiXinOrderQuery.getResponse("https://api.mch.weixin.qq.com/pay/orderquery", this.orderQueryRequest = string.Format("<xml>{0}</xml>", string.Join("", paramater.ToArray())));//entity.PayUrl
            return new XmlSerializer(typeof(OrderQueryInfo)).Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(this.orderQueryResponse.Replace("xml>", "OrderQueryInfo>")))) as OrderQueryInfo;
        }

        public class OrderQueryPara
        {
            public string appid { get; set; }
            public string mch_id { get; set; }
            //public string transaction_id { get; set; }
            public string out_trade_no { get; set; }
            public string nonce_str { get; set; }
            public string signKey { get; set; }
        }
        /// <summary>
        /// 返回参数
        /// </summary>
        public class OrderQueryInfo
        {
            public string return_code { get; set; }
            public string appid { get; set; }
            public string mch_id { get; set; }
            public string nonce_str { get; set; }
            public string sign { get; set; }
            public string result_code { get; set; }
            public string err_code { get; set; }
            public string err_code_des { get; set; }


            public string device_info { get; set; }
            public string openid { get; set; }
            public string is_subscribe { get; set; }
            public string trade_type { get; set; }
            /// <summary>
            /// 交易状态:SUCCESS—支付成功;REFUND—转入退款;NOTPAY—未支付;CLOSED—已关闭;REVOKED—已撤销（刷卡支付）;USERPAYING--用户支付中;PAYERROR--支付失败(其他原因，如银行返回失败)
            /// </summary>
            public string trade_state { get; set; }
            public string bank_type { get; set; }
            public int total_fee { get; set; }
            public int cash_fee_type { get; set; }
            public int coupon_fee { get; set; }
            public int coupon_count { get; set; }
            public string coupon_batch_id { get; set; }
            public string coupon_id { get; set; }
            public string transaction_id { get; set; }
            public string out_trade_no { get; set; }
            public string attach { get; set; }
            public string time_end { get; set; }
            public string trade_state_desc { get; set; }
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
    }
}
