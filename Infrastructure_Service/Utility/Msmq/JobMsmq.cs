using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Msmq.Base;
using JIT.Utility.TaskScheduler;
using System.Messaging;
using JIT.Utility.Msmq.Builder;

namespace JIT.Utility.Msmq
{
    /// <summary>
    /// 任务调度消息队列类
    /// </summary>
    public class JobMsmq:BaseMSMQ<JITTaskJob>
    {
        public JobMsmq()
            : base()
        { }

        public JobMsmq(Action<Message> ac)
            : base(ac)
        { }
        /// <summary>
        /// 创建任务调度类消息缓存队列
        /// </summary>
        /// <returns></returns>
        public override MessageQueue CreateMsq()
        {
            return MsmqBulder.Create("TestJob");
        }

        /// <summary>
        /// 创建任务调度类消息通知队列
        /// </summary>
        /// <returns></returns>
        public override MessageQueue CreateNotifyMsq()
        {
            return MsmqBulder.Create("TestJobNotify");
        }
    }
}
