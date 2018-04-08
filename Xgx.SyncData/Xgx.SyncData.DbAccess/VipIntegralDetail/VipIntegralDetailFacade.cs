using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.VipIntegralDetail
{
    public class VipIntegralDetailFacade
    {
        private readonly VipIntegralDetailCMD _cmd = new VipIntegralDetailCMD();
        public void Create(VipIntegralDetailEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(VipIntegralDetailEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(VipIntegralDetailEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }        
    }
}
