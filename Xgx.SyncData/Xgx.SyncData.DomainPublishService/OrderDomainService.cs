using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.Contract.Dependency;
using Xgx.SyncData.Contract.Enum;
using Zmind.EventBus.Contract;
using Xgx.SyncData.DbAccess.T_Inout;
using Xgx.SyncData.DbAccess.T_Item;
using Xgx.SyncData.DbAccess.T_Sku;
using Xgx.SyncData.DbEntity;
using OptEnum = Xgx.SyncData.Contract.OptEnum;

namespace Xgx.SyncData.DomainPublishService
{
    public class OrderDomainService : IPublish
    {
        private const string _dataFrom = "3";

        public void Deal(EventContract msg)
        {
            var bus = MqBusMgr.GetInstance();
            OptEnum operation;
            Enum.TryParse(msg.Operation.ToString(), out operation);
            var orderContract = new OrderContract()
            {
                Operation = operation,
                OrderId = msg.Id
            };
            if (msg.Operation != Zmind.EventBus.Contract.OptEnum.Delete)
            {
                var orderFacade = new T_InoutFacade();
                var result = orderFacade.GetOrderByOrderId(msg.Id);
                if (result == null) return;

                var orderStatus = (EnumOrderStatus) Enum.Parse(typeof(EnumOrderStatus), result.Field7);
                if (orderStatus != EnumOrderStatus.PendingPay)
                {
                    //return;
                }
                orderContract.OrderNo = result.order_no;
                orderContract.OrderType = GetEnumOrderType(result.order_type_id);
                orderContract.OrderReason = GetEnumOrderReason(result.order_reason_id);
                orderContract.OrderDate = result.order_date ?? String.Empty;
                orderContract.RequestDate = result.reserveDay ?? String.Empty;
                orderContract.RequestDateQuantum = result.reserveQuantum ?? String.Empty;
                orderContract.CompleteDate = result.complete_date ?? String.Empty;
                orderContract.CreateUnit = result.unit_id ?? String.Empty;
                orderContract.TotalAmount = result.total_amount;
                orderContract.DiscountRate = result.discount_rate;
                orderContract.ActualAmount = result.actual_amount;
                orderContract.ReceivePoints = result.receive_points;
                orderContract.PayPoints = result.pay_points;
                orderContract.Remark = result.remark ?? String.Empty;
                orderContract.Status = orderStatus;
                orderContract.TotalQty = result.total_qty;
                orderContract.TotalRetail = result.total_retail;
                orderContract.KeepTheChange = result.keep_the_change;
                orderContract.WipingZero = result.wiping_zero;
                orderContract.VipNo = result.vip_no ?? String.Empty;
                orderContract.CreateTime = result.create_time ?? String.Empty;
                orderContract.CreateUserId = result.create_user_id ?? String.Empty;
                orderContract.ApproveTime = result.approve_time ?? String.Empty;
                orderContract.ApproveUserId = result.approve_user_id ?? String.Empty;
                orderContract.SendUserId = result.send_user_id ?? String.Empty;
                orderContract.SendTime = result.send_time ?? String.Empty;
                orderContract.AccpectUserId = result.accpect_user_id ?? String.Empty;
                orderContract.AccpectTime = result.accpect_time ?? String.Empty;
                orderContract.ModifyUserId = result.modify_user_id ?? String.Empty;
                orderContract.ModifyTime = result.modify_time ?? String.Empty;
                orderContract.DataFrom =
                    (EnumOrderDataFrom) Enum.Parse(typeof(EnumOrderDataFrom), result.data_from_id ?? _dataFrom);
                orderContract.SalesUnt = result.sales_unit_id ?? String.Empty;
                orderContract.PurchaseUnit = result.purchase_unit_id ?? String.Empty;
                orderContract.SalesWarehouse = result.sales_warehouse_id ?? String.Empty;
                orderContract.PurchaseWarehouse = result.purchase_warehouse_id ?? String.Empty;
                orderContract.IsPay = (EnumIsPay)int.Parse(result.Field1);
                orderContract.TrackingNumber = result.Field2 ?? String.Empty;
                orderContract.BalancePayment = result.Field3 ?? String.Empty;
                orderContract.Address = result.Field4 ?? String.Empty;
                orderContract.Phone = result.Field6 ?? String.Empty;
                orderContract.Delivery = (EnumDelivery)int.Parse(result.Field8);
                orderContract.DeliveryDateTime = result.Field9 ?? String.Empty;
                orderContract.UserName = result.Field14 ?? String.Empty;
                orderContract.CashBack = result.ReturnCash;
                orderContract.DetailList = new List<OrderDetail>();
                
                #region 明细数据

                var orderDetailFacade = new T_InoutDetailFacade();
                var itemFacade = new T_ItemFacade();

                var resultList = orderDetailFacade.GetOrderDetailListByOrderId(msg.Id);
                OrderDetail tmpDetail;

                foreach (var detialEntity in resultList)
                {
                    var item = itemFacade.GetItemBySkuId(detialEntity.sku_id);

                    tmpDetail = new OrderDetail();
                    tmpDetail.OrderDetailId = detialEntity.order_detail_id;
                    tmpDetail.SKUID = detialEntity.sku_id;
                    tmpDetail.UnitId = orderContract.CreateUnit;
                    tmpDetail.OrderQty = detialEntity.order_qty;
                    tmpDetail.EnterQty = detialEntity.enter_qty;
                    tmpDetail.EnterPrice = detialEntity.enter_price;
                    tmpDetail.EnterAmount = detialEntity.enter_amount;
                    tmpDetail.StdPrice = detialEntity.std_price;
                    tmpDetail.RetailPrice = detialEntity.retail_price;
                    tmpDetail.RetailAmount = detialEntity.retail_amount;
                    tmpDetail.PlanPrice = detialEntity.plan_price;
                    tmpDetail.ReceiverPoints = detialEntity.receive_points;
                    tmpDetail.PayPoints = detialEntity.pay_points;
                    tmpDetail.Remark = detialEntity.remark;
                    tmpDetail.PosOrderCode = detialEntity.pos_order_code;
                    tmpDetail.DisplayIndex = detialEntity.display_index;
                    tmpDetail.CreateTime = detialEntity.create_time;
                    tmpDetail.CreateUserId = detialEntity.create_user_id;
                    tmpDetail.ModifyTime = detialEntity.modify_time;
                    tmpDetail.ModifyUser = detialEntity.modify_user_id;
                    tmpDetail.ReturnCash = detialEntity.ReturnCash;

                    if (item.ifservice == 1)
                    {
                        tmpDetail.SKUID = ConfigMgr.VirtualGoodsSkuId;
                    }

                    orderContract.DetailList.Add(tmpDetail);
                }
                #endregion

            }
            var json = new JavaScriptSerializer().Serialize(orderContract);//测试用，抓对象的json格式
            bus.Publish<IZmindToXgx>(orderContract);
        }

        public EnumOrderType GetEnumOrderType(string orderType)
        {
            switch (orderType)
            {
                case "1F0A100C42484454BAEA211D4C14B80F":
                    return EnumOrderType.CKD;
                default:
                    throw new Exception("");
            }
        }

        public EnumOrderReason GetEnumOrderReason(string orderReason)
        {
            switch (orderReason)
            {
                case "2F6891A2194A4BBAB6F17B4C99A6C6F5":
                    return EnumOrderReason.Pos;
                default:
                    throw new Exception("");
            }
        }
    }
}
