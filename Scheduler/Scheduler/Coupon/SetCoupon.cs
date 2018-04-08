using JIT.CPOS.BS.BLL.RedisOperationBLL.Coupon;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Coupon
{
    public class SetCoupon : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "后台 生成优惠券队列" });

            var service = new RedisCouponBLL();
            //执行
            service.RedisSetAllCoupon();
        }
    }
}
