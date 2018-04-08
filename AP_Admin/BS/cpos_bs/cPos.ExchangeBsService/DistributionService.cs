using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Service;
using cPos.Model.User;
using cPos.Model;

namespace cPos.ExchangeBsService
{
    public class DistributionService : BaseInfouAuthService
    {
        /// <summary>
        /// 获取总部生成的配送单（总部未审批）
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <returns></returns>
        public IList<InoutInfo> GetDistributionList(string Customer_Id, string Unit_Id, string User_Id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            IList<InoutInfo> inoutInfoList = new List<InoutInfo>();
            cPos.Service.DistributionService server = new cPos.Service.DistributionService();
            inoutInfoList = server.GetDistributionListByStatus(loggingSessionInfo, Unit_Id, "1");
            return inoutInfoList;
        }

        /// <summary>
        /// 设置记录配送单打包批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="inoutInfoList">配送单集合</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetDistributionBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id, IList<InoutInfo> inoutInfoList)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            cPos.Service.InoutService server = new cPos.Service.InoutService();
            InoutInfo inoutInfo = new InoutInfo();
            inoutInfo.bat_id = bat_id;
            inoutInfo.InoutInfoList = inoutInfoList;
            bool b = server.SetInoutUpdateUnDownloadBatIdWeb(Customer_Id, inoutInfo);
            return b;
        }
    }
}
