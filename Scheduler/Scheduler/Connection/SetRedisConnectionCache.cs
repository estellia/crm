
using JIT.CPOS.BS.BLL.RedisOperationBLL.Connection;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Scheduler.Connection
{
   public  class SetRedisConnectionCache : IJob
    {
       public void Execute(IJobExecutionContext context)
       {
           Loggers.Debug(new DebugLogInfo() { Message = "商户数据库链接 开始全量种植" });

           var Service = new SetConnectionJobBLL();
           //执行
           Service.AutoSetConnectionCache();
       }

    }
}
