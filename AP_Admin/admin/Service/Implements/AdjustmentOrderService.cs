using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Admin.Component.SqlMappers;

namespace cPos.Admin.Service
{
    /// <summary>
    /// 商品调价单
    /// </summary>
    public class AdjustmentOrderService:BaseService
    {
        #region 查询
        /// <summary>
        /// 查询商品调价单
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_no">订单号</param>
        /// <param name="order_date">订单日期(yyyy-MM-dd)</param>
        /// <param name="begin_date">起始日期(yyyy-MM-dd)</param>
        /// <param name="end_date">终止日期(yyyy-MM-dd)</param>
        /// <param name="item_price_type_id">价格类型</param>
        /// <param name="status">状态</param>   
        /// <param name="maxRowCount">每页数量</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns></returns>
        public AdjustmentOrderInfo SearchItemAdjustmentOrderList(LoggingSessionInfo loggingSessionInfo
                                                                            , string order_no
                                                                            , string order_date
                                                                            , string begin_date
                                                                            , string end_date
                                                                            , string item_price_type_id
                                                                            , string status
                                                                            , int maxRowCount
                                                                            , int startRowIndex)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderNo", order_no);
                _ht.Add("OrderDate", order_date);
                _ht.Add("BeginDate", begin_date);
                _ht.Add("EndDate", end_date);
                _ht.Add("ItemPriceTypeId", item_price_type_id);
                _ht.Add("Status", status);
                _ht.Add("StartRow", startRowIndex);
                _ht.Add("EndRow", startRowIndex + maxRowCount);
                _ht.Add("customer_id", loggingSessionInfo.CurrentLoggingManager.Customer_Id);

                AdjustmentOrderInfo adjustmentOrderInfo = new AdjustmentOrderInfo();
                int iCount = MSSqlMapper.Instance().QueryForObject<int>("AdjustmentOrder.SearchCount", _ht);

                IList<AdjustmentOrderInfo> adjustmentOrderInfoList = new List<AdjustmentOrderInfo>();
                adjustmentOrderInfoList = MSSqlMapper.Instance().QueryForList<AdjustmentOrderInfo>("AdjustmentOrder.Search", _ht);

