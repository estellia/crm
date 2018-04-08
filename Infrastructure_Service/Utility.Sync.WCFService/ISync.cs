using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Utility.Sync.WCFService.WCF.API;


namespace Utility.Sync.WCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ISync”。
    [ServiceContract]
    public interface ISync
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SyncLog", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        SyncRespose SyncLog(SyncRequest sRequest);
    }
}
