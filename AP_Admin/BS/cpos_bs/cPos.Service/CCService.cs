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
    /// 盘点单类
    /// </summary>
    public class CCService : BaseService
    {
        #region 查询
        /// <summary>
        /// 盘点单查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_no">单据号码</param>
        /// <param name="status">状态标识</param>
        /// <param name="unit_id">单位标识</param>
        /// <param name="warehouse_id">仓库标识</param>
        /// <param name="order_date_begin">单据日期开始【yyyy-MM-dd】</param>
        /// <param name="order_date_end">单据日期结束【yyyy-MM-dd】</param>
        /// <param name="complete_date_begin">完成日期开始【yyyy-MM-dd】</param>
        /// <param name="complete_date_end">完成日期结束【yyyy-MM-dd】</param>
        /// <param name="data_from_id">数据来源标识</param>
        /// <param name="maxRowCount">每页最大数量</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns></returns>
        public CCInfo SearchCCInfo(LoggingSessionInfo loggingSessionInfo
                                        , string order_no
                                        , string status
                                        , string unit_id
                                        , string warehouse_id
                                        , string order_date_begin
                                        , string order_date_end
                                        , string complete_date_begin
                                        , string complete_date_end
                                        , string data_from_id
                                        , int maxRowCount
                                        , int startRowIndex
                                        )
        {
            try
            {
                OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
                orderSearchInfo.order_no = order_no;
                orderSearchInfo.status = status;
                orderSearchInfo.unit_id = unit_id;
                orderSearchInfo.warehouse_id = warehouse_id;
                orderSearchInfo.order_date_begin = order_date_begin;
                orderSearchInfo.order_date_end = order_date_end;
                orderSearchInfo.complete_date_begin = complete_date_begin;
                orderSearchInfo.complete_date_end = complete_date_end;
                orderSearchInfo.data_from_id = data_from_id;
                orderSearchInfo.StartRow = startRowIndex;
                orderSearchInfo.EndRow = startRowIndex + maxRowCount;
                orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;

                orderSearchInfo.order_type_id = "51BE351BFA5E49A490103669EA21BC3C";
                

                CCInfo ccInfo = new CCInfo();
                int iCount = cSqlMapper.Instance().QueryForObject<int>("CC.SearchCount", orderSearchInfo);

                IList<CCInfo> ccInfoList = new List<CCInfo>();
                ccInfoList = cSqlMapper.Instance().QueryForList<CCInfo>("CC.Search", orderSearchInfo);

                ccInfo.ICount = iCount;
                ccInfo.CCInfoList = ccInfoList;
                return ccInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 获取单个盘点单明细
        
        /// <summary>
        /// 获取单个盘点单明细
        /// </summary>
        /// <param name="loggingSessionInfo">登录model【必须】</param>
        /// <param name="orderId">订单标识【必须】</param>
        /// <param name="maxRowCount">商品明细当前页显示数量</param>
        /// <param name="startRowIndex">商品明细当前页开始行</param>
        /// <returns></returns>
        public CCInfo GetCCInfoById(LoggingSessionInfo loggingSessionInfo, string orderId, int maxRowCount, int startRowIndex)
        {
            try
            {
                CCInfo ccInfo = new CCInfo();
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", orderId);
                ccInfo = (CCInfo)cSqlMapper.Instance().QueryForObject("CC.SelectById", _ht);

                CCInfo ccInfo1 = new CCInfo();
                ccInfo1 = GetCCDetailInfoByOrderId(loggingSessionInfo, orderId, maxRowCount, startRowIndex);
                ccInfo.CCDetail_ICount = ccInfo1.CCDetail_ICount;
                ccInfo.CCDetailInfoList = ccInfo1.CCDetailInfoList;
                return ccInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        /// <summary>
        /// 获取单个盘点单sku明细
        /// </summary>
        /// <param name="loggingSessionInfo">登录model【必须】</param>
        /// <param name="orderId">订单标识【必须】</param>
        /// <param name="maxRowCount">商品明细当前页显示数量</param>
        /// <param name="startRowIndex">商品明细当前页开始行</param>
        /// <returns></returns>
        public CCInfo GetCCDetailInfoByOrderId(LoggingSessionInfo loggingSessionInfo, string orderId, int maxRowCount, int startRowIndex)
        {
            try
            {
                CCInfo ccInfo = new CCInfo();
                IList<CCDetailInfo> ccDetailList = new List<CCDetailInfo>();
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", orderId);
                _ht.Add("StartRow", startRowIndex);
                _ht.Add("EndRow", startRowIndex + maxRowCount);
                int iCount = cSqlMapper.Instance().QueryForObject<int>("CCDetail.SelectByOrderIdCount", _ht);

                ccDetailList = cSqlMapper.Instance().QueryForList<CCDetailInfo>("CCDetail.SelectByOrderId", _ht);
                ccInfo.CCDetail_ICount = iCount;
                ccInfo.CCDetailInfoList = ccDetailList;

                return ccInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 获取商品明细
        /// <summary>
        /// 获取商品明细
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_id">订单标识</param>
        /// <param name="unit_id">组织标识</param>
        /// <param name="warehouse_id">门店标识</param>
        /// <param name="maxRowCount">最大数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns>ccDetailInfo.icount = 总数量 ccDetailInfo.CCDetailInfoList = 商品明细集合</returns>
        public CCDetailInfo GetCCDetailListStockBalance(LoggingSessionInfo loggingSessionInfo,string order_id, string unit_id, string warehouse_id, int maxRowCount, int startRowIndex)
        {
            try
            {
                CCDetailInfo ccDetailInfo = new CCDetailInfo();

                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", order_id);
                _ht.Add("UnitId", unit_id);
                _ht.Add("WarehouseId", warehouse_id);
                _ht.Add("StartRow", startRowIndex);
                _ht.Add("EndRow", startRowIndex + maxRowCount);

                int iCount = cSqlMapper.Instance().QueryForObject<int>("CCDetail.SelectStockBanlanceCount", _ht);

                IList<CCDetailInfo> ccDetailList = new List<CCDetailInfo>();
                ccDetailList = cSqlMapper.Instance().QueryForList<CCDetailInfo>("CCDetail.SelectStockBanlance", _ht);
                ccDetailInfo.icount = iCount;
                ccDetailInfo.CCDetailInfoList = ccDetailList;
                return ccDetailInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 处理盘点单信息（新建，修改）
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="ccInfo">盘点单model</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <param name="strError">返回错误信息</param>
        /// <returns></returns>
        public bool SetCCInfo(LoggingSessionInfo loggingSessionInfo, CCInfo ccInfo, bool IsTrans, out string strError)
        {
            string strDo = string.Empty;
            if (IsTrans) cSqlMapper.Instance().BeginTransaction();
            try
            {
                ccInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                if (ccInfo.if_flag == null || ccInfo.if_flag.ToString().Trim().Equals(""))
                {
                    ccInfo.if_flag = "0";
                }
                if (ccInfo.order_type_id == null || ccInfo.order_type_id.Equals(""))
                {
                    ccInfo.order_type_id = "51BE351BFA5E49A490103669EA21BC3C";
                }
                if (ccInfo.BillKindCode == null || ccInfo.BillKindCode.Equals(""))
                {
                    ccInfo.BillKindCode = "CC";
                }
                if (ccInfo.order_id == null || ccInfo.order_id.Equals(""))
                {
                    ccInfo.order_id = NewGuid();
                }
                //1.判断重复
                if (!IsExistOrderCode(loggingSessionInfo, ccInfo.order_no, ccInfo.order_id))
                {
                    strError = "订单号码已经存在。";
                    throw (new System.Exception(strError));
                }
                if (ccInfo.operate == null || ccInfo.operate.Equals(""))
                {
                    ccInfo.operate = "Create";
                }
                if (ccInfo.operate.Equals("Create"))
                {
                    //2.提交表单
                    if (!SetInoutOrderInsertBill(loggingSessionInfo, ccInfo))
                    {
                        strError = "盘点单表单提交失败。";
                        throw (new System.Exception(strError));
                    }
                    //3.更改状态
                    BillModel billInfo = new cBillService().GetBillById(loggingSessionInfo, ccInfo.order_id);
                    if (billInfo != null)
                    {
                        ccInfo.status = billInfo.Status;
                        ccInfo.status_desc = billInfo.BillStatusDescription;
                    }
                }

                //4.提交cc与ccdetail信息
                if (!SetCCTableInfo(loggingSessionInfo, ccInfo))
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
        /// 判断盘点单号码是否重复
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_no">订单号</param>
        /// <param name="order_id">订单标识</param>
        /// <returns></returns>
        public bool IsExistOrderCode(LoggingSessionInfo loggingSessionInfo, string order_no, string order_id)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderNo", order_no);
                _ht.Add("OrderId", order_id);
                int n = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("CC.IsExsitOrderCode", _ht);
                return n > 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 盘点单提交到表单中
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="ccInfo"></param>
        /// <returns></returns>
        private bool SetInoutOrderInsertBill(LoggingSessionInfo loggingSessionInfo, CCInfo ccInfo)
        {
            try
            {
                cPos.Model.BillModel bill = new BillModel();
                cPos.Service.cBillService bs = new cBillService();

                bill.Id = ccInfo.order_id;
                string order_type_id = bs.GetBillKindByCode(loggingSessionInfo, ccInfo.BillKindCode).Id.ToString(); //loggingSession, OrderType
                bill.Code = bs.GetBillNextCode(loggingSessionInfo, ccInfo.BillKindCode); //BillKindCode
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

        /// <summary>
        /// 提交盘点单据
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="ccInfo">盘点单信息</param>
        /// <returns></returns>
        private bool SetCCTableInfo(LoggingSessionInfo loggingSessionInfo, CCInfo ccInfo)
        {
            //try
            //{
                if (ccInfo != null)
                {
                    if (ccInfo.data_from_id == null || ccInfo.data_from_id.Equals(""))
                    {
                        ccInfo.data_from_id = "B8DF5D46D3CA430ABE21E20F8D71E212";
                    }
                    if (ccInfo.create_user_id == null || ccInfo.create_user_id.Equals(""))
                    {
                        ccInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
                        ccInfo.create_time = GetCurrentDateTime();
                    }
                    if (ccInfo.modify_user_id == null || ccInfo.modify_user_id.Equals(""))
                    {
                        ccInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                        ccInfo.modify_time = GetCurrentDateTime();
                    }
                    if (ccInfo.CCDetailInfoList != null)
                    {
                        foreach (CCDetailInfo ccDetailInfo in ccInfo.CCDetailInfoList)
                        {
                            if (ccDetailInfo.order_detail_id == null || ccDetailInfo.order_detail_id.Equals(""))
                            {
                                ccDetailInfo.order_detail_id = NewGuid();
                            }
                            if (ccDetailInfo.if_flag.ToString() == null || ccDetailInfo.if_flag.ToString().Equals(""))
                            {
                                ccDetailInfo.if_flag = 0;
                            }
                            ccDetailInfo.order_id = ccInfo.order_id;
                            ccDetailInfo.order_no = ccInfo.order_no;
                        }
                    }
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("CC.InsertOrUpdate", ccInfo);
                }
                return true;
            //}
            //catch (Exception ex)
            //{
            //    strError = ex.ToString();
            //    throw (ex);
            //}
        }
        #endregion

        #region 盘点单状态更新
        /// <summary>
        /// 盘点单审批
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="order_id"></param>
        /// <param name="billActionType"></param>
        /// <returns></returns>
        public bool SetCCOrderStatus(LoggingSessionInfo loggingSessionInfo, string order_id, BillActionType billActionType)
        {
            string strResult = string.Empty;
            try
            {
                cPos.Service.cBillService bs = new cBillService();

                BillOperateStateService state = bs.ApproveBill(loggingSessionInfo, order_id, "", billActionType);
                if (state == BillOperateStateService.ApproveSuccessful)
                {
                    //获取要改变的表单信息
                    BillModel billInfo = new cBillService().GetBillById(loggingSessionInfo, order_id);
                    //设置要改变的用户信息
                    CCInfo ccInfo = new CCInfo();
                    ccInfo.status = billInfo.Status;
                    ccInfo.status_desc = billInfo.BillStatusDescription;
                    ccInfo.order_id = order_id;
                    ccInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    ccInfo.modify_time = GetCurrentDateTime(); //获取当前时间
                    if (billActionType == BillActionType.Approve)
                    {
                        ccInfo.approve_time = GetCurrentDateTime();
                        ccInfo.approve_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    }
                    //提交
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("CC.UpdateStatus", ccInfo);
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

        #region 批量保存盘点单
        /// <summary>
        /// 批量保存盘点单
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="ccInfoList"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetCCInfoList(LoggingSessionInfo loggingSessionInfo, IList<CCInfo> ccInfoList, out string strError)
        {
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).BeginTransaction();
            try
            {
                string sError = string.Empty;
                bool bReturn = true;
                
                foreach (CCInfo ccInfo in ccInfoList)
                {
                    bReturn = SetCCInfo(loggingSessionInfo, ccInfo, false, out sError);
                    if (!bReturn) { break; }
                    //判断是否已经审批
                    if (new cBillService().CanApproveBill(loggingSessionInfo, "", ccInfo.order_id) && ccInfo.order_reason_id.Trim().Equals("58EE2F8732144C8F95E7809FFAF45827")) 
                    {
                        //自动审批
                        bReturn = SetCCOrderStatus(loggingSessionInfo, ccInfo.order_id, BillActionType.Approve);
                        if (!bReturn) { break; }
                       
                    }
                }
                strError = sError;
                if (bReturn)
                {
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).CommitTransaction();
                    return true;
                }
                else
                {
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).RollBackTransaction();
                    return false;
                }

            }
            catch (Exception ex)
            {
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).RollBackTransaction();
                throw (ex);
            }
        }
        #endregion

        #region 判断盘点单是否能成成调整单
        /// <summary>
        /// 判断盘点单是否有生成调整单
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="orderId">单据标识</param>
        /// <returns></returns>
        public bool IsExistAJOrder(LoggingSessionInfo loggingSessionInfo, string orderId)
        {
            bool bReturn = false;
            Hashtable _ht = new Hashtable();
            _ht.Add("OrderId", orderId);
            int iCount = cSqlMapper.Instance().QueryForObject<int>("CC.IsExistAJ", _ht);
            int iCount1 = cSqlMapper.Instance().QueryForObject<int>("CC.IsHaveAJCount", _ht);
            if (iCount == 1 && iCount1 == 0) {
                bReturn = true;
            }
            return bReturn;
        }
        #endregion

        #region 盘点单生产调整单
        /// <summary>
        /// 盘点单生产调整单
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_id">盘点单标识</param>
        /// <returns></returns>
        public bool SetCCToAJ(LoggingSessionInfo loggingSessionInfo, string order_id)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", order_id);
                _ht.Add("OrderNo",GetNo(loggingSessionInfo,"AJ"));
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("CC.SetCCToAJ", _ht);
                return true;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        /// <summary>
        /// 盘点单是否有差异
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_id">盘点单标识</param>
        /// <returns>ture=有差异；false=无差异，则不能生产调整单</returns>
        public bool IsCCDifference(LoggingSessionInfo loggingSessionInfo, string order_id)
        {
            try
            {
                bool bReturn = false;
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", order_id);

                int iCount = cSqlMapper.Instance().QueryForObject<int>("CC.IsCCDifference", _ht);
                if (iCount.ToString().Equals("0") || iCount.ToString().Equals(""))
                {
                    bReturn = false;
                }else{
                    bReturn = true;
                }
                return bReturn;
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
        /// <param name="CCInfoList">单据集合</param>
        /// <param name="IsTrans">是否提交</param>
        /// <returns></returns>
        public bool SetCCIfFlag(LoggingSessionInfo loggingSessionInfo, string if_flag, IList<CCInfo> CCInfoList, bool IsTrans)
        {
            if (IsTrans) cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).BeginTransaction();
            try
            {
                bool bReturn = false;
                CCInfo inoutInfo = new CCInfo();
                inoutInfo.if_flag = if_flag;
                inoutInfo.CCInfoList = CCInfoList;
                inoutInfo.modify_time = GetCurrentDateTime();
                inoutInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("CC.UpdateIfflag", inoutInfo);
                if (IsTrans) cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).CommitTransaction();
                return bReturn;
            }
            catch (Exception ex)
            {
                if (IsTrans) cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).RollBackTransaction();
                throw (ex);
            }
        }
        #endregion

    }
}
