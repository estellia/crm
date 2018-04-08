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
    public interface IAuthService
    {
        // Validate
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "validate?user_code={userCode}&customer_code={customerCode}&password={password}&type={type}")]
        ValidateContract ValidateJson(string userCode, string customerCode, string password, string type);

        //// ChangePassword
        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    UriTemplate = "ChangePassword?user_id={userId}&token={token}" +
        //    "&unit_id={unitId}&new_pwd={newPassword}")]
        //BaseContract ChangePasswordJson(string userId, string token, string unitId, string newPassword);
    }
}
