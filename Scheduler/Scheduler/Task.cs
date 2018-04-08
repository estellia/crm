using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using JIT.TradeCenter.BLL;
using JIT.Utility;
using Quartz;
using Quartz.Impl;
using Scheduler.Pay;
using JIT.Utility.Log;
using Scheduler.Marketing;
using Scheduler.Role;
using Scheduler.Order;
using Scheduler.Coupon;
using Scheduler.Contact;
using Scheduler.BasicSetting;
using Scheduler.Connection;
using Scheduler.PrizePools;
using Scheduler.SuperRetailTrader;
using Scheduler.VipGold;

namespace Scheduler
{
    public partial class Task : ServiceBase
    {
        public Task()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "onstart" });

            ControlCenter();//调用控制中心
        }
        protected override void OnStop()
        {

        }
        /// <summary>
        /// 控制中心
        /// </summary>
        protected void ControlCenter()
        {
            Loggers.Debug(new DebugLogInfo() { Message = "ControlCenter" });

            try
            {
                //从工厂中获取一个调度器实例化
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();       //开启调度器

                //================第三方支付通知失败处理==============================

                IJobDetail setNotificationFailedJob = JobBuilder.Create<SetNotificationFailed>()
                                            .WithIdentity("setNotificationFailedJob", "group1")
                                            .UsingJobData("jobSays", "Hello World!")
                                            .Build();

                ITrigger setNotificationFailedTrigger = TriggerBuilder.Create()
                                 .WithIdentity("setNotificationFailedTrigger", "group1")
                                 .StartNow()
                                 .WithCronSchedule("0 */30 * * * ?")    //时间表达式，30分钟一次 
                                 .Build();
                scheduler.ScheduleJob(setNotificationFailedJob, setNotificationFailedTrigger);    //把作业、触发器加入调度器

                //===============支付中心通知失败重新通知到业务平台=================
                IJobDetail payCenterNotifyJob = JobBuilder.Create<PayCenterNotify>()
                                       .WithIdentity("payCenterNotifyJob", "group1")
                                       .UsingJobData("jobSays", "Hello World!")
                                       .Build();


                ITrigger payCenterNotifyJobTrigger = TriggerBuilder.Create()
                                            .WithIdentity("payCenterNotifyJobTrigger", "group1")
                                            .StartNow()
                                            .WithCronSchedule("/5 * * ? * *")    //时间表达式，5秒一次     
                                            .Build();

                scheduler.ScheduleJob(payCenterNotifyJob, payCenterNotifyJobTrigger);

                // -----------------------------------------------------------------------------------------------------
                //===============营销活动送券=================
				//IJobDetail SendCouponJob = JobBuilder.Create<MarketingActivitySendCoupon>()
				//					   .WithIdentity("SendCouponJob", "group1")
				//					   .UsingJobData("jobSays", "Hello World!")
				//					   .Build();


				//ITrigger SendCouponJobTrigger = TriggerBuilder.Create()
				//							.WithIdentity("SendCouponJobTrigger", "group1")
				//							.StartNow()
				//							.WithCronSchedule("0 */3 * * * ?")    //3分钟     
				//							.Build();

				//scheduler.ScheduleJob(SendCouponJob, SendCouponJobTrigger);

				////===============营销活动发消息=================
				//IJobDetail SendMessageJob = JobBuilder.Create<MarketingActivitySendMessage>()
				//					   .WithIdentity("SendMessageJob", "group1")
				//					   .UsingJobData("jobSays", "Hello World!")
				//					   .Build();


				//ITrigger SendMessageJobTrigger = TriggerBuilder.Create()
				//							.WithIdentity("SendMessageJobTrigger", "group1")
				//							.StartNow()
				//							.WithCronSchedule("0 */3 * * * ?")    //3分钟     
				//							.Build();

				//scheduler.ScheduleJob(SendMessageJob, SendMessageJobTrigger);

                ////=================角色菜单缓存种植====================
                IJobDetail setRoleMenuJob = JobBuilder.Create<SetRedisRoleCache>()
                                                                            .WithIdentity("setRoleMenuJob", "group1")
                                                                            .UsingJobData("jobSays", "Hello World!")
                                                                            .Build();

                ITrigger setRoleMenuJobTrigger = TriggerBuilder.Create()
                                                                                            .WithIdentity("setRoleMenuJobTrigger", "group1")
                                                                                            .StartNow()
                                                                                            .WithCronSchedule("0 0 1 * * ?")  //  每天 1点 触发
                                                                                            .Build();

                scheduler.ScheduleJob(setRoleMenuJob, setRoleMenuJobTrigger);

                ////=================订单支付完成 队列 消息处理====================
                IJobDetail processPaySuccessJob = JobBuilder.Create<ProcessOrderPaySuccess>()
                                                                                        .WithIdentity("processPaySuccessJob", "group1")
                                                                                        .UsingJobData("jobSays", "Hello World!")
                                                                                        .Build();

                ITrigger processPaySuccessJobTrigger = TriggerBuilder.Create()
                                                                                                    .WithIdentity("processPaySuccessJobTrigger", "group1")
                                                                                                    .StartNow()
                                                                                                    .WithCronSchedule("/10 * * ? * *")   //  10s 一次
                                                                                                    .Build();

                scheduler.ScheduleJob(processPaySuccessJob, processPaySuccessJobTrigger);

                ////=================订单服务完成后，15天用户未确认，自动确认处理====================
                IJobDetail processOrderConfrim = JobBuilder.Create<ProcessOrderConfrim>()
                                                                                        .WithIdentity("processOrderConfrim", "group1")
                                                                                        .UsingJobData("jobSays", "Hello World!")
                                                                                        .Build();

                ITrigger processOrderConfrimTrigger = TriggerBuilder.Create()
                                                                                                    .WithIdentity("processOrderConfrimTrigger", "group1")
                                                                                                    .StartNow()
                                                                                                    //.WithCronSchedule("0 0 1 * * ?")   //  10s 一次
                                                                                                    .WithCronSchedule("0 */3 * * * ?")                                                                                                    
                                                                                                    .Build();
                //一天一次 0 0 1 * * ?
                scheduler.ScheduleJob(processOrderConfrim, processOrderConfrimTrigger);


                ////=================APP/后台订单发货-发送微信模板消息 队伍 消息处理====================
                IJobDetail processOrderSendJob = JobBuilder.Create<ProcessOrderSend>()
                                                                                        .WithIdentity("processOrderSendJob", "group1")
                                                                                        .UsingJobData("jobSays", "Hello World!")
                                                                                        .Build();

                ITrigger processOrderSendJobTrigger = TriggerBuilder.Create()
                                                                                                    .WithIdentity("processOrderSendJobTrigger", "group1")
                                                                                                    .StartNow()
                                                                                                    .WithCronSchedule("/10 * * ? * *")   //  10s 一次
                                                                                                    .Build();

                scheduler.ScheduleJob(processOrderSendJob, processOrderSendJobTrigger);//这里是新建的job**

                ////=================确认收货时处理积分、返现、佣金 队伍 队伍 消息处理====================
                IJobDetail processOrderRewardJob = JobBuilder.Create<ProcessOrderReward>()
                                                                                        .WithIdentity("processOrderRewardJob", "group1")
                                                                                        .UsingJobData("jobSays", "Hello World!")
                                                                                        .Build();

                ITrigger processOrderRewardJobTrigger = TriggerBuilder.Create()
                                                                                                    .WithIdentity("processOrderRewardJobTrigger", "group1")
                                                                                                    .StartNow()
                                                                                                    .WithCronSchedule("/10 * * ? * *")   //  10s 一次
                                                                                                    .Build();

                scheduler.ScheduleJob(processOrderRewardJob, processOrderRewardJobTrigger);


                ////=================商户数据库链接缓存种植====================
                IJobDetail setConnectionJob = JobBuilder.Create<SetRedisConnectionCache>()
                                                                            .WithIdentity("setConnectionJob", "group1")
                                                                            .UsingJobData("jobSays", "Hello World!")
                                                                            .Build();

                ITrigger setConnectionJobTrigger = TriggerBuilder.Create()
                                                                                            .WithIdentity("setConnectionJobTrigger", "group1")
                                                                                            .StartNow()
                                                                                            .WithCronSchedule("0 0 1 * * ?")  //  每天 1点 触发
                                                                                            .Build();

                scheduler.ScheduleJob(setConnectionJob, setConnectionJobTrigger);
                //=================奖品池队列种植====================
                IJobDetail setPrizePoolsJob = JobBuilder.Create<SetPrizePools>()
                                                                            .WithIdentity("setPrizePoolsJob", "group1")
                                                                            .UsingJobData("jobSays", "Hello World!")
                                                                            .Build();

                ITrigger setPrizePoolsJobTrigger = TriggerBuilder.Create()
                                                                                            .WithIdentity("setPrizePoolsJobTrigger", "group1")
                                                                                            .StartNow()
                                                                                            .WithCronSchedule("0 0 1 * * ?")  //  每天 1点 触发
                                                                                            .Build();

                scheduler.ScheduleJob(setPrizePoolsJob, setPrizePoolsJobTrigger);
                //=================优惠券队列种植====================
                IJobDetail setCouponJob = JobBuilder.Create<SetCoupon>()
                                                                            .WithIdentity("setCouponJob", "group1")
                                                                            .UsingJobData("jobSays", "Hello World!")
                                                                            .Build();

                ITrigger setCouponJobTrigger = TriggerBuilder.Create()
                                                                                            .WithIdentity("setCouponJobTrigger", "group1")
                                                                                            .StartNow()
                                                                                            .WithCronSchedule("0 0 1 * * ?")  //  每天 1点 触发
                                                                                            .Build();

                scheduler.ScheduleJob(setCouponJob, setCouponJobTrigger);
                //=================触点 队列 消息处理====================
                IJobDetail contactJob = JobBuilder.Create<SetContact>()
                                                                                        .WithIdentity("contactJob", "group1")
                                                                                        .UsingJobData("jobSays", "Hello World!")
                                                                                        .Build();

                ITrigger contactJobTrigger = TriggerBuilder.Create()
                                                                                                    .WithIdentity("contactJobTrigger", "group1")
                                                                                                    .StartNow()
                                                                                                    .WithCronSchedule("/40 * * ? * *")   //  10s 一次
                                                                                                    .Build();

                scheduler.ScheduleJob(contactJob, contactJobTrigger);
                //=================vip绑定优惠券 队列 消息处理====================
                IJobDetail vipMappingCouponJob = JobBuilder.Create<VipMappingCoupon>()
                                                                                        .WithIdentity("vipMappingCouponJob", "group1")
                                                                                        .UsingJobData("jobSays", "Hello World!")
                                                                                        .Build();

                ITrigger vipMappingCouponJobTrigger = TriggerBuilder.Create()
                                                                                                    .WithIdentity("vipMappingCouponJobTrigger", "group1")
                                                                                                    .StartNow()
                                                                                                    .WithCronSchedule("/20 * * ? * *")   //  40s 一次
                                                                                                    .Build();

                scheduler.ScheduleJob(vipMappingCouponJob, vipMappingCouponJobTrigger);
                //=================虚拟商品发优惠券成功后通知====================
                IJobDetail _CouponCouponNoticeGetJob = JobBuilder.Create<CouponCouponNoticeGetJob>()
                                                                                        .WithIdentity("CouponCouponNoticeGetJob", "group1")
                                                                                        .UsingJobData("jobSays", "Hello World!")
                                                                                        .Build();

                ITrigger _CouponCouponNoticeGetJobTrigger = TriggerBuilder.Create()
                                                                                                    .WithIdentity("CouponCouponNoticeGetJobTrigger", "group1")
                                                                                                    .StartNow()
                                                                                                    .WithCronSchedule("/20 * * ? * *")   //  40s 一次
                                                                                                    .Build();

                scheduler.ScheduleJob(_CouponCouponNoticeGetJob, _CouponCouponNoticeGetJobTrigger);//绑定job和trigger
                
                
                ////=================商户基础设置缓存种植====================
                IJobDetail setBasicSettingJob = JobBuilder.Create<SetBasicSettingCache>()
                                                                            .WithIdentity("setBasicSettingJob", "group1")
                                                                            .UsingJobData("jobSays", "Hello World!")
                                                                            .Build();

                ITrigger setBasicSettingJobTrigger = TriggerBuilder.Create()
                                                                                            .WithIdentity("setBasicSettingJobTrigger", "group1")
                                                                                            .StartNow()
                  .WithCronSchedule("0 0 1 * * ?")  //  每天 1点 触发
                    //      .WithCronSchedule("0 */3 * * * ?")    //3分钟     (为了方便测试，临时设为三分钟一次)
                                                                                            .Build();

                scheduler.ScheduleJob(setBasicSettingJob, setBasicSettingJobTrigger);


                ////=================订单未付款模板消息种植====================
                IJobDetail processOrderNotPaySetJob = JobBuilder.Create<ProcessOrderNotPaySet>()
                                                                            .WithIdentity("ProcessOrderNotPaySetJob", "group1")
                                                                            .UsingJobData("jobSays", "Hello World!")
                                                                            .Build();

                ITrigger processOrderNotPaySetJobTrigger = TriggerBuilder.Create()
                                                                                            .WithIdentity("processOrderNotPaySetJobTrigger", "group1")
                                                                                            .StartNow()
                    .WithCronSchedule("0 0 1 * * ?")  //  每天 1点 触发
                                                                                        //   .WithCronSchedule("0 */3 * * * ?")    //3分钟     (为了方便测试，临时设为三分钟一次)
                                                                                            .Build();

                scheduler.ScheduleJob(processOrderNotPaySetJob, processOrderNotPaySetJobTrigger);

                ////=================订单未付款模板消息发送====================
                IJobDetail processOrderNotPayGetJob = JobBuilder.Create<ProcessOrderNotPayGet>()
                                                                            .WithIdentity("ProcessOrderNotPayGetJob", "group1")
                                                                            .UsingJobData("jobSays", "Hello World!")
                                                                            .Build();

                ITrigger processOrderNotPayGetJobTrigger = TriggerBuilder.Create()
                                                                                            .WithIdentity("processOrderNotPayGetJobTrigger", "group1")
                                                                                            .StartNow()
                                                                                        //     .WithCronSchedule("0 0 9 * * ?")  //  每天 1点 触发
                                                                                            .WithCronSchedule("0 */3 * * * ?")    //3分钟     (为了方便测试，临时设为三分钟一次)
                                                                                            .Build();

                scheduler.ScheduleJob(processOrderNotPayGetJob, processOrderNotPayGetJobTrigger);


                ////=================优惠券即将过期  种植缓存====================
                IJobDetail CouponToBeExpiredMsgSetJob = JobBuilder.Create<CouponToBeExpiredMsgSetJob>()
                                                                            .WithIdentity("CouponToBeExpiredMsgSetJob", "group1")
                                                                            .UsingJobData("jobSays", "Hello World!")
                                                                            .Build();

                ITrigger CouponToBeExpiredMsgSetJobTrigger = TriggerBuilder.Create()
                                                                                            .WithIdentity("CouponToBeExpiredMsgSetJobTrigger", "group1")
                                                                                            .StartNow()
                    .WithCronSchedule("0 0 1 * * ?")  //  每天 1点 触发
                                                                                         //   .WithCronSchedule("0 */3 * * * ?")    //3分钟       (为了方便测试，临时设为三分钟一次)
                                                                                            .Build();

                scheduler.ScheduleJob(CouponToBeExpiredMsgSetJob, CouponToBeExpiredMsgSetJobTrigger);


                ////=================优惠券即将过期     发送====================
                IJobDetail CouponToBeExpiredMsgGetJob = JobBuilder.Create<CouponToBeExpiredMsgGetJob>()
                                                                            .WithIdentity("CouponToBeExpiredMsgGetJob", "group1")
                                                                            .UsingJobData("jobSays", "Hello World!")
                                                                            .Build();

                ITrigger CouponToBeExpiredMsgGetJobTrigger = TriggerBuilder.Create()
                                                                                            .WithIdentity("CouponToBeExpiredMsgGetJobTrigger", "group1")
                                                                                            .StartNow()
                  .WithCronSchedule("0 0 9 * * ?")  //  每天 9点 触发
                                                                                         //   .WithCronSchedule("0 */3 * * * ?")   //3分钟       (为了方便测试，临时设为三分钟一次)

                                                                                            .Build();

                scheduler.ScheduleJob(CouponToBeExpiredMsgGetJob, CouponToBeExpiredMsgGetJobTrigger);
                //=================计算超级分销商分润佣金====================
                IJobDetail calculateSuperRetailTraderOrderJob = JobBuilder.Create<CalculateSuperRetailTraderOrder>()
                                                                                        .WithIdentity("calculateSuperRetailTraderOrderJob", "group1")
                                                                                        .UsingJobData("jobSays", "Hello World!")
                                                                                        .Build();

                ITrigger calculateSuperRetailTraderOrderJobTrigger = TriggerBuilder.Create()
                                                                                                    .WithIdentity("calculateSuperRetailTraderOrderJobTrigger", "group1")
                                                                                                    .StartNow()
                                                                                                    .WithCronSchedule("/40 * * ? * *")   //  40s 一次
                                                                                                    .Build();
                scheduler.ScheduleJob(calculateSuperRetailTraderOrderJob, calculateSuperRetailTraderOrderJobTrigger);

                //=================根据订单状态，做出不同的推送消息(员工（客服）或者会员)====================
                IJobDetail ProcessOrderPushMessageJob = JobBuilder.Create<ProcessOrderPushMessage>()
                                                                                        .WithIdentity("ProcessOrderPushMessageJob", "group1")
                                                                                        .UsingJobData("jobSays", "Hello World!")
                                                                                        .Build();

                ITrigger ProcessOrderPushMessageJobTrigger = TriggerBuilder.Create()
                                                                                                    .WithIdentity("ProcessOrderPushMessageJobTrigger", "group1")
                                                                                                    .StartNow()
                                                                                                    .WithCronSchedule("/30 * * ? * *")   //  30s 一次
                                                                                                    .Build();
                scheduler.ScheduleJob(ProcessOrderPushMessageJob, ProcessOrderPushMessageJobTrigger);
				//=================计算售卡分润====================
				IJobDetail calculateSalesVipCardOrderJob = JobBuilder.Create<SalesVipCardOrder>()
																						.WithIdentity("calculateSalesVipCardOrderJob", "group1")
																						.UsingJobData("jobSays", "Hello World!")
																						.Build();

				ITrigger calculateSalesVipCardOrderJobTrigger = TriggerBuilder.Create()
																									.WithIdentity("calculateSalesVipCardOrderJobTrigger", "group1")
																									.StartNow()
																									.WithCronSchedule("/40 * * ? * *")   //  40s 一次
																									.Build();
				scheduler.ScheduleJob(calculateSalesVipCardOrderJob, calculateSalesVipCardOrderJobTrigger);
				//=================计算充值分润====================
				IJobDetail CalculateRechargeOrderJob = JobBuilder.Create<RechargeOrder>()
																						.WithIdentity("CalculateRechargeOrderJob", "group1")
																						.UsingJobData("jobSays", "Hello World!")
																						.Build();

				ITrigger CalculateRechargeOrderJobTrigger = TriggerBuilder.Create()
																									.WithIdentity("CalculateRechargeOrderJobTrigger", "group1")
																									.StartNow()
																									.WithCronSchedule("/40 * * ? * *")   //  40s 一次
																									.Build();
				scheduler.ScheduleJob(CalculateRechargeOrderJob, CalculateRechargeOrderJobTrigger);
				//=================消费升级====================
				IJobDetail CalculateVipConsumeForUpgradeJob = JobBuilder.Create<VipConsumeForUpgradeOrder>()
																						.WithIdentity("CalculateVipConsumeForUpgradeJob", "group1")
																						.UsingJobData("jobSays", "Hello World!")
																						.Build();

				ITrigger CalculateVipConsumeForUpgradeJobTrigger = TriggerBuilder.Create()
																									.WithIdentity("CalculateVipConsumeForUpgradeJobTrigger", "group1")
																									.StartNow()
																									.WithCronSchedule("/40 * * ? * *")   //  40s 一次
																									.Build();
				scheduler.ScheduleJob(CalculateVipConsumeForUpgradeJob, CalculateVipConsumeForUpgradeJobTrigger);

            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                Loggers.Debug(new DebugLogInfo() { Message = ex.Message });

            }

            //scheduler.Shutdown();         //关闭调度器。
        }
    }
}
