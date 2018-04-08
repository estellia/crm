using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.Utility.Pay.Alipay.Interface.Wap.Request;
using JIT.Utility.Pay.Alipay;
using System.Web;
using JIT.Utility.Pay.Alipay.Interface.Wap;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.Alipay.Channel;

namespace JIT.TestUtility.TestPay.TestClass.AliPay.Wap
{
    [TestFixture]
    public class TestAliPay
    {
        AliPayChannel channel = new AliPayChannel();
        [Test]
        public void TestGetToken()
        {
            AliPayWapTokenRequest request = new AliPayWapTokenRequest(channel)
            {
                CallBackUrl = "http://112.124.43.61:7777/AliPay/WebForm1.aspx",
                NotifyUrl = "http://115.29.186.161:9004/AlipayWapTrade2/Notify.aspx",
                OutTradeNo = Guid.NewGuid().ToString().Replace("-", ""),
                Partner = channel.Partner,
                ReqID = Guid.NewGuid().ToString().Replace("-", ""),
                Subject = "测试fasdf",
                TotalFee = "0.01",
                SellerAccountName = "account@jitmarketing.cn",
            };
            var t = AliPayWapGeteway.GetQueryTradeResponse(request, channel);
            Console.WriteLine(t);
        }

        [Test]
        public void TestTrade()
        {
            AliPayWapTokenRequest request = new AliPayWapTokenRequest(channel)
            {
                CallBackUrl = "http://115.29.186.161:9004/AlipayWapTrade2/Call_Back.aspx",
                NotifyUrl = "http://115.29.186.161:9004/AlipayWapTrade2/Notify.aspx",
                OutTradeNo = Guid.NewGuid().ToString().Replace("-", ""),
                Partner = AliPayConfig.Partner_Royalty,
                ReqID = Guid.NewGuid().ToString().Replace("-", ""),
                Subject = "测试fasdf",
                TotalFee = "0.01",
                SellerAccountName = AliPayConfig.Seller_Account_Name_Royalty,
            };
            var URL = AliPayWapGeteway.GetQueryTradeResponse(request, channel);
        }

        [Test]
        public void TestRoyalty()
        {
            RoyaltyRequest reqpuest = new RoyaltyRequest
            {
                TradeNo = "2014010338528432",
                OutTradeNo = "851c062ce42e4aa780b2c7506072d65a",
                OutBillNo = "2014131645555",
                Partner = "2088011289712913",
                RoyaltyType = "10",
                RoyaltyParameters = "harvey0930@hotmail.com^0.03^Test",
            };
            var response = AliPayWapGeteway.GetRoyaltyResponse(reqpuest);
        }

        /// <summary>
        /// 获取16位随机数
        /// </summary>
        /// <returns></returns>
        public string GetDataRandom()
        {
            string strData = string.Empty;
            strData += DateTime.Now.Year;
            strData += DateTime.Now.Month;
            strData += DateTime.Now.Day;
            strData += DateTime.Now.Hour;
            strData += DateTime.Now.Minute;
            strData += DateTime.Now.Second;
            Random r = new Random();
            strData = strData + r.Next(100);
            return strData;
        }
    }
}
