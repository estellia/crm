using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.DbAccess.T_Item;
using Xgx.SyncData.DbAccess.T_ItemSkuProp;
using Xgx.SyncData.DbAccess.T_Item_Delivery_Mapping;
using Xgx.SyncData.DbAccess.T_Item_Property;
using Xgx.SyncData.DbAccess.T_Prop;
using Xgx.SyncData.DbAccess.T_Sku;
using Xgx.SyncData.DbAccess.T_Sku_Price;
using Xgx.SyncData.DbEntity;
namespace Xgx.SyncData.DomainSubscribeService
{
    public class ItemDomainService
    {
        public void Deal(ItemContract contract)
        {
            var itemFacade = new T_ItemFacade();
            var itemSkuPropFacade = new T_ItemSkuPropFacade();
            var skuPriceFacade = new T_Sku_PriceFacade();
            var skuFacade = new T_SkuFacade();
            var itemDeliveryMappingFacade = new T_Item_Delivery_MappingFacade();
            var itemPropertyFacade = new T_Item_PropertyFacade();
            var propFacade = new T_PropFacade();
            itemFacade.Delete(new T_ItemEntity {item_id = contract.ItemId});
            itemSkuPropFacade.DeleteByItemId(contract.ItemId);
            skuPriceFacade.DeleteByItemId(contract.ItemId);
            skuFacade.DeleteByItemId(contract.ItemId);
            itemDeliveryMappingFacade.DeleteByItemId(contract.ItemId);
            switch (contract.Operation)
            {
                case OptEnum.Create:
                case OptEnum.Update:
                    if (contract.ItemDetailList == null || contract.ItemDetailList.Count == 0)
                        return;
                    var itemEntity = ConvertToT_ItemEntity(contract);
                    var itemSkuPropEntity = ConvertToT_ItemSkuPropEntity(contract);
                    var skuPriceEntityList = ConvertToT_Sku_PriceEntityList(contract);
                    var skuEntityList = ConvertToT_SkuEntityList(contract);
                    var deliveryList = ConvertToT_Item_Delivery_MappingEntityList(contract);
                    itemFacade.Create(itemEntity);
                    if (itemSkuPropEntity != null)
                    {
                        itemSkuPropFacade.Create(itemSkuPropEntity);
                    }
                    foreach (var i in skuPriceEntityList)
                    {
                        skuPriceFacade.Create(i);
                    }
                    foreach (var i in skuEntityList)
                    {
                        skuFacade.Create(i);
                    }
                    foreach (var i in deliveryList)
                    {
                        itemDeliveryMappingFacade.Create(i);
                    }
                    if (contract.ItemId.StartsWith("S_"))
                    {
                        //删除库存
                        itemPropertyFacade.DeleteByItemId(contract.ItemId);
                        //添加库存
                        var inventory = new T_Item_PropertyEntity
                        {
                            item_property_id = Guid.NewGuid().ToString("N"),
                            item_id = contract.ItemId,
                            prop_id = propFacade.GetIdByCode("Qty"),
                            prop_value = "10000000",
                            status = "1",
                            create_time =
                                contract.CreateTime == null
                                    ? null
                                    : contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                            modify_time =
                                contract.ModifyTime == null
                                    ? null
                                    : contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss")
                        };
                        itemPropertyFacade.Create(inventory);
                    }
                    break;
            }
        }
        private T_ItemEntity ConvertToT_ItemEntity(ItemContract contract)
        {
            var result = new T_ItemEntity
            {
                item_id = contract.ItemId,
                item_category_id = contract.ItemCategoryId,
                item_code = contract.ItemCode,
                item_name = contract.ItemName,
                item_name_en = contract.ItemNameEn,
                item_name_short = contract.ItemNameShort,
                pyzjm = contract.Pyzjm,
                item_remark = contract.ItemRemark,
                status = "1",
                status_desc = "正常",
                create_time = contract.CreateTime == null ? null : contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                modify_time = contract.ModifyTime == null ? null : contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                bat_id = null,
                if_flag = "0",
                ifgifts = 0,
                ifoften = 0,
                ifservice = 0,
                IsGB = 1,
                data_from = "xgx",
                display_index = 0,
                imageUrl = null,
                CustomerId = ConfigMgr.CustomerId
            };
            return result;
        }

        private T_ItemSkuPropEntity ConvertToT_ItemSkuPropEntity(ItemContract contract)
        {
            if (contract.SkuNameIdList == null)
                return null;
            var skuCount = contract.SkuNameIdList.Count;
            if (skuCount == 0)
                return null;
            var itemSkuPropEntity = new T_ItemSkuPropEntity
            {
                ItemSkuPropID = Guid.NewGuid().ToString("N"),
                Item_id = contract.ItemId,
                ItemSku_prop_1_id = skuCount > 0 ? contract.SkuNameIdList[0] : null,
                ItemSku_prop_2_id = skuCount > 1 ? contract.SkuNameIdList[1] : null,
                ItemSku_prop_3_id = skuCount > 2 ? contract.SkuNameIdList[2] : null,
                ItemSku_prop_4_id = skuCount > 3 ? contract.SkuNameIdList[3] : null,
                ItemSku_prop_5_id = skuCount > 4 ? contract.SkuNameIdList[4] : null,
                status = "1",
                IsDelete = 0,
                create_time = contract.CreateTime,
                modify_time = contract.ModifyTime
            };
            return itemSkuPropEntity;
        }

