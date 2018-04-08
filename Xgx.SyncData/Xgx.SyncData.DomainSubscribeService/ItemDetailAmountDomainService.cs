using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.Contract.Contract;
using Xgx.SyncData.DbAccess.T_Sku_Price;
using Xgx.SyncData.DbAccess.T_Item_Property;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DomainSubscribeService
{
    public class ItemDetailAmountDomainService
    {
        public void Deal(ItemDetailAmountContract contract)
        {
            var facade = new T_Sku_PriceFacade();
            var itemPropFacade = new T_Item_PropertyFacade();
            facade.DeleteBySkuIdAndPriceTypeId(contract.ItemDetailId, ConfigMgr.ItemPriceTypeId_Inventory);
            facade.DeleteBySkuIdAndPriceTypeId(contract.ItemDetailId, ConfigMgr.ItemPriceTypeId_SalesVolume);
            itemPropFacade.DeleteBySkuId(contract.ItemDetailId);
            var dbEntityList = ConvertToT_Sku_PriceEntityList(contract);
            switch (contract.Operation)
            {
                case OptEnum.Create:
                case OptEnum.Update:
                    foreach (var i in dbEntityList)
                    {
                        facade.Create(i);
                    }
                    itemPropFacade.CreateQty(contract.ItemDetailId);
                    itemPropFacade.CreateSalesCount(contract.ItemDetailId);
                    break;
                case OptEnum.Delete:
                    break;
            }
        }

        private List<T_Sku_PriceEntity> ConvertToT_Sku_PriceEntityList(ItemDetailAmountContract contract)
        {
            var result = new List<T_Sku_PriceEntity>();
            var inventory = new T_Sku_PriceEntity
            {
                sku_price_id = Guid.NewGuid().ToString("N"),
                sku_id = contract.ItemDetailId,
                item_price_type_id = ConfigMgr.ItemPriceTypeId_Inventory,
                sku_price = contract.Inventory,
                status = "1",
                create_time = contract.CreateTime == null ? null : contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                modify_time = contract.ModifyTime == null ? null : contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                bat_id = null,
                if_flag = "0",
                customer_id = ConfigMgr.CustomerId
            };
            result.Add(inventory);
            var salesVolume = new T_Sku_PriceEntity
            {
                sku_price_id = Guid.NewGuid().ToString("N"),
                sku_id = contract.ItemDetailId,
                item_price_type_id = ConfigMgr.ItemPriceTypeId_SalesVolume,
                sku_price = contract.SalesVolume,
                status = "1",
                create_time = contract.CreateTime == null ? null : contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                modify_time = contract.ModifyTime == null ? null : contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                bat_id = null,
                if_flag = "0",
                customer_id = ConfigMgr.CustomerId
            };
            result.Add(salesVolume);
            return result;
        }
    }
}
