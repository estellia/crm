using JIT.CPOS.BS.BLL.RedisOperationBLL.PrizePools;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Scheduler.PrizePools
{
    public class SetPrizePools : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "后台 生成奖品池队列" });

            var service = new SetRedisPrizePoolsJobBLL();
            //执行
            service.SetRedisPrizePools();
        }
    }
}
