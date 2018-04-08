using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Admin.Model;
using System.Collections;
using cPos.Admin.Component.SqlMappers;
using cPos.Admin.Component;
using cPos.Model.Advertise;

namespace cPos.Admin.Service
{
    /// <summary>
    /// CCOrderService
    /// </summary>
    public class CCOrderService : BaseService
    {
        #region CCOrder保存
        /// <summary>
        /// CCOrder保存
        /// </summary>
        /// <param name="models">models</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <returns>Hashtable: 
        ///  status(成功：true, 失败：false)
        ///  error(错误描述)
        /// </returns>
        public Hashtable SaveCCOrderList(bool IsTrans, IList<CCInfo> models, string type, string userId)
        {
            if (type == "MOBILE")
            {
                return SaveMobileCCOrderList(IsTrans, models, userId);
            }
            Hashtable ht = new Hashtable();
            ht["status"] = false;
            try
            {
                if (IsTrans) MSSqlMapper.Instance().BeginTransaction();
                foreach (var model in models)
                {
                    if (!CheckExistCCOrder(model))
                    {
                        MSSqlMapper.Instance().Insert("CCOrder.InsertCCOrder", model);
                    }
                }

                if (IsTrans) MSSqlMapper.Instance().CommitTransaction();
                ht["status"] = true;
            }
            catch (Exception ex)
            {
                if (IsTrans) MSSqlMapper.Instance().RollBackTransaction();
                throw (ex);
            }
            return ht;
        }

        /// <summary>
        /// 检查CCOrder是否已存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CheckExistCCOrder(CCInfo model)
        {
            int count = MSSqlMapper.Instance().QueryForObject<int>("CCOrder.CheckExistCCOrder", model);
            return count > 0 ? true : false;
        }
        #endregion

