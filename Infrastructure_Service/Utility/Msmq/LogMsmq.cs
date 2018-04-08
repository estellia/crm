using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Msmq.Base;
using JIT.Utility.Log;
using System.Messaging;
using JIT.Utility.Msmq.Builder;

namespace JIT.Utility.Msmq
{
    /// <summary>
    /// 日志类消息队列
    /// </summary>
    public class LogMsmq:BaseMSMQ<BaseLogInfo>
    {
        public LogMsmq()
            : base()
        { }
        public LogMsmq(Action<Message> ac)
            : base(ac)
        { }

        public override System.Messaging.MessageQueue CreateMsq()
        {
            return MsmqBulder.Create("TestLog");
        }

        public override System.Messaging.MessageQueue CreateNotifyMsq()
        {
            return MsmqBulder.Create("TestLogNotify");
        }
    }
}
