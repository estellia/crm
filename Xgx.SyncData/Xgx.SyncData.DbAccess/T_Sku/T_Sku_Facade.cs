using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Sku
{
    public class T_SkuFacade
    {
        private readonly T_SkuCMD _cmd = new T_SkuCMD();
        public void Create(T_SkuEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_SkuEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_SkuEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public void DeleteByItemId(string itemId)
        {
            _cmd.DeleteByItemId(itemId);
        }
    }
}
