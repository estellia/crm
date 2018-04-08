using JIT.TradeCenter.BLL;
using JIT.Utility;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Pay
{
    public class PayCenterNotify : IJob
    {
        public void Execute(IJobExecutionContext context)
        {

            //Loggers.Debug(new DebugLogInfo() { Message = "支付中心通知失败重新通知到业务平台" });

            OrderQueryBLL orderQueryBLL = new OrderQueryBLL(new BasicUserInfo());

            orderQueryBLL.PayCenterNotify();//支付中心通知失败重新通知到业务平台

            //Loggers.Debug(new DebugLogInfo() { Message = "支付中心通知失败重新通知到业务平台" });

            
        }
    }

}