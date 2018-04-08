using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_ItemSkuProp
{
    public class T_ItemSkuPropFacade
    {
        private readonly T_ItemSkuPropCMD _cmd = new T_ItemSkuPropCMD();
        public void Create(T_ItemSkuPropEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_ItemSkuPropEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_ItemSkuPropEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public void DeleteByItemId(string itemId)
        {
            _cmd.DeleteByItemId(itemId);
        }
    }
}
