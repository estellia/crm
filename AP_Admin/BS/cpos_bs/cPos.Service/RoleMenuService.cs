using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Components.SqlMappers;

namespace cPos.Service
{
    /// <summary>
    /// 角色与菜单关系
    /// </summary>
    public class RoleMenuService
    {
        #region 根据组织获取所有角色与菜单关系
        /// <summary>
        /// 根据组织与系统，获取角色与菜单关系
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitId">组织标识</param>
        /// <param name="appCode">系统号码</param>
        /// <returns></returns>
        public IList<RoleMenuModel> GetRoleMenuListByUnitIdAndAppCode(LoggingSessionInfo loggingSessionInfo, string unitId, string appCode)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("UnitId", unitId);
            _ht.Add("AppCode",appCode);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<RoleMenuModel>("RoleMenu.SelectByUnitIdAndAppCode", _ht);


        }
        #endregion
    }
}
