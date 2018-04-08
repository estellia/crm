using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.VipIntegral
{
    public class VipIntegralFacade
    {
        private readonly VipIntegralCMD _cmd = new VipIntegralCMD();
        private readonly VipIntegralQuery _query = new VipIntegralQuery();
        public void Create(VipIntegralEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(VipIntegralEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(VipIntegralEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }
        public int GetVipIntegralCountByVipId(string vipId)
        {
            return _query.GetVipIntegralCountByVipId(vipId);
        }
        public void UpdateVipIntegral(string vipId, int integral)
        {
            _cmd.UpdateVipIntegral(vipId, integral);
        }
    }
}
