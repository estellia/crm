using System;
using JIT.CPOS.BS.BLL.RedisOperationBLL;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderPaySuccess;
using JIT.Utility.Log;
using Quartz;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JIT.CPOS.BS.BLL.RedisOperationBLL.CouponsUpcomingExpired;

namespace Scheduler.Coupon
{
   public class CouponToBeExpiredMsgSetJob:IJob
    {
       public void Execute(IJobExecutionContext context)
       {
           Loggers.Debug(new DebugLogInfo() { Message = "优惠券即将过期推送模板消息 开始全量种植" });

           var service = new CouponToBeExpiredMsgSetJobBLL();
           //执行
           service.AutoSetCouponToBeExpiredCache();
       }
    }
}
