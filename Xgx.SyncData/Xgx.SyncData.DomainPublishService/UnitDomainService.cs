using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.DbAccess.t_unit;
using Zmind.EventBus.Contract;
using OptEnum = Xgx.SyncData.Contract.OptEnum;

namespace Xgx.SyncData.DomainPublishService
{
    public class UnitDomainService : IPublish
    {
        public void Deal(EventContract msg)
        {
            var bus = MqBusMgr.GetInstance();
            OptEnum operation;
            Enum.TryParse(msg.Operation.ToString(), out operation);
            var unitContract = new UnitContract
            {
                Operation = operation,
                UnitId = msg.Id
            };
            if (msg.Operation != Zmind.EventBus.Contract.OptEnum.Delete)
            {
                var unitFacade = new t_unitFacade();
                var result = unitFacade.GetUnitById(msg.Id);
                if (result == null) return;
                unitContract.UnitCode = result.unit_code;
                unitContract.UnitName = result.unit_name;
                unitContract.TypeCode = result.type_code;
                unitContract.ParentUnitId = result.src_unit_id;
                unitContract.UnitNameEn = result.unit_name_en;
                unitContract.UnitNameShort = result.unit_name_short;
                unitContract.City1Name = result.city1_name;
                unitContract.City2Name = result.city2_name;
                unitContract.City3Name = result.city3_name;
                unitContract.UnitAddress = result.unit_address;
                unitContract.UnitContact = result.unit_contract;
                unitContract.UnitTel = result.unit_tel;
                unitContract.UnitFax = result.unit_fax;
                unitContract.UnitEmail = result.unit_email;
                unitContract.UnitPostcode = result.unit_postcode;
                unitContract.UnitRemark = result.unit_remark;
                unitContract.CreateTime = string.IsNullOrEmpty(result.create_time) ? null : DateTime.Parse(result.create_time);
                unitContract.ModifyTime = string.IsNullOrEmpty(result.modify_time) ? null : DateTime.Parse(result.modify_time);
                unitContract.StoreType = result.StoreType;
            }
            bus.Publish<IZmindToXgx>(unitContract);
        }
    }
}
