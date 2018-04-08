/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/26 16:43:53
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
using System.Text;

using NUnit.Framework;
using JIT.Utility.Pay.UnionPay.ValueObject;
using JIT.Utility.Pay.UnionPay.Interface.Wap.Request;
using JIT.Utility.Pay.UnionPay.Interface.Wap.ValueObject;

namespace JIT.TestUtility.TestPay.TestClass.UnionPay.Interface.Wap.Request
{
    /// <summary>
    /// TestTransactionNotificationRequest  
    /// </summary>
    [TestFixture]
    public class TestTransactionNotificationRequest
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestTransactionNotificationRequest()
        {
        }
        #endregion

        [Test]
        public void TestProperties()
        {
            string strReq1 = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><upbp application=\"MTransNotify.Req\" version =\"1.0.0\" sendTime =\"20131226154125\" sendSeqId =\"13122615412589571\"><transType>01</transType><merchantId>630056832596</merchantId><merchantOrderId>dc10ae3119014d8180b9f8780398af45</merchantOrderId><merchantOrderAmt>1</merchantOrderAmt><settleDate>1226</settleDate><setlAmt>1</setlAmt><setlCurrency>156</setlCurrency><converRate>null</converRate><cupsQid>201312261540170019212</cupsQid><cupsTraceNum>001921</cupsTraceNum><cupsTraceTime>1226154017</cupsTraceTime><cupsRespCode>40</cupsRespCode><cupsRespDesc></cupsRespDesc></upbp>";
            TransactionNotificationRequest req1 = new TransactionNotificationRequest();
            req1.Load(strReq1);
            Assert.IsTrue(req1.ConverRate == null);
            Assert.IsTrue(req1.CupsQid == "201312261540170019212");
            Assert.IsTrue(req1.CupsRespCode == "40");
            Assert.IsTrue(req1.CupsRespDesc == string.Empty);
            Assert.IsTrue(req1.CupsTraceNum == "001921");
            Assert.IsTrue(req1.CupsTraceTime == "1226154017");
            Assert.IsTrue(req1.MerchantID == "630056832596");
            Assert.IsTrue(req1.MerchantOrderAmt == 1);
            Assert.IsTrue(req1.MerchantOrderID == "dc10ae3119014d8180b9f8780398af45");
            Assert.IsTrue(req1.SetlAmt == 1);
            Assert.IsTrue(req1.SetlCurrency == Currencys.RMB);
            Assert.IsTrue(req1.SettleDate == "1226");
            Assert.IsTrue(req1.TransType == WapTransTypes.Consumption);

        }

        [Test]
        public void TestGetContent()
        {
            string strReq1 = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><upbp application=\"MTransNotify.Req\" version =\"1.0.0\" sendTime =\"20131226154125\" sendSeqId =\"13122615412589571\"><transType>01</transType><merchantId>630056832596</merchantId><merchantOrderId>dc10ae3119014d8180b9f8780398af45</merchantOrderId><merchantOrderAmt>1</merchantOrderAmt><settleDate>1226</settleDate><setlAmt>1</setlAmt><setlCurrency>156</setlCurrency><converRate>null</converRate><cupsQid>201312261540170019212</cupsQid><cupsTraceNum>001921</cupsTraceNum><cupsTraceTime>1226154017</cupsTraceTime><cupsRespCode>40</cupsRespCode><cupsRespDesc></cupsRespDesc></upbp>";
            TransactionNotificationRequest req1 = new TransactionNotificationRequest();
            req1.Load(strReq1);

            var strReq2 = req1.GetContent();
            Assert.IsTrue(strReq1.Replace(" ", string.Empty).Replace("\r\n", string.Empty) == strReq2.Replace(" ", string.Empty).Replace("\r\n", string.Empty));
        }
    }
}
