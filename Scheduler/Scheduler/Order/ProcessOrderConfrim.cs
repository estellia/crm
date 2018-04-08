using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderNotPay;
using JIT.Utility.Log;
using Quartz;

namespace Scheduler.Order
{
    public class ProcessOrderConfrim : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() {Message = "订单完成后，15天用户未确认的"});

            var service = new ProcessOrderNotPayMsgJobBLL();
            //执行
            service.ProcessNotPayMsg();
        }
    }
}
