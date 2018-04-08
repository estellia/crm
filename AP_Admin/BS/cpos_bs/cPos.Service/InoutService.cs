using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Components.SqlMappers;

namespace cPos.Service
{
    /// <summary>
    /// 进出库单据服务
    /// </summary>
    public class InoutService:BaseService
    {
        #region 出入库单据查询
        /// <summary>
        /// 出入库单据查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录用户信息集合</param>
        /// <param name="order_no">单据号码</param>
        /// <param name="order_reason_type_id">类型标识</param>
        /// <param name="sales_unit_id">销售单位标识</param>
        /// <param name="warehouse_id">仓库标识</param>
        /// <param name="purchase_unit_id">采购单位标识</param>
        /// <param name="status">状态code</param>
        /// <param name="order_date_begin">单据日期起(yyyy-MM-dd)</param>
        /// <param name="order_date_end">单据日期止(yyyy-MM-dd)</param>
        /// <param name="complete_date_begin">完成日期起(yyyy-MM-dd)</param>
        /// <param name="complete_date_end">完成日期止(yyyy-MM-dd)</param>
        /// <param name="data_from_id">数据来源标识</param>
        /// <param name="ref_order_no">原单据号码</param>
        /// <param name="order_type_id">出入库单据标签：（出库单=1F0A100C42484454BAEA211D4C14B80F，入库单=C1D407738E1143648BC7980468A399B8）</param>
        /// <param name="maxRowCount">当前页显示数量</param>
        /// <param name="startRowIndex">当前页开始数量</param>
        /// <returns></returns>
        public InoutInfo SearchInoutInfo(LoggingSessionInfo loggingSessionInfo
                                       , string order_no
                                       , string order_reason_type_id
                                       , string sales_unit_id
                                       , string warehouse_id
                                       , string purchase_unit_id
                                       , string status
                                       , string order_date_begin
                                       , string order_date_end
                                       , string complete_date_begin
                                       , string complete_date_end
                                       , string data_from_id
                                       , string ref_order_no
                                       , string order_type_id
                                       , int maxRowCount
                                       , int startRowIndex)
        {
            InoutInfo inoutInfo = new InoutInfo();
            OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
            orderSearchInfo.order_no = order_no;
            orderSearchInfo.order_reason_id = order_reason_type_id;
            orderSearchInfo.sales_unit_id = sales_unit_id;
            orderSearchInfo.warehouse_id = warehouse_id;
            orderSearchInfo.purchase_unit_id = purchase_unit_id;
            orderSearchInfo.status = status;
            orderSearchInfo.order_date_begin = order_date_begin;
            orderSearchInfo.order_date_end = order_date_end;
            orderSearchInfo.complete_date_begin = complete_date_begin;
            orderSearchInfo.complete_date_end = complete_date_end;
            orderSearchInfo.data_from_id = data_from_id;
            orderSearchInfo.ref_order_no = ref_order_no;
            orderSearchInfo.order_type_id = order_type_id;
            orderSearchInfo.StartRow = startRowIndex;
            orderSearchInfo.EndRow = startRowIndex + maxRowCount;
            orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;

            inoutInfo = SearchInoutInfo(loggingSessionInfo, orderSearchInfo);
            return inoutInfo;
        }
        #endregion

