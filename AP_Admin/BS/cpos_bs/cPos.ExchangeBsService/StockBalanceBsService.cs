using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Service;
using cPos.Model.User;
using cPos.Model;

namespace cPos.ExchangeBsService
{
    /// <summary>
    /// 库存中间类
    /// </summary>
    public class StockBalanceBsService : BaseInfouAuthService
    {
        /// <summary>
        /// 获取需要打包的库存数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns></returns>
        public int GetStockBalanceCount(string Customer_Id, string User_Id, string Unit_Id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            int iCount = 0;
            StockBalanceService stockBalanceService = new StockBalanceService();
            iCount = stockBalanceService.GetStockBalanceCountByUnitId(loggingSessionInfo, Unit_Id);
            return iCount;
        }

        /// <summary>
        /// 需要打包的库存集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="startRow">开始行</param>
        /// <param name="rowsCount">每页行数</param>
        /// <returns>未打包的库存集合</returns>
        public IList<StockBalanceInfo> GetStockBalanceList(string Customer_Id, string User_Id, string Unit_Id, int startRow, int rowsCount)
        {
            IList<StockBalanceInfo> stockBalanceInfoList = new List<StockBalanceInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            StockBalanceService stockBalanceService = new StockBalanceService();
            stockBalanceInfoList = stockBalanceService.GetStockBalanceByUnitId(loggingSessionInfo, Unit_Id, rowsCount,startRow);
            return stockBalanceInfoList;

        }
    }
}
