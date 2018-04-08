using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Sku_Price
{
    public class T_Sku_PriceFacade
    {
        private readonly T_Sku_PriceCMD _cmd = new T_Sku_PriceCMD();
        public void Create(T_Sku_PriceEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_Sku_PriceEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_Sku_PriceEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public void DeleteByItemId(string itemId)
        {
            _cmd.DeleteByItemId(itemId);
        }

        public void DeleteBySkuIdAndPriceTypeId(string skuId, string priceTypeId)
        {
            _cmd.DeleteBySkuIdAndPriceTypeId(skuId, priceTypeId);
        }
    }
}
