/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/24 17:04:26
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
using JIT.Utility.Pay.UnionPay.Interface.Wap.Response;
using JIT.Utility.Pay.UnionPay.Interface.Wap.ValueObject;

namespace JIT.TestUtility.TestPay.TestClass.UnionPay.Interface.Wap.Response
{
    /// <summary>
    /// PreOrderResponse  
    /// </summary>
    [TestFixture]
    public class TestPreOrderResponse
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestPreOrderResponse()
        {
        }
        #endregion

        [Test]
        public void TestProperties()
        {
            string strRsp = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><upbp application=\"MGw.Rsp\" version=\"1.0.0\" sendTime=\"20131224163540\" sendSeqId=\"d1be6b43fdfa490caf86e860ce198c07\">    <merchantName>WAP测试</merchantName>    <merchantId>630056832596</merchantId>    <merchantOrderId>e4464c61975447fbb29bffb1678354d3</merchantOrderId>    <merchantOrderTime>20131224163040</merchantOrderTime>    <merchantOrderAmt>1</merchantOrderAmt>    <merchantOrderCurrency>156</merchantOrderCurrency>    <gwInvokeCmd>http://58.246.136.11:8089/wapDetect/sgw/35c1509bba9681517c1e68e1a8da16071253728194722450</gwInvokeCmd>    <merchantUserId></merchantUserId>    <mobileNum>15388157741</mobileNum>    <cardNum></cardNum>    <misc></misc>    <respCode>0000</respCode>    <respDesc>处理成功</respDesc></upbp>";
            //string strRsp = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><upbp application=\"MGw.Rsp\" version=\"1.0.0\" sendTime=\"20131224184127\" sendSeqId=\"c1aa5cc334bf463dba2fda13dbdbb24b\">    <merchantName>WAP测试</merchantName>    <merchantId>630056832596</merchantId>    <merchantOrderId>40371b7bb2b14bb6bd615422de156d04</merchantOrderId>    <merchantOrderTime>20131224183627</merchantOrderTime>    <merchantOrderAmt>1</merchantOrderAmt>    <merchantOrderCurrency>156</merchantOrderCurrency>    <gwInvokeCmd>http://58.246.136.11:8089/wapDetect/sgw/62d024a23a39300662dc932a9859268b1253728194722463</gwInvokeCmd>    <merchantUserId></merchantUserId>    <mobileNum>15388157741</mobileNum>    <cardNum></cardNum>    <misc></misc>    <respCode>0000</respCode>    <respDesc>处理成功</respDesc></upbp>";
            var rsp1 = new PreOrderResponse();
            rsp1.Load(strRsp);
            Assert.IsTrue(rsp1.MerchantName == "WAP测试");
            Assert.IsTrue(rsp1.MerchantID == "630056832596");
            Assert.IsTrue(rsp1.MerchantOrderID == "e4464c61975447fbb29bffb1678354d3");
            Assert.IsTrue(rsp1.MerchantOrderTime == new DateTime(2013,12,24,16,30,40));
            Assert.IsTrue(rsp1.MerchantOrderAmt == 1);
            Assert.IsTrue(rsp1.MerchantOrderCurrency == Currencys.RMB);
            Assert.IsTrue(rsp1.RedirectURL == "http://58.246.136.11:8089/wapDetect/sgw/35c1509bba9681517c1e68e1a8da16071253728194722450");
            Assert.IsTrue(rsp1.MerchantUserID == string.Empty);
            Assert.IsTrue(rsp1.MobileNum == "15388157741");
            Assert.IsTrue(rsp1.CarNum == string.Empty);
            Assert.IsTrue(rsp1.Misc == string.Empty);
            Assert.IsTrue(rsp1.Code == "0000");
            Assert.IsTrue(rsp1.Description == "处理成功");
        }
    }
}
