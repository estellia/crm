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
    /// 调整单服务
    /// </summary>
    public class AJService
    {
        #region 查询
        /// <summary>
        /// 调整单查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_no">单据号</param>
        /// <param name="status">状态</param>
        /// <param name="unit_id">单位</param>
        /// <param name="warehouse_id">仓库</param>
        /// <param name="order_date_begin">开始日期（yyyy-MM-dd）</param>
        /// <param name="order_date_end">结束日期（yyyy-MM-dd）</param>
        /// <param name="order_reason_type_id">类型2</param>
        /// <param name="ref_order_no">上级单号</param>
        /// <param name="data_from_id">来源</param>
        /// <param name="maxRowCount">当前页数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public InoutInfo SearchAJInfo(LoggingSessionInfo loggingSessionInfo
                                    , string order_no
                                    , string status
                                    , string unit_id
                                    , string warehouse_id
                                    , string order_date_begin
                                    , string order_date_end
                                    , string order_reason_type_id
                                    , string ref_order_no
                                    , string data_from_id
                                    , int maxRowCount
                                    , int startRowIndex
            )
        {
            try
            {
                InoutInfo inoutInfo = new InoutInfo();
                OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
                orderSearchInfo.order_no = order_no;
                orderSearchInfo.status = status;
                orderSearchInfo.unit_id = unit_id;
                orderSearchInfo.warehouse_id = warehouse_id;
                orderSearchInfo.order_date_begin = order_date_begin;
                orderSearchInfo.order_date_end = order_date_end;
                orderSearchInfo.order_reason_id = order_reason_type_id;
                orderSearchInfo.ref_order_no = ref_order_no;
                orderSearchInfo.data_from_id = data_from_id;
                orderSearchInfo.StartRow = startRowIndex;
                orderSearchInfo.EndRow = startRowIndex + maxRowCount;

                orderSearchInfo.order_type_id = "5F11A199E3CD42DE9CAE70442FC3D991";
                InoutService inoutService = new InoutService();
                inoutInfo = inoutService.SearchInoutInfo(loggingSessionInfo, orderSearchInfo);

                return inoutInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 保存调整单

        /// <summary>
        /// 批量保存调整单
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="inoutInfoList">调整单集合</param>
        /// <returns></returns>
        public bool SetAJInfoList(LoggingSessionInfo loggingSessionInfo, IList<InoutInfo> inoutInfoList)
        {
            MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).BeginTransaction();
            string strError = string.Empty;
            try
            {
                bool bReturn = false;
                 
                foreach (InoutInfo inoutInfo in inoutInfoList) {
                     //新建
                    bReturn = SetAJInfo(loggingSessionInfo, false, inoutInfo, out strError);
                    if (!bReturn) break;
                    //审批
                    if (new cBillService().CanApproveBill(loggingSessionInfo, "", inoutInfo.order_id))
                    {
                        bReturn = new InoutService().SetInoutOrderStatus(loggingSessionInfo, inoutInfo.order_id, BillActionType.Approve, out strError);
                        if (!bReturn) break;
                    }
                    //影响库存
                    bReturn = new StockBalanceService().SetStockBalance(loggingSessionInfo, inoutInfo.order_id);
                    if (!bReturn) { break; }
                }
                MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).CommitTransaction();
                return true;
            }
            catch (Exception ex) {
                MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).RollBackTransaction();
                throw (ex);
            }
        }

        /// <summary>
        /// 保存调整单（新建修改）
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <param name="inoutInfo">进入库模板</param>
        /// <param name="strError">输出错误信息</param>
        /// <returns></returns>
        public bool SetAJInfo(LoggingSessionInfo loggingSessionInfo, bool IsTrans, InoutInfo inoutInfo,out string strError)
        {
            if (IsTrans) MSSqlMapper.Instance().BeginTransaction();
            try
            {
                InoutService inoutService = new InoutService();

                if (inoutInfo.order_type_id == null || inoutInfo.order_type_id.Equals("")) {
                    inoutInfo.order_type_id = "5F11A199E3CD42DE9CAE70442FC3D991";
                }
                if (inoutInfo.BillKindCode == null || inoutInfo.BillKindCode.Equals(""))
                {
                    inoutInfo.BillKindCode = "AJ";
                }
                if (inoutInfo.data_from_id == null || inoutInfo.data_from_id.Equals(""))
                {
                    inoutInfo.data_from_id = "B8DF5D46D3CA430ABE21E20F8D71E212";
                }

                if (inoutInfo.operate == null || inoutInfo.operate.Equals(""))
                {
                    inoutInfo.operate = "Create";
                }
                 
                if(inoutInfo.sales_unit_id == null || inoutInfo.sales_unit_id.Equals(""))
                {
                    if (inoutInfo.unit_id != null && (!inoutInfo.unit_id.Equals("")))
                    {
                        inoutInfo.sales_unit_id = inoutInfo.unit_id;
                    }
                }
                if (inoutService.SetInoutInfo(loggingSessionInfo, inoutInfo, false, out strError))
                {
                    if (IsTrans) MSSqlMapper.Instance().CommitTransaction();
                    return true;
                }
                else {
                    if (IsTrans) MSSqlMapper.Instance().RollBackTransaction();
                    return false;
                }
                
            }
            catch (Exception ex) {
                if (IsTrans) MSSqlMapper.Instance().RollBackTransaction();
                throw (ex);
            }
        }
        
        #endregion

        #region 获取未审批的调整单
        /// <summary>
        /// 根据状态与组织获取相关的调整单集合
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unit_id">组织</param>
        /// <param name="status">状态值</param>
        /// <returns></returns>
        public IList<InoutInfo> GetAJListByStatus(LoggingSessionInfo loggingSessionInfo, string unit_id, string status)
        {
            try
            {
                IList<InoutInfo> inoutInfoList = new List<InoutInfo>();
                OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
                orderSearchInfo.unit_id = unit_id;
                //orderSearchInfo.sales_unit_id = unit_id;
                orderSearchInfo.status = status;
                orderSearchInfo.order_type_id = "5F11A199E3CD42DE9CAE70442FC3D991";
                orderSearchInfo.StartRow = 0;
                orderSearchInfo.EndRow = 36500;

                InoutInfo inoutInfo = new InoutInfo();
                inoutInfo = new InoutService().SearchInoutInfo(loggingSessionInfo, orderSearchInfo);
                inoutInfoList = inoutInfo.InoutInfoList;
                foreach (InoutInfo inout in inoutInfoList)
                {
                    inout.InoutDetailList = new InoutService().GetInoutDetailInfoByOrderId(loggingSessionInfo, inout.order_id);
                }
                return inoutInfoList;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion
    }
}
