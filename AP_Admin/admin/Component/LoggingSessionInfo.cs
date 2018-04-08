using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Component
{
    /// <summary>
    /// 当前登录的信息
    /// </summary>
    public class LoggingSessionInfo
    {

        /// <summary>
        /// 登录用户的ID
        /// </summary>
        public string UserID
        { get; set; }

        /// <summary>
        /// 登录用户的姓名
        /// </summary>
        public string UserName
        { get; set; }
        /// <summary>
        /// 
        /// 登录用户的的客户ID
        /// </summary>
        public string CustomerID
        { get; set; }
        /// <summary>
        /// 登录用户的的客户编码
        /// </summary>
        public string CustomerCode
        { get; set; }
        /// <summary>
        /// 登录用户的的客户名称
        /// </summary>
        public string CustomerName
        { get; set; }
        /// <summary>
        /// 登录用户的的角色ID
        /// </summary>
        public string RoleID
        { get; set; }
        /// <summary>
        /// 登录用户的的角色名称
        /// </summary>
        public string RoleName
        { get; set; }
        /// <summary>
        /// 运营商id
        /// </summary>
        public string unit_id
        { get; set; }
    }
}
