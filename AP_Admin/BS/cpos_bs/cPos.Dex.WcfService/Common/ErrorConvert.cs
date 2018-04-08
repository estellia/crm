using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using cPos.Dex.Model;
using cPos.Dex.ContractModel;

namespace cPos.Dex.WcfService
{
    public class ErrorConvert
    {
        public static T Export<T>(Hashtable htError) where T : BaseContract
        {
            BaseContract contract = null;
            if (typeof(T) == typeof(BaseContract)) contract = new BaseContract();
            if (typeof(T) == typeof(ValidateContract)) contract = new ValidateContract();
            if (typeof(T) == typeof(GetUserUnitRelationsContract)) contract = new GetUserUnitRelationsContract();
            if (typeof(T) == typeof(GetUsersProfileContract)) contract = new GetUsersProfileContract();
            if (typeof(T) == typeof(GetPosCodeContract)) contract = new GetPosCodeContract();
            if (typeof(T) == typeof(GetLogsContract)) contract = new GetLogsContract();
            if (typeof(T) == typeof(GetLogContract)) contract = new GetLogContract();
            if (typeof(T) == typeof(GetPackagesContract)) contract = new GetPackagesContract();
            if (typeof(T) == typeof(GetPackagesCountContract)) contract = new GetPackagesCountContract();
            if (typeof(T) == typeof(GetPackageFilesContract)) contract = new GetPackageFilesContract();
            if (typeof(T) == typeof(GetPackageFilesCountContract)) contract = new GetPackageFilesCountContract();
            if (typeof(T) == typeof(UploadInoutOrdersContract)) contract = new UploadInoutOrdersContract();
            if (typeof(T) == typeof(GetCountContract)) contract = new GetCountContract();
            if (typeof(T) == typeof(GetStockBalancesContract)) contract = new GetStockBalancesContract();
            if (typeof(T) == typeof(UploadContract)) contract = new UploadContract();
            if (typeof(T) == typeof(GetVipsContract)) contract = new GetVipsContract();
            if (typeof(T) == typeof(GetVipContract)) contract = new GetVipContract();
            if (typeof(T) == typeof(ExistVipNoContract)) contract = new ExistVipNoContract();
            if (typeof(T) == typeof(DownloadInoutOrdersContract)) contract = new DownloadInoutOrdersContract();
            if (typeof(T) == typeof(DownloadCcOrdersContract)) contract = new DownloadCcOrdersContract();
            if (typeof(T) == typeof(GetAnnsContract)) contract = new GetAnnsContract();
            if (typeof(T) == typeof(GetPriceOrdersContract)) contract = new GetPriceOrdersContract();
            if (typeof(T) == typeof(CheckVersionContract)) contract = new CheckVersionContract();
            if (typeof(T) == typeof(GetUnitXmlConfigInfoContract)) contract = new GetUnitXmlConfigInfoContract();
            if (typeof(T) == typeof(GetItemInfoContract)) contract = new GetItemInfoContract();
            if (typeof(T) == typeof(ApplyCustomerAndUnitContract)) contract = new ApplyCustomerAndUnitContract();

            contract.status = htError["status"].ToString();
            contract.error_code = htError["error_code"].ToString();
            contract.error_full_desc = htError["error_desc"].ToString();
            return (T)contract;
        }
    }
}