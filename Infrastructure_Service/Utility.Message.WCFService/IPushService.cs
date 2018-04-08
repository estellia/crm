using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using JIT.Utility.Message.WCF;

namespace JIT.Utility.Message.WCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IPushService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Process", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        PushResponse Process(PushRequest pRequest);

    }

}
