using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace JIT.Utility.MobileDeviceManagement.WcfService.Contracts
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IMobileDeviceManagementService”。
    [ServiceContract]
    public interface IMobileDeviceManagementService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ProcessRequest", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        CommonResponse ProcessRequest(LogRequest[] pRequests);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetCommand", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string[] GetCommand(CommandRequest pRequest);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ReturnResult", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        CommonResponse ReturnResult(CommandResponse pRequest);
    }
}
