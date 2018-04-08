using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderPushMessage;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderReward;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler.Order
{
    public class ProcessOrderPushMessage : IJob
    {
        /// <summary>
        ///  根据订单状态，做出不同的推送消息(员工（客服）或者会员)
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "根据订单状态，做出不同的推送消息(员工（客服）或者会员)" });

            var service = new ProcessOrderPushMessageBLL();
            //执行
            service.ProcessOrderPushMessage();
        }
    }
}
