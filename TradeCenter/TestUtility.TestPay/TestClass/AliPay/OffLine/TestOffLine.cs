using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.Alipay.ValueObject;
using JIT.Utility.Pay.Alipay.Interface.Offline;
using JIT.Utility.Pay.Alipay.Interface.Offline.CreateAndPay;
using JIT.Utility.Pay.Alipay.Interface.Offline.QRCodePre;
using JIT.Utility.Pay.Alipay.Channel;
using JIT.Utility.ExtensionMethod;

namespace JIT.TestUtility.TestPay.TestClass.AliPay.OffLine
{
    [TestFixture]
    public class TestOffLine
    {
        AliPayChannel channel = new AliPayChannel()
        {
            Partner = "2088901596405274",
            MD5Key = "z4hj1a4dzuk0d459ha3bhp236nxt9dng",
            AgentID = "8582928j2"
        };

        [Test]
        public void TestQRPay()
        {
            var channelstr = channel.ToJSON();
            OfflineQRCodePreRequest request = new OfflineQRCodePreRequest(channel)
            {
                Subject = "测试",
                TotalFee = "0.01",
                OutTradeNo = Guid.NewGuid().ToString().Replace("-", ""),
            };
            var t = AliPayOffLineGeteway.OfflineQRPay(request);
            Console.WriteLine(t.SmallPicUrl);
        }

        [Test]
        public void TestCreateAndPay()
        {
            CreateAndPayRequest request = new CreateAndPayRequest(channel)
            {
                Subject = "测试",
                TotalFee = "0.01",
                OutTradeNo = Guid.NewGuid().ToString().Replace("-", "")
            };
            var t = AliPayOffLineGeteway.OfflineCreateAndPay(request);
            Console.WriteLine(t);
        }
    }
}