                adjustmentOrderInfo.ICount = iCount;
                adjustmentOrderInfo.AdjustmentOrderInfoList = adjustmentOrderInfoList;
                return adjustmentOrderInfo;
                //return MSSqlMapper.Instance().QueryForList<AdjustmentOrderInfo>("AdjustmentOrder.Search", _ht);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 修改状态
        /// <summary>
        /// 修改调价单状态
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_id">标识</param>
        /// <param name="billActionType">类型</param>
        /// <returns></returns>
        public bool SetAdjustmentOrderStatus(LoggingSessionInfo loggingSessionInfo, string order_id, BillActionType billActionType)
        {
            string strResult = string.Empty;
            try
            {
                cPos.Admin.Service.BillService bs = new BillService();

                BillOperateStateService state = bs.ApproveBill(loggingSessionInfo, order_id, "", billActionType);
                if (state == BillOperateStateService.ApproveSuccessful)
                {
                    //获取要改变的表单信息
                    BillModel billInfo = new BillService().GetBillById(loggingSessionInfo, order_id);
                    //设置要改变的用户信息
                    AdjustmentOrderInfo itenAdjustmentOrderInfo = new AdjustmentOrderInfo();
                    itenAdjustmentOrderInfo.status = billInfo.Status;
                    itenAdjustmentOrderInfo.status_desc = billInfo.BillStatusDescription;
                    itenAdjustmentOrderInfo.order_id = order_id;
                    itenAdjustmentOrderInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    itenAdjustmentOrderInfo.modify_time = GetCurrentDateTime(); //获取当前时间
                    //提交
                    MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("AdjustmentOrder.UpdateStatus", itenAdjustmentOrderInfo);
                    return true;
                }
                else {
                    return false;
                }

                
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        
        #endregion

        #region 获取单个调价单的明细信息
        /// <summary>
        /// 根据订单标识获取订单详细信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="orderId">订单标识</param>
        /// <returns></returns>
        public AdjustmentOrderInfo GetAdjustmentOrderByOrderId(LoggingSessionInfo loggingSessionInfo, string orderId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", orderId);
                AdjustmentOrderInfo adjustmentOrderInfo = new AdjustmentOrderInfo();
                adjustmentOrderInfo = (AdjustmentOrderInfo)MSSqlMapper.Instance().QueryForObject("AdjustmentOrder.SelectById", _ht);
                if (adjustmentOrderInfo != null)
                {
                    adjustmentOrderInfo.AdjustmentOrderDetailItemList = new AdjustmentOrderDetailItemService().GetAdjustmentOrderDetailItemListByOrderId(loggingSessionInfo, orderId);
                    adjustmentOrderInfo.AdjustmentOrderDetailSkuList = new AdjustmentOrderDetailSkuService().GetAdjustmentOrderDetailSkuListByOrderId(loggingSessionInfo, orderId);
                    adjustmentOrderInfo.AdjustmentOrderDetailUnitList = new AdjustmentOrderDetailUnitService().GetAdjustmentOrderDetailUnitListByOrderId(loggingSessionInfo, orderId);
                }
                return adjustmentOrderInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存调价单
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="adjustmentOrderInfo"></param>
        /// <returns></returns>
        public string SetAdjustmentOrderInfo(LoggingSessionInfo loggingSessionInfo,AdjustmentOrderInfo adjustmentOrderInfo)
        {
            string strResult = string.Empty;

            //事物信息
            MSSqlMapper.Instance().BeginTransaction();
            try
            {
                adjustmentOrderInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                //处理是新建还是修改
                string strDo = string.Empty;
                if (adjustmentOrderInfo.order_id == null || adjustmentOrderInfo.order_id.Equals(""))
                {
                    adjustmentOrderInfo.order_id = NewGuid();
                    strDo = "Create";

                }
                else {
                    strDo = "Modify";

                }
                //1 判断调价单号码是否唯一
                if (!IsExistOrderCode(loggingSessionInfo, adjustmentOrderInfo.order_no, adjustmentOrderInfo.order_id))
                {
                    strResult = "订单号码已经存在。";
                    return strResult;
                }
                //2.提交调价单信息至表单
                if (strDo.Equals("Create"))
                {
                    if (!SetAdjustmentOrderInsertBill(loggingSessionInfo, adjustmentOrderInfo))
                    {
                        strResult = "调价单表单提交失败。";
                        return strResult;
                    }
                }

                //3.获取调价单表单信息,设置调价单状态与状态描述
                BillModel billInfo = new BillService().GetBillById(loggingSessionInfo, adjustmentOrderInfo.order_id);
                adjustmentOrderInfo.status = billInfo.Status;
                adjustmentOrderInfo.status_desc = billInfo.BillStatusDescription;

                //4.提交调价单信息
                if (!SetAdjustmentOrderTableInfo(loggingSessionInfo, adjustmentOrderInfo))
                {
                    strResult = "提交用户表失败";
                    return strResult;
                }
                //5.提交调价单商品明细关系
                if (adjustmentOrderInfo.AdjustmentOrderDetailItemList != null)
                {
                    
                    if (!new AdjustmentOrderDetailItemService().SetAdjustmentOrderDetailItemInfo(loggingSessionInfo, adjustmentOrderInfo))
                    {
                        strResult = "提交调价单商品明细失败";
                        return strResult;
                    }
                }
                //6.提交调价单商品明细关系
                if (adjustmentOrderInfo.AdjustmentOrderDetailSkuList != null)
                {

                    if (!new AdjustmentOrderDetailSkuService().SetAdjustmentOrderDetailSkuInfo(loggingSessionInfo, adjustmentOrderInfo))
                    {
                        strResult = "提交调价单Sku明细失败";
                        return strResult;
                    }
                }
                //7.提交调价单商品明细关系
                if (adjustmentOrderInfo.AdjustmentOrderDetailUnitList != null)
                {

                    if (!new AdjustmentOrderDetailUnitService().SetAdjustmentOrderDetailUnitInfo(loggingSessionInfo, adjustmentOrderInfo))
                    {
                        strResult = "提交调价单组织明细失败";
                        return strResult;
                    }
                }

                MSSqlMapper.Instance().CommitTransaction();
                strResult = "保存成功!";
                return strResult; 

            }catch(Exception ex){
                MSSqlMapper.Instance().RollBackTransaction();
                strResult = ex.ToString();
                throw (ex); 
            }
        }

        /// <summary>
        /// 判断调价单号码是否重复
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="order_no"></param>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public bool IsExistOrderCode(LoggingSessionInfo loggingSessionInfo, string order_no, string order_id)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderNo", order_no);
                _ht.Add("OrderId", order_id);
                int n = MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("AdjustmentOrder.IsExsitOrderCode", _ht);
                return n > 0 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            }
        }

        /// <summary>
        /// 调价单提交到表单
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="adjustmentOrderInfo"></param>
        /// <returns></returns>
        private bool SetAdjustmentOrderInsertBill(LoggingSessionInfo loggingSessionInfo, AdjustmentOrderInfo adjustmentOrderInfo)
        {
            try
            {
                cPos.Model.BillModel bill = new BillModel();
                cPos.Admin.Service.BillService bs = new BillService();

                bill.Id = adjustmentOrderInfo.order_id;//order_id
                string order_type_id = bs.GetBillKindByCode(loggingSessionInfo, "ADJUSTMENTORDER").Id.ToString(); //loggingSession, OrderType
                bill.Code = bs.GetBillNextCode(loggingSessionInfo,"CreateAdjustmentPrice"); //BillKindCode
                bill.KindId = order_type_id;
                bill.UnitId = loggingSessionInfo.CurrentUserRole.UnitId;
                bill.AddUserId = loggingSessionInfo.CurrentUser.User_Id;

                BillOperateStateService state = bs.InsertBill(loggingSessionInfo, bill);

                if (state == BillOperateStateService.CreateSuccessful)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            }
        }

        /// <summary>
        /// 提交调价单主表信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="adjustmentOrderInfo"></param>
        /// <returns></returns>
        private bool SetAdjustmentOrderTableInfo(LoggingSessionInfo loggingSessionInfo, AdjustmentOrderInfo adjustmentOrderInfo)
        {
            try
            {
                if (adjustmentOrderInfo != null)
                {
                    if (adjustmentOrderInfo.create_user_id == null || adjustmentOrderInfo.create_user_id.Equals(""))
                    {
                        adjustmentOrderInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
                        adjustmentOrderInfo.create_time = GetCurrentDateTime();
                    }
                    if (adjustmentOrderInfo.modify_user_id == null || adjustmentOrderInfo.modify_user_id.Equals(""))
                    {
                        adjustmentOrderInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                        adjustmentOrderInfo.modify_time = GetCurrentDateTime();
                    }
                    MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("AdjustmentOrder.InsertOrUpdate", adjustmentOrderInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            }
        }
        #endregion

        #region 下载
        /// <summary>
        /// 获取未下载的调价单数量
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unit_id">组织标识</param>
        /// <param name="no">序号</param>
        /// <param name="item_price_type_id">价格类型</param>
        /// <returns></returns>
        public int GetAdjustmentOrderNotPackagedCount(LoggingSessionInfo loggingSessionInfo, string unit_id, int no, string item_price_type_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("UnitId", unit_id);
            _ht.Add("No", no);
            _ht.Add("ItemPriceTypeId", item_price_type_id);
            return MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("AdjustmentOrder.SelectUnDownloadCount", _ht);
        }

        /// <summary>
        /// 下载调价单
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unit_id">组织标识</param>
        /// <param name="no">序号</param>
        /// <param name="item_price_type_id">价格类型</param>
        /// <param name="maxRowCount">当前页数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public IList<AdjustmentOrderInfo> GetAdjustmentOrderListPackaged(LoggingSessionInfo loggingSessionInfo
                                                                        , string unit_id
                                                                        , int no
                                                                        , string item_price_type_id
                                                                        , int maxRowCount
                                                                        , int startRowIndex
                                                                        )
        {
            IList<AdjustmentOrderInfo> adjustmentOrderList = new List<AdjustmentOrderInfo>();
            Hashtable _ht = new Hashtable();
            _ht.Add("UnitId", unit_id);
            _ht.Add("No", no);
            _ht.Add("ItemPriceTypeId", item_price_type_id);
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            adjustmentOrderList = MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<AdjustmentOrderInfo>("AdjustmentOrder.SelectUnDownload", _ht);
            if (adjustmentOrderList != null)
            {
                foreach (AdjustmentOrderInfo adjustmentOrderInfo in adjustmentOrderList)
                {
                    adjustmentOrderInfo.AdjustmentOrderDetailSkuList = new AdjustmentOrderDetailSkuService().GetAdjustmentOrderDetailSkuListByOrderIdUnDownload(loggingSessionInfo, adjustmentOrderInfo.order_id);
                }
            }
            return adjustmentOrderList;
        }
        #endregion
    }
}
