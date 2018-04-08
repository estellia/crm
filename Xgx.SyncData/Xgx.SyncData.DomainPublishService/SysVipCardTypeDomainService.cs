using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.DbAccess.SysVipCardType;
using Xgx.SyncData.DbAccess.t_unit;
using Zmind.EventBus.Contract;
using OptEnum = Xgx.SyncData.Contract.OptEnum;

namespace Xgx.SyncData.DomainPublishService
{
    public class SysVipCardTypeDomainService: IPublish
    {
        public void Deal(EventContract msg)
        {
            var bus = MqBusMgr.GetInstance();
            OptEnum operation;
            Enum.TryParse(msg.Operation.ToString(), out operation);
            operation = OptEnum.Create;
            var sysVipCardType = new SysVipCardTypeContract()
            {
                Operation = operation,
                VipCardTypeID = Convert.ToInt32(msg.Id)
            };
            if (msg.Operation != Zmind.EventBus.Contract.OptEnum.Delete)
            {
                var sysVipCardTypeFacade = new SysVipCardTypeFacade();
                var result = sysVipCardTypeFacade.GetCardTypeIDByTypeId(sysVipCardType.VipCardTypeID);
                if (result == null) return;
                sysVipCardType.VipCardTypeName = result.VipCardTypeName;
                sysVipCardType.VipCardLevel = result.VipCardLevel.ToString();
            }
            bus.Publish<IZmindToXgx>(sysVipCardType);
        }
    }
}
