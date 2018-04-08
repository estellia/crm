using System.Collections.Generic;
using Xgx.SyncData.DbAccess.T_City;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_User
{
    public class T_UserFacade
    {
        private readonly T_UserCMD _cmd = new T_UserCMD();
        private readonly T_UserQuery _query = new T_UserQuery();
        public void Create(T_UserEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_UserEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_UserEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public dynamic GetUserById(string id)
        {
            return _query.GetUserById(id);
        }

        public List<string> GetUserRoleCode(string id)
        {
            return _query.GetUserRoleCode(id);
        }

        public string GetUserPwd(string id)
        {
            return _query.GetUserPwd(id);
        }
    }
}
