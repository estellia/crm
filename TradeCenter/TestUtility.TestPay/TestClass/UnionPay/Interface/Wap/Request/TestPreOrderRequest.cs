/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/20 15:54:17
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
    /// TestCreateOrderRequest  
    /// </summary>
    [TestFixture]
    public class TestPreOrderRequest
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestPreOrderRequest()
        {
        }
        #endregion

        [Test]
        public void TestGetContent()
        {
            PreOrderRequest req1 = new PreOrderRequest();
            req1.SendTime = DateTime.Now;
            req1.SendSeqID = "12345888888000";
            req1.FrontUrl = "http://www.163.com";
            req1.MerchantOrderDesc = "呵呵";
            req1.Misc = string.Empty;
            //req1.GatewayType = GatewayTypes.WAP;
            req1.TransTimeout = DateTime.Now.AddHours(1);
            req1.BackUrl = "http://192.168.1.156:8080/Test/Notify";
            req1.MerchantOrderCurrency = Currencys.RMB;
            req1.MerchantOrderAmt = 1;
            req1.MerchantID = "630056832596";
            req1.MerchantOrderTime = DateTime.Now.AddMinutes(-5);
            req1.MerchantOrderID = Guid.NewGuid().ToString("N");
            req1.MerchantUserID = string.Empty;
            req1.MobileNum = "15388157741";
            req1.CarNum = string.Empty;

            var content1 = req1.GetContent();
            Assert.IsTrue(content1 != null);
        }
    }
}
