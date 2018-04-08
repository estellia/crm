using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using cPos.Dex.Model;
using cPos.Dex.Common;
using System.Data;
using System.Collections;

namespace cPos.Dex.WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class BaseService
    {
        #region Token
        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <param name="certId">凭证ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="refresh">true:立即刷新，false:过期刷新</param>
        /// <returns></returns>
        protected CertTokenInfo GetCertToken(string certId, string userId, bool refresh)
        {
            Dex.Services.AuthService authService = new Dex.Services.AuthService();
            return authService.GetCurrentCertToken(certId, userId, refresh);
        }

        protected CertTokenInfo GetCertToken(string certId, string userId)
        {
            return GetCertToken(certId, userId, false);
        }
        #endregion

    }
}