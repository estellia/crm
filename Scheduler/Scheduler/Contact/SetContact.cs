using JIT.CPOS.BS.BLL.RedisOperationBLL.Contact;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Contact
{
    public class SetContact : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "后台 触点相关" });

            var service = new RedisContactBLL();
            //执行
            service.GetContact();
        }
    }
}
