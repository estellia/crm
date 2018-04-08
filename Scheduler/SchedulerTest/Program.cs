using JIT.CPOS.Web;
using Quartz;
using Quartz.Impl;
using Scheduler.Coupon;
using Scheduler.Marketing;
using Scheduler.Order;
using Scheduler.Role;
using Scheduler.VipGold;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("作业执行，jobSays:");



            //从工厂中获取一个调度器实例化
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();       //开启调度器

            //==========例子1（简单使用）===========

            IJobDetail job1 = JobBuilder.Create<VipConsumeForUpgradeOrder>()  //创建一个作业
                .WithIdentity("myJob", "group1")
                .Build();

            ITrigger trigger1 = TriggerBuilder.Create()
                                        .WithIdentity("触发器名称", "触发器组")
                                        .StartNow()                        //现在开始
                                        .WithSimpleSchedule(x => x         //触发时间，5秒一次。
                                            .WithIntervalInSeconds(50)
                                            .WithRepeatCount(0))
                                            //.RepeatForever())              //不间断重复执行
                                        .Build();


            scheduler.ScheduleJob(job1, trigger1);      //把作业，触发器加入调度器。


            //==========例子2 (执行时 作业数据传递，时间表达式使用)===========

            //IJobDetail job2 = JobBuilder.Create<HelloJob2>()
            //                            .WithIdentity("myJob2", "group1")
            //                            .UsingJobData("jobSays", "Hello World!")
            //                            .Build();


            //ITrigger trigger2 = TriggerBuilder.Create()
            //                            .WithIdentity("mytrigger", "group1")
            //                            .StartNow()
            //                            .WithCronSchedule("/5 * * ? * *")    //时间表达式，5秒一次     
            //                            .Build();

            //ITrigger trigger2 = TriggerBuilder.Create()
            //                            .WithIdentity("mytrigger", "group1")
            //                            .StartNow()
            //                            .WithCronSchedule("0 */1 * * * ?")    //时间表达式，1分钟一次 
            //                            .Build();
            //ITrigger trigger2 = TriggerBuilder.Create()
            //                            .WithIdentity("mytrigger", "group1")
            //                            .StartNow()
            //                            .WithCronSchedule("0 */30 * * * ?")    //时间表达式，30分钟一次 
            //                            .Build();

            //ITrigger trigger2 = TriggerBuilder.Create()
            //                            .WithIdentity("mytrigger", "group1")
            //                            .StartNow()
            //                            .WithCronSchedule("0 0 1 * * ?")    //时间表达式，30分钟一次 
            //                            .Build();

            //IJobDetail SendCouponJob = JobBuilder.Create<MarketingActivitySendCoupon>()
            //                           .WithIdentity("SendCouponJob", "group1")
            //                           .UsingJobData("jobSays", "Hello World!")
            //                           .Build();


            //ITrigger SendCouponJobTrigger = TriggerBuilder.Create()
            //                            .WithIdentity("SendCouponJobTrigger", "group1")
            //                            .StartNow()
            //                            .WithCronSchedule("/20 * * ? * *")    //时间表达式，15秒一次     
            //                            .Build();

            //scheduler.ScheduleJob(SendCouponJob, SendCouponJobTrigger);

            //IJobDetail SendMessageJob = JobBuilder.Create<MarketingActivitySendMessage>()
            //                           .WithIdentity("SendMessageJob", "group1")
            //                           .UsingJobData("jobSays", "Hello World!")
            //                           .Build();


            //ITrigger SendMessageJobTrigger = TriggerBuilder.Create()
            //                            .WithIdentity("SendMessageJobTrigger", "group1")
            //                            .StartNow()
            //                            .WithCronSchedule("/50 * * ? * *")    //时间表达式，50秒一次     
            //                            .Build();

            //scheduler.ScheduleJob(SendMessageJob, SendMessageJobTrigger);


            //var Service = ActivityJobBLL.CreateInstance();
            ////执行
            ////Service.MarketingActivityMethod();
            //Service.MarketingActivitySendMessigeMethod();


   //         IJobDetail vipMappingCouponJob = JobBuilder.Create<VipMappingCoupon>()
			//																		 .WithIdentity("vipMappingCouponJob", "group1")
			//																		 .UsingJobData("jobSays", "Hello World!")
			//																		 .Build();

			//ITrigger vipMappingCouponJobTrigger = TriggerBuilder.Create()
			//																					.WithIdentity("vipMappingCouponJobTrigger", "group1")
			//																					.StartNow()
			//																					.WithCronSchedule("/20 * * ? * *")   //  10s 一次
			//																					.Build();

			//scheduler.ScheduleJob(vipMappingCouponJob, vipMappingCouponJobTrigger);



            ////scheduler.Shutdown();         //关闭调度器。
			//IJobDetail ProcessOrderPushMessageJob = JobBuilder.Create<ProcessOrderPushMessage>()
			//															.WithIdentity("ProcessOrderPushMessageJob", "group1")
			//															.UsingJobData("jobSays", "Hello World!")
			//															.Build();

			//ITrigger ProcessOrderPushMessageJobTrigger = TriggerBuilder.Create()
			//																					.WithIdentity("ProcessOrderPushMessageJobTrigger", "group1")
			//																					.StartNow()
			//																					.WithCronSchedule("/30 * * ? * *")   //  30s 一次
			//																					.Build();
			//scheduler.ScheduleJob(ProcessOrderPushMessageJob, ProcessOrderPushMessageJobTrigger);
			////=================计算售卡分润====================
			//IJobDetail calculateSalesVipCardOrderJob = JobBuilder.Create<SalesVipCardOrder>()
			//																		.WithIdentity("calculateSalesVipCardOrderJob", "group1")
			//																		.UsingJobData("jobSays", "Hello World!")
			//																		.Build();

			//ITrigger calculateSalesVipCardOrderJobTrigger = TriggerBuilder.Create()
			//																					.WithIdentity("calculateSalesVipCardOrderJobTrigger", "group1")
			//																					.StartNow()
			//																					.WithCronSchedule("/40 * * ? * *")   //  40s 一次
			//																					.Build();
			//scheduler.ScheduleJob(calculateSalesVipCardOrderJob, calculateSalesVipCardOrderJobTrigger);
			////=================计算充值分润====================
			//IJobDetail CalculateRechargeOrderJob = JobBuilder.Create<RechargeOrder>()
			//																		.WithIdentity("CalculateRechargeOrderJob", "group1")
			//																		.UsingJobData("jobSays", "Hello World!")
			//																		.Build();

			//ITrigger CalculateRechargeOrderJobTrigger = TriggerBuilder.Create()
			//																					.WithIdentity("CalculateRechargeOrderJobTrigger", "group1")
			//																					.StartNow()
			//																					.WithCronSchedule("/40 * * ? * *")   //  40s 一次
			//																					.Build();
			//scheduler.ScheduleJob(CalculateRechargeOrderJob, CalculateRechargeOrderJobTrigger);
			//=================消费升级====================
			//IJobDetail CalculateVipConsumeForUpgradeJob = JobBuilder.Create<VipConsumeForUpgradeOrder>()
			//																		.WithIdentity("CalculateVipConsumeForUpgradeJob", "group1")
			//																		.UsingJobData("jobSays", "Hello World!")
			//																		.Build();

			//ITrigger CalculateVipConsumeForUpgradeJobTrigger = TriggerBuilder.Create()
			//																					.WithIdentity("CalculateVipConsumeForUpgradeJobTrigger", "group1")
			//																					.StartNow()
			//																					.WithCronSchedule("/40 * * ? * *")   //  40s 一次
			//																					.Build();
			//scheduler.ScheduleJob(CalculateVipConsumeForUpgradeJob, CalculateVipConsumeForUpgradeJobTrigger);



        }
    }
}