        #region inout类型单据保存
        /// <summary>
        /// inout 单据保存
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="inoutInfo">inout model</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <param name="strError">输出错误信息</param>
        /// <returns></returns>
        public bool SetInoutInfo(LoggingSessionInfo loggingSessionInfo, InoutInfo inoutInfo,bool IsTrans,out string strError)
        {
            string strDo = string.Empty;
            if (IsTrans) cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).BeginTransaction();
            try
            {
                if (inoutInfo.order_id == null || inoutInfo.order_id.Equals(""))
                {
                    inoutInfo.order_id = NewGuid();
                    inoutInfo.operate = "Create";
                }
                if (inoutInfo.Equals("Create"))
                {
                    //1.判断重复
                    if (!IsExistOrderCode(loggingSessionInfo, inoutInfo.order_no, inoutInfo.order_id))
                    {
                        strError = "订单号码已经存在。";
                        //return false;
                        throw (new System.Exception(strError));
                    }
                }
                //判断是否有上级订单信息
                if ( inoutInfo.ref_order_no != null && (!inoutInfo.ref_order_no.Equals("")) && (inoutInfo.ref_order_id == null || inoutInfo.ref_order_id.Equals("")))
                {
                    string orderId = GetOrderIdByOrderCode(loggingSessionInfo, inoutInfo.ref_order_no);
                    if (orderId != null && (!orderId.Equals("")))
                    {
                        inoutInfo.ref_order_id = orderId;
                    }
                }
                string strCount = string.Empty;
                if (inoutInfo.operate.Equals("Create") 
                    && (!new cBillService().CanHaveBill(loggingSessionInfo, inoutInfo.order_id,out strCount))
                    )
                {
                    //2.提交表单
                    if (!SetInoutOrderInsertBill(loggingSessionInfo, inoutInfo))
                    {
                        strError = "inout表单提交失败。";
                        throw (new System.Exception(strError));
                    }
                    //3.更改状态
                    BillModel billInfo = new cBillService().GetBillById(loggingSessionInfo, inoutInfo.order_id);
                    if (billInfo != null)
                    {
                        inoutInfo.status = billInfo.Status;
                        inoutInfo.status_desc = billInfo.BillStatusDescription;
                        strDo = billInfo.Status + "--" + inoutInfo.order_id;
                    }
                    else
                    {
                        strDo = "没找到对应的bill";
                    }
                }
                else {
                    strDo = "存在相同的表单:" + strCount + ":-- " + loggingSessionInfo.CurrentLoggingManager.Connection_String + "--";
                }
                
                //4.提交inout与inoutdetail信息
                if (!SetInoutTableInfo(loggingSessionInfo, inoutInfo))
                {
                    strError = "提交用户表失败";
                    throw (new System.Exception(strError));
                }
                if (IsTrans) cSqlMapper.Instance().CommitTransaction();
                strError = "保存成功!"; 
                return true;
            }
            catch (Exception ex)
            {
                if (IsTrans) cSqlMapper.Instance().RollBackTransaction();
                throw (ex);
            }
        }

