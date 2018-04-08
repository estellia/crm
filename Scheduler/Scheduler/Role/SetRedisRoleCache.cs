
using JIT.CPOS.BS.BLL;
using JIT.Utility.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Role
{
    public class SetRedisRoleCache : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "后台 角色菜单 开始全量种植" });

            var Service = new SetRoleMenuJobBLL();
            //执行
            Service.AutoSetRoleMenuCache();
        }
    }
}
