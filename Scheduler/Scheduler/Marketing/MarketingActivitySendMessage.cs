using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Marketing
{
    public class MarketingActivitySendMessage  : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
			//Loggers.Debug(new DebugLogInfo() { Message = "营销活动发送消息开始" });
           
			//var Service = ActivityJobBLL.CreateInstance();
			////执行
			//Service.MarketingActivitySendMessigeMethod();


        }
    }
}
