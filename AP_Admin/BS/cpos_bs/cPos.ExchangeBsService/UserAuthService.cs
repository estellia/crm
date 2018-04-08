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
    /// 用户信息接口
    /// </summary>
    public class UserAuthService
    {
        /// <summary>
        ///返回用户信息、客户信息及用户所属门店的信息集合。(C005-下载用户信息与所属门店关系接口)
        /// </summary>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Customer_Id">客户标识</param>
        /// <returns>返回用户model对象</returns>
        public UserInfo GetUserInfoByUserId(string User_Id, string Customer_Id)
        {
            UserInfo userInfo = new UserInfo();
            cUserService userServices = new cUserService();
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo = new BaseService().GetLoggingSessionInfoByCustomerId(Customer_Id);
            userInfo = userServices.GetUserById(loggingSessionInfo, User_Id);
            userInfo.LoggingManagerInfo = loggingSessionInfo.CurrentLoggingManager;
            userInfo.UnitList = new UnitService().GetUnitListByUserId(loggingSessionInfo, User_Id);
            return userInfo;
        }

        /// <summary>
        /// 下载用户配置信息接口(C006-下载门店用户（多个）配置信息接口)
        /// </summary>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">门店标识</param>
        /// <returns>基础信息对象</returns>
        public BaseInfo GetUserBaseByUserId(string User_Id, string Customer_Id,string Unit_Id)
        {
            BaseInfo baseInfo = new BaseInfo();
            baseInfo = new cUserService().GetUserBaseInfoByUserId(User_Id, Customer_Id,Unit_Id);
            return baseInfo;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="User_Id">访问的用户标识</param>
        /// <param name="Customer_Id">访问的客户标识</param>
        /// <param name="Unit_Id">访问的组织标识</param>
        /// <param name="userId">需要修改的用户标识</param>
        /// <param name="userPwd">需要修改的密码</param>
        /// <returns></returns>
        public bool SetUserPassword(string User_Id, string Customer_Id, string Unit_Id, string  userId,string userPwd)
        {
            UserInfo userInfo = new UserInfo();
            cUserService userServices = new cUserService();
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo = new BaseService().GetLoggingSessionInfoByCustomerId(Customer_Id);
            string strError = string.Empty;
            cUserService userService = new cUserService();
            bool b = userService.ModifyUserPassword_JK(loggingSessionInfo, userId, userPwd, out strError);
            if (b)
            {
                return b;
            }
            else {
                throw new Exception(string.Format(strError, strError));
                return b;
            }
        }
    }
}
