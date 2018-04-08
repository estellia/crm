using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Service;
using cPos.Model;

namespace cPos.ExchangeBsService
{
    /// <summary>
    /// 调价单
    /// </summary>
    public class AdjustmentOrderBsService : BaseInfouAuthService
    {
        #region 下载调价单
        /// <summary>
        /// 获取未下载的调价单数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="no">序号</param>
        /// <returns></returns>
        public int GetAdjustmentOrderNotPackagedCount(string Customer_Id, string User_Id, string Unit_Id, int no)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            int iCount = 0;
            AdjustmentOrderService adjustmentOrderService = new AdjustmentOrderService();
            iCount = adjustmentOrderService.GetAdjustmentOrderNotPackagedCount(loggingSessionInfo, Unit_Id, no, "75412168A16C4D2296B92CA0E596A188");
            return iCount;
        }
        /// <summary>
        /// 获取要下载的调价单集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="no">序号</param>
        /// <param name="startRow">开始行</param>
        /// <param name="rowsCount">每页行数</param>
        /// <returns></returns>
        public IList<AdjustmentOrderInfo> GetAdjustmentOrderListPackaged(string Customer_Id, string User_Id, string Unit_Id, int no, int startRow, int rowsCount)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            IList<AdjustmentOrderInfo> adjustmentOrderList = new List<AdjustmentOrderInfo>();
            AdjustmentOrderService adjustmentOrderService = new AdjustmentOrderService();
            adjustmentOrderList = adjustmentOrderService.GetAdjustmentOrderListPackaged(loggingSessionInfo, Unit_Id, no, "75412168A16C4D2296B92CA0E596A188", rowsCount, startRow);
            return adjustmentOrderList;

        }

        #endregion
    }
}
