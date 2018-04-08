using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Item
{
    public class T_ItemFacade
    {
        private readonly T_ItemCMD _cmd = new T_ItemCMD();
        private readonly T_ItemQuery _query = new T_ItemQuery();
        public void Create(T_ItemEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_ItemEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_ItemEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public T_ItemEntity GetItemBySkuId(string skuid)
        {
            return _query.GetItemBySkuId(skuid);
        }


    }
}
