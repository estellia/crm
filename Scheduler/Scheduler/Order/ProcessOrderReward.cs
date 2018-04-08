using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderPaySuccess;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderReward;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderSend;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Order
{
    public class ProcessOrderReward : IJob
    {
        /// <summary>
        /// 确认收货时处理积分、返现、佣金 队伍 消息处理
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "确认收货时处理积分、返现、佣金 开始从缓存中读取数据，写入到数据库中，以便于下一步发送" });

            var service = new ProcessOrderRewardMsgJobBLL();
            //执行
            service.ProcessOrderRewardMsg();
        }
    }
}
