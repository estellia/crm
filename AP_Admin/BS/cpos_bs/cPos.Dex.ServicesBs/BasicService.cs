using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Model;
using cPos.Dex.Components.SqlMappers;
using cPos.Dex.Common;
using cPos.Dex.ContractModel;
using cPos.Dex.Services;
using cPos.Model;

namespace cPos.Dex.ServicesBs
{
    public class BasicService
    {
        #region GetPCInitInfo
        /// <summary>
        /// 根据客户号与门店号获取终端初始信息
        /// </summary>
        public InitialInfo GetPCInitInfo(string customer_code, string unit_code, string pos_sn)
        {
            // Get
            var initService = new ExchangeBsService.InitialService();
            return initService.GetPCInitialInfo(customer_code, unit_code, pos_sn);
        }
        #endregion
    }
}
