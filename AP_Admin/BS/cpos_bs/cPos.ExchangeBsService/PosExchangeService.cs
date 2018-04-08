using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cPos.Model;
using cPos.Service;

namespace cPos.ExchangeBsService
{
    /// <summary>
    /// POS相关信息服务
    /// </summary>
    public class PosExchangeService
    {
        /// <summary>
        /// 获取终端的编号
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="unitID">门店ID</param>
        /// <param name="posSN">POS序列号</param>
        /// <returns></returns>
        public string GetPosCode(string customerID, string userID, string unitID, string posSN)
        {
            PosService pos_service = new PosService();
            string pos_code = pos_service.GetPosNo(customerID, unitID, posSN);
            return pos_code;
        }

        /// <summary>
        /// 更新终端的版本（程序版本和数据版本)
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="unitID">门店ID</param>
        /// <param name="appVersion">程序版本</param>
        /// <param name="dbVersion">数据版本</param>
        /// <returns></returns>
        public bool ModifyPosVersion(string customerID, string userID, string unitID, string SN, string appVersion, string dbVersion)
        {
            LoggingSessionInfo ls = new BaseInfouAuthService().GetLoggingSessionInfo(customerID, userID, unitID);
            PosService pos_service = new PosService();
            return pos_service.ModifyPosVersion(ls, SN, appVersion, dbVersion);
        }
    }
}
