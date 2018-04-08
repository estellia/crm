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
    /// 商品信息类
    /// </summary>
    public class ItemAuthService : BaseInfouAuthService
    {
        #region 商品信息
        /// <summary>
        /// 获取未打包的商品数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns>未打包商品数量</returns>
        public int GetItemNotPackagedCount(string Customer_Id,string User_Id,string Unit_Id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            int iCount = 0;
            ItemService itemService = new ItemService();
            iCount = itemService.GetItemNotPackagedCount(loggingSessionInfo);
            return iCount;
        }
        /// <summary>
        /// 需要打包的Item集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="strartRow">开始行</param>
        /// <param name="rowsCount">每页行数</param>
        /// <returns>未打包的商品集合</returns>
        public IList<ItemInfo> GetItemListPackaged(string Customer_Id, string User_Id, string Unit_Id, int strartRow, int rowsCount)
        {
            IList<ItemInfo> itemInfoList = new List<ItemInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            ItemService itemService = new ItemService();
            itemInfoList = itemService.GetItemListPackaged(loggingSessionInfo, rowsCount, strartRow);
            return itemInfoList;

        }
        /// <summary>
        /// 设置记录Item打包批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="ItemInfoList">商品集合</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetItemBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id, IList<ItemInfo> ItemInfoList)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            string strError = string.Empty;
            ItemService itemService = new ItemService();
            bReturn = itemService.SetItemBatInfo(loggingSessionInfo,bat_id, ItemInfoList,out strError);
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
        public bool SetItemIfFlagInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            string strError = string.Empty;
            ItemService itemService = new ItemService();
            bReturn = itemService.SetItemIfFlagInfo(loggingSessionInfo, bat_id, out strError);
            return bReturn;
        }
        #endregion

        #region 商品属性
        /// <summary>
        /// 获取商品属性集合信息
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="Item_Id">商品标识</param>
        /// <returns></returns>
        public IList<ItemPropInfo> GetItemPropInfoListPackaged(string Customer_Id, string User_Id, string Unit_Id, string Item_Id)
        {
            IList<ItemPropInfo> itemPropInfoList = new List<ItemPropInfo>();
            IList<ItemInfo> itemInfoList = new List<ItemInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            ItemPropService itemPropService = new ItemPropService();
            itemPropInfoList = itemPropService.GetItemPropListByItemId(loggingSessionInfo, Item_Id);
            return itemPropInfoList;
        }
        #endregion


        #region 属性集合（商品）
        /// <summary>
        /// 下载属性集合（商品）
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns></returns>
        public IList<PropByItemInfo> GetPropByItemPackaged(string Customer_Id, string User_Id, string Unit_Id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            ItemPropService itemPropService = new ItemPropService();
            IList<PropByItemInfo> PropByItemInfoList = new List<PropByItemInfo>();
            PropByItemInfoList = itemPropService.GetPropByItemList(loggingSessionInfo);
            return PropByItemInfoList;
        }
        #endregion
    }
}
