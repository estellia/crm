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

namespace cPos.Dex.ServicesAP
{
    public class OrderService
    {
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
                    if (orderDetail.item_name == null || orderDetail.item_name.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的商品名称不能为空", true);
                        return htError;
                    }
                    if (orderDetail.item_code == null || orderDetail.item_code.Trim().Length == 0)
                    {
                        orderDetail.item_code = orderDetail.item_name;
                        htError = ErrorService.OutputError(ErrorCode.A016, order.order_no + "明细项的商品代码不能为空", true);
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

            var orderInfoList = new List<cPos.Admin.Model.CCInfo>();
            foreach (var order in orders)
            {
                cPos.Admin.Model.CCInfo orderInfo = new cPos.Admin.Model.CCInfo();
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
                orderInfo.CCDetailInfoList = new List<cPos.Admin.Model.CCDetailInfo>();
                foreach (var detail in order.details)
                {
                    cPos.Admin.Model.CCDetailInfo detailInfo = new cPos.Admin.Model.CCDetailInfo();
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
            var apWsCCOrderService = new cPos.Dex.WsService.AP.APOrderService();
            string error = string.Empty;
            switch (orderType)
            {
                case "MOBILE":
                    cPos.Admin.Model.CCInfo ccInfoObj = new cPos.Admin.Model.CCInfo();
                    ccInfoObj.CCInfoList = orderInfoList;

                    string content = string.Empty;
                    Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
                    Jayrock.Json.Conversion.JsonConvert.Export(ccInfoObj, writer);
                    content = writer.ToString();
                    error = apWsCCOrderService.SetCCOrderList(userId, orderType, content);
                    break;
                default:
                    throw new Exception(string.Format("'{0}'单据处理类型错误", orderType));
            }
        }
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
    }
}
