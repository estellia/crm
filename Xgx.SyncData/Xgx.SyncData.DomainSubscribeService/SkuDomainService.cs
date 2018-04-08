using System;
using System.Collections.Generic;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.DbAccess.T_Prop;
using Xgx.SyncData.DbAccess.T_Sku_Property;
using Xgx.SyncData.DbAccess.Vip;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DomainSubscribeService
{
    public class SkuDomainService
    {
        public void Deal(SkuContract contract)
        {
            var skuNameDbEntity = ConvertToSkuName(contract);  //转换成t_prop对象
            var facade = new T_PropFacade();
            var skuFacade = new T_Sku_PropertyFacade();
            var result = facade.GetIdByParentId(contract.SkuId);  //获取子数据
            foreach (var i in result)  //把所有子数据都删除
            {
                facade.Delete(new T_PropEntity { prop_id = i});
            }
            var skuValueDbEntityList = ConvertToSkuValue(contract);  //生成新的sku值的列表（t_prop表）
            var skuDbEntity = ConvertToSku(contract);  //转换成T_Sku_Property表
            switch (contract.Operation)
            {
                case OptEnum.Create:
                    facade.Create(skuNameDbEntity);
                    skuFacade.Create(skuDbEntity);
                    foreach (var i in skuValueDbEntityList)
                    {
                        facade.Create(i);
                    }
                    break;
                case OptEnum.Update:
                    facade.Update(skuNameDbEntity);
                    skuFacade.Update(skuDbEntity);
                    foreach (var i in skuValueDbEntityList)//创建子对象
                    {
                        facade.Create(i);
                    }
                    break;
                case OptEnum.Delete:
                    facade.Delete(skuNameDbEntity);
                    skuFacade.Delete(skuDbEntity);
                    break;
            }
        }

        private T_PropEntity ConvertToSkuName(SkuContract contract)
        {
            var dbEntity = new T_PropEntity
            {
                prop_id = contract.SkuId,
                prop_code = contract.SkuCode,
                prop_name = contract.SkuName,
                prop_type = "2",
                parent_prop_id = "-99",
                prop_level = 1,
                prop_domain = "SKU",
                prop_input_flag = "select",
                prop_max_lenth = 1000,
                status = 1,
                display_index = 0,
                create_time = contract.CreateTime != null ? contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                modify_time = contract.ModifyTime != null ? contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
            };
            return dbEntity;
        }

        private List<T_PropEntity> ConvertToSkuValue(SkuContract contract)
        {
            var result = new List<T_PropEntity>();
            int displayIndex = 0;
            foreach (var i in contract.ValueList)
            {
                var dbEntity = new T_PropEntity
                {
                    prop_id = i.SkuValueId,
                    prop_code = i.SkuValueCode,
                    prop_name = i.SkuValueName,
                    prop_type = "3",
                    parent_prop_id = contract.SkuId,
                    prop_level = 2,
                    prop_domain = "SKU",
                    prop_input_flag = "select",
                    prop_max_lenth = 1000,
                    status = 1,
                    display_index = ++displayIndex,
                    create_time = i.CreateTime != null ? contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                    modify_time = i.ModifyTime != null ? contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                };
                result.Add(dbEntity);
            }
            return result;

        }

        private T_Sku_PropertyEntity ConvertToSku(SkuContract contract)
        {
            var result = new T_Sku_PropertyEntity
            {
                sku_prop_id = contract.SkuId,
                prop_id = contract.SkuId,
                display_index = 0,
                status = "1",
                create_time = contract.CreateTime != null ? contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                modify_time = contract.ModifyTime != null ? contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                CustomerID = ConfigMgr.CustomerId
            };
            return result;
        }
    }
}
