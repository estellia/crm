using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using JIT.Utility.Message.WCF;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Message.WCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    public class PushService : IPushService
    {
        public PushResponse Process(PushRequest pRequest)
        {
            Loggers.Debug(new DebugLogInfo() { Message = pRequest.ToJSON() });
            PushResponse response;
            switch (pRequest.PlatForm)
            {
                case 1:
                    AndroidRequestHandler handler1 = new AndroidRequestHandler();
                    response = handler1.Process(pRequest);
                    break;
                case 2:
                    IOSRequestHandler handler2 = new IOSRequestHandler();
                    response = handler2.Process(pRequest);
                    break;
                default:
                    response = new PushResponse();
                    response.ResultCode = 100;
                    response.Message = "错误的平台,只支持Android和IOS";
                    break;
            }
            return response;
        }
    }
}
