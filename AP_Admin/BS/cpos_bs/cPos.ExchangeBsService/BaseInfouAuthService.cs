using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Service;
using cPos.Model.User;
using cPos.Model;

namespace cPos.ExchangeBsService
{
    /// <summary>
    /// 公共类
    /// </summary>
    public class BaseInfouAuthService
    {
        /// <summary>
        /// 获取登录的model信息
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns></returns>
        public LoggingSessionInfo GetLoggingSessionInfo(string Customer_Id, string User_Id,string Unit_Id)
        {
            UserInfo userInfo = new UserInfo();
            cUserService userServices = new cUserService();
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            UserRoleInfo userRoleInfo = new UserRoleInfo();

            loggingSessionInfo = new BaseService().GetLoggingSessionInfoByCustomerId(Customer_Id);
            userInfo = userServices.GetUserById(loggingSessionInfo, User_Id);
            userInfo.LoggingManagerInfo = loggingSessionInfo.CurrentLoggingManager;
            userRoleInfo.UnitId = Unit_Id;
            userRoleInfo.RoleId = "7064243380E24B0BA24E4ADC4E03968B";
            loggingSessionInfo.CurrentUserRole = userRoleInfo;
            loggingSessionInfo.CurrentUser = userInfo;

            return loggingSessionInfo;
        }
    }
}
