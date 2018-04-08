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


namespace ConsoleApplication1
{
    public class HelloJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("作业执行1"+DateTime.Now);

            Loggers.Debug(new DebugLogInfo() { Message = "作业执行1" });


            Loggers.Debug(new DebugLogInfo() { Message = "第三方支付通知失败处理,开始-" + DateTime.Now });

            OrderQueryBLL orderQueryBLL = new OrderQueryBLL(new BasicUserInfo());
            orderQueryBLL.SetNotificationFailed();//第三方支付通知失败处理

            Loggers.Debug(new DebugLogInfo() { Message = "第三方支付通知失败处理,结束-" + DateTime.Now });



        }
    }
}