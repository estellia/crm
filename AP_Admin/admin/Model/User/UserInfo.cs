using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.User
{
    /// <summary>
    /// 管理平台下的用户信息
    /// </summary>
    public class UserInfo : cPos.Admin.Model.Base.ObjectOperateInfo
    {
        public UserInfo()
            : base()
        { }

        /// <summary>
        /// ID
        /// </summary>
        public string ID
        { get; set; }

        /// <summary>
        /// 登录帐号
        /// </summary>
        public string Account
        { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        { get; set; }

        /// <summary>
        /// 状态(编码)
        /// </summary>
        public int Status
        { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusDescription
        { get; set; }
        /// <summary>
        /// 运营商ID
        /// </summary>
        public string unit_id
        { get; set; }
    }
}
