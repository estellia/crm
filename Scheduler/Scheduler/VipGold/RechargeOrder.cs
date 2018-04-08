using JIT.CPOS.BS.BLL.RedisOperationBLL.Order;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.VipGold {
	public class RechargeOrder : IJob {
		public void Execute(IJobExecutionContext context) {
			Loggers.Debug(new DebugLogInfo() { Message = "计算充值分润" });

			var service = new RedisRechargeOrderBLL();
			//执行
			service.CalculateRechargeOrderJob();
		}
	}
}
