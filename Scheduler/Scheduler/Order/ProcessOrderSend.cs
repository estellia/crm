using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderPaySuccess;
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
    public class ProcessOrderSend : IJob
    {
        /// <summary>
        /// APP/后台订单发货-发送微信模板消息 队伍 消息处理
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "APP/后台订单发货-发送微信模板消息 开始从缓存中读取数据，写入到数据库中，以便于下一步发送" });

            var service = new ProcessOrderSendMsgJobBLL();
            //执行
            service.ProcessOrderSendMsg();
        }
    }
}
