using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using cPos.Model;
using cPos.Model.User;


namespace cPos.Components
{
    public class SessionManager
    {
        public static string KEY_CUSTOMER_ID = "customer_id";
        private static string KEY_LANGUAGE_KIND_ID = "Language.KindId";
        /// <summary>
        /// 获取或设置登录用户的数据库连接
        /// </summary>
        public string CurrentCustomerId
        {
            get { return HttpContext.Current.Session[KEY_CUSTOMER_ID] == null ? "" : (string)HttpContext.Current.Session[KEY_CUSTOMER_ID]; }
            set { HttpContext.Current.Session[KEY_CUSTOMER_ID] = value; }
        }

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public LoggingSessionInfo loggingSessionInfo
        {
            get { return HttpContext.Current.Session["loggingSessionInfo"] == null ? null : (LoggingSessionInfo)HttpContext.Current.Session["loggingSessionInfo"]; }
            set { HttpContext.Current.Session["loggingSessionInfo"] = value; }
        }

        /// <summary>
        /// 获取或设置当前用户角色信息
        /// add by zhaoyu 2008-09-09
        /// </summary>
        public UserInfo UserInfo
        {
            get { return HttpContext.Current.Session["UserInfo"] == null ? null : (UserInfo)HttpContext.Current.Session["UserInfo"]; }
            set { HttpContext.Current.Session["UserInfo"] = value; }
        }

        /// <summary>
        /// 获取登录管理平台信息
        /// </summary>
        public LoggingManager LoggingManager
        {
            get { return HttpContext.Current.Session["LoggingManager"] == null ? null : (LoggingManager)HttpContext.Current.Session["LoggingManager"]; }
            set { HttpContext.Current.Session["LoggingManager"] = value; }
        }

        /// <summary>
        /// 获取或设置当前用户角色信息
        /// add by zhaoyu 2008-09-09
        /// </summary>
        public UserRoleInfo UserRoleInfo
        {
            get { return HttpContext.Current.Session["UserRoleInfo"] == null ? null : (UserRoleInfo)HttpContext.Current.Session["UserRoleInfo"]; }
            set { HttpContext.Current.Session["UserRoleInfo"] = value; }
        }

        /// <summary>
        /// 获取或设置当前语言的种类的Id
        /// </summary>
        public string CurrentLanguageKindId
        {
            get { return HttpContext.Current.Session[KEY_LANGUAGE_KIND_ID] == null ? "" : (string)HttpContext.Current.Session[KEY_LANGUAGE_KIND_ID]; }
            set { HttpContext.Current.Session[KEY_LANGUAGE_KIND_ID] = value; }
        }
    }
}
