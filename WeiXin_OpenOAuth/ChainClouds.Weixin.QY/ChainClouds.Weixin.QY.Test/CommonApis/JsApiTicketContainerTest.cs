using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChainClouds.Weixin.QY.CommonAPIs;
using ChainClouds.Weixin.QY.Test.CommonApis;

namespace ChainClouds.Weixin.QY.Test.CommonAPIs
{
    //已测试通过
    [TestClass]
    public class JsApiTicketContainerTest : CommonApiTest
    {
        [TestMethod]
        public void ContainerTest()
        {
            //注册
            JsApiTicketContainer.Register(base._corpId, base._corpSecret);

            //获取Ticket完整结果（包括当前过期秒数）
            var ticketResult = JsApiTicketContainer.GetTicketResult(base._corpId);
            Assert.IsNotNull(ticketResult);

            //只获取Ticket字符串
            var ticket = JsApiTicketContainer.GetTicket(base._corpId);
            Assert.AreEqual(ticketResult.ticket, ticket);

            //getNewTicket
            {
                ticket = JsApiTicketContainer.TryGetTicket(base._corpId, base._corpSecret, false);
                Assert.AreEqual(ticketResult.ticket, ticket);

                ticket = JsApiTicketContainer.TryGetTicket(base._corpId, base._corpSecret, true);
                Assert.AreNotEqual(ticketResult.ticket, ticket);
            }

        }
    }
}
