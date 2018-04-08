using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using cPos.Model;
using cPos.Components.SqlMappers;

namespace cPos.Service
{
    /// <summary>
    /// 基础服务
    /// </summary>
    public class AppSysService
    {

        #region 角色
        /// <summary>
        /// 根据角色Id获取角色
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public RoleModel GetRoleById(LoggingSessionInfo loggingSession, string roleId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("RoleId", roleId);
            hashTable.Add("CustomerId", loggingSession.CurrentLoggingManager.Customer_Id);
            return cSqlMapper.Instance().QueryForObject<RoleModel>("Role.SelectById", hashTable);
        }

        #endregion

        #region 菜单
        /// <summary>
        /// 获取某个角色所能操作的菜单列表
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public IList<MenuModel> GetRoleMenus(LoggingSessionInfo loggingSession, string roleId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);

            //分隔出角色ID和单位ID
            string[] arr_role = roleId.Split(new char[] { ',' });
            hashTable.Add("RoleId", arr_role[0]);

            IList<MenuModel> menulist = cSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForList<MenuModel>("Menu.SelectAllMenusByRoleId", hashTable);

            if (menulist != null && menulist.Count > 0)
            {
                foreach (MenuModel menu in menulist)
                {
                    menu.SubMenuList = new List<MenuModel>();
                    foreach (MenuModel subMenu in menulist)
                    {
                        if (subMenu.Parent_Menu_Id == menu.Menu_Id)
                        {
                            menu.SubMenuList.Add(subMenu);
                        }
                    }
                }
            }
            return menulist;
        }
        #endregion 菜单
    }
}
