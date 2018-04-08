using System;
using JIT.CPOS.BS.BLL.RedisOperationBLL;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderPaySuccess;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderNotPay;

namespace Scheduler.Order
{
    public class ProcessOrderNotPayGet : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "订单未付款推送模板消息 开始读取发送" });

            var service = new  ProcessOrderNotPayMsgJobBLL();
            //执行
            service.ProcessNotPayMsg();
        }
    }
}
