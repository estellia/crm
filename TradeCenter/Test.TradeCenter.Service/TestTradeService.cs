using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ExtensionMethod;
using JIT.TradeCenter.Framework;
using NUnit.Framework;
using JIT.TradeCenter.Framework.DataContract;
using System.Net;
using System.IO;

namespace Test.TradeCenter.Service
{
    [TestFixture]
    public class TestTradeService
    {
        public static readonly string sss = Guid.NewGuid().ToString("N");
        //public static string Url = "http://121.199.42.125:6001/Gateway.ashx?";
        public static string Url = "http://localhost:1266/Gateway.ashx?";
        //public static string Url = "http://localhost:1266/DevPayTest.ashx?";
        //public static string Url = "http://121.199.42.125:6001/DevPayTest.ashx?";
        //public static string Url = "http://121.199.42.125:6002/DevPayTest.ashx?";
        //public static string Url = "http://Jp.lzlj.com:6001/DevPayTest.ashx?";
        [Test]
        public void TestService()
        {
            CreateOrderParameters para = new CreateOrderParameters()
            {
                AppOrderAmount = 1,
                AppOrderDesc = "测试",
                AppOrderID = sss,
                AppOrderTime = DateTime.Now.ToJITFormatString(),
                PayChannelID = 16,
                MobileNO = "18019438327"
            };
            TradeRequest request = new TradeRequest()
            {
                AppID = 1,
                ClientID = "27",
                Parameters = para,
                UserID = "1111"
            };
            string parameter = string.Format("action=CreateOrder&request={1}", Url, request.ToJSON());
            //parameter = "action=CreateOrder&request={\"AppID\":1,\"ClientID\":\"e703dbedadd943abacf864531decdac1\",\"UserID\":\"00193aeff94341a1a8f64e224c7c249c\",\"Token\":null,\"Parameters\":{\"PayChannelID\":3,\"AppOrderID\":\"61ebfd4cbb1b4716b40605845cc761cd\",\"AppOrderTime\":\"2014-01-17 09:46:30 562\",\"AppOrderAmount\":1,\"AppOrderDesc\":\"jitmarketing\",\"Currency\":1,\"MobileNO\":\"\",\"ReturnUrl\":\"http://www.o2omarketing.cn:9004/HtmlApp/LJ/html/pay_success.html?orderId=61ebfd4cbb1b4716b40605845cc761cd\",\"DynamicID\":null,\"DynamicIDType\":null}}";
            var data = Encoding.GetEncoding("utf-8").GetBytes(parameter);
            var res = GetResponseStr(Url, data).DeserializeJSONTo<TradeResponse>();
            Console.WriteLine(res.ToJSON());
        }

        [Test]
        public void TestQueryOrder()
        {
            QueryOrderParameters para = new QueryOrderParameters() { OrderID = 1 };
            TradeRequest request = new TradeRequest()
            {
                AppID = 1,
                ClientID = "27",
                Parameters = para,
                UserID = "1111"
            };
            string parameter = string.Format("action=QueryOrder&request={1}", Url, request.ToJSON());
            var data = Encoding.GetEncoding("utf-8").GetBytes(parameter);
            var res = GetResponseStr(Url, data);
            Console.WriteLine(res.ToJSON());
        }

        [Test]
        public void TestQueryOrderBySourceID()
        {
            QueryOrderByAppInfoParameters para = new QueryOrderByAppInfoParameters() { AppOrderID = "f7410ab4bf4e40979f04dc76808c15ac" };
            TradeRequest request = new TradeRequest()
            {
                AppID = 1,
                ClientID = "e703dbedadd943abacf864531decdac1",
                Parameters = para,
                UserID = "1111"
            };
            string parameter = string.Format("action=IsOrderPaid&request={1}", Url, request.ToJSON());
            var data = Encoding.GetEncoding("utf-8").GetBytes(parameter);
            var res = GetResponseStr(Url, data);
            Console.WriteLine(res.ToJSON());
        }

        [Test]
        public void TestGetSign()
        {
            var para = new Dictionary<string, object>();
            para["appid"] = "wx8f74386d57405ec5";
            para["timestamp"] = "1398653150";
            para["openid"] = "oUcanju-XbWR0IJmdF_Y68Kt0szw";
            var request = new
            {
                AppID = 1,
                ClientID = "27",
                UserID = "1111",
                Parameters = new
                {
                    PayChannelID = 9,
                    NoSignDic = para
                }
            };
            string str = string.Format("action=GetSign&request={1}", Url, request.ToJSON());
            string res = JIT.Utility.Web.HttpClient.PostQueryString(Url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void Test()
        {
            string str = "action=CreateOrder&request={\"AppID\":1,\"ClientID\":\"e703dbedadd943abacf864531decdac1\",\"Parameters\":{\"PayChannelID\":9,\"AppOrderID\":\"0057e0e9ae944868b70a0aaf477e1f4e\",\"AppOrderTime\":\"2014-04-22 11:42:23 562\",\"AppOrderAmount\":1,\"AppOrderDesc\":\"微信支付订单\",\"Currency\":\"1\",\"MobileNO\":\"\",\"DynamicID\":\"\",\"DynamicIDType\":\"\",\"Paras\":{\"SpbillCreateIp\":\"127.0.0.1\"}}}";
            var data = Encoding.GetEncoding("utf-8").GetBytes(str);
            var res = GetResponseStr(Url, data);
            Console.WriteLine(res.ToJSON());
        }

        [Test]
        public void WXGetUpdateFeedBackUrl()
        {
            var request = new
            {
                AppID = 1,
                ClientID = "27",
                UserID = "1111",
                Parameters = new
                {
                    PayChannelID = 9,
                    FeedBackID = "10279402725891407675",
                    OpenID = "oUcanju-XbWR0IJmdF_Y68Kt0szw"
                }
            };
            string str = string.Format("action=WXGetUpdateFeedBackUrl&request={1}", Url, request.ToJSON());
            string res = JIT.Utility.Web.HttpClient.PostQueryString(Url, str);
            var dic = res.DeserializeJSONTo<Dictionary<string, object>>();
            var dic2 = dic["Datas"].ToJSON().DeserializeJSONTo<Dictionary<string, string>>();
            string url = dic2["Url"];
            var res2 = JIT.Utility.Web.HttpClient.GetQueryString(url, "");
            Console.WriteLine(res);
            Console.WriteLine(res2);
        }

        public static string GetResponseStr(string url, byte[] data)
        {
            Encoding code = Encoding.GetEncoding("utf-8");

            //请求远程HTTP
            string strResult = "";
            try
            {
                //设置HttpWebRequest基本信息
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(url);
                myReq.Method = "post";
                myReq.ContentType = "application/x-www-form-urlencoded";

                var stream = myReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                //发送POST数据请求服务器
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();

                //获取服务器返回信息
                StreamReader reader = new StreamReader(myStream, code);
                StringBuilder responseData = new StringBuilder();
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    responseData.Append(line);
                }

                //释放
                myStream.Close();

                return strResult = responseData.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

    }
}
