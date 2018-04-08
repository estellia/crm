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
    /// 库存类
    /// </summary>
    public class StockBalanceService
    {
        #region 根据组织门店获取库存
        /// <summary>
        /// 根据组织门店获取库存
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unit_id">组织标识</param>
        /// <param name="warehouse_id">门店标识</param>
        /// <param name="maxRowCount">最大数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public StockBalanceInfo GetStockBalanceListByUnitIdWarehouseId(LoggingSessionInfo loggingSessionInfo
                                                                            , string unit_id
                                                                            , string warehouse_id
                                                                            , int maxRowCount
                                                                            , int startRowIndex
                                                                            )
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("StartRow",startRowIndex);
                _ht.Add("EndRow",startRowIndex + maxRowCount);
                _ht.Add("UnitId",unit_id);
                _ht.Add("WarehouseId",warehouse_id);
                _ht.Add("TypeValue", "1");

                StockBalanceInfo stockBalanceInfo = new StockBalanceInfo();

                int iCount = cSqlMapper.Instance().QueryForObject<int>("StockBalance.SearchCount", _ht);

                IList<StockBalanceInfo> stockBalanceInfoList = new List<StockBalanceInfo>();
                stockBalanceInfoList = cSqlMapper.Instance().QueryForList<StockBalanceInfo>("StockBalance.Search", _ht);
                stockBalanceInfo.icount = iCount;
                stockBalanceInfo.StockBalanceInfoList = stockBalanceInfoList;

                return stockBalanceInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }                                                                   

        #endregion

        #region 库存查询
        /// <summary>
        /// 库存查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录信息集合</param>
        /// <param name="unit_id">组织标识</param>
        /// <param name="warehouse_id">仓库标识</param>
        /// <param name="item_code">商品号码</param>
        /// <param name="item_name">商品名称</param>
        /// <param name="typeValue">类型</param>
        /// <param name="yearMonth">年月</param>
        /// <param name="maxRowCount">最大数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public StockBalanceInfo SearchStockBalance(LoggingSessionInfo loggingSessionInfo
                                                  , string unit_id
                                                  , string warehouse_id
                                                  , string item_code
                                                  , string item_name
                                                  , string typeValue
                                                  , string yearMonth
                                                  , int maxRowCount
                                                  , int startRowIndex
                                                  )
        {
            StockBalanceInfo stockBalanceInfo = new StockBalanceInfo();

            Hashtable _ht = new Hashtable();
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            _ht.Add("UnitId", unit_id);
            _ht.Add("WarehouseId", warehouse_id);
            _ht.Add("ItemCode", item_code);
            _ht.Add("ItemName", item_name);
            _ht.Add("TypeValue", typeValue);
            _ht.Add("YearMonth", yearMonth);

            int iCount = cSqlMapper.Instance().QueryForObject<int>("StockBalance.SearchCount", _ht);

            IList<StockBalanceInfo> stockBalanceInfoList = new List<StockBalanceInfo>();
            stockBalanceInfoList = cSqlMapper.Instance().QueryForList<StockBalanceInfo>("StockBalance.Search", _ht);
            stockBalanceInfo.icount = iCount;
            stockBalanceInfo.StockBalanceInfoList = stockBalanceInfoList;

            return stockBalanceInfo;
        }
        #endregion

        #region 中间层
        #region 库存下载
        /// <summary>
        /// 根据组织获取该组织的有库存记录的实时库存总数量
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unitId">组织标识</param>
        /// <returns></returns>
        public int GetStockBalanceCountByUnitId(LoggingSessionInfo loggingSessionInfo, string unitId)
        {
            //try
            //{
                Hashtable _ht = new Hashtable();
                _ht.Add("UnitId", unitId);
                return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("StockBalance.SelectByUnitIdCount", _ht);
            //}
            //catch (Exception ex) {
            //    throw (ex);
            //}
        }
        /// <summary>
        /// 根据组织获取该组织的有库存记录的实时库存
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unitId">组织标识</param>
        /// <param name="maxRowCount">最大数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public IList<StockBalanceInfo> GetStockBalanceByUnitId(LoggingSessionInfo loggingSessionInfo, string unitId, int maxRowCount, int startRowIndex)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("UnitId", unitId);
                _ht.Add("StartRow", startRowIndex);
                _ht.Add("EndRow", startRowIndex + maxRowCount);
                return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<StockBalanceInfo>("StockBalance.SelectByUnitId", _ht);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion
        #endregion

        #region 扣库存
        /// <summary>
        /// 扣库存
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_id">单据标识</param>
        /// <returns></returns>
        public bool SetStockBalance(LoggingSessionInfo loggingSessionInfo, string order_id)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", order_id);
                _ht.Add("UserId", loggingSessionInfo.CurrentUser.User_Id.ToString());
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("StockBalance.SetStockBalance", _ht);
                return true;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion
    }
}
