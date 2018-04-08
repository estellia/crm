using System.Collections.Generic;
using Xgx.SyncData.DbAccess.t_unit;
using Xgx.SyncData.DbAccess.T_User_Role;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_City
{
    public class T_User_RoleFacade
    {
        private readonly T_User_RoleCMD _cmd = new T_User_RoleCMD();
        private readonly T_User_RoleQuery _query = new T_User_RoleQuery();
        public void Create(T_User_RoleEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_User_RoleEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_User_RoleEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }
        public List<string> GetIdByUserId(string userId)
        {
            return _query.GetIdByUserId(userId);
        }
    }
}
