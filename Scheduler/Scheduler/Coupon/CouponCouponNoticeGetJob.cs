﻿using JIT.CPOS.BS.BLL.RedisOperationBLL;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderPaySuccess;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderNotPay;
using JIT.CPOS.BS.BLL.RedisOperationBLL.CouponsUpcomingExpired;
using JIT.CPOS.BS.BLL.RedisOperationBLL.Coupon;

namespace Scheduler.Coupon
{
    public class CouponCouponNoticeGetJob : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "优惠券即将过期推送模板消息 开始推送消息" });

            var service = new ProcessCouponNoticeJobBLL();
            //执行
            service.ProcessCouponNotice();
        }

    }
}
