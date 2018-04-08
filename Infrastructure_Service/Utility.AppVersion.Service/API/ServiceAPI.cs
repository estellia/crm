using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.AppVersion.Framework;
using JIT.Utility.AppVersion.Framework.DataContract;
using JIT.Utility.AppVersion.BLL;

namespace JIT.Utility.AppVersion.Service.API
{
    public static class ServiceAPI
    {
        internal static object GetBusinessZoneInfo(AppMgrRequest pRequest)
        {
            return null;
        }

        internal static object CheckAppVersion(AppMgrRequest pRequest)
        {
            var para = pRequest.GetParameters<GetVersionReqPara>();
            var bll = new AppVersionBLL(pRequest.GetUserInfo());
            var item = bll.GetVersion(pRequest.ClientID, pRequest.AppCode, para.Version);
            if (item == null)
                return new { };
            var temp = new
            {
                AppID = item.AppID,
                AppCode = item.AppCode,
                Description = item.Description,
                Version = item.Version,
                PackageUrl = string.IsNullOrEmpty(pRequest.Plat) ? item.AndroidPackageUrl : (pRequest.Plat == "2" ? item.IOSPackageUrl : item.AndroidPackageUrl),
                Name = item.Name
            };
            return temp;
        }
    }
}