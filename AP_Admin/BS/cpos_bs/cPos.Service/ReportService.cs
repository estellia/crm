using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Components.SqlMappers;
using cPos.Model.Report;

namespace cPos.Service
{
    /// <summary>
    /// 报表服务
    /// </summary>
    public class ReportService
    {
        #region 销售报表
        /// <summary>
        /// 查询销售报表
        /// </summary>
        /// <param name="loggingSessionInfo">登录model[必须]</param>
        /// <param name="unit_ids">门店集合（不同的门店标识，请用逗号分隔："id1,id2"）[必须]</param>
        /// <param name="order_no">单据号码</param>
        /// <param name="order_date_begin">单据日期起[必须]</param>
        /// <param name="order_date_end">单据日期止[必须]</param>
        /// <param name="maxRowCount">每页数量[必须]</param>
        /// <param name="startRowIndex">开始行号[必须]</param>
        /// <returns></returns>
        public SalesReportInfo SearchSalesReport(LoggingSessionInfo loggingSessionInfo
                                                , string unit_ids
                                                , string order_no
                                                , string order_date_begin
                                                , string order_date_end
                                                , int maxRowCount
                                                , int startRowIndex
                                                )
        {
            try
            {
                SalesReportInfo salesReportInfo = new SalesReportInfo();
                Hashtable _ht = new Hashtable();
                _ht.Add("UnitIds", unit_ids);
                _ht.Add("order_no", order_no);
                _ht.Add("order_date_begin", order_date_begin);
                _ht.Add("order_date_end", order_date_end);
                _ht.Add("StartRow", startRowIndex);
                _ht.Add("EndRow", startRowIndex + maxRowCount);

                IList<SalesReportInfo> salesReportList = new List<SalesReportInfo>();
                salesReportList = cSqlMapper.Instance().QueryForList<SalesReportInfo>("SalesReport.Search", _ht);

                SalesReportInfo salesReportInfo1 = (SalesReportInfo)cSqlMapper.Instance().QueryForObject("SalesReport.SearchTotal", _ht);
                salesReportInfo.sales_total_qty = salesReportInfo1.sales_total_qty; //总销售笔数
                salesReportInfo.sales_total_amount = salesReportInfo1.sales_total_amount;//总销售金额
                salesReportInfo.icount = salesReportInfo1.icount; //总记录数

                salesReportInfo.SalesReportList = salesReportList;
                return salesReportInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        /// <summary>
        /// 销售报名明细
        /// </summary>
        /// <param name="loggingSessionInfo">登录model[必须]</param>
        /// <param name="order_date">日期[必须]</param>
        /// <param name="unit_id">组织标识[必须]</param>
        /// <returns></returns>
        public IList<InoutInfo> GetSalesReportDetail(LoggingSessionInfo loggingSessionInfo, string order_date, string unit_id)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderDate", order_date);
                _ht.Add("UnitId", unit_id);
                return cSqlMapper.Instance().QueryForList<InoutInfo>("SalesReport.GetSalesReportDetail", _ht);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 商品销售报表
        /// <summary>
        /// 商品销售报表查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="item_code">商品号码</param>
        /// <param name="item_name">商品名称</param>
        /// <param name="barcode">条形码</param>
        /// <param name="unit_ids">门店集合（不同的门店标识，请用逗号分隔："id1,id2"）[必须]</param>
        /// <param name="order_date_begin">单据开始[必须]</param>
        /// <param name="order_date_end">单据结束[必须]</param>
        /// <param name="maxRowCount">每页数量[必须]</param>
        /// <param name="startRowIndex">开始行号[必须]</param>
        /// <returns></returns>
        public ItemSalesReportInfo SearchItemSalesReport(LoggingSessionInfo loggingSessionInfo
                                                        , string item_code
                                                        , string item_name
                                                        , string barcode
                                                        , string unit_ids
                                                        , string order_date_begin
                                                        , string order_date_end
                                                        , int maxRowCount
                                                        , int startRowIndex)
        {
            try
            {
                ItemSalesReportInfo itemSalesReportInfo = new ItemSalesReportInfo();
                Hashtable _ht = new Hashtable();
                _ht.Add("UnitIds", unit_ids);
                _ht.Add("item_code", item_code);
                _ht.Add("item_name", item_name);
                _ht.Add("barcode", barcode);
                _ht.Add("order_date_begin", order_date_begin);
                _ht.Add("order_date_end", order_date_end);
                _ht.Add("StartRow", startRowIndex);
                _ht.Add("EndRow", startRowIndex + maxRowCount);

                int iCount = cSqlMapper.Instance().QueryForObject<int>("ItemSalesReport.SearchCount", _ht);
                IList<ItemSalesReportInfo> itemSalesReportList = new List<ItemSalesReportInfo>();
                itemSalesReportList = cSqlMapper.Instance().QueryForList<ItemSalesReportInfo>("ItemSalesReport.Search", _ht);

               

                itemSalesReportInfo.icount = iCount; //总记录数
                itemSalesReportInfo.ItemSalesReportList = itemSalesReportList;
                return itemSalesReportInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 班次统计报表
        /// <summary>
        /// 班次报表统计查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unit_ids">组织集合(不同的门店标识，请用逗号分隔："id1,id2"）[必须]</param>
        /// <param name="user_names">销售人员集合(同门店一样，用逗号分隔，请注意，这里是用户名集合，不是用户标识集合)</param>
        /// <param name="order_date_begin">单据日期开始[必须]</param>
        /// <param name="order_date_end">单据日期结束[必须]</param>
        /// <param name="maxRowCount">每页数量[必须]</param>
        /// <param name="startRowIndex">开始行号[必须]</param>
        /// <returns></returns>
        public ShiftInfo SearchShiftReport(LoggingSessionInfo loggingSessionInfo
                                         , string unit_ids
                                         , string user_names
                                         , string order_date_begin
                                         , string order_date_end
                                         , int maxRowCount
                                         , int startRowIndex
                                         )
        {
            try
            {
                ShiftInfo shiftInfo = new ShiftInfo();
                Hashtable _ht = new Hashtable();
                _ht.Add("UnitIds", unit_ids);
                _ht.Add("UserNames", user_names);
                _ht.Add("order_date_begin", order_date_begin);
                _ht.Add("order_date_end", order_date_end);
                _ht.Add("StartRow", startRowIndex);
                _ht.Add("EndRow", startRowIndex + maxRowCount);

                IList<ShiftInfo> shiftReportList = new List<ShiftInfo>();
                shiftReportList = cSqlMapper.Instance().QueryForList<ShiftInfo>("Shift.SearchReport", _ht);

                ShiftInfo shiftInfo1 = (ShiftInfo)cSqlMapper.Instance().QueryForObject("Shift.SearchReportTotal", _ht);
                shiftInfo.sales_total_qty = shiftInfo1.sales_total_qty; //总销售笔数
                shiftInfo.sales_total_amount = shiftInfo1.sales_total_amount;//总销售金额
                shiftInfo.sales_total_total_amount = shiftInfo1.sales_total_total_amount;
                shiftInfo.total_deposit_amount = shiftInfo1.total_deposit_amount;
                shiftInfo.total_return_amount = shiftInfo1.total_return_amount;
                shiftInfo.total_sale_amount = shiftInfo1.total_sale_amount;
                shiftInfo.icount = shiftInfo1.icount; //总记录数

                shiftInfo.ShiftListInfo = shiftReportList;
                return shiftInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion
    }
}
