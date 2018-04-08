using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.MessageService.Entity;
using JIT.MessageService;

namespace JIT.TestMessageService
{
    [TestFixture]
    public class SendTest
    {
        [Test]
        public void TestProcess()
        {
            MessageHandler handler = new MessageHandler();
            SMSSendEntity entity = new SMSSendEntity();
            entity.SMSSendNO = 66472;
            entity.MobileNO = "18019438327";
            entity.SMSContent = "测试111";
            var str= handler.Process(entity);
        }
    }
}
