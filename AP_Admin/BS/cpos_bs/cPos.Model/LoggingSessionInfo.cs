using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Model
{
    /// <summary>
    /// 登录用户信息
    /// </summary>
    [Serializable]
    public class LoggingSessionInfo
    {
        private string currentLanguageKindId;


        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Conn { get; set; }
  
        /// <summary>
        /// 语言
        /// </summary>
        public string CurrentLanguageKindId
        {
            get { return currentLanguageKindId; }
            set { currentLanguageKindId = value; }
        }
        

        private User.UserInfo currentUser;
        /// <summary>
        /// 登录用户
        /// </summary>
        public User.UserInfo CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }
        private User.UserRoleInfo currentUserRole;
        /// <summary>
        /// 登录用户的登录角色
        /// </summary>
        public User.UserRoleInfo CurrentUserRole
        {
            get { return currentUserRole; }
            set { currentUserRole = value; }
        }

        /// <summary>
        /// 管理平台用户登录传输的信息
        /// </summary>
        public LoggingManager CurrentLoggingManager { get; set; }
    }
}
