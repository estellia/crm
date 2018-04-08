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
    ///  商品价格
    /// </summary>
    public class ItemPriceBsService: BaseInfouAuthService
    {
        #region 商品零售价格
        /// <summary>
        /// 获取未打包的商品零售价格数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns>未打包商品价格数量</returns>
        public int GetItemPriceNotPackagedCount(string Customer_Id, string User_Id, string Unit_Id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            int iCount = 0;
            ItemPriceService itemPriceService = new ItemPriceService();
            iCount = itemPriceService.GetItemPriceNotPackagedCount(loggingSessionInfo);
            return iCount;
        }
        /// <summary>
        /// 需要打包的商品零售价格集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="strartRow">开始行</param>
        /// <param name="rowsCount">每页行数</param>
        /// <returns>未打包的商品零售价格集合</returns>
        public IList<ItemPriceInfo> GetItemPriceListPackaged(string Customer_Id, string User_Id, string Unit_Id, int strartRow, int rowsCount)
        {
            IList<ItemPriceInfo> itemPriceInfoList = new List<ItemPriceInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            ItemPriceService itemPriceService = new ItemPriceService();
            itemPriceInfoList = itemPriceService.GetItemPriceListPackaged(loggingSessionInfo, rowsCount, strartRow);
            return itemPriceInfoList;

        }
        /// <summary>
        /// 设置记录商品零售价格打包批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="ItemPriceInfoList">商品集合</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetItemPriceBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id, IList<ItemPriceInfo> ItemPriceInfoList)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            string strError = string.Empty;
            ItemPriceService itemPriceService = new ItemPriceService();
            bReturn = itemPriceService.SetItemPriceBatInfo(loggingSessionInfo, bat_id, ItemPriceInfoList, out strError);
            return bReturn;
        }
        /// <summary>
        /// 更新商品零售价格打包标识方法
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetItemPriceIfFlagInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            string strError = string.Empty;
            ItemPriceService itemPriceService = new ItemPriceService();
            bReturn = itemPriceService.SetItemPriceIfFlagInfo(loggingSessionInfo, bat_id, out strError);
            return bReturn;
        }
        #endregion
    }
}
