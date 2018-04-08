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

    public class VipMappingCoupon : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "后台 vip绑定优惠券" });

            var service = new RedisVipMappingCouponBLL();
            //执行
            service.InsertDataBase();
        }
    }
}
