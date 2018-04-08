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
    /// 商品类别
    /// </summary>
    public class ItemCategoryBsService : BaseInfouAuthService
    {
        /// <summary>
        /// 获取未打包的商品类别数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns></returns>
        public int GetItemCategoryNotPackagedCount(string Customer_Id, string User_Id, string Unit_Id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            int iCount = 0;
            ItemCategoryService itemCategoryService = new ItemCategoryService();
            iCount = itemCategoryService.GetItemCategoryNotPackagedCount(loggingSessionInfo);
            return iCount;
        }

        /// <summary>
        /// 需要打包的Item category集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns>未打包的商品集合</returns>
        public IList<ItemCategoryInfo> GetItemCategoryListPackaged(string Customer_Id, string User_Id, string Unit_Id)
        {
            IList<ItemCategoryInfo> itemCategoryInfoList = new List<ItemCategoryInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            ItemCategoryService itemCategoryService = new ItemCategoryService();
            itemCategoryInfoList = itemCategoryService.GetItemCategoryListPackaged(loggingSessionInfo);
            return itemCategoryInfoList;

        }
        /// <summary>
        /// 设置记录Item category打包批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="ItemCategoryInfoList">商品集合</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetItemCategoryBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id, IList<ItemCategoryInfo> ItemCategoryInfoList)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            ItemCategoryService itemCategoryService = new ItemCategoryService();
            bReturn = itemCategoryService.SetItemCategoryBatInfo(loggingSessionInfo, bat_id, ItemCategoryInfoList);
            return bReturn;
        }
        /// <summary>
        /// 更新Item表打包标识方法
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetItemCategoryIfFlagInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            ItemCategoryService itemCategoryService = new ItemCategoryService();
            bReturn = itemCategoryService.SetItemCategoryIfFlagInfo(loggingSessionInfo, bat_id);
            return bReturn;
        }
    }
}
