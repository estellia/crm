/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/17 11:16:35
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.AppVersion.DataAccess;
using JIT.Utility.AppVersion.Entity.BusinessEntity;

namespace JIT.Utility.AppVersion.BLL
{
    /// <summary>
    /// 业务处理： 商圈表 
    /// </summary>
    public partial class BusinessZoneBLL
    {
        public BusinessZoneInfo GetBusinessZoneInfo(int pBusinessZoneID,int pAppID)
        {
            var bentity = this._currentDAO.GetByID(pBusinessZoneID);
            var dao = new AppVersionDAO(this.CurrentUserInfo);
            var aEntitys = dao.GetLatestVersion(pBusinessZoneID,pAppID);
            var binfo = new BusinessZoneInfo()
            {
                BusinessZoneCode = bentity.BusinessZoneCode,
                BusinessZoneID = bentity.BusinessZoneID.Value,
                BusinessZoneName = bentity.BusinessZoneName,
                ServiceURL = bentity.ServiceUrl
            };
            if (aEntitys.Length > 0)
                binfo.VersionInfo = new AppVersionInfo()
                {
                    AppID = aEntitys[0].AppID.Value,
                    Description = aEntitys[0].Description,
                    Version = aEntitys[0].Version
                };
            return binfo;
        }
    }
}