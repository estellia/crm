using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.Right
{
    /// <summary>
    /// 角色对应的菜单
    /// </summary>
    public class RoleMenuInfo
    {
        public RoleMenuInfo()
        {
            Role = new RoleInfo();
            Menu = new MenuInfo();
        }

        /// <summary>
        /// ID
        /// </summary>
        public string ID
        { get; set; }

        /// <summary>
        /// 所属的角色
        /// </summary>
        public RoleInfo Role
        { get; set; }

        /// <summary>
        /// 对应的菜单
        /// </summary>
        public MenuInfo Menu
        { get; set; }
    }
}
