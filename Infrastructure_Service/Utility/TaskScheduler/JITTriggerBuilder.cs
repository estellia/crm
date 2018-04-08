using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;
using System.Configuration;
using Quartz.Impl.Triggers;


namespace JIT.Utility.TaskScheduler
{
    public class JITTriggerBuilder
    {
        /// <summary>
        /// 生成一个重复次数和间隔时间的触发器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="repeat"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static ITrigger CreateSimpleTrigger(string name, string group, int repeat, int interval)
        {
            SimpleTriggerImpl trigger = new SimpleTriggerImpl(name, group, repeat, TimeSpan.FromSeconds(interval));
            return trigger;
        }

        /// <summary>
        /// 生成一个Cro表达式的触发器
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">组名称</param>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        public static ITrigger CreateCroTrigger(string name, string group, string express)
        {
            CronTriggerImpl trigger = new CronTriggerImpl(name, group, express);
            return trigger;
        }

        /// <summary>
        /// 生成一个秒间隔触发器
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">组名称</param>
        /// <param name="interval">间隔</param>
        /// <returns></returns>
        public static ITrigger CreateSecondTrigger(string name, string group, int interval)
        {
            string express = string.Format("*/{0} * * * * ?", interval);
            return CreateCroTrigger(name, group, express);
        }

        /// <summary>
        /// 生成一个天间隔触发器
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">组名称</param>
        /// <param name="hour">时</param>
        /// <param name="minite">分</param>
        /// <returns></returns>
        public static ITrigger CreateDailyTrigger(string name, string group, int hour, int minite)
        {
            string express = string.Format("0 {0} {1} * * ?", minite, hour);
            return CreateCroTrigger(name, group, express);
        }

        /// <summary>
        /// 生成一个周间隔触发器
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">组名称</param>
        /// <param name="dayofweek">星期几（1~7 1=SUN 或 SUN，MON，TUE，WED，THU，FRI，SAT）</param>
        /// <param name="hour">时</param>
        /// <param name="minite">分</param>
        /// <returns></returns>
        public static ITrigger CreateWeeklyTrigger(string name, string group, int dayofweek, int hour, int minite)
        {
            string express = string.Format("0 {0} {1} ? * {2}", minite, hour, dayofweek);
            return CreateCroTrigger(name, group, express);
        }

        /// <summary>
        /// 生成一个月间隔触发器
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">组名称</param>
        /// <param name="dayofmonth">日</param>
        /// <param name="hour">时</param>
        /// <param name="minite">分</param>
        /// <returns></returns>
        public static ITrigger CreateMonthlyTrigger(string name, string group, int dayofmonth, int hour, int minite)
        {
            string express = string.Format("0 {0} {1} {2} * ?", minite, hour, dayofmonth);
            return CreateCroTrigger(name, group, express);
        }
    }
}
