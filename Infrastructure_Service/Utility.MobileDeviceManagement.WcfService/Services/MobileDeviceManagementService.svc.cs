using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using JIT.Utility.MobileDeviceManagement.WcfService.Contracts;

namespace JIT.Utility.MobileDeviceManagement.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“MobileDeviceManagementService”。

    public class MobileDeviceManagementService : IMobileDeviceManagementService
    {
        public CommonResponse ProcessRequest(LogRequest[] pRequests)
        {
            LogRequestHandler handler = new LogRequestHandler();
            return handler.Process(pRequests);
        }


        public string[] GetCommand(CommandRequest pRequest)
        {
            CommandRequestHandler handler = new CommandRequestHandler();
            return handler.Process(pRequest);
        }


        public CommonResponse ReturnResult(CommandResponse pRequest)
        {
            ResultReqeustHandler handler = new ResultReqeustHandler();
            return handler.Process(pRequest);
        }
    }

}
