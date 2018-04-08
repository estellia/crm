using JIT.CPOS.BS.BLL.RedisOperationBLL.Order;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.VipGold {
	public class VipConsumeForUpgradeOrder : IJob {
		public void Execute(IJobExecutionContext context) {
			Loggers.Debug(new DebugLogInfo() { Message = "计算消费升级" });

			var service = new RedisCalculateVipConsumeForUpgrade();
			//执行
			service.CalculateVipConsumeForUpgradeJob();
		}
	}
}
