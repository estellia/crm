using Xgx.SyncData.DbAccess.T_City;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.Vip
{
    public class VipFacade
    {
        private readonly VipCMD _cmd = new VipCMD();
        private readonly VipQuery _query = new VipQuery();
        public void Create(VipEntity dbEntity)
        {
         

            _cmd.Create(dbEntity);
        }
        public void Update(VipEntity dbEntity)
        {
            
            _cmd.Update(dbEntity);
        }

        public void Delete(VipEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public VipEntity GetById(string id)
        {
            return _query.GetById(id);
        }
    }
}
