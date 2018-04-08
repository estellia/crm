using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.t_customer_user
{
    public class t_customer_userFacade
    {
        private readonly t_customer_userCMD _cmd = new t_customer_userCMD();
        public void Create(t_customer_userEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(t_customer_userEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(t_customer_userEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }
    }
}
