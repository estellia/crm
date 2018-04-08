using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using cPos.WebServices;
using System.Configuration;

namespace cPos.Service
{
    /// <summary>
    /// 登录的服务
    /// </summary>
    public class CLoggingSessionService
    {
        /// <summary>
        /// 获取登录用户的具体信息
        /// </summary>
        /// <param name="cid">客户id</param>
        /// <param name="tid">令牌id</param>
        /// <returns></returns>
        public LoggingSessionInfo GetLoggingSessionInfo(string cid, string tid)
        {
            //获取登录管理平台的用户信息
            cPos.WebServices.AuthManagerWebServices.AuthService AuthWebService = new cPos.WebServices.AuthManagerWebServices.AuthService();
            AuthWebService.Url = ConfigurationManager.AppSettings["sso_url"].ToString() + "/AuthService.asmx";
            string str = AuthWebService.GetLoginUserInfo(tid);//"0b3b4d8b8caa4c71a7c201f53699afcc"

            cPos.Model.LoggingManager myLoggingManager = (cPos.Model.LoggingManager)cXMLService.Deserialize(str, typeof(cPos.Model.LoggingManager));

            //判断用户是否存在,并且返回用户信息
            cPos.Model.User.UserInfo login_user = new cPos.Model.User.UserInfo();
            cPos.Service.cUserService userService = new cUserService();

            cPos.Model.LoggingSessionInfo loggingSessionInfo1 = new LoggingSessionInfo();
            loggingSessionInfo1.CurrentLoggingManager = myLoggingManager;

            //获取用户信息
            if (userService.IsExistUser(myLoggingManager))
            {
                login_user = userService.GetUserById(loggingSessionInfo1, myLoggingManager.User_Id);
            }
            else
            {
                login_user.User_Id = "1";
            }

            cPos.Model.LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();


            loggingSessionInfo.CurrentUser = login_user;
            loggingSessionInfo.CurrentLoggingManager = myLoggingManager;

            cPos.Model.User.UserRoleInfo ur = new cPos.Model.User.UserRoleInfo();
            ur.RoleId = "7064243380E24B0BA24E4ADC4E03968B";
            ur.UnitId = "1";
            loggingSessionInfo.CurrentUserRole = ur;

            return loggingSessionInfo;
        }
    }
}
