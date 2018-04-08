using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.t_customer_shop
{
    public class t_customer_shopFacade
    {
        private readonly t_customer_shopCMD _cmd = new t_customer_shopCMD();
        public void Create(t_customer_shopEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(t_customer_shopEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(t_customer_shopEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }
    }
}
