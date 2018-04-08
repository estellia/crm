using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Admin.Model;
using System.Collections;
using cPos.Admin.Component.SqlMappers;
using cPos.Admin.Component;
using cPos.Model.Advertise;
using cPos.Model;

namespace cPos.Admin.Service
{
    /// <summary>
    /// StockBalanceOrderService
    /// </summary>
    public class StockBalanceOrderService : BaseService
    {
        #region 根据组织门店获取库存
        ///// <summary>
        ///// 根据组织门店获取库存
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <param name="unit_id">组织标识</param>
        ///// <param name="warehouse_id">门店标识</param>
        ///// <param name="maxRowCount">最大数量</param>
        ///// <param name="startRowIndex">开始数量</param>
        ///// <returns></returns>
        //public StockBalanceInfo GetStockBalanceListByUnitIdWarehouseId(LoggingSessionInfo loggingSessionInfo
        //                                                                    , string unit_id
        //                                                                    , string warehouse_id
        //                                                                    , int maxRowCount
        //                                                                    , int startRowIndex
        //                                                                    )
        //{
        //    try
        //    {
        //        Hashtable _ht = new Hashtable();
        //        _ht.Add("StartRow", startRowIndex);
        //        _ht.Add("EndRow", startRowIndex + maxRowCount);
        //        _ht.Add("UnitId", unit_id);
        //        _ht.Add("WarehouseId", warehouse_id);
        //        _ht.Add("TypeValue", "1");

        //        StockBalanceInfo stockBalanceInfo = new StockBalanceInfo();

        //        int iCount = cSqlMapper.Instance().QueryForObject<int>("StockBalance.SearchCount", _ht);

        //        IList<StockBalanceInfo> stockBalanceInfoList = new List<StockBalanceInfo>();
        //        stockBalanceInfoList = cSqlMapper.Instance().QueryForList<StockBalanceInfo>("StockBalance.Search", _ht);
        //        stockBalanceInfo.icount = iCount;
        //        stockBalanceInfo.StockBalanceInfoList = stockBalanceInfoList;

        //        return stockBalanceInfo;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}
        #endregion

        #region 扣库存
        /// <summary>
        /// 扣库存
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_id">单据标识</param>
        /// <returns></returns>
        public void SetStockBalance(cPos.Model.LoggingSessionInfo loggingSessionInfo, string order_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("OrderId", order_id);
            _ht.Add("UserId", loggingSessionInfo.CurrentUser.User_Id);
            MSSqlMapper.Instance().Update("StockBalance.SetStockBalance", _ht);
        }
        #endregion

        #region 保存库存
        /// <summary>
        /// 保存库存
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="stockInfo">库存数据</param>
        /// <returns></returns>
        public void SaveStockBalance(cPos.Model.StockBalanceInfo stockInfo)
        {
            MSSqlMapper.Instance().Insert("StockBalance.InsertOrUpdate", stockInfo);
        }
        #endregion

        #region 获取库存ID
        /// <summary>
        /// 获取库存ID
        /// </summary>
        /// <param name="ht">unit_id, warehouse_id, sku_id</param>
        /// <returns></returns>
        public string GetStockBalanceIdBySkuId(Hashtable ht)
        {
            return MSSqlMapper.Instance().QueryForObject<string>("StockBalance.GetStockBalanceIdBySkuId", ht);
        }
        #endregion
    }
}
