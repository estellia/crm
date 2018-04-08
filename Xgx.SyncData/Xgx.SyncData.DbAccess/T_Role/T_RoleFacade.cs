using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Role
{
    public class T_RoleFacade
    {
        private readonly T_RoleCMD _cmd = new T_RoleCMD();
        private readonly T_RoleQuery _query = new T_RoleQuery();
        public void Create(T_RoleEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_RoleEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_RoleEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }
        public string GetIdByCode(string roleCode, string customerId)
        {
            return _query.GetIdByCode(roleCode, customerId);
        }
    }
}
