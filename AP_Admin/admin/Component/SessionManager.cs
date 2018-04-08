using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;

namespace cPos.Admin.Component
{
    public class SessionManager
    {
        /// <summary>
        /// 存放在session中的验证码的key
        /// </summary>
        public static string KEY_AUTH_CODE = "User.AuthCode";
        /// <summary>
        /// 存放在当前登录信息的key
        /// </summary>
        public static string KEY_LOGGING_SESSION = "LoggingSession";

        /// <summary>
        /// 当前正在登录的session的信息
        /// </summary>
        public static LoggingSessionInfo CurrentLoggingSession
        {
            get
            {
                if (HttpContext.Current.Session[KEY_LOGGING_SESSION] == null)
                {
                    return null;
                }
                else
                {
                    return (LoggingSessionInfo)HttpContext.Current.Session[KEY_LOGGING_SESSION];
                }
            }
            set
            {
                HttpContext.Current.Session[KEY_LOGGING_SESSION] = value;
            }
        }

        /// <summary>
        /// session
        /// </summary>
        public static string SessionID
        {
            get
            {
                return HttpContext.Current.Session.SessionID;
            }
        }

        /// <summary>
        /// IP
        /// </summary>
        public static string CurrentIP
        {
            get
            {
                string s = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(s)) 
                { 
                    s = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.IsNullOrEmpty(s)) 
                { 
                    s = HttpContext.Current.Request.UserHostAddress;
                }
                if (string.IsNullOrEmpty(s))
                { 
                    return "127.0.0.1";
                }


                //验证IP地址的合法性
                IPAddress ip = null;
                if (IPAddress.TryParse(s, out ip))
                {
                    return ip.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
