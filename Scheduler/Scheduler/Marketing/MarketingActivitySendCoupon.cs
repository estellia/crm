
using JIT.CPOS.Web;
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
    public class MarketingActivitySendCoupon:IJob
    {
        public void Execute(IJobExecutionContext context)
        {
			//Loggers.Debug(new DebugLogInfo() { Message = "营销活动送券开始" });
            
			//var Service = ActivityJobBLL.CreateInstance();
			////执行
			//Service.MarketingActivityMethod();
        }
    }
}
