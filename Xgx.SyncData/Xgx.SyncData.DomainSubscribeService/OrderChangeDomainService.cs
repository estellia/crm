using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.Contract.Contract;
using Xgx.SyncData.Contract.Dependency;
using Xgx.SyncData.DbAccess.T_Inout;

namespace Xgx.SyncData.DomainSubscribeService
{
    public class OrderChangeDomainService
    {
        public void Deal(OrderChangeContract contract)
        {
            var orderFacade = new T_InoutFacade();

            switch (contract.ChangeOperation)
            {
                case EnumOrderChange.OrderChangeTime:
                    orderFacade.UpdateDeliveryTime(contract.ChangeValue, contract.OrderId);
                    break;
                case EnumOrderChange.Approval:
                    orderFacade.UpdateStatus(contract.ChangeValue, contract.OrderId, ConfigMgr.CustomerId);
                    break;
                case EnumOrderChange.StockDone:
                    orderFacade.UpdateStockDoneStatus(contract.OrderId, ConfigMgr.CustomerId);
                    break;
                case EnumOrderChange.OrderUnit:

                    if (ConfigMgr.XgxHeadUnitId == contract.ChangeValue)
                    {
                        orderFacade.UpdateSalesUnitId(ConfigMgr.HeadUnitId, contract.OrderId, ConfigMgr.CustomerId);
                    }
                    break;
                case EnumOrderChange.NoApproval:
                    orderFacade.UpdateNoApprovalStatus(contract.OrderId, ConfigMgr.CustomerId);
                    break;
                default:
                    break;

            }
        }
    }
}
