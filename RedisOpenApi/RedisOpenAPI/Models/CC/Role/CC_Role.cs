using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC
{
    public class CC_Role
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        /// Menu 列表
        /// </summary>
        public List<CC_Menu> MenuList { get; set; }
    }
}
