using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Item_Property
{
    public class T_Item_PropertyFacade
    {
        private readonly T_Item_PropertyCMD _cmd = new T_Item_PropertyCMD();
        public void Create(T_Item_PropertyEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_Item_PropertyEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_Item_PropertyEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public void DeleteByItemId(string itemId)
        {
            _cmd.DeleteByItemId(itemId);
        }
        public void DeleteBySkuId(string skuId)
        {
            _cmd.DeleteBySkuId(skuId);
        }
        public void CreateQty(string skuId)
        {
            _cmd.CreateQty(skuId);
        }
        public void CreateSalesCount(string skuId)
        {
            _cmd.CreateSalesCount(skuId);
        }
    }
}
