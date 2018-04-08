/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/24 16:30:18
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
using System.IO;
using System.Net;
using System.Text;

using NUnit.Framework;
using JIT.Utility;
using JIT.Utility.Pay.UnionPay.Interface;
using JIT.Utility.Pay.UnionPay.ValueObject;
using JIT.Utility.Pay.UnionPay.Interface.Wap;
using JIT.Utility.Pay.UnionPay.Interface.Wap.Request;
using JIT.Utility.Pay.UnionPay.Interface.Wap.ValueObject;

namespace JIT.TestUtility.TestPay.TestClass.UnionPay.Interface.Wap
{
    /// <summary>
    /// TestGateway  
    /// </summary>
    [TestFixture]
    public class TestWapGateway
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestWapGateway()
        {
        }
        #endregion

        [Test]
        public void TestPreOrder()
        {
            UnionPayChannel channel = new UnionPayChannel() { CertificateFilePassword = "123456", CertificateFilePath = "D:/cer/630056832596.pfx", MerchantID = "630056832596", PacketEncryptKey = "654321" };
            PreOrderRequest req = new PreOrderRequest();
            req.SendTime = DateTime.Now;
            req.SendSeqID = Guid.NewGuid().ToString("N");
            req.FrontUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
            req.MerchantOrderDesc = "呵呵";
            req.Misc = string.Empty;
            //req.GatewayType = GatewayTypes.WAP;
            req.TransTimeout = DateTime.Now.AddHours(1);
            req.BackUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
            req.MerchantOrderCurrency = Currencys.RMB;
            req.MerchantOrderAmt = 1;
            req.MerchantID = "630056832596";
            req.MerchantOrderTime = DateTime.Now.AddMinutes(-5);
            req.MerchantOrderID = Guid.NewGuid().ToString("N");
            req.MerchantUserID = string.Empty;
            req.MobileNum = "15388157741";
            req.CarNum = string.Empty;

            var rsp =WapGateway.PreOrder(channel, req);
            Assert.IsTrue(rsp.IsSuccess);
        }

        [Test]
        public void TestQueryOrder()
        {
            //先发预订单请求
            UnionPayChannel channel = new UnionPayChannel() { CertificateFilePassword = "123456", CertificateFilePath = "D:/cer/630056832596.pfx", MerchantID = "630056832596", PacketEncryptKey = "654321" };
            PreOrderRequest req = new PreOrderRequest();
            req.SendTime = DateTime.Now;
            req.SendSeqID = Guid.NewGuid().ToString("N");
            req.FrontUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
            req.MerchantOrderDesc = "呵呵";
            req.Misc = string.Empty;
            //req.GatewayType = GatewayTypes.WAP;
            req.TransTimeout = DateTime.Now.AddHours(1);
            req.BackUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
            req.MerchantOrderCurrency = Currencys.RMB;
            req.MerchantOrderAmt = 1;
            req.MerchantID = "630056832596";
            req.MerchantOrderTime = DateTime.Now.AddMinutes(-5);
            req.MerchantOrderID = Guid.NewGuid().ToString("N");
            req.MerchantUserID = string.Empty;
            req.MobileNum = "15388157741";
            req.CarNum = string.Empty;

            var rsp = WapGateway.PreOrder(channel, req);
            Assert.IsTrue(rsp.IsSuccess);
            //跳转到支付平台页面
            WebClient wc = new WebClient();
            string strResponse = wc.UploadString(rsp.RedirectURL, string.Empty);
            //在查询
            QueryOrderRequest req2 = new QueryOrderRequest();
            req2.SendTime = DateTime.Now;
            req2.SendSeqID = Guid.NewGuid().ToString("N");
            req2.TransType = WapTransTypes.PreAuthorization;
            req2.MerchantID = "630056832596";
            req2.MerchantOrderID = req.MerchantOrderID;
            req2.MerchantOrderTime = req.MerchantOrderTime;

            var rsp2 = WapGateway.QueryOrder(channel, req2);
            Assert.IsTrue(rsp2.IsSuccess);
        }

