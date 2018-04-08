using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using cPos.Model;
using cPos.Model.User;
//using cPos.SqlHelper;
using IBatisNet.DataMapper;
using cPos.Components.SqlMappers;
using cPos.Components;
using cPos.Service;
//using cPos.ExchangeService;

namespace cPos.Service
{
    /// <summary>
    /// 用户与角色关系类
    /// </summary>
    public class UserRoleService
    {
        #region 根据组织标识，获取用户与角色关系
        /// <summary>
        /// 根据组织标识，获取用户与角色关系
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitId">组织标识</param>
        /// <returns></returns>
        public IList<UserRoleInfo> GetUserRoleListByUnitId(LoggingSessionInfo loggingSessionInfo, string unitId)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("UnitId", unitId);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UserRoleInfo>("UserRole.SelectByUnitId", _ht);

        }

        #endregion

        #region 根据用户标识，获取角色与用户关系集合
        /// <summary>
        /// 根据用户标识，获取角色与用户关系集合
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        public IList<UserRoleInfo> GetUserRoleListByUserId(LoggingSessionInfo loggingSessionInfo, string userId)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("UserId", userId);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UserRoleInfo>("UserRole.SelectByUserIdAndApplicationId", _ht);

        }
        #endregion
    }
}
