using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;

namespace JIT.Utility.TaskScheduler
{
    public class JITJobBuilder
    {
        public static IJobDetail CreateJob<T>(string name, string group)
        {
            return JobBuilder.Create(typeof(T)).WithIdentity(name, group).Build();
        }
    }
}
