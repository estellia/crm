using System;
using System.Collections.Generic;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.Contract.Dependency;
using Xgx.SyncData.Contract.Enum;
using Xgx.SyncData.DbAccess.T_Inout;
using Xgx.SyncData.DbAccess.Vip;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DomainSubscribeService
{
    public class OrderDomainService
    {
        private const string red_flag = "1";

        public void Deal(OrderContract contract)
        {

            var orderDetailFacade = new T_InoutDetailFacade();
            var orderFacade = new T_InoutFacade();

            var inoutDetails = new List<T_Inout_DetailEntity>();
            foreach (var detail in contract.DetailList)
            {
                var tmp = ConvertToT_InoutDetial(contract.OrderId, detail);
                inoutDetails.Add(tmp);
            }

            switch (contract.Operation)
            {
                case OptEnum.Create:
                    var tInout = ConvertToT_Inout(contract);

                    orderFacade.Create(tInout);
                    foreach (var entity in inoutDetails)
                    {
                        orderDetailFacade.Create(entity);

                    }
                    break;
                case OptEnum.Update:
                    var tinout = orderFacade.GetOrderByOrderId(contract.OrderId);

                    if (contract.Status == EnumOrderStatus.Done)
                    {
                        
                    }

                    tinout.total_amount = tinout.total_amount + inoutDetails[0].enter_amount;
                    tinout.actual_amount = tinout.total_amount*(tinout.discount_rate/100);
                    tinout.modify_time = contract.ModifyTime;
                    tinout.modify_user_id = contract.ModifyUserId;

                    orderFacade.Update(tinout);
                    foreach (var entity in inoutDetails)
                    {
                        orderDetailFacade.Create(entity);
                    }
                    break;
                case OptEnum.Delete:

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private T_InoutEntity ConvertToT_Inout(OrderContract contract)
        {

            var vipFacade = new VipFacade();
            var vipEntity = vipFacade.GetById(contract.VipNo);

            var carrierId = contract.Delivery == EnumDelivery.HomeDelivery ? string.Empty : contract.CreateUnit;
            var statusStr = ((int)contract.Status).ToString();

            var result = new T_InoutEntity
            {
                order_id = contract.OrderId,
                order_no = contract.OrderNo,
                VipCardCode = vipEntity.VipCode,
                order_type_id = GetEnumOrderType(contract.OrderType),
                order_reason_id = GetEnumOrderReason(contract.OrderReason),
                red_flag = red_flag,
                warehouse_id = "",
                order_date = contract.OrderDate,
                request_date = string.Empty,
                complete_date = contract.CompleteDate,
                create_unit_id = contract.CreateUnit,
                unit_id = contract.CreateUnit,
                related_unit_id = string.Empty,
                related_unit_code = string.Empty,
                pos_id = string.Empty,
                shift_id = string.Empty,
                sales_user = string.Empty,
                total_amount = contract.TotalAmount,
                discount_rate = contract.DiscountRate,
                actual_amount = contract.ActualAmount,
                receive_points = contract.ReceivePoints,
                pay_points = contract.PayPoints,
                pay_id = string.Empty,
                print_times = 0,
                carrier_id = carrierId,
                remark = contract.Remark,
                status = statusStr,
                status_desc = GetStatusDescByStatus(contract.Status),
                total_qty = contract.TotalQty,
                total_retail = contract.TotalRetail,
                keep_the_change = contract.KeepTheChange,
                wiping_zero = contract.WipingZero,
                vip_no = contract.VipNo,
                create_time = contract.CreateTime,
                create_user_id = contract.CreateUserId,
                approve_time = contract.ApproveTime,
                approve_user_id = contract.ApproveUserId,
                send_user_id = contract.SendUserId,
                send_time = contract.SendTime,
                accpect_user_id = contract.AccpectUserId,
                accpect_time = contract.AccpectTime,
                modify_user_id = contract.ModifyUserId,
                modify_time = contract.ModifyTime,
                data_from_id = "3",
                sales_unit_id = contract.SalesUnt,
                purchase_unit_id = contract.PurchaseUnit,
                if_flag = "0",
                customer_id = ConfigMgr.CustomerId,
                sales_warehouse_id = contract.SalesWarehouse,
                purchase_warehouse_id = contract.PurchaseWarehouse,
                Field1 = ((int) contract.IsPay).ToString(),
                Field2 = contract.TrackingNumber,
                Field3 = contract.BalancePayment,
                Field4 = contract.Address,
                Field6 = contract.Phone,
                Field7 = statusStr,
                Field8 = ((int) contract.Delivery).ToString(),
                Field9 = contract.DeliveryDateTime,
                Field10 = GetStatusDescByStatus(contract.Status),
                Field11 = string.Empty,
                Field12 = string.Empty,
                Field13 = vipEntity.WeiXinUserId,
                Field14 = contract.UserName,
                Field15 = string.Empty,
                Field16 = string.Empty,
                Field17 = string.Empty,
                Field18 = string.Empty,
                Field19 = string.Empty,
                Field20 = string.Empty,
                reserveQuantum = contract.RequestDateQuantum,
                reserveDay = contract.RequestDate,
                paymentcenter_id = null,
                ReturnCash = contract.CashBack,
            };
            return result;
        }

        private T_Inout_DetailEntity ConvertToT_InoutDetial(string orderid, OrderDetail detail)
        {
            var result = new T_Inout_DetailEntity
            {
                order_detail_id = detail.OrderDetailId,
                order_id = orderid,
                sku_id = detail.SKUID,
                unit_id = detail.UnitId,
                order_qty = detail.OrderQty,
                enter_qty = detail.EnterQty,
                enter_price = detail.EnterPrice,
                enter_amount = detail.EnterAmount,
                std_price = detail.StdPrice,
                retail_price = detail.RetailPrice,
                retail_amount = detail.RetailAmount,
                plan_price = detail.PlanPrice,
                receive_points = detail.ReceiverPoints,
                pay_points = detail.PayPoints,
                remark = detail.Remark,
                pos_order_code = detail.PosOrderCode,
                display_index = detail.DisplayIndex,
                create_time = detail.CreateTime,
                create_user_id = detail.CreateUserId,
                modify_time = detail.ModifyTime,
                modify_user_id = detail.ModifyUser,
                ReturnCash = detail.ReturnCash ?? 0.00m
            };
            return result;
        }

        public string GetEnumOrderType(EnumOrderType orderType)
        {
            switch (orderType)
            {
                case EnumOrderType.CKD:
                    return "1F0A100C42484454BAEA211D4C14B80F";
                default:
                    throw new Exception("");
            }
        }

        public string GetEnumOrderReason(EnumOrderReason orderReason)
        {
            switch (orderReason)
            {
                case EnumOrderReason.Pos:
                    return "2F6891A2194A4BBAB6F17B4C99A6C6F5";
                default:
                    throw new Exception("");
            }
        }
        
        public string GetStatusDescByStatus(EnumOrderStatus status)
        {
            switch (status)
            {
                case EnumOrderStatus.PendingPay:
                    return "待付款";
                case EnumOrderStatus.Pending:
                    return "待审核";
                case EnumOrderStatus.PendingService:
                    return "待服务";
                case EnumOrderStatus.Stocking:
                    return "备货中";
                case EnumOrderStatus.PendingDelivery:
                    return "待发货";
                case EnumOrderStatus.PendingShip:
                    return "待发货";
                case EnumOrderStatus.Serviced:
                    return "已服务";
                case EnumOrderStatus.Picked:
                    return "已提货";
                case EnumOrderStatus.Shipped:
                    return "已发货";
                case EnumOrderStatus.Done:
                    return "交易完成";
                case EnumOrderStatus.Refunding:
                    return "退款中";
                case EnumOrderStatus.RefundDone:
                    return "退款完成";
                default:
                    return "错误";
            }
        }
    }
}
	
	
	
	
	
	
	
	
	
	