        private List<T_Sku_PriceEntity> ConvertToT_Sku_PriceEntityList(ItemContract contract)
        {
            var result = new List<T_Sku_PriceEntity>();
            if (contract.ItemDetailList == null)
                return result;
            foreach (var i in contract.ItemDetailList)
            {
                var skuPriceEntityOriginalPrice = new T_Sku_PriceEntity
                {
                    sku_price_id = Guid.NewGuid().ToString("N"),
                    sku_id = i.ItemDetailId,
                    item_price_type_id = ConfigMgr.ItemPriceTypeId_OriginalPrice,
                    sku_price = i.OriginalPrice,
                    status = "1",
                    create_time = i.CreateTime == null ? null : i.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                    modify_time = i.ModifyTime == null ? null : i.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                    bat_id = null,
                    if_flag = "0",
                    customer_id = ConfigMgr.CustomerId
                };
                result.Add(skuPriceEntityOriginalPrice);
                var skuPriceEntityRetailPrice = new T_Sku_PriceEntity
                {
                    sku_price_id = Guid.NewGuid().ToString("N"),
                    sku_id = i.ItemDetailId,
                    item_price_type_id = ConfigMgr.ItemPriceTypeId_RetailPrice,
                    sku_price = i.RetailPrice,
                    status = "1",
                    create_time = i.CreateTime == null ? null : i.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                    modify_time = i.ModifyTime == null ? null : i.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                    bat_id = null,
                    if_flag = "0",
                    customer_id = ConfigMgr.CustomerId
                };
                result.Add(skuPriceEntityRetailPrice);
                if (contract.ItemId.StartsWith("S_"))
                {
                    var skuPriceEntityInventory = new T_Sku_PriceEntity
                    {
                        sku_price_id = Guid.NewGuid().ToString("N"),
                        sku_id = i.ItemDetailId,
                        item_price_type_id = ConfigMgr.ItemPriceTypeId_Inventory,
                        sku_price = 10000000,
                        status = "1",
                        create_time = i.CreateTime == null ? null : i.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                        modify_time = i.ModifyTime == null ? null : i.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                        bat_id = null,
                        if_flag = "0",
                        customer_id = ConfigMgr.CustomerId
                    };
                    result.Add(skuPriceEntityInventory);
                }
            }
            return result;
        }

        private List<T_SkuEntity> ConvertToT_SkuEntityList(ItemContract contract)
        {
            var result = new List<T_SkuEntity>();
            if (contract.ItemDetailList == null)
                return result;
            foreach (var i in contract.ItemDetailList)
            {
                var skuValueCount = 0;
                if (i.SkuValueIdList != null)
                {
                    skuValueCount = i.SkuValueIdList.Count;
                }
                var skuEntity = new T_SkuEntity
                {
                    sku_id = i.ItemDetailId,
                    item_id = contract.ItemId,
                    sku_prop_id1 = skuValueCount > 0 ? i.SkuValueIdList[0] : null,
                    sku_prop_id2 = skuValueCount > 1 ? i.SkuValueIdList[1] : null,
                    sku_prop_id3 = skuValueCount > 2 ? i.SkuValueIdList[2] : null,
                    sku_prop_id4 = skuValueCount > 3 ? i.SkuValueIdList[3] : null,
                    sku_prop_id5 = skuValueCount > 4 ? i.SkuValueIdList[4] : null,
                    barcode = i.BarCode,
                    status = "1",
                    create_time = i.CreateTime == null ? null : i.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                    modify_time = i.ModifyTime == null ? null : i.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                    bat_id = "1",
                    if_flag = "0"
                };
                result.Add(skuEntity);
            }
            return result;
        }

        private List<T_Item_Delivery_MappingEntity> ConvertToT_Item_Delivery_MappingEntityList(ItemContract contract)
        {
            var result = new List<T_Item_Delivery_MappingEntity>();
            if (contract.DeliveryList == null || contract.DeliveryList.Count == 0)
            {
                var itemDeliveryMappingEntityUnkown = new T_Item_Delivery_MappingEntity
                {
                    Item_Delivery_Id = Guid.NewGuid().ToString("N"),
                    CustomerId = ConfigMgr.CustomerId,
                    Item_Id = contract.ItemId,
                    DeliveryId = 2,
                    Create_Time = contract.CreateTime == null ? null : contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                    Modify_Time = contract.ModifyTime == null ? null : contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                };
                result.Add(itemDeliveryMappingEntityUnkown);
                return result;
            }

            foreach (var i in contract.DeliveryList)
            {
                if (i == EnumDelivery.Unknown)
                {
                    var itemDeliveryMappingEntityUnkown = new T_Item_Delivery_MappingEntity
                    {
                        Item_Delivery_Id = Guid.NewGuid().ToString("N"),
                        CustomerId = ConfigMgr.CustomerId,
                        Item_Id = contract.ItemId,
                        DeliveryId = (int)EnumDelivery.ShopPickUp,
                        Create_Time = contract.CreateTime == null ? null : contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                        Modify_Time = contract.ModifyTime == null ? null : contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                    };
                    result.Add(itemDeliveryMappingEntityUnkown);
                    break;
                }
                var itemDeliveryMappingEntity = new T_Item_Delivery_MappingEntity
                {
                    Item_Delivery_Id = Guid.NewGuid().ToString("N"),
                    CustomerId = ConfigMgr.CustomerId,
                    Item_Id = contract.ItemId,
                    DeliveryId = (int)i,
                    Create_Time = contract.CreateTime == null ? null : contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                    Modify_Time = contract.ModifyTime == null ? null : contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                };
                result.Add(itemDeliveryMappingEntity);
            }
            return result;
        }
    }
}
