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
    public interface ITestService
    {
        // TestConnect
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "testconnect?user_id={userId}")]
        BaseContract TestConnect(string userId);
    }
}
