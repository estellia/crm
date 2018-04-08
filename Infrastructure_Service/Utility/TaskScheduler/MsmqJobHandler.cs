using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Msmq;
using System.Messaging;
using JIT.Utility.Log;

namespace JIT.Utility.TaskScheduler
{
    public class MsmqJobHandler
    {
        public MsmqJobHandler()
        {
            msmq = new JobMsmq(ProcessJob);
        }

        JobMsmq msmq;

        public void Run()
        {
            msmq.Listen();
        }

        public void ProcessJob(Message msg)
        {
            var job = msg.Body as JITTaskJob;
            if (job != null)
            {
                switch (job.OperationType)
                {
                    case JobOperationType.Add:
                        Scheduler.ScheduleJob(job);
                        break;
                    case JobOperationType.Delete:
                        Scheduler.DeleteJob(job);
                        break;
                    case JobOperationType.Pause:
                        Scheduler.PauseJob(job);
                        break;
                    case JobOperationType.Resume:
                        Scheduler.ResumeJob(job);
                        break;
                    default:
                        break;
                }
            }
            else
                Loggers.Exception(new ExceptionLogInfo(new Exception("不能转换成JitJob,真实类型为:" + msg.Body.GetType().Name)));
        }
    }
}
