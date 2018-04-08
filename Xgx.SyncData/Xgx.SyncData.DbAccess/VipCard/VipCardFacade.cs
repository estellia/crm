using Xgx.SyncData.DbAccess.t_unit;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.VipCard
{
    public class VipCardFacade
    {
        private readonly VipCardCMD _cmd = new VipCardCMD();
  
        public void Create(VipCardEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(VipCardEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(VipCardEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

    }
}
