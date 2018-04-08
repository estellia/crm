using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;

namespace JIT.Utility.TaskScheduler
{
    [Serializable]
    public class JITTaskJob
    {
        public JITTaskJob(IJobDetail pJob, ITrigger pTrigger, JobOperationType pType)
        {
            Job = pJob;
            Trigger = pTrigger;
            OperationType = pType;
        }

        public JITTaskJob(IJobDetail pJob, ITrigger pTrigger)
        {
            Job = pJob;
            Trigger = pTrigger;
        }

        /// <summary>
        /// 操作类型（只在作为序列化调用时使用）
        /// </summary>
        public JobOperationType OperationType { get; set; }
        public IJobDetail Job { get; private set; }
        public ITrigger Trigger { get; private set; }
        public JobKey JobKey { get { return Job.Key; } }
        public TriggerKey TriggerKey { get { return Trigger.Key; } }
    }
}
