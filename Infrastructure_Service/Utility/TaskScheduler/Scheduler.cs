using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;
using JIT.Utility.Msmq;

namespace JIT.Utility.TaskScheduler
{
    public class Scheduler
    {
        static Scheduler()
        {
            Default = StdSchedulerFactory.GetDefaultScheduler();
            Default.Start();
        }

        private static readonly IScheduler Default = null;

        /// <summary>
        /// 预定一个作业
        /// </summary>
        /// <param name="pJob"></param>
        /// <returns></returns>
        public static DateTimeOffset ScheduleJob(JITTaskJob pJob)
        {
            return Default.ScheduleJob(pJob.Job, pJob.Trigger);
        }

        /// <summary>
        /// 取消预定一个作业
        /// </summary>
        /// <param name="pJob"></param>
        /// <returns></returns>
        public static bool UnscheduleJob(JITTaskJob pJob)
        {
            return Default.UnscheduleJob(pJob.Trigger.Key);
        }

        /// <summary>
        /// 暂停作业
        /// </summary>
        /// <param name="pJob"></param>
        public static void PauseJob(JITTaskJob pJob)
        {
            Default.PauseJob(pJob.JobKey);
        }

        /// <summary>
        /// 继续作业
        /// </summary>
        /// <param name="pJob"></param>
        public static void ResumeJob(JITTaskJob pJob)
        {
            Default.ResumeJob(pJob.JobKey);
        }

        /// <summary>
        /// 删除作业
        /// </summary>
        /// <param name="pJob"></param>
        public static void DeleteJob(JITTaskJob pJob)
        {
            Default.DeleteJob(pJob.JobKey);
        }
    }
}