        #region Mobile CCOrder保存
        /// <summary>
        /// Mobile CCOrder保存
        /// </summary>
        /// <param name="orders">models</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <returns>Hashtable: 
        ///  status(成功：true, 失败：false)
        ///  error(错误描述)
        /// </returns>
        public Hashtable SaveMobileCCOrderList(bool IsTrans, IList<CCInfo> orders, string userId)
        {
            Hashtable ht = new Hashtable();
            ht["status"] = false;
            try
            {
                string error = string.Empty;
                SkuService skuService = new SkuService();
                ItemService itemService = new ItemService();
                InoutService inoutService = new InoutService();
                StockBalanceOrderService stockService = new StockBalanceOrderService();
                UnitService unitService = new UnitService();
                cPos.Model.LoggingSessionInfo loggingSessionInfo = new cPos.Model.LoggingSessionInfo();
                loggingSessionInfo.CurrentUser = new cPos.Model.User.UserInfo();
                loggingSessionInfo.CurrentUser.User_Id = userId;

                if (IsTrans) MSSqlMapper.Instance().BeginTransaction();

                // 处理SKU
                bool exsitBarcode = false;
                cPos.Model.SkuInfo skuInfo = null;
                foreach (var order in orders)
                {
                    order.order_type_id = "5F11A199E3CD42DE9CAE70442FC3D991"; // AJ
                    var unitInfo = unitService.GetUnitById(loggingSessionInfo, order.unit_id);
                    order.customer_id = unitInfo.customer_id;

                    if (order.CCDetailInfoList == null) continue;
                    foreach (var orderDetail in order.CCDetailInfoList)
                    {
                        exsitBarcode = skuService.CheckBarcode(orderDetail.barcode, null);
                        if (!exsitBarcode)
                        {
                            cPos.Model.ItemInfo itemInfo = new cPos.Model.ItemInfo();
                            itemInfo.SkuList = new List<cPos.Model.SkuInfo>();
                            cPos.Model.SkuInfo newSkuInfo = new cPos.Model.SkuInfo();
                            newSkuInfo.sku_id = orderDetail.sku_id;
                            newSkuInfo.barcode = orderDetail.barcode;
                            newSkuInfo.status = "1";
                            newSkuInfo.create_user_id = orderDetail.create_user_id;
                            newSkuInfo.create_time = orderDetail.create_time;
                            newSkuInfo.modify_user_id = orderDetail.modify_user_id;
                            newSkuInfo.modify_time = orderDetail.modify_time;
                            newSkuInfo.prop_1_detail_id = orderDetail.sku_prop_1_name;
                            newSkuInfo.prop_2_detail_id = orderDetail.sku_prop_2_name;
                            newSkuInfo.prop_3_detail_id = orderDetail.sku_prop_3_name;
                            newSkuInfo.prop_4_detail_id = orderDetail.sku_prop_4_name;
                            newSkuInfo.prop_5_detail_id = orderDetail.sku_prop_5_name;

                            // 商品
                            var itemListObj = itemService.GetItemByCode(orderDetail.item_code);
                            cPos.Model.ItemInfo itemObj = new cPos.Model.ItemInfo();
                            if (itemListObj != null && itemListObj.Count > 0)
                            {
                                itemObj = itemListObj[0];
                                newSkuInfo.item_id = itemObj.Item_Id;

                                if (itemObj.SkuList == null)
                                {
                                    itemObj.SkuList = new List<cPos.Model.SkuInfo>();
                                }
                                itemObj.SkuList.Add(newSkuInfo);
                                skuService.SetSkuInfo(itemObj, out error);
                            }
                            else
                            {
                                itemObj = new cPos.Model.ItemInfo();
                                itemObj.Item_Category_Id = "10e1e76ced9b45e1ac722d3e8b193419";
                                itemObj.Item_Id = NewGUID();
                                itemObj.Item_Code = orderDetail.item_code;
                                itemObj.Item_Name = orderDetail.item_name;
                                itemObj.ifgifts = 0;
                                itemObj.ifoften = 0;
                                itemObj.ifservice = 0;
                                itemObj.isGB = 1;
                                itemObj.data_from = "1";

                                // 价格
                                var skuPriceService = new SkuPriceService();
                                var skuUnitPriceInfo = new cPos.Model.SkuUnitPriceInfo();
                                skuUnitPriceInfo.SkuUnitPriceInfoList = new List<cPos.Model.SkuUnitPriceInfo>();
                                if (orderDetail.enter_price != null && 
                                    orderDetail.enter_price.Trim().Length > 0)
                                {
                                    cPos.Model.SkuUnitPriceInfo unitPriceInfo = new cPos.Model.SkuUnitPriceInfo();
                                    unitPriceInfo.item_price_type_id = "12EDF2F0C5BE4FB2B4FE3ECE870FF723";
                                    unitPriceInfo.price = Decimal.Parse(orderDetail.enter_price.Trim());
                                    unitPriceInfo.sku_unit_price_id = NewGuid();
                                    unitPriceInfo.sku_id = orderDetail.sku_id;
                                    unitPriceInfo.unit_id = order.unit_id;
                                    unitPriceInfo.status = "1";
                                    unitPriceInfo.start_date = "1900-01-01";
                                    unitPriceInfo.end_date = "9999-12-31";
                                    unitPriceInfo.create_user_id = userId;
                                    unitPriceInfo.create_time = GetNow();
                                    skuUnitPriceInfo.SkuUnitPriceInfoList.Add(unitPriceInfo);
                                }
                                if (orderDetail.sales_price != null &&
                                    orderDetail.sales_price.Trim().Length > 0)
                                {
                                    cPos.Model.SkuUnitPriceInfo unitPriceInfo = new cPos.Model.SkuUnitPriceInfo();
                                    unitPriceInfo.item_price_type_id = "75412168A16C4D2296B92CA0E596A188";
                                    unitPriceInfo.price = Decimal.Parse(orderDetail.sales_price.Trim());
                                    unitPriceInfo.sku_unit_price_id = NewGuid();
                                    unitPriceInfo.sku_id = orderDetail.sku_id;
                                    unitPriceInfo.unit_id = order.unit_id;
                                    unitPriceInfo.status = "1";
                                    unitPriceInfo.start_date = "1900-01-01";
                                    unitPriceInfo.end_date = "9999-12-31";
                                    unitPriceInfo.create_user_id = userId;
                                    unitPriceInfo.create_time = GetNow();
                                    skuUnitPriceInfo.SkuUnitPriceInfoList.Add(unitPriceInfo);
                                }
                                skuPriceService.InsertOrUpdateSkuUnitPrice(false, skuUnitPriceInfo);

                                itemObj.SkuList = new List<cPos.Model.SkuInfo>();
                                newSkuInfo.item_id = itemObj.Item_Id;
                                itemObj.SkuList.Add(newSkuInfo);

                                itemService.SetItemInfo(false, loggingSessionInfo, itemObj, out error);
                                orderDetail.sku_id = newSkuInfo.sku_id;
                            }
                        }
                        else
                        {
                            skuInfo = skuService.GetSkuByBarcode(orderDetail.barcode);
                            orderDetail.sku_id = skuInfo.sku_id;
                        }
                    }
                }

                // 处理Order
                foreach (var order in orders)
                {
                    if (!CheckIsEndMobileCCOrder(order))
                    {
                        MSSqlMapper.Instance().Insert("CC.InsertAndUpdateMobileCCOrder", order);
                    }

                    // 获取仓库
                    var warehouseObj = (new UnitService()).GetDefaultWarehouse(order.unit_id);
                    string warehouseId = warehouseObj == null ? "" : warehouseObj.warehouse_id;
                    Hashtable htStockId = new Hashtable();
                    string tmpStockId;

                    // 保存库存
                    foreach (var orderDetail in order.CCDetailInfoList)
                    {
                        cPos.Model.StockBalanceInfo stockInfo = new cPos.Model.StockBalanceInfo();

                        htStockId["unit_id"] = order.unit_id;
                        htStockId["warehouse_id"] = warehouseId;
                        htStockId["sku_id"] = orderDetail.sku_id;
                        tmpStockId = stockService.GetStockBalanceIdBySkuId(htStockId);
                        if (tmpStockId == null || tmpStockId.Trim().Length == 0)
                        {
                            tmpStockId = NewGUID();
                        }

                        stockInfo.stock_balance_id = tmpStockId;
                        stockInfo.unit_id = order.unit_id;
                        stockInfo.warehouse_id = warehouseId;
                        stockInfo.sku_id = orderDetail.sku_id;
                        stockInfo.begin_qty = orderDetail.end_qty;
                        stockInfo.end_qty = orderDetail.end_qty;
                        stockInfo.price = orderDetail.enter_price == null ? 0 : Convert.ToDecimal(orderDetail.enter_price);
                        stockInfo.item_label_type_id = "522B623A30C243F2853AA34CD01B510B";
                        stockInfo.status = "1";
                        stockInfo.create_time = GetNow();
                        stockInfo.create_user_id = order.create_user_id;

                        stockService.SaveStockBalance(stockInfo);
                    }
                }

                if (IsTrans) MSSqlMapper.Instance().CommitTransaction();
                ht["status"] = true;
            }
            catch (Exception ex)
            {
                if (IsTrans) MSSqlMapper.Instance().RollBackTransaction();
                throw (ex);
            }
            return ht;
        }

        /// <summary>
        /// 检查Mobile CCOrder是否已存在
        /// </summary>
        public bool CheckExistMobileCCOrder(CCInfo model)
        {
            int count = MSSqlMapper.Instance().QueryForObject<int>("CC.CheckExistMobileCCOrder", model);
            return count > 0 ? true : false;
        }

        /// <summary>
        /// 检查Mobile CCOrderDetail是否已存在
        /// </summary>
        public bool CheckExistMobileCCOrderDetail(CCDetailInfo model)
        {
            int count = MSSqlMapper.Instance().QueryForObject<int>("CC.CheckExistMobileCCOrderDetail", model);
            return count > 0 ? true : false;
        }

        /// <summary>
        /// 检查Mobile CCOrder是否已完成
        /// </summary>
        public bool CheckIsEndMobileCCOrder(CCInfo model)
        {
            int count = MSSqlMapper.Instance().QueryForObject<int>("CC.CheckIsEndMobileCCOrder", model);
            return count > 0 ? true : false;
        }
        #endregion
    }
}
