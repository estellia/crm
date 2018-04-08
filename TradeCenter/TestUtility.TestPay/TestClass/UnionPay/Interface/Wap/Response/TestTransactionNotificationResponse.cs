/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/27 13:47:06
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

namespace JIT.TestUtility.TestPay.TestClass.UnionPay.Interface.Wap.Response
{
    /// <summary>
    /// TestTransactionNotificationResponse  
    /// </summary>
    [TestFixture]
    public class TestTransactionNotificationResponse
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestTransactionNotificationResponse()
        {
        }
        #endregion

        [Test]
        public void TestGetContent()
        {
            var okRsp = TransactionNotificationResponse.OK;
            var rs1 = okRsp.GetContent();
            Assert.IsTrue(rs1 != null);

            var failedRsp = TransactionNotificationResponse.FAILED;
            var rs2 = failedRsp.GetContent();
            Assert.IsTrue(rs2 != null);
        }
    }
}
