using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.VipAmount
{
    public class VipAmountFacade
    {
        private readonly VipAmountCMD _cmd = new VipAmountCMD();
        private readonly VipAmountQuery _query = new VipAmountQuery();
        public void Create(VipAmountEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(VipAmountEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(VipAmountEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }
        public int GetVipAmountCountByVipId(string vipId)
        {
            return _query.GetVipAmountCountByVipId(vipId);
        }
        public void UpdateVipAmount(string vipId, decimal amount)
        {
            _cmd.UpdateVipAmount(vipId, amount);
        }
    }
}
