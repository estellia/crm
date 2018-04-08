using System;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.DbAccess.t_customer_shop;
using Xgx.SyncData.DbAccess.t_unit;
using Xgx.SyncData.DbAccess.T_City;
using Xgx.SyncData.DbAccess.T_Type;
using Xgx.SyncData.DbAccess.T_Unit_Relation;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DomainSubscribeService
{
    public class UnitDomainService
    {
        public void Deal(UnitContract contract)
        {
            if (contract.UnitId == ConfigMgr.XgxHeadUnitId)
                return;
            if (contract.ParentUnitId == ConfigMgr.XgxHeadUnitId)
            {
                contract.ParentUnitId = ConfigMgr.HeadUnitId;
            }
            var dbEntity = ConvertTot_unit(contract);
            var dbApEntity = ConvertTot_customer_shopEntity(contract);
            var unitFacade = new t_unitFacade();
            var unitRelationFacade = new T_Unit_RelationFacade();
            var unitApFacade = new t_customer_shopFacade();
            switch (contract.Operation)
            {
                case OptEnum.Create:
                    unitFacade.Create(dbEntity);
                    unitApFacade.Create(dbApEntity);
                    unitRelationFacade.Create(ConvertToT_Unit_Relation(contract));
                    break;
                case OptEnum.Update:
                    unitFacade.Update(dbEntity);
                    unitApFacade.Update(dbApEntity);
                    unitRelationFacade.Update(ConvertToT_Unit_Relation(contract));
                    break;
                case OptEnum.Delete:
                    unitFacade.Delete(dbEntity);
                    unitApFacade.Delete(dbApEntity);
                    unitRelationFacade.Delete(ConvertToT_Unit_Relation(contract));
                    break;
            }
        }

        private t_unitEntity ConvertTot_unit(UnitContract contract)
        {
            var typeFacade = new T_TypeFacade();
            var cityFacade = new T_CityFacade();
            var dbEntity = new t_unitEntity
            {
                unit_id = contract.UnitId,
                unit_code = contract.UnitCode,
                unit_name = contract.UnitName,
                type_id = typeFacade.GetIdByCode(contract.TypeCode, ConfigMgr.CustomerId),
                unit_name_en = contract.UnitNameEn,
                unit_name_short = contract.UnitNameShort,
                unit_city_id = cityFacade.GetIdByName(contract.City1Name, contract.City2Name, contract.City3Name),
                unit_address = contract.UnitAddress,
                unit_contact = contract.UnitContact,
                unit_tel = contract.UnitTel,
                unit_fax = contract.UnitFax,
                unit_email = contract.UnitEmail,
                unit_postcode = contract.UnitPostcode,
                unit_remark = contract.UnitRemark,
                create_time = contract.CreateTime != null ? contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : null,
                modify_time = contract.ModifyTime != null ? contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : null,
                StoreType = contract.StoreType,
                customer_id = ConfigMgr.CustomerId,
                longitude = contract.Longitude,
                dimension = contract.Latitude,
                Status = "1"
            };
            return dbEntity;
        }

        private T_Unit_RelationEntity ConvertToT_Unit_Relation(UnitContract contract)
        {
            if (contract.Operation == OptEnum.Create)
            {
                var dbEntity = new T_Unit_RelationEntity
                {
                    unit_relation_id = Guid.NewGuid().ToString("N"),
                    src_unit_id = contract.ParentUnitId,
                    dst_unit_id = contract.UnitId,
                    status = 1,
                    create_time = contract.CreateTime,
                    modify_time = contract.ModifyTime
                };
                return dbEntity;
            }
            else
            {
                var unitRelationFacade = new T_Unit_RelationFacade();
                var dbEntity = unitRelationFacade.GetEntityByDstId(contract.UnitId);
                dbEntity.src_unit_id = contract.ParentUnitId;
                dbEntity.dst_unit_id = contract.UnitId;
                dbEntity.status = 1;
                dbEntity.create_time = contract.CreateTime;
                dbEntity.modify_time = contract.ModifyTime;
                return dbEntity;
            }
        }

        private t_customer_shopEntity ConvertTot_customer_shopEntity(UnitContract contract)
        {
            var result = new t_customer_shopEntity
            {
                customer_id = ConfigMgr.CustomerId,
                customer_shop_id = contract.UnitId,
                cs_code = contract.UnitCode,
                cs_name = contract.UnitName,
                cs_status = 1,
                cs_status_desc = "正常"
            };
            return result;
        }
    }
}
