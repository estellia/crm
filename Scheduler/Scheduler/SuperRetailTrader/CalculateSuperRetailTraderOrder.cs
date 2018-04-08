using JIT.CPOS.BS.BLL.RedisOperationBLL.Order;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.SuperRetailTrader
{
    public class CalculateSuperRetailTraderOrder : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "计算超级分销商分润佣金" });

            var service = new SuperRetailTraderOrderBLL();
            //执行
            service.CalculateSuperRetailTraderOrderJob();
        }
    }
}
