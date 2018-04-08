using Xgx.SyncData.DbAccess.t_unit;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.VipCardVipMapping
{
    public class VipCardVipMappingFacade
    {
        private readonly VipCardVipMappingCMD _cmd = new VipCardVipMappingCMD();
        private readonly VipCardVipMappingQuery _query = new VipCardVipMappingQuery();

        public void Create(VipCardVipMappingEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(VipCardVipMappingEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(VipCardVipMappingEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }
        public string GetVipCardCodeByVipId(string vipId)
        {
           return _query.GetVipCardCodeByVipId(vipId);
        }
    }
}
