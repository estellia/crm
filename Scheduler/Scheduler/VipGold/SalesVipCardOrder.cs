using JIT.CPOS.BS.BLL.RedisOperationBLL.Order;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Scheduler.VipGold {
	public class SalesVipCardOrder : IJob {
		public void Execute(IJobExecutionContext context) {
			Loggers.Debug(new DebugLogInfo() { Message = "计算售卡分润" });

			var service = new RedisSalesVipCardOrderBLL();
			//执行
			service.CalculateSalesVipCardOrderJob();
		}
	}
}
