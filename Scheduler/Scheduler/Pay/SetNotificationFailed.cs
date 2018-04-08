
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIT.Utility.Web;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.TradeCenter.BLL;
using JIT.Utility;


namespace Scheduler.Pay
{
    public class SetNotificationFailed : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //Loggers.Debug(new DebugLogInfo() { Message = "第三方支付通知失败处理开始" });

            OrderQueryBLL orderQueryBLL = new OrderQueryBLL(new BasicUserInfo());
            orderQueryBLL.SetNotificationFailed();//第三方支付通知失败处理

            //Loggers.Debug(new DebugLogInfo() { Message = "第三方支付通知失败处理结束" });

        }
    }

}
