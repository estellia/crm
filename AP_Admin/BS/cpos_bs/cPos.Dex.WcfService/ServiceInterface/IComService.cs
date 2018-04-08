using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using cPos.Dex.ContractModel;

namespace cPos.Dex.WcfService
{
    [ServiceContract(Namespace = Common.Config.NS)]
    public interface IComService
    {
        // UploadMonitorLog
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "MonitorLog?user_id={userId}&token={token}&unit_id={unitId}")]
        UploadContract UploadMonitorLogJson(MonitorLogContract order,
            string userId, string token, string unitId);
    }
}