        //[Test]
        //public void TestCancelTransaction()
        //{
        //    //先发预订单请求
        //    UnionPayChannel channel = new UnionPayChannel() { CertificateFilePassword = "123456", CertificateFilePath = "D:/cer/630056832596.pfx", MerchantID = "630056832596", PacketEncryptKey = "654321" };
        //    PreOrderRequest req = new PreOrderRequest();
        //    req.SendTime = DateTime.Now;
        //    req.SendSeqID = Guid.NewGuid().ToString("N");
        //    req.FrontUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
        //    req.MerchantOrderDesc = "呵呵";
        //    req.Misc = string.Empty;
        //    //req.GatewayType = GatewayTypes.WAP;
        //    req.TransTimeout = DateTime.Now.AddHours(1);
        //    req.BackUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
        //    req.MerchantOrderCurrency = Currencys.RMB;
        //    req.MerchantOrderAmt = 1;
        //    req.MerchantID = "630056832596";
        //    req.MerchantOrderTime = DateTime.Now.AddMinutes(-5);
        //    req.MerchantOrderID = Guid.NewGuid().ToString("N");
        //    req.MerchantUserID = string.Empty;
        //    req.MobileNum = "15388157741";
        //    req.CarNum = string.Empty;

        //    var rsp = WapGateway.PreOrder(channel, req);
        //    Assert.IsTrue(rsp.IsSuccess);
        //    //再撤销交易
        //    CancelTransactionRequest req2 = new CancelTransactionRequest();
        //    req2.BackUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
        //    req2.CupsQid = Guid.NewGuid().ToString("N");
        //    req2.MerchantID = rsp.MerchantID;
        //    req2.MerchantName = rsp.MerchantName;
        //    req2.MerchantOrderAmt = 1;
        //    req2.MerchantOrderCurrency = Currencys.RMB;
        //    req2.MerchantOrderID = req.MerchantOrderID;
        //    req2.MerchantOrderTime = req.MerchantOrderTime;
        //    req2.TransType = WapTransTypes.PreAuthorizationRevoked;

        //    var rsp2 = WapGateway.CancelTransaction(channel, req2);
        //    Assert.IsTrue(rsp2.IsSuccess);
        //}

        //[Test]
        //public void TestReturnOrder()
        //{
        //    //先发预订单请求
        //    UnionPayChannel channel = new UnionPayChannel() { CertificateFilePassword = "123456", CertificateFilePath = "D:/cer/630056832596.pfx", MerchantID = "630056832596", PacketEncryptKey = "654321" };
        //    PreOrderRequest req = new PreOrderRequest();
        //    req.SendTime = DateTime.Now;
        //    req.SendSeqID = Guid.NewGuid().ToString("N");
        //    req.FrontUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
        //    req.MerchantOrderDesc = "呵呵";
        //    req.Misc = string.Empty;
        //    //req.GatewayType = GatewayTypes.WAP;
        //    req.TransTimeout = DateTime.Now.AddHours(1);
        //    req.BackUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
        //    req.MerchantOrderCurrency = Currencys.RMB;
        //    req.MerchantOrderAmt = 1;
        //    req.MerchantID = "630056832596";
        //    req.MerchantOrderTime = DateTime.Now.AddMinutes(-5);
        //    req.MerchantOrderID = Guid.NewGuid().ToString("N");
        //    req.MerchantUserID = string.Empty;
        //    req.MobileNum = "15388157741";
        //    req.CarNum = string.Empty;

        //    var rsp = WapGateway.PreOrder(channel, req);
        //    Assert.IsTrue(rsp.IsSuccess);
        //    //再退货
        //    ReturnOrderRequest req2 = new ReturnOrderRequest();
        //    req2.BackUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
        //    req2.CupsQid = Guid.NewGuid().ToString("N");
        //    req2.MerchantID = rsp.MerchantID;
        //    req2.MerchantName = rsp.MerchantName;
        //    req2.MerchantOrderAmt = 1;
        //    req2.MerchantOrderCurrency = Currencys.RMB;
        //    req2.MerchantOrderID = req.MerchantOrderID;
        //    req2.MerchantOrderTime = req.MerchantOrderTime;

