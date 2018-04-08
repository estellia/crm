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
    public interface IBasicService
    {
        // GetPackages
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "getpackages?user_id={userId}&token={token}" +
            "&unit_id={unitId}&seq={pkgSeq}&type={pkgtCode}&start_row={startRow}&rows_count={rowsCount}")]
        GetPackagesContract GetPackagesJson(string userId, string token,
            string unitId, string pkgSeq, string pkgtCode, long startRow, long rowsCount);

        // GetPackagesCount
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "getpackagescount?user_id={userId}&token={token}" +
            "&unit_id={unitId}&seq={pkgSeq}&type={pkgtCode}")]
        GetPackagesCountContract GetPackagesCountJson(string userId, string token,
            string unitId, string pkgSeq, string pkgtCode);

        // GetPackageFiles
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "getpackagefiles?user_id={userId}&token={token}" +
            "&unit_id={unitId}&package_id={packageId}&start_row={startRow}&rows_count={rowsCount}")]
        GetPackageFilesContract GetPackageFilesJson(string userId, string token,
            string unitId, string packageId, long startRow, long rowsCount);

        // GetPackageFilesCount
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "getpackagefilescount?user_id={userId}&token={token}" +
            "&unit_id={unitId}&package_id={packageId}")]
        GetPackageFilesCountContract GetPackageFilesCountJson(string userId, string token,
            string unitId, string packageId);

        // CheckVersion
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "CheckVersion?user_id={userId}&token={token}" +
            "&unit_id={unitId}&pos_id={posId}&version={version}&db_version={dbVersion}")]
        CheckVersionContract CheckVersionJson(string userId, string token,
            string unitId, string posId, string version, string dbVersion);

        // GetUnitXmlConfigInfo
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "GetUnitXmlConfigInfo?customer_code={customerCode}&unit_code={unitCode}&pos_sn={posSn}")]
        GetUnitXmlConfigInfoContract GetUnitXmlConfigInfoJson(string customerCode, string unitCode, string posSn);

        // GetItemInfo
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "GetItemInfo?user_id={userId}&token={token}" +
            "&unit_id={unitId}&barcode={barcode}")]
        GetItemInfoContract GetItemInfoJson(string userId, string token,
            string unitId, string barcode);

        // GetPosCode
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "getposcode?user_id={userId}&token={token}&type={type}&unit_id={unitId}&sn={posSn}")]
        GetPosCodeContract GetPosCodeJson(string userId, string token, string type, string unitId, string posSn);

        // ApplyCustomerAndUnit
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "ApplyCustomerAndUnit?user_id={userId}&token={token}&type={type}")]
        ApplyCustomerAndUnitContract ApplyCustomerAndUnitJson(string userId, string token,
            string type, CustomerUnitApply customerUnitApply);

        // GetUserUnitRelations
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "GetUserUnitRelations?user_id={userId}&token={token}&type={type}")]
        GetUserUnitRelationsContract GetUserUnitRelationsJson(string userId, string token, string type);

        // GetUsersProfile
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "getusersprofile?user_id={userId}&token={token}" +
            "&unit_id={unitId}&package_seq={packageSeq}")]
        GetUsersProfileContract GetUsersProfileJson(string userId, string token,
            string unitId, int packageSeq);
    }
}
