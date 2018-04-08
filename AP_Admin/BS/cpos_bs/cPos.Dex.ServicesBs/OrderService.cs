using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Model;
using cPos.Dex.Components.SqlMappers;
using cPos.Dex.Common;
using cPos.Dex.ContractModel;
using cPos.Dex.Services;

namespace cPos.Dex.ServicesBs
{
    public class OrderService
    {
        #region CheckInoutOrders
        /// <summary>
        /// 检查InoutOrder
        /// </summary>
        public Hashtable CheckInoutOrder(string orderType, InoutOrderContract order)
        {
            Hashtable htError = new Hashtable();
            if (order == null)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据信息不能为空", true);
                return htError;
            }
            if (order.order_id == null || order.order_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据ID不能为空", true);
                return htError;
            }
            if (order.order_no == null || order.order_no.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据号码不能为空", true);
                return htError;
            }
            if (order.order_type_id == null || order.order_type_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据类型1不能为空", true);
                return htError;
            }
            if (order.order_reason_id == null || order.order_reason_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据类型2不能为空", true);
                return htError;
            }
            if (order.red_flag == null || order.red_flag.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据红单标记不能为空", true);
                return htError;
            }
            if (!(order.red_flag == "1" || order.red_flag == "-1"))
            {
                htError = ErrorService.OutputError(ErrorCode.A019, "单据红单标记数据不合法", true);
                return htError;
            }
            if (order.order_date == null || order.order_date.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据日期不能为空", true);
                return htError;
            }
            if (order.create_unit_id == null || order.create_unit_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据创建单位不能为空", true);
                return htError;
            }
            switch (orderType)
            {
                case "POS":

                    break;
                case "RT":
                    break;
                case "AJ":
                    if (order.warehouse_id == null || order.warehouse_id.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, "仓库标识不能为空", true);
                        return htError;
                    }
                    if (order.sales_unit_id == null || order.sales_unit_id.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, "销售单位标识不能为空", true);
                        return htError;
                    }
                    //if (order.purchase_unit_id == null || order.purchase_unit_id.Trim().Length == 0)
                    //{
                    //    htError = ErrorService.OutputError(ErrorCode.A016, "采购单位标识不能为空", true);
                    //    return htError;
                    //}
                    break;
            }
            // details
            if (order.details == null || order.details.Count == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "单据明细信息不能为空", true);
                return htError;
            }
            foreach (var orderDetail in order.details)
            {
                if (orderDetail.order_detail_id == null || orderDetail.order_detail_id.Trim().Length == 0)
                {
                    htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的ID不能为空", true);
                    return htError;
                }
                if (orderDetail.order_id == null || orderDetail.order_id.Trim().Length == 0)
                {
                    htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的主单据ID不能为空", true);
                    return htError;
                }
                if (orderDetail.sku_id == null || orderDetail.sku_id.Trim().Length == 0)
                {
                    htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的SKU不能为空", true);
                    return htError;
                }
                if (orderDetail.unit_id == null || orderDetail.unit_id.Trim().Length == 0)
                {
                    htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的单位ID不能为空", true);
                    return htError;
                }
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }

        /// <summary>
        /// 检查InoutOrder集合
        /// </summary>
        public Hashtable CheckInoutOrders(string orderType, IList<InoutOrderContract> orders)
        {
            Hashtable htError = new Hashtable();
            if (orders == null || orders.Count == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据集合不能为空", true);
                return htError;
            }
            foreach (var order in orders)
            {
                htError = CheckInoutOrder(orderType, order);
                if (!Convert.ToBoolean(htError["status"])) return htError;
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }
        #endregion

        #region SaveInoutOrders
        /// <summary>
        /// 保存InoutOrder集合
        /// </summary>
        public void SaveInoutOrders(IList<InoutOrderContract> orders,
            string customerId, string unitId, string userId, string orderType)
        {
            if (customerId == null || customerId.Trim().Length == 0)
                throw new Exception("客户ID不能为空");
            if (userId == null || userId.Trim().Length == 0)
                throw new Exception("用户ID不能为空");
            if (orderType == null || orderType.Trim().Length == 0)
                throw new Exception("单据处理类型不能为空");

            var orderInfoList = new List<cPos.Model.InoutInfo>();
            foreach (var order in orders)
            {
                cPos.Model.InoutInfo orderInfo = new cPos.Model.InoutInfo();
                #region order
                orderInfo.order_id = order.order_id;
                orderInfo.order_no = order.order_no;
                orderInfo.order_type_id = order.order_type_id;
                orderInfo.order_reason_id = order.order_reason_id;
                orderInfo.red_flag = order.red_flag;
                orderInfo.ref_order_id = order.ref_order_id;
                orderInfo.ref_order_no = order.ref_order_no;
                orderInfo.warehouse_id = order.warehouse_id;
                orderInfo.order_date = order.order_date;
                orderInfo.request_date = order.request_date;
                orderInfo.complete_date = order.complete_date;
                orderInfo.create_unit_id = order.create_unit_id;
                orderInfo.unit_id = order.unit_id;
                orderInfo.related_unit_id = order.related_unit_id;
                orderInfo.related_unit_code = order.related_unit_code;
                orderInfo.pos_id = order.pos_id;
                orderInfo.shift_id = order.shift_id;
                orderInfo.sales_user = order.sales_user;
                orderInfo.total_amount = Utils.GetDecimalVal(order.total_amount);
                orderInfo.discount_rate = Utils.GetDecimalVal(order.discount_rate);
                orderInfo.actual_amount = Utils.GetDecimalVal(order.actual_amount);
                orderInfo.receive_points = Utils.GetDecimalVal(order.receive_points);
                orderInfo.pay_points = Utils.GetDecimalVal(order.pay_points);
                orderInfo.pay_id = order.pay_id;
                orderInfo.print_times = Utils.GetIntVal(order.print_times);
                orderInfo.carrier_id = order.carrier_id;
                orderInfo.remark = order.remark;
                orderInfo.status = order.status;
                orderInfo.create_time = order.create_time;
                orderInfo.create_user_id = order.create_user_id;
                orderInfo.approve_time = order.approve_time;
                orderInfo.approve_user_id = order.approve_user_id;
                orderInfo.send_user_id = order.send_user_id;
                orderInfo.send_time = order.send_time;
                orderInfo.accpect_user_id = order.accpect_user_id;
                orderInfo.accpect_time = order.accpect_time;
                orderInfo.modify_user_id = order.modify_user_id;
                orderInfo.modify_time = order.modify_time;
                orderInfo.total_qty = Utils.GetDecimalVal(order.total_qty);
                orderInfo.total_retail = Utils.GetDecimalVal(order.total_retail);
                orderInfo.keep_the_change = Utils.GetDecimalVal(order.keep_the_change);
                orderInfo.wiping_zero = Utils.GetDecimalVal(order.wiping_zero);
                orderInfo.vip_no = order.vip_no;
                orderInfo.data_from_id = order.data_from_id;
                orderInfo.sales_unit_id = order.sales_unit_id;
                orderInfo.purchase_unit_id = order.purchase_unit_id;
                orderInfo.if_flag = order.if_flag;
                orderInfo.Field1 = order.field1;
                orderInfo.Field2 = order.field2;
                orderInfo.Field3 = order.field3;
                orderInfo.Field4 = order.field4;
                orderInfo.Field5 = order.field5;
                orderInfo.Field6 = order.field6;
                orderInfo.Field7 = order.field7;
                orderInfo.Field8 = order.field8;
                orderInfo.Field9 = order.field9;
                orderInfo.Field10 = order.field10;
                orderInfo.Field11 = order.field11;
                orderInfo.Field12 = order.field12;
                orderInfo.Field13 = order.field13;
                orderInfo.Field14 = order.field14;
                orderInfo.Field15 = order.field15;
                orderInfo.Field16 = order.field16;
                orderInfo.Field17 = order.field17;
                orderInfo.Field18 = order.field18;
                orderInfo.Field19 = order.field19;
                orderInfo.Field20 = order.field20;
                #endregion

                #region details
                orderInfo.InoutDetailList = new List<cPos.Model.InoutDetailInfo>();
                foreach (var detail in order.details)
                {
                    cPos.Model.InoutDetailInfo detailInfo = new cPos.Model.InoutDetailInfo();
                    detailInfo.order_detail_id = detail.order_detail_id;
                    detailInfo.order_id = detail.order_id;
                    detailInfo.ref_order_detail_id = detail.ref_order_detail_id;
                    detailInfo.sku_id = detail.sku_id;
                    detailInfo.unit_id = detail.unit_id;
                    detailInfo.enter_qty = Utils.GetDecimalVal(detail.enter_qty);
                    detailInfo.order_qty = Utils.GetDecimalVal(detail.order_qty);
                    detailInfo.enter_price = Utils.GetDecimalVal(detail.enter_price);
                    detailInfo.std_price = Utils.GetDecimalVal(detail.std_price);
                    detailInfo.discount_rate = Utils.GetDecimalVal(detail.discount_rate);
                    detailInfo.retail_price = Utils.GetDecimalVal(detail.retail_price);
                    detailInfo.retail_amount = Utils.GetDecimalVal(detail.retail_amount);
                    detailInfo.enter_amount = Utils.GetDecimalVal(detail.enter_amount);
                    detailInfo.receive_points = Utils.GetDecimalVal(detail.receive_points);
                    detailInfo.pay_points = Utils.GetDecimalVal(detail.pay_points);
                    detailInfo.remark = detail.remark;
                    detailInfo.order_detail_status = detail.order_detail_status;
                    detailInfo.display_index = Utils.GetIntVal(detail.display_index);
                    detailInfo.create_time = detail.create_time;
                    detailInfo.create_user_id = detail.create_user_id;
                    detailInfo.modify_time = detail.modify_time;
                    detailInfo.modify_user_id = detail.modify_user_id;
                    detailInfo.ref_order_id = detail.ref_order_id;
                    detailInfo.if_flag = Utils.GetIntVal(detail.if_flag);
                    detailInfo.pos_order_code = detail.pos_order_code;
                    detailInfo.plan_price = Utils.GetDecimalVal(detail.plan_price);
                    detailInfo.Field1 = detail.field1;
                    detailInfo.Field2 = detail.field2;
                    detailInfo.Field3 = detail.field3;
                    detailInfo.Field4 = detail.field4;
                    detailInfo.Field5 = detail.field5;
                    detailInfo.Field6 = detail.field6;
                    detailInfo.Field7 = detail.field7;
                    detailInfo.Field8 = detail.field8;
                    detailInfo.Field9 = detail.field9;
                    detailInfo.Field10 = detail.field10;

                    orderInfo.InoutDetailList.Add(detailInfo);
                }
                #endregion

                orderInfoList.Add(orderInfo);
            }

            // Save
            var posInoutAuthService = new ExchangeBsService.PosInoutAuthService();
            var ccAuthService = new ExchangeBsService.CCAuthService();
            switch (orderType)
            {
                case "POS":
                    posInoutAuthService.SetPosInoutInfo(customerId, unitId, userId, orderInfoList);
                    break;
                case "RT":
                    break;
                case "AJ":
                    ccAuthService.SetAJInfoList(customerId, unitId, userId, orderInfoList);
                    break;
                default:
                    throw new Exception(string.Format("'{0}'单据处理类型错误", orderType));
            }
        }
        #endregion

        #region CheckOrders
        /// <summary>
        /// 检查Order
        /// </summary>
        public Hashtable CheckOrder(OrderContract order)
        {
            Hashtable htError = new Hashtable();
            if (order == null)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据信息不能为空", true);
                return htError;
            }
            if (order.order_id == null || order.order_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据ID不能为空", true);
                return htError;
            }
            if (order.order_no == null || order.order_no.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据号码不能为空", true);
                return htError;
            }
            if (order.order_type_id == null || order.order_type_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据类型1不能为空", true);
                return htError;
            }
            //if (order.order_reason_id == null || order.order_reason_id.Trim().Length == 0)
            //{
            //    htError = ErrorService.OutputError(ErrorCode.A016, "单据类型2不能为空", true);
            //    return htError;
            //}
            if (order.red_flag == null || order.red_flag.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据红单标记不能为空", true);
                return htError;
            }
            if (!(order.red_flag == "1" || order.red_flag == "-1"))
            {
                htError = ErrorService.OutputError(ErrorCode.A019, "单据红单标记数据不合法", true);
                return htError;
            }
            if (order.order_date == null || order.order_date.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据日期不能为空", true);
                return htError;
            }
            if (order.create_unit_id == null || order.create_unit_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据创建单位不能为空", true);
                return htError;
            }
            // details
            if (order.details == null || order.details.Count == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "单据明细信息不能为空", true);
                return htError;
            }
            foreach (var orderDetail in order.details)
            {
                if (orderDetail.order_detail_id == null || orderDetail.order_detail_id.Trim().Length == 0)
                {
                    htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的ID不能为空", true);
                    return htError;
                }
                if (orderDetail.order_id == null || orderDetail.order_id.Trim().Length == 0)
                {
                    htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的主单据ID不能为空", true);
                    return htError;
                }
                if (orderDetail.sku_id == null || orderDetail.sku_id.Trim().Length == 0)
                {
                    htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的SKU不能为空", true);
                    return htError;
                }
                if (orderDetail.unit_id == null || orderDetail.unit_id.Trim().Length == 0)
                {
                    htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的单位ID不能为空", true);
                    return htError;
                }
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }

        /// <summary>
        /// 检查Order集合
        /// </summary>
        public Hashtable CheckOrders(IList<OrderContract> orders)
        {
            Hashtable htError = new Hashtable();
            if (orders == null || orders.Count == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据集合不能为空", true);
                return htError;
            }
            foreach (var order in orders)
            {
                htError = CheckOrder(order);
                if (!Convert.ToBoolean(htError["status"])) return htError;
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }
        #endregion

        #region SaveOrders
        /// <summary>
        /// 保存Order集合
        /// </summary>
        public void SaveOrders(IList<OrderContract> orders,
            string customerId, string unitId, string userId, string orderType)
        {
            //if (customerId == null || customerId.Trim().Length == 0)
            //    throw new Exception("客户ID不能为空");
            //if (userId == null || userId.Trim().Length == 0)
            //    throw new Exception("用户ID不能为空");
            //if (orderType == null || orderType.Trim().Length == 0)
            //    throw new Exception("单据处理类型不能为空");

            //var orderInfoList = new List<cPos.Model.InoutInfo>();
            //foreach (var order in orders)
            //{
            //    cPos.Model.OrderInfo orderInfo = new cPos.Model.OrderInfo();
            //    #region order
            //    orderInfo.order_id = order.order_id;
            //    orderInfo.order_type_id = order.order_type_id;
            //    orderInfo.red_flag = order.red_flag;
            //    orderInfo.order_no = order.order_no;
            //    orderInfo.ref_order_no = order.ref_order_no;
            //    orderInfo.ref_order_type_id = order.ref_order_type_id;
            //    orderInfo.ref_order_id = order.ref_order_id;
            //    orderInfo.warehouse_id = order.warehouse_id;
            //    orderInfo.order_date = order.order_date;
            //    orderInfo.request_date = order.request_date;
            //    orderInfo.promise_date = order.promise_date;
            //    orderInfo.complete_date = order.complete_date;
            //    orderInfo.create_unit_id = order.create_unit_id;
            //    orderInfo.unit_id = order.unit_id;
            //    orderInfo.related_unit_id = order.related_unit_id;
            //    orderInfo.pos_id = order.pos_id;
            //    orderInfo.total_amount = order.total_amount;
            //    orderInfo.discount_rate = order.discount_rate;
            //    orderInfo.actual_amount = order.actual_amount;
            //    orderInfo.receive_points = order.receive_points;
            //    orderInfo.pay_points = order.pay_points;
            //    orderInfo.address_1 = order.address_1;
            //    orderInfo.address_2 = order.address_2;
            //    orderInfo.zip = order.zip;
            //    orderInfo.phone = order.phone;
            //    orderInfo.fax = order.fax;
            //    orderInfo.email = order.email;
            //    orderInfo.remark = order.remark;
            //    orderInfo.carrier_id = order.carrier_id;
            //    orderInfo.order_status = order.order_status;
            //    orderInfo.print_times = order.print_times;
            //    orderInfo.create_time = order.create_time;
            //    orderInfo.create_user_id = order.create_user_id;
            //    orderInfo.modify_time = order.modify_time;
            //    orderInfo.modify_user_id = order.modify_user_id;
            //    orderInfo.send_user_id = order.send_user_id;
            //    orderInfo.send_time = order.send_time;
            //    orderInfo.accept_user_id = order.accept_user_id;
            //    orderInfo.accept_time = order.accept_time;
            //    orderInfo.approve_user_id = order.approve_user_id;
            //    orderInfo.approve_time = order.approve_time;
            //    #endregion

            //    #region details
            //    orderInfo.OrderDetailList = new List<cPos.Model.OrderDetailInfo>();
            //    foreach (var detail in order.details)
            //    {
            //        cPos.Model.OrderDetailInfo detailInfo = new cPos.Model.OrderDetailInfo();
            //        detailInfo.order_detail_id = detail.order_detail_id;
            //        detailInfo.order_id = detail.order_id;
            //        detailInfo.ref_order_detail_id = detail.ref_order_detail_id;
            //        detailInfo.sku_id = detail.sku_id;
            //        detailInfo.unit_id = detail.unit_id;
            //        detailInfo.enter_qty = detail.enter_qty;
            //        detailInfo.order_qty = detail.order_qty;
            //        detailInfo.enter_price = detail.enter_price;
            //        detailInfo.std_price = detail.std_price;
            //        detailInfo.discount_rate = detail.discount_rate;
            //        detailInfo.retail_price = detail.retail_price;
            //        detailInfo.retail_amount = detail.retail_amount;
            //        detailInfo.enter_amount = detail.enter_amount;
            //        detailInfo.receive_points = detail.receive_points;
            //        detailInfo.pay_points = detail.pay_points;
            //        detailInfo.remark = detail.remark;
            //        detailInfo.status = detail.status;
            //        detailInfo.display_index = detail.display_index;
            //        detailInfo.create_time = detail.create_time;
            //        detailInfo.create_user_id = detail.create_user_id;
            //        detailInfo.modify_time = detail.modify_time;
            //        detailInfo.modify_user_id = detail.modify_user_id;
            //        detailInfo.if_flag = detail.if_flag;

            //        orderInfo.DetailList.Add(detailInfo);
            //    }
            //    #endregion

            //    orderInfoList.Add(orderInfo);
            //}

            //// Save
            //var posInoutAuthService = new ExchangeBsService.PosInoutAuthService();
            //switch (orderType)
            //{
            //    case "ORDER":
            //        //posInoutAuthService.SetPosInoutInfo(customerId, unitId, userId, orderInfoList);
            //        break;
            //    case "MV":
            //        break;
            //    default:
            //        throw new Exception(string.Format("'{0}'单据处理类型错误", orderType));
            //}
        }
        #endregion

        #region CheckCcOrders
        /// <summary>
        /// 检查CcOrder
        /// </summary>
        public Hashtable CheckCcOrder(CcOrderContract order)
        {
            Hashtable htError = new Hashtable();
            if (order == null)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据信息不能为空", true);
                return htError;
            }
            if (order.order_id == null || order.order_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据ID不能为空", true);
                return htError;
            }
            if (order.order_no == null || order.order_no.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据号码不能为空", true);
                return htError;
            }
            if (order.order_type_id == null || order.order_type_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据类型1不能为空", true);
                return htError;
            }
            if (order.order_reason_id == null || order.order_reason_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据类型2不能为空", true);
                return htError;
            }
            if (order.order_date == null || order.order_date.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据日期不能为空", true);
                return htError;
            }
            if (order.warehouse_id == null || order.warehouse_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "仓库标识不能为空", true);
                return htError;
            }
            if (order.create_user_id == null || order.create_user_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "创建人不能为空", true);
                return htError;
            }
            if (order.create_time == null || order.create_time.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "创建时间不能为空", true);
                return htError;
            }
            // details
            if (order.details == null || order.details.Count == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "单据明细信息不能为空", true);
                return htError;
            }
            foreach (var orderDetail in order.details)
            {
                if (orderDetail.order_detail_id == null || orderDetail.order_detail_id.Trim().Length == 0)
                {
                    htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的ID不能为空", true);
                    return htError;
                }
                if (orderDetail.order_id == null || orderDetail.order_id.Trim().Length == 0)
                {
                    htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的主单据ID不能为空", true);
                    return htError;
                }
                if (orderDetail.sku_id == null || orderDetail.sku_id.Trim().Length == 0)
                {
                    htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的SKU不能为空", true);
                    return htError;
                }
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }

        /// <summary>
        /// 检查手机端盘点单
        /// </summary>
        public Hashtable CheckMobileCcOrder(CcOrderContract order)
        {
            Hashtable htError = new Hashtable();
            if (order == null)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据信息不能为空", true);
                return htError;
            }
            if (order.order_id == null || order.order_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据ID不能为空", true);
                return htError;
            }
            if (order.order_no == null || order.order_no.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据号码不能为空", true);
                return htError;
            }
            if (order.order_date == null || order.order_date.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据日期不能为空", true);
                return htError;
            }
            if (order.unit_id == null || order.unit_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "门店标识不能为空", true);
                return htError;
            }
            if (order.status == null || order.status.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "状态不能为空", true);
                return htError;
            }
            if (order.create_user_id == null || order.create_user_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "创建人不能为空", true);
                return htError;
            }
            if (order.create_time == null || order.create_time.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "创建时间不能为空", true);
                return htError;
            }
            // details
            //if (order.details == null || order.details.Count == 0)
            //{
            //    htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "单据明细信息不能为空", true);
            //    return htError;
            //}
            if (order.details != null && order.details.Count > 0)
            {
                foreach (var orderDetail in order.details)
                {
                    if (orderDetail.order_detail_id == null || orderDetail.order_detail_id.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的ID不能为空", true);
                        return htError;
                    }
                    if (orderDetail.order_id == null || orderDetail.order_id.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的主单据ID不能为空", true);
                        return htError;
                    }
                    if (orderDetail.sku_id == null || orderDetail.sku_id.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的SKU不能为空", true);
                        return htError;
                    }
                    if (orderDetail.end_qty == null || orderDetail.end_qty.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的库存数量不能为空", true);
                        return htError;
                    }
                    if (orderDetail.item_code == null || orderDetail.item_code.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的商品代码不能为空", true);
                        return htError;
                    }
                    if (orderDetail.item_name == null || orderDetail.item_name.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的商品名称不能为空", true);
                        return htError;
                    }
                    if (orderDetail.barcode == null || orderDetail.barcode.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的商品条码不能为空", true);
                        return htError;
                    }
                    if (orderDetail.sku_prop_1_name == null || orderDetail.sku_prop_1_name.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的规格1名称不能为空", true);
                        return htError;
                    }
                    if (orderDetail.enter_price == null || orderDetail.enter_price.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的商品进价不能为空", true);
                        return htError;
                    }
                    if (orderDetail.sales_price == null || orderDetail.sales_price.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的商品售价不能为空", true);
                        return htError;
                    }
                }
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }

        /// <summary>
        /// 检查CcOrder集合
        /// </summary>
        public Hashtable CheckCcOrders(IList<CcOrderContract> orders, string orderType)
        {
            Hashtable htError = new Hashtable();
            if (orders == null || orders.Count == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据集合不能为空", true);
                return htError;
            }
            foreach (var order in orders)
            {
                if (orderType == UploadCcOrderType.MOBILE.ToString()) // 手机端
                {
                    htError = CheckMobileCcOrder(order);
                }
                else
                {
                    htError = CheckCcOrder(order);
                }
                if (!Convert.ToBoolean(htError["status"])) return htError;
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }

        public Hashtable CheckCcOrders(IList<CcOrderContract> orders)
        {
            return CheckCcOrders(orders, UploadCcOrderType.CC.ToString());
        }
        #endregion

        #region SaveCcOrders
        /// <summary>
        /// 保存CcOrder集合
        /// </summary>
        public void SaveCcOrders(IList<CcOrderContract> orders,
            string customerId, string unitId, string userId, string orderType)
        {
            if (userId == null || userId.Trim().Length == 0)
                throw new Exception("用户ID不能为空");
            if (orderType == UploadCcOrderType.CC.ToString())
            {
                if (customerId == null || customerId.Trim().Length == 0)
                    throw new Exception("客户ID不能为空");
                if (orderType == null || orderType.Trim().Length == 0)
                    throw new Exception("单据处理类型不能为空");
            }

            var orderInfoList = new List<cPos.Model.CCInfo>();
            foreach (var order in orders)
            {
                cPos.Model.CCInfo orderInfo = new cPos.Model.CCInfo();
                #region order
                orderInfo.order_id = order.order_id;
                orderInfo.order_no = order.order_no;
                orderInfo.order_type_id = order.order_type_id;
                orderInfo.order_reason_id = order.order_reason_id;
                orderInfo.ref_order_id = order.ref_order_id;
                orderInfo.ref_order_no = order.ref_order_no;
                orderInfo.order_date = order.order_date;
                orderInfo.request_date = order.request_date;
                orderInfo.complete_date = order.complete_date;
                orderInfo.unit_id = order.unit_id;
                orderInfo.pos_id = order.pos_id;
                orderInfo.warehouse_id = order.warehouse_id;
                orderInfo.remark = order.remark;
                orderInfo.status = order.status;
                orderInfo.create_time = order.create_time;
                orderInfo.create_user_id = order.create_user_id;
                orderInfo.modify_time = order.modify_time;
                orderInfo.modify_user_id = order.modify_user_id;
                orderInfo.data_from_id = order.data_from_id;
                orderInfo.send_user_id = order.send_user_id;
                orderInfo.send_time = order.send_time;
                orderInfo.approve_user_id = order.approve_user_id;
                orderInfo.approve_time = order.approve_time;
                orderInfo.accpect_user_id = order.accept_user_id;
                orderInfo.accpect_time = order.accept_time;
                orderInfo.if_flag = order.if_flag;
                #endregion

                #region details
                orderInfo.CCDetailInfoList = new List<cPos.Model.CCDetailInfo>();
                foreach (var detail in order.details)
                {
                    cPos.Model.CCDetailInfo detailInfo = new cPos.Model.CCDetailInfo();
                    detailInfo.order_detail_id = detail.order_detail_id;
                    detailInfo.order_id = detail.order_id;
                    detailInfo.ref_order_detail_id = detail.ref_order_detail_id;
                    detailInfo.order_no = detail.order_no;
                    detailInfo.warehouse_id = detail.warehouse_id;
                    detailInfo.sku_id = detail.sku_id;
                    detailInfo.end_qty = Utils.GetDecimalVal(detail.end_qty);
                    detailInfo.order_qty = Utils.GetDecimalVal(detail.order_qty);
                    detailInfo.remark = detail.remark;
                    detailInfo.display_index = Utils.GetIntVal(detail.display_index);
                    detailInfo.create_time = detail.create_time;
                    detailInfo.create_user_id = detail.create_user_id;
                    detailInfo.modify_time = detail.modify_time;
                    detailInfo.modify_user_id = detail.modify_user_id;

                    detailInfo.item_code = detail.item_code;
                    detailInfo.item_name = detail.item_name;
                    detailInfo.barcode = detail.barcode;
                    detailInfo.sku_prop_1_name = detail.sku_prop_1_name;
                    detailInfo.sku_prop_2_name = detail.sku_prop_2_name;
                    detailInfo.sku_prop_3_name = detail.sku_prop_3_name;
                    detailInfo.sku_prop_4_name = detail.sku_prop_4_name;
                    detailInfo.sku_prop_5_name = detail.sku_prop_5_name;
                    detailInfo.enter_price = detail.enter_price;
                    detailInfo.sales_price = detail.sales_price;
                    detailInfo.brand_name = detail.brand_name;
                    detailInfo.item_category_id = detail.item_category_id;

                    orderInfo.CCDetailInfoList.Add(detailInfo);
                }
                #endregion

                orderInfoList.Add(orderInfo);
            }

            // Save
            var ccService = new ExchangeBsService.CCAuthService();
            switch (orderType)
            {
                case "CC":
                    ccService.SetCCInfoList(customerId, unitId, userId, orderInfoList);
                    break;
                case "MOBILE":
                    //ccService.SetCCInfoList(customerId, unitId, userId, orderInfoList);
                    break;
                default:
                    throw new Exception(string.Format("'{0}'单据处理类型错误", orderType));
            }
        }
        #endregion

        #region ToInoutModels
        public IList<cPos.Model.InoutInfo> ToInoutModels(IList<InoutOrderContract> models)
        {
            if (models == null) return null;
            var objs = new List<cPos.Model.InoutInfo>();
            foreach (var model in models)
            {
                objs.Add(ToInoutModel(model));
            }
            return objs;
        }

        public IList<InoutOrderContract> ToInoutContracts(IList<cPos.Model.InoutInfo> models)
        {
            if (models == null) return null;
            var objs = new List<InoutOrderContract>();
            foreach (var model in models)
            {
                objs.Add(ToInoutContract(model));
            }
            return objs;
        }

        #region ToInoutModel
        public cPos.Model.InoutInfo ToInoutModel(InoutOrderContract model)
        {
            var obj = new cPos.Model.InoutInfo();
            obj.order_id = model.order_id;
            obj.order_no = model.order_no;
            obj.order_type_id = model.order_type_id;
            obj.order_reason_id = model.order_reason_id;
            obj.red_flag = model.red_flag;
            obj.ref_order_id = model.ref_order_id;
            obj.ref_order_no = model.ref_order_no;
            obj.warehouse_id = model.warehouse_id;
            obj.order_date = model.order_date;
            obj.request_date = model.request_date;
            obj.complete_date = model.complete_date;
            obj.create_unit_id = model.create_unit_id;
            obj.unit_id = model.unit_id;
            obj.related_unit_id = model.related_unit_id;
            //obj.ref_unit_id = model.ref_unit_id;
            obj.pos_id = model.pos_id;
            obj.shift_id = model.shift_id;
            obj.sales_user = model.sales_user;
            obj.total_amount = Utils.GetDecimalVal(model.total_amount);
            obj.discount_rate = Utils.GetDecimalVal(model.discount_rate);
            obj.actual_amount = Utils.GetDecimalVal(model.actual_amount);
            obj.receive_points = Utils.GetDecimalVal(model.receive_points);
            obj.pay_points = Utils.GetDecimalVal(model.pay_points);
            obj.pay_id = model.pay_id;
            obj.print_times = Utils.GetIntVal(model.print_times);
            obj.carrier_id = model.carrier_id;
            obj.remark = model.remark;
            obj.status = model.status;
            obj.create_time = model.create_time;
            obj.create_user_id = model.create_user_id;
            obj.approve_time = model.approve_time;
            obj.approve_user_id = model.approve_user_id;
            obj.send_user_id = model.send_user_id;
            obj.send_time = model.send_time;
            obj.accpect_user_id = model.accpect_user_id;
            obj.accpect_time = model.accpect_time;
            obj.modify_user_id = model.modify_user_id;
            obj.modify_time = model.modify_time;
            obj.total_qty = Utils.GetDecimalVal(model.total_qty);
            obj.total_retail = Utils.GetDecimalVal(model.total_retail);
            obj.keep_the_change = Utils.GetDecimalVal(model.keep_the_change);
            obj.wiping_zero = Utils.GetDecimalVal(model.wiping_zero);
            obj.vip_no = model.vip_no;
            obj.data_from_id = model.data_from_id;
            obj.sales_unit_id = model.sales_unit_id;
            obj.purchase_unit_id = model.purchase_unit_id;
            obj.if_flag = model.if_flag;

            if (model.details != null)
            {
                obj.InoutDetailList = new List<cPos.Model.InoutDetailInfo>();
                foreach (var detail in model.details)
                {
                    var objDetail = new cPos.Model.InoutDetailInfo();
                    objDetail.order_detail_id = detail.order_detail_id;
                    objDetail.order_id = detail.order_id;
                    objDetail.ref_order_detail_id = detail.ref_order_detail_id;
                    objDetail.sku_id = detail.sku_id;
                    objDetail.unit_id = detail.unit_id;
                    objDetail.enter_qty = Utils.GetDecimalVal(detail.enter_qty);
                    objDetail.order_qty = Utils.GetDecimalVal(detail.order_qty);
                    objDetail.enter_price = Utils.GetDecimalVal(detail.enter_price);
                    objDetail.std_price = Utils.GetDecimalVal(detail.std_price);
                    objDetail.discount_rate = Utils.GetDecimalVal(detail.discount_rate);
                    objDetail.retail_price = Utils.GetDecimalVal(detail.retail_price);
                    objDetail.retail_amount = Utils.GetDecimalVal(detail.retail_amount);
                    objDetail.enter_amount = Utils.GetDecimalVal(detail.enter_amount);
                    objDetail.receive_points = Utils.GetDecimalVal(detail.receive_points);
                    objDetail.pay_points = Utils.GetDecimalVal(detail.pay_points);
                    objDetail.remark = detail.remark;
                    objDetail.order_detail_status = detail.order_detail_status;
                    objDetail.display_index = Utils.GetIntVal(detail.display_index);
                    objDetail.create_time = detail.create_time;
                    objDetail.create_user_id = detail.create_user_id;
                    objDetail.modify_time = detail.modify_time;
                    objDetail.modify_user_id = detail.modify_user_id;
                    objDetail.ref_order_id = detail.ref_order_id;
                    objDetail.if_flag = Utils.GetIntVal(detail.if_flag);
                    objDetail.pos_order_code = detail.pos_order_code;
                    objDetail.plan_price = Utils.GetDecimalVal(detail.plan_price);
                    obj.InoutDetailList.Add(objDetail);
                }
            }
            return obj;
        }
        #endregion

        #region ToInoutContract
        public InoutOrderContract ToInoutContract(cPos.Model.InoutInfo model)
        {
            var obj = new InoutOrderContract();
            obj.order_id = model.order_id;
            obj.order_no = model.order_no;
            obj.order_type_id = model.order_type_id;
            obj.order_reason_id = model.order_reason_id;
            obj.red_flag = model.red_flag;
            obj.ref_order_id = model.ref_order_id;
            obj.ref_order_no = model.ref_order_no;
            obj.warehouse_id = model.warehouse_id;
            obj.order_date = model.order_date;
            obj.request_date = model.request_date;
            obj.complete_date = model.complete_date;
            obj.create_unit_id = model.create_unit_id;
            obj.unit_id = model.unit_id;
            obj.related_unit_id = model.related_unit_id;
            //obj.ref_unit_id = model.ref_unit_id;
            obj.pos_id = model.pos_id;
            obj.shift_id = model.shift_id;
            obj.sales_user = model.sales_user;
            obj.total_amount = Utils.GetStrVal(model.total_amount);
            obj.discount_rate = Utils.GetStrVal(model.discount_rate);
            obj.actual_amount = Utils.GetStrVal(model.actual_amount);
            obj.receive_points = Utils.GetStrVal(model.receive_points);
            obj.pay_points = Utils.GetStrVal(model.pay_points);
            obj.pay_id = model.pay_id;
            obj.print_times = Utils.GetStrVal(model.print_times);
            obj.carrier_id = model.carrier_id;
            obj.remark = model.remark;
            obj.status = model.status;
            obj.create_time = model.create_time;
            obj.create_user_id = model.create_user_id;
            obj.approve_time = model.approve_time;
            obj.approve_user_id = model.approve_user_id;
            obj.send_user_id = model.send_user_id;
            obj.send_time = model.send_time;
            obj.accpect_user_id = model.accpect_user_id;
            //obj.accpect_date = model.accpect_date;
            obj.modify_user_id = model.modify_user_id;
            obj.modify_time = model.modify_time;
            obj.total_qty = Utils.GetStrVal(model.total_qty);
            obj.total_retail = Utils.GetStrVal(model.total_retail);
            obj.keep_the_change = Utils.GetStrVal(model.keep_the_change);
            obj.wiping_zero = Utils.GetStrVal(model.wiping_zero);
            obj.vip_no = model.vip_no;
            obj.data_from_id = model.data_from_id;
            obj.sales_unit_id = model.sales_unit_id;
            obj.purchase_unit_id = model.purchase_unit_id;
            obj.if_flag = model.if_flag;
            
            if (model.InoutDetailList != null)
            {
                obj.details = new List<InoutOrderDetailContract>();
                foreach (var detail in model.InoutDetailList)
                {
                    var objDetail = new InoutOrderDetailContract();
                    objDetail.order_detail_id = detail.order_detail_id;
                    objDetail.order_id = detail.order_id;
                    objDetail.ref_order_detail_id = detail.ref_order_detail_id;
                    objDetail.sku_id = detail.sku_id;
                    objDetail.unit_id = detail.unit_id;
                    objDetail.enter_qty = Utils.GetStrVal(detail.enter_qty);
                    objDetail.order_qty = Utils.GetStrVal(detail.order_qty);
                    objDetail.enter_price = Utils.GetStrVal(detail.enter_price);
                    objDetail.std_price = Utils.GetStrVal(detail.std_price);
                    objDetail.discount_rate = Utils.GetStrVal(detail.discount_rate);
                    objDetail.retail_price = Utils.GetStrVal(detail.retail_price);
                    objDetail.retail_amount = Utils.GetStrVal(detail.retail_amount);
                    objDetail.enter_amount = Utils.GetStrVal(detail.enter_amount);
                    objDetail.receive_points = Utils.GetStrVal(detail.receive_points);
                    objDetail.pay_points = Utils.GetStrVal(detail.pay_points);
                    objDetail.remark = detail.remark;
                    objDetail.order_detail_status = detail.order_detail_status;
                    objDetail.display_index = Utils.GetStrVal(detail.display_index);
                    objDetail.create_time = detail.create_time;
                    objDetail.create_user_id = detail.create_user_id;
                    objDetail.modify_time = detail.modify_time;
                    objDetail.modify_user_id = detail.modify_user_id;
                    objDetail.ref_order_id = detail.ref_order_id;
                    objDetail.if_flag = Utils.GetStrVal(detail.if_flag);
                    objDetail.pos_order_code = detail.pos_order_code;
                    objDetail.plan_price = Utils.GetStrVal(detail.plan_price);
                    obj.details.Add(objDetail);
                }
            }
            return obj;
        }
        #endregion

        #endregion

        #region ToCcModels
        public IList<cPos.Model.CCInfo> ToCcModels(IList<CcOrderContract> models)
        {
            if (models == null) return null;
            var objs = new List<cPos.Model.CCInfo>();
            foreach (var model in models)
            {
                objs.Add(ToCcModel(model));
            }
            return objs;
        }

        public IList<CcOrderContract> ToCcContracts(IList<cPos.Model.CCInfo> models)
        {
            if (models == null) return null;
            var objs = new List<CcOrderContract>();
            foreach (var model in models)
            {
                objs.Add(ToCcContract(model));
            }
            return objs;
        }

        #region ToCcModel
        public cPos.Model.CCInfo ToCcModel(CcOrderContract model)
        {
            var obj = new cPos.Model.CCInfo();
            obj.order_id = model.order_id;
            obj.order_no = model.order_no;
            obj.order_type_id = model.order_type_id;
            obj.order_reason_id = model.order_reason_id;
            obj.ref_order_id = model.ref_order_id;
            obj.ref_order_no = model.ref_order_no;
            obj.order_date = model.order_date;
            obj.request_date = model.request_date;
            obj.complete_date = model.complete_date;
            obj.unit_id = model.unit_id;
            obj.pos_id = model.pos_id;
            obj.warehouse_id = model.warehouse_id;
            obj.remark = model.remark;
            obj.status = model.status;
            obj.create_time = model.create_time;
            obj.create_user_id = model.create_user_id;
            obj.modify_time = model.modify_time;
            obj.modify_user_id = model.modify_user_id;
            obj.data_from_id = model.data_from_id;
            obj.send_user_id = model.send_user_id;
            obj.send_time = model.send_time;
            obj.approve_user_id = model.approve_user_id;
            obj.approve_time = model.approve_time;
            obj.accpect_user_id = model.accept_user_id;
            obj.accpect_time = model.accept_time;
            obj.if_flag = model.if_flag;

            if (model.details != null)
            {
                obj.CCDetailInfoList = new List<cPos.Model.CCDetailInfo>();
                foreach (var detail in model.details)
                {
                    var objDetail = new cPos.Model.CCDetailInfo();
                    objDetail.order_detail_id = detail.order_detail_id;
                    objDetail.order_id = detail.order_id;
                    objDetail.ref_order_detail_id = detail.ref_order_detail_id;
                    objDetail.order_no = detail.order_no;
                    objDetail.warehouse_id = detail.warehouse_id;
                    objDetail.sku_id = detail.sku_id;
                    objDetail.end_qty = Utils.GetDecimalVal(detail.end_qty);
                    objDetail.order_qty = Utils.GetDecimalVal(detail.order_qty);
                    objDetail.remark = detail.remark;
                    objDetail.display_index = Utils.GetIntVal(detail.display_index);
                    objDetail.create_time = detail.create_time;
                    objDetail.create_user_id = detail.create_user_id;
                    objDetail.modify_time = detail.modify_time;
                    objDetail.modify_user_id = detail.modify_user_id;
                    objDetail.difference_qty = Utils.GetDecimalVal(detail.difference_qty);
                    obj.CCDetailInfoList.Add(objDetail);
                }
            }
            return obj;
        }
        #endregion

        #region ToCcContract
        public CcOrderContract ToCcContract(cPos.Model.CCInfo model)
        {
            var obj = new CcOrderContract();
            obj.order_id = model.order_id;
            obj.order_no = model.order_no;
            obj.order_type_id = model.order_type_id;
            obj.order_reason_id = model.order_reason_id;
            obj.ref_order_id = model.ref_order_id;
            obj.ref_order_no = model.ref_order_no;
            obj.order_date = model.order_date;
            obj.request_date = model.request_date;
            obj.complete_date = model.complete_date;
            obj.unit_id = model.unit_id;
            obj.pos_id = model.pos_id;
            obj.warehouse_id = model.warehouse_id;
            obj.remark = model.remark;
            obj.status = model.status;
            obj.create_time = model.create_time;
            obj.create_user_id = model.create_user_id;
            obj.modify_time = model.modify_time;
            obj.modify_user_id = model.modify_user_id;
            obj.data_from_id = model.data_from_id;
            obj.send_user_id = model.send_user_id;
            obj.send_time = model.send_time;
            obj.approve_user_id = model.approve_user_id;
            obj.approve_time = model.approve_time;
            obj.accept_user_id = model.accpect_user_id;
            obj.accept_time = model.accpect_time;
            obj.if_flag = model.if_flag;
            
            if (model.CCDetailInfoList != null)
            {
                obj.details = new List<CcOrderDetailContract>();
                foreach (var detail in model.CCDetailInfoList)
                {
                    var objDetail = new CcOrderDetailContract();
                    objDetail.order_detail_id = detail.order_detail_id;
                    objDetail.order_id = detail.order_id;
                    objDetail.ref_order_detail_id = detail.ref_order_detail_id;
                    objDetail.order_no = detail.order_no;
                    objDetail.warehouse_id = detail.warehouse_id;
                    objDetail.sku_id = detail.sku_id;
                    objDetail.end_qty = Utils.GetStrVal(detail.end_qty);
                    objDetail.order_qty = Utils.GetStrVal(detail.order_qty);
                    objDetail.remark = detail.remark;
                    objDetail.display_index = Utils.GetStrVal(detail.display_index);
                    objDetail.create_time = detail.create_time;
                    objDetail.create_user_id = detail.create_user_id;
                    objDetail.modify_time = detail.modify_time;
                    objDetail.modify_user_id = detail.modify_user_id;
                    objDetail.difference_qty = Utils.GetStrVal(detail.difference_qty);
                    obj.details.Add(objDetail);
                }
            }
            return obj;
        }
        #endregion

        #endregion

        #region CheckInoutOrderIds
        /// <summary>
        /// 检查单据ID集合
        /// </summary>
        public Hashtable CheckInoutOrderIds(IList<InoutOrderContract> orders)
        {
            Hashtable htError = new Hashtable();
            if (orders == null || orders.Count == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据ID集合不能为空", true);
                return htError;
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }
        #endregion

        #region SetInoutOrdersDldFlag
        /// <summary>
        /// 更新Inout单据下载标识
        /// </summary>
        public void SetInoutOrdersDldFlag(IList<InoutOrderContract> orders,
            string customerId, string unitId, string userId)
        {
            if (customerId == null || customerId.Trim().Length == 0)
                throw new Exception("客户ID不能为空");
            if (userId == null || userId.Trim().Length == 0)
                throw new Exception("用户ID不能为空");

            var orderInfoList = new List<cPos.Model.InoutInfo>();
            foreach (var order in orders)
            {
                cPos.Model.InoutInfo orderInfo = new cPos.Model.InoutInfo();
                #region order
                orderInfo.order_id = order.order_id;
                #endregion

                #region details
                if (order.details != null)
                {
                    orderInfo.InoutDetailList = new List<cPos.Model.InoutDetailInfo>();
                    foreach (var detail in order.details)
                    {
                        cPos.Model.InoutDetailInfo detailInfo = new cPos.Model.InoutDetailInfo();
                        detailInfo.order_detail_id = detail.order_detail_id;
                        detailInfo.order_id = detail.order_id;

                        orderInfo.InoutDetailList.Add(detailInfo);
                    }
                }
                #endregion

                orderInfoList.Add(orderInfo);
            }

            // Save
            var inoutBsService = new ExchangeBsService.InoutBsService();
            inoutBsService.SetInoutIfFlag(customerId, unitId, userId, orderInfoList, "1");
        }
        #endregion

        #region CheckCcOrderIds
        /// <summary>
        /// 检查单据ID集合
        /// </summary>
        public Hashtable CheckCcOrderIds(IList<CcOrderContract> orders)
        {
            Hashtable htError = new Hashtable();
            if (orders == null || orders.Count == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据ID集合不能为空", true);
                return htError;
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }
        #endregion

        #region SetCcOrdersDldFlag
        /// <summary>
        /// 更新Cc单据下载标识
        /// </summary>
        public void SetCcOrdersDldFlag(IList<CcOrderContract> orders,
            string customerId, string unitId, string userId)
        {
            if (customerId == null || customerId.Trim().Length == 0)
                throw new Exception("客户ID不能为空");
            if (userId == null || userId.Trim().Length == 0)
                throw new Exception("用户ID不能为空");

            var orderInfoList = new List<cPos.Model.CCInfo>();
            foreach (var order in orders)
            {
                cPos.Model.CCInfo orderInfo = new cPos.Model.CCInfo();
                #region order
                orderInfo.order_id = order.order_id;
                #endregion

                #region details
                if (order.details != null)
                {
                    orderInfo.CCDetailInfoList = new List<cPos.Model.CCDetailInfo>();
                    foreach (var detail in order.details)
                    {
                        cPos.Model.CCDetailInfo detailInfo = new cPos.Model.CCDetailInfo();
                        detailInfo.order_detail_id = detail.order_detail_id;
                        detailInfo.order_id = detail.order_id;

                        orderInfo.CCDetailInfoList.Add(detailInfo);
                    }
                }
                #endregion

                orderInfoList.Add(orderInfo);
            }

            // Save
            var ccBsService = new ExchangeBsService.CCAuthService();
            ccBsService.SetInoutIfFlag(customerId, unitId, userId, orderInfoList, "1");
        }
        #endregion

        #region CheckAdOrderLogs
        /// <summary>
        /// 检查AdOrderLog
        /// </summary>
        public Hashtable CheckAdOrderLog(AdOrderLogContract order)
        {
            Hashtable htError = new Hashtable();
            if (order == null)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "日志信息不能为空", true);
                return htError;
            }
            if (order.order_id == null || order.order_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "单据ID不能为空", true);
                return htError;
            }
            if (order.log_id == null || order.log_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "日志ID不能为空", true);
                return htError;
            }
            if (order.unit_id == null || order.unit_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "门店ID不能为空", true);
                return htError;
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }

        /// <summary>
        /// 检查AdOrderLog集合
        /// </summary>
        public Hashtable CheckAdOrderLogs(IList<AdOrderLogContract> orders)
        {
            Hashtable htError = new Hashtable();
            if (orders == null || orders.Count == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "集合不能为空", true);
                return htError;
            }
            foreach (var order in orders)
            {
                htError = CheckAdOrderLog(order);
                if (!Convert.ToBoolean(htError["status"])) return htError;
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }
        #endregion

        #region SaveAdOrderLogs
        /// <summary>
        /// 保存AdOrderLog集合
        /// </summary>
        public void SaveAdOrderLogs(IList<AdOrderLogContract> orders,
            string customerId, string unitId, string userId)
        {
            if (customerId == null || customerId.Trim().Length == 0)
                throw new Exception("客户ID不能为空");
            if (userId == null || userId.Trim().Length == 0)
                throw new Exception("用户ID不能为空");

            var orderInfoList = new List<cPos.Model.AdjustmentOrderInfo>();
            foreach (var order in orders)
            {
                cPos.Model.AdjustmentOrderInfo orderInfo = new cPos.Model.AdjustmentOrderInfo();
                #region order
                //orderInfo.log_id = order.log_id;
                //orderInfo.order_id = order.order_id;
                //orderInfo.unit_id = order.unit_id;
                //orderInfo.advertise_date = order.advertise_date;
                //orderInfo.advertise_time = order.advertise_time;
                //orderInfo.advertis_no = order.advertis_no;
                //orderInfo.upload_time = order.upload_time;
                #endregion

                orderInfoList.Add(orderInfo);
            }

            // Save
            var adService = new ExchangeBsService.AdvertiseOrderBsService();
            //adService.SaveAdOrderLogs(customerId, unitId, userId, orderInfoList);
        }
        #endregion
    }
}
