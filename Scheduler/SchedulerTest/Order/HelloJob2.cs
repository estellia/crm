using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class HelloJob2 : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("作业执行2"+DateTime.Now);

            Loggers.Debug(new DebugLogInfo() { Message = "作业执行2" });
        }
    }

}