        //    var rsp2 = WapGateway.ReturnOrder(channel, req2);
        //    Assert.IsTrue(rsp2.IsSuccess);
        //}

        //[Test]
        //public void TestPreAuthorizationCompleted()
        //{

        //    //先发预订单请求
        //    UnionPayChannel channel = new UnionPayChannel() { CertificateFilePassword = "123456", CertificateFilePath = "D:/cer/630056832596.pfx", MerchantID = "630056832596", PacketEncryptKey = "654321" };
        //    PreOrderRequest req = new PreOrderRequest();
        //    req.SendTime = DateTime.Now;
        //    req.SendSeqID = Guid.NewGuid().ToString("N");
        //    req.FrontUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
        //    req.MerchantOrderDesc = "呵呵";
        //    req.Misc = string.Empty;
        //    //req.GatewayType = GatewayTypes.WAP;
        //    req.TransTimeout = DateTime.Now.AddHours(1);
        //    req.BackUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
        //    req.MerchantOrderCurrency = Currencys.RMB;
        //    req.MerchantOrderAmt = 1;
        //    req.MerchantID = "630056832596";
        //    req.MerchantOrderTime = DateTime.Now.AddMinutes(-5);
        //    req.MerchantOrderID = Guid.NewGuid().ToString("N");
        //    req.MerchantUserID = string.Empty;
        //    req.MobileNum = "15388157741";
        //    req.CarNum = string.Empty;

        //    var rsp = WapGateway.PreOrder(channel, req);
        //    Assert.IsTrue(rsp.IsSuccess);
        //    //再预授权完成
        //    PreAuthorizationCompletedRequest req2 = new PreAuthorizationCompletedRequest();
        //    req2.BackUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
        //    req2.CupsQid = Guid.NewGuid().ToString("N");
        //    req2.MerchantID = rsp.MerchantID;
        //    req2.MerchantName = rsp.MerchantName;
        //    req2.MerchantOrderAmt = 1;
        //    req2.MerchantOrderCurrency = Currencys.RMB;
        //    req2.MerchantOrderID = req.MerchantOrderID;
        //    req2.MerchantOrderTime = req.MerchantOrderTime;

        //    var rsp2 = WapGateway.PreAuthorizationCompleted(channel, req2);
        //    Assert.IsTrue(rsp2.IsSuccess);
        //}

        [Test]
        public void TestParseTransactionNotificationRequest()
        {
            string encrypedReq1 = null;
            using (var s = ReflectionUtils.GetEmbeddedResource("JIT.TestUtility.TestPay.TestMaterial.notification_req1.txt"))
            {
                TextReader reader = new StreamReader(s);
                encrypedReq1 = reader.ReadToEnd();
            }
            var req1 = WapGateway.ParseTransactionNotificationRequest("D:/cer/5101200070003100001.cer", encrypedReq1);
            Assert.IsTrue(req1.ConverRate == null);
            Assert.IsTrue(req1.CupsQid == "201312261540170019212");
            Assert.IsTrue(req1.CupsRespCode == "40");
            Assert.IsTrue(req1.CupsRespDesc == string.Empty);
            Assert.IsTrue(req1.CupsTraceNum == "001921");
            Assert.IsTrue(req1.MerchantID == "630056832596");
            Assert.IsTrue(req1.MerchantOrderAmt == 1);
            Assert.IsTrue(req1.MerchantOrderID == "dc10ae3119014d8180b9f8780398af45");
            Assert.IsTrue(req1.SetlAmt == 1);
            Assert.IsTrue(req1.SetlCurrency == Currencys.RMB);
            Assert.IsTrue(req1.SettleDate == "1226");
            Assert.IsTrue(req1.TransType == WapTransTypes.Consumption);
        }
    }
}
