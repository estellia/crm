/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/26 13:52:12
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

using NUnit.Framework;
using JIT.Utility.Pay.UnionPay.Interface;
using JIT.Utility.Pay.UnionPay.ValueObject;
using JIT.Utility.Pay.UnionPay.Interface.IVR;
using JIT.Utility.Pay.UnionPay.Interface.IVR.Request;
using JIT.Utility.Pay.UnionPay.Interface.IVR.ValueObject;

namespace JIT.TestUtility.TestPay.TestClass.UnionPay.Interface.IVR
{
    /// <summary>
    /// TestIVRGateway  
    /// </summary>
    [TestFixture]
    public class TestIVRGateway
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestIVRGateway()
        {
        }
        #endregion

        [Test]
        public void TestPreOrder()
        {
            UnionPayChannel channel = new UnionPayChannel() { CertificateFilePassword = "000000", CertificateFilePath = "D:/cer/700000000000001.pfx", MerchantID = "267099633218", PacketEncryptKey = "654321" };
            PreOrderRequest req = new PreOrderRequest();
            req.SendTime = DateTime.Now;
            req.SendSeqID = Guid.NewGuid().ToString("N");
            req.FrontUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
            req.MerchantOrderDesc = "呵呵";
            req.Misc = string.Empty;
            req.Mode = IVRModes.Callback;
            //req.GatewayType = GatewayTypes.WAP;
            req.TransTimeout = DateTime.Now.AddHours(1);
            req.BackUrl = "http://www.jitmarketing.cn:8090/TranNotification.ashx";
            req.MerchantOrderCurrency = Currencys.RMB;
            req.MerchantOrderAmt = 1;
            req.MerchantID = channel.MerchantID;
            req.MerchantOrderTime = DateTime.Now.AddMinutes(-5);
            req.MerchantOrderID = Guid.NewGuid().ToString("N").Substring(0, 30);
            req.MerchantUserID = string.Empty;
            req.MobileNum = "18019438327";
            req.CarNum = string.Empty;

            var rsp = IVRGateway.PreOrder(channel, req);
            Assert.IsTrue(rsp.IsSuccess);
        }

        [Test]
        public void TestParseTransactionNotificationRequest()
        {
            string encrypedReq1 = "MjY3MDk5NjMzMjE4|Aa/qpl2e+s6+kfKQY0H9rYQTbA6n0o1k/JlBTQezC81hXdIXjZLohIF1lOGTtC6q1luGeXrSHHxusCZdg5gHCz79gEQkdq4eHWN6aZPryu/DbvNv7jXVxnors+ZqFqJE2zeWp0Lr5Fu9S3zXl0w7unAxfz766Kltruy+4MXYKr4=|oIUNRpv1nzvfQQunMn70rG260iepVSeX8UmvVFxewcSjdiFPEoqP1LcMevlQBj+6wk8yKWtH8V3A51/JVgA4mWQoqYtaSrsne5wUN0wvb8ejbjgSiyu00wg3EDHSW+enaHNg399iPu+DQ929XVGyHvDYrpXqv3eZwROP+LKZ/2QR5zGZGqPYxiZu6N1rAyK4ZqZvabWLC6rlDLnmA23lXXL4eGhRixbKSmXuTJ7AsIsQbVEi5mkIMM5LIncQDVUqmYzLPNQHESv07WoWjzAb01dox1//kxzVj1k90p7uPaczkhjort48xdoasbqeixSMVA49oa52qgFkDPUTbxioOVdox1//kxzV47Uzt3uucC0ocl1rZ3TPEV0L7rqTMTY2m+KOz89IscKqR7zrrDujO+qPccNnwwSG0Os8Aeb0DoeVhiyjpWW4lrErBXrLWx/HdbHN9FH31ayW2PFVxfFzysochNP2S/1ONE1bx6WsJ2VaUoGP4ptUTzRr7QOQKKfDBUQ/uOZk3QD5MYUy/JQsgDfv2YNd17NV1zESRTLwDE5sxg+BeGVAytstx6xPeBveg0PdvV1Rsh6p8/RQBVIuVxlC4kYCUfMHjiqueqWn4ovuFnT3e1qzZJ10PfG44mwdJrLSNhTPc+fc32ObyDWV4pcRqHqTOVHC8wI895/+IwpvSnVFU1KXnyay0jYUz3PndSO7nNl7cLrZMDmrTpmgJyhv/Pz3Jma7XZEYHjzXrVUUb3uOJmyEgtLSb86Nxext5aZLdb2GG9vgDCZaa9izZZkHmP5I9erDrlU+4B4wiH0=";
            var req1 = IVRGateway.ParseTransactionNotificationRequest("D:/cer/ivr.cer", encrypedReq1);
            Assert.IsTrue(req1 != null);
        }
    }
}