        /// <summary>
        /// 提交inout信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="inoutInfo"></param>
        /// <returns></returns>
        private bool SetInoutTableInfo(LoggingSessionInfo loggingSessionInfo, InoutInfo inoutInfo)
        {
            try
            {
                if (inoutInfo != null)
                {
                    inoutInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                    if (inoutInfo.create_user_id == null || inoutInfo.create_user_id.Equals(""))
                    {
                        inoutInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
                        inoutInfo.create_time = GetCurrentDateTime();
                    }
                    if (inoutInfo.modify_user_id == null || inoutInfo.modify_user_id.Equals(""))
                    {
                        inoutInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                        inoutInfo.modify_time = GetCurrentDateTime();
                    }
                    if (inoutInfo.InoutDetailList != null) {
                        foreach (InoutDetailInfo inoutDetailInfo in inoutInfo.InoutDetailList)
                        {
                            if (inoutDetailInfo.order_detail_id == null || inoutDetailInfo.order_detail_id.Equals(""))
                            {
                                inoutDetailInfo.order_detail_id = NewGuid();
                            }
                            inoutDetailInfo.order_id = inoutInfo.order_id;
                        }
                    }
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Inout.InsertOrUpdate", inoutInfo);
                }
                return true;
            }
            catch (Exception ex) {
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
                int n = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Inout.IsExsitOrderCode", _ht);
                return n > 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 调价单提交到表单
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="inoutInfo"></param>
        /// <returns></returns>
        private bool SetInoutOrderInsertBill(LoggingSessionInfo loggingSessionInfo, InoutInfo inoutInfo)
        {
            try
            {
                cPos.Model.BillModel bill = new BillModel();
                cPos.Service.cBillService bs = new cBillService();

                bill.Id = inoutInfo.order_id;
                string order_type_id = bs.GetBillKindByCode(loggingSessionInfo, inoutInfo.BillKindCode).Id.ToString(); //loggingSession, OrderType
                bill.Code = bs.GetBillNextCode(loggingSessionInfo,inoutInfo.BillKindCode); //BillKindCode
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
                throw (ex);
            }
        }

        #endregion

        #region inout单据，状态修改

        /// <summary>
        /// Inout状态修改（审核，删除。。。。）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="order_id"></param>
        /// <param name="billActionType"></param>
        /// <returns></returns>
        public bool SetInoutOrderStatus(LoggingSessionInfo loggingSessionInfo, string order_id, BillActionType billActionType)
        {
            string err;
            return SetInoutOrderStatus(loggingSessionInfo, order_id, billActionType, out err);
        }

        /// <summary>
        /// Inout状态修改（审核，删除。。。。）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="order_id"></param>
        /// <param name="billActionType"></param>
        /// <param name="strError">输出信息</param>
        /// <returns></returns>
        public bool SetInoutOrderStatus(LoggingSessionInfo loggingSessionInfo, string order_id, BillActionType billActionType,out string strError)
        {
            string strResult = string.Empty;
            try
            {
                cPos.Service.cBillService bs = new cBillService();

                BillOperateStateService state = bs.ApproveBill(loggingSessionInfo, order_id, "", billActionType,out strResult);
                if (state == BillOperateStateService.ApproveSuccessful)
                {
                    //获取要改变的表单信息
                    BillModel billInfo = new cBillService().GetBillById(loggingSessionInfo, order_id);
                    //设置要改变的用户信息
                    InoutInfo inoutInfo = new InoutInfo();
                    inoutInfo.status = billInfo.Status;
                    inoutInfo.status_desc = billInfo.BillStatusDescription;
                    inoutInfo.order_id = order_id;
                    inoutInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    inoutInfo.modify_time = GetCurrentDateTime(); //获取当前时间
                    if (billActionType == BillActionType.Approve) {
                        inoutInfo.approve_time = GetCurrentDateTime();
                        inoutInfo.approve_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    }
                    //提交
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Inout.UpdateStatus", inoutInfo);
                    strError = "审批成功";
                    return true;
                }
                else
                {
                    strError = "获取状态失败--" + strResult;
                    return false;
                }


            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                throw (ex);
            }
        }

        #endregion

        #region inout单据查询
        /// <summary>
        /// inout 单据查询
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public InoutInfo SearchInoutInfo(LoggingSessionInfo loggingSessionInfo, OrderSearchInfo orderSearchInfo)
        {
            try
            {
                orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                InoutInfo inoutInfo = new InoutInfo();
                int iCount = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Inout.SearchCount", orderSearchInfo);
                IList<InoutInfo> inoutInfoList = new List<InoutInfo>();
                inoutInfoList = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<InoutInfo>("Inout.Search", orderSearchInfo);
                inoutInfo.ICount = iCount;
                inoutInfo.InoutInfoList = inoutInfoList;
                return inoutInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 单个单据详细信息
        /// <summary>
        /// 获取单个进出库单据的详细信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public InoutInfo GetInoutInfoById(LoggingSessionInfo loggingSessionInfo, string orderId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", orderId);
                InoutInfo inoutInfo = new InoutInfo();
                inoutInfo = (InoutInfo)cSqlMapper.Instance().QueryForObject("Inout.SelectById", _ht);
                inoutInfo.InoutDetailList = GetInoutDetailInfoByOrderId(loggingSessionInfo, orderId);
                return inoutInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        /// <summary>
        /// 根据订单号，获取订单标识
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        public string GetOrderIdByOrderCode(LoggingSessionInfo loggingSessionInfo, string orderNo)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("OrderNo", orderNo);
            string order_id = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<string>("Inout.SelectOrderIdByOrderNo", _ht);
            return order_id;

        }
        /// <summary>
        /// 获取明细集合
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IList<InoutDetailInfo> GetInoutDetailInfoByOrderId(LoggingSessionInfo loggingSessionInfo, string orderId)
        {
            try
            {
                IList<InoutDetailInfo> inoutDetailList = new List<InoutDetailInfo>();
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", orderId);
                inoutDetailList = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<InoutDetailInfo>("InoutDetail.SelectByOrderId", _ht);
                return inoutDetailList;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        #endregion

        #region 根据班次获取inout pos小票集合
        /// <summary>
        /// 根据班次标识获取pos小票信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="shiftId">班次标识</param>
        /// <returns></returns>
        public IList<InoutInfo> GetInoutListByShiftId(LoggingSessionInfo loggingSessionInfo, string shiftId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("ShiftId", shiftId);
                return cSqlMapper.Instance().QueryForList<InoutInfo>("Inout.SelectByShiftId", _ht);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        #endregion

        #region 批量处理上传标志
        /// <summary>
        /// 批量修改上传标志
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="if_flag">上传标志</param>
        /// <param name="InoutInfoList">单据集合</param>
        /// <param name="IsTrans">是否提交</param>
        /// <returns></returns>
        public bool SetInoutIfFlag(LoggingSessionInfo loggingSessionInfo, string if_flag, IList<InoutInfo> InoutInfoList, bool IsTrans)
        {
            if (IsTrans) cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).BeginTransaction();
            try
            {
                bool bReturn = false;
                InoutInfo inoutInfo = new InoutInfo();
                inoutInfo.if_flag = if_flag;
                inoutInfo.InoutInfoList = InoutInfoList;
                inoutInfo.modify_time = GetCurrentDateTime();
                inoutInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Inout.UpdateIfflag", inoutInfo);
                if (IsTrans) cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).CommitTransaction();
                return bReturn;
            }
            catch (Exception ex) {
                if (IsTrans) cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).RollBackTransaction();
                throw (ex);
            }
        }
        #endregion

        #region 上传到管理平台的inout单据
        /// <summary>
        /// 获取Inout的未下载数据集合
        /// </summary>
        /// <param name="orderSearchInfo">参数对像</param>
        /// <returns></returns>
        public int GetInoutNotPackagedCountWeb(OrderSearchInfo orderSearchInfo)
        {
            LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(orderSearchInfo.customer_id);
            return cSqlMapper.Instance(loggingManager).QueryForObject<int>("Inout.SelectUnDownloadCount", orderSearchInfo);
        }
        /// <summary>
        /// 获取Inout信息集合
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public IList<InoutInfo> GetInoutListPackagedWeb(OrderSearchInfo orderSearchInfo)
        {
            LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(orderSearchInfo.customer_id);
            return cSqlMapper.Instance(loggingManager).QueryForList<InoutInfo>("Inout.SelectUnDownloadInout", orderSearchInfo);
        }
        /// <summary>
        /// 下载进出库单明细
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="inoutInfoList"></param>
        /// <returns></returns>
        public IList<InoutDetailInfo> GetInoutDetailListPackageWeb(string Customer_Id, string Unit_Id, List<InoutInfo> inoutInfoList)
        {
            try
            {
                LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(Customer_Id);
                InoutInfo inoutInfo = new InoutInfo();
                inoutInfo.InoutInfoList = inoutInfoList;
                return cSqlMapper.Instance(loggingManager).QueryForList<InoutDetailInfo>("InoutDetail.SelectUnDownloadInoutDetail", inoutInfo);
            }
            catch (Exception ex) {
                return null;
            }
        }

        /// <summary>
        /// 更新批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="inoutInfo">订单信息</param>
        /// <returns></returns>
        public bool SetInoutUpdateUnDownloadBatIdWeb(string Customer_Id, InoutInfo inoutInfo)
        {
            LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(Customer_Id);
            cSqlMapper.Instance(loggingManager).Update("Inout.UpdateUnDownloadBatId", inoutInfo);
            return true;
            
        }
        /// <summary>
        /// 更改上传标志
        /// </summary>
        /// <param name="Customer_Id"></param>
        /// <param name="inoutInfo"></param>
        /// <returns></returns>
        public bool SetInoutIfFlagInfoWeb(string Customer_Id, InoutInfo inoutInfo)
        {
            LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(Customer_Id);
            cSqlMapper.Instance(loggingManager).Update("Inout.UpdateUnDownloadIfFlag", inoutInfo);
            return true;
        }
        #endregion

        /// <summary>
        /// 更改上传标志
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次标识</param>
        /// <param name="strError">错误信息返回</param>
        /// <returns></returns>
        public bool SetInoutOrderIfFlagInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, out string strError)
        {
            InoutInfo inoutInfo = new InoutInfo();
            inoutInfo.bat_id = bat_id;

            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Inout.UpdateUnDownloadIfFlag", inoutInfo);
            strError = "Success";
            return true;
        }
    }
}
