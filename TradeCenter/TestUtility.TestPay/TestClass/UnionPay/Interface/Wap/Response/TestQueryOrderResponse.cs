/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/25 13:08:38
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
using JIT.Utility.Pay.UnionPay.Interface.Wap.Response;
using JIT.Utility.Pay.UnionPay.Interface.Wap.ValueObject;

namespace JIT.TestUtility.TestPay.TestClass.UnionPay.Interface.Wap.Response
{
    /// <summary>
    /// TestQueryOrderResponse  
    /// </summary>
    [TestFixture]
    public class TestQueryOrderResponse
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestQueryOrderResponse()
        {
        }
        #endregion

        [Test]
        public void TestProperties()
        {
            string strRsp = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><upbp application=\"MTransInfo.Rsp\" version=\"1.0.0\" sendTime=\"20131225130703\" sendSeqId=\"a4d678595e4c4b73b341e30d97c29109\">    <transType>02</transType>    <merchantName>WAP测试</merchantName>    <merchantId>630056832596</merchantId>    <merchantOrderId>237e8f7c496f460ca57970a0793e5a5e</merchantOrderId>    <merchantOrderTime>20131225130202</merchantOrderTime>    <respCode>0000</respCode>    <respDesc>操作成功</respDesc>    <queryResult>1</queryResult>    <settleDate></settleDate>    <setlCurrency></setlCurrency>    <cupsQid></cupsQid>    <cupsTraceNum></cupsTraceNum>    <cupsTraceTime></cupsTraceTime>    <cupsRespCode>84</cupsRespCode>    <cupsRespDesc></cupsRespDesc></upbp>";
            var rsp1 = new QueryOrderResponse();
            rsp1.Load(strRsp);
            Assert.IsTrue(rsp1.TransType == WapTransTypes.PreAuthorization);
            Assert.IsTrue(rsp1.MerchantName == "WAP测试");
            Assert.IsTrue(rsp1.MerchantID == "630056832596");
            Assert.IsTrue(rsp1.MerchantOrderID == "237e8f7c496f460ca57970a0793e5a5e");
            Assert.IsTrue(rsp1.MerchantOrderTime == new DateTime(2013, 12, 25, 13, 2, 2));
            Assert.IsTrue(rsp1.Code == "0000");
            Assert.IsTrue(rsp1.Description == "操作成功");
            Assert.IsTrue(rsp1.QueryResult == OrderQueryResults.Failed);
            Assert.IsTrue(rsp1.SettleDate == string.Empty);
            Assert.IsTrue(rsp1.SetlCurrency == null);
            Assert.IsTrue(rsp1.CupsQid == string.Empty);
            Assert.IsTrue(rsp1.CupsTraceNum == string.Empty);
            Assert.IsTrue(rsp1.CupsRespCode == "84");
            Assert.IsTrue(rsp1.CupsRespDesc == string.Empty);
            Assert.IsTrue(rsp1.ConverRate == null);
            Assert.IsTrue(rsp1.SetlAmt == null);

        }
    }
}
