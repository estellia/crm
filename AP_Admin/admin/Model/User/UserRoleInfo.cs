using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.User
{
    /// <summary>
    /// 用户对应的角色
    /// </summary>
    [Serializable]
    public class UserRoleInfo
    {
        public UserRoleInfo()
        {
            User = new UserInfo();
            Role = new Right.RoleInfo();
        }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("user_role_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 所属用户
        /// </summary>
        [XmlElement("user")]
        public UserInfo User
        { get; set; }

        /// <summary>
        /// 对应的角色
        /// </summary>
        [XmlElement("role")]
        public Right.RoleInfo Role
        { get; set; }
    }
}
