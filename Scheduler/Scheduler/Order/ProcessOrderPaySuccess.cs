using JIT.CPOS.BS.BLL.RedisOperationBLL;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderPaySuccess;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Order
{
    public class ProcessOrderPaySuccess : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "支付成功推送模板消息 开始全量种植" });

            var service = new ProcessOrderPaySuccessMsgJobBLL();
            //执行
            service.ProcessPaySuccessMsg();
        }
    }
}
