using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.VipAmountDetail
{
    public class VipAmountDetailFacade
    {
        private readonly VipAmountDetailCMD _cmd = new VipAmountDetailCMD();
        public void Create(VipAmountDetailEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(VipAmountDetailEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(VipAmountDetailEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }
    }
}
