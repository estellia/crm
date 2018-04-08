using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIT.Utility.Log;
using Quartz;
using JIT.CPOS.BS.BLL.RedisOperationBLL.BasicSetting;

namespace Scheduler.BasicSetting
{
    public class SetBasicSettingCache : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "后台 BasicSetting 开始全量种植" });

            var Service = new SetBasicSettingJobBLL();
            //执行
            Service.TimeSetBasicSetting();
        }
    }
}
