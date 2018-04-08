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
    /// 图片
    /// </summary>
    public class ObjectImagesBsService : BaseInfouAuthService
    {
        /// <summary>
        /// 获取未打包的图片数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns></returns>
        public int GetObjectImagesNotPackagedCount(string Customer_Id, string User_Id, string Unit_Id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            int iCount = 0;
            ObjectImagesService itemService = new ObjectImagesService();
            iCount = itemService.GetItemNotPackagedCount(loggingSessionInfo);
            return iCount;
        }

        /// <summary>
        /// 需要打包的图片集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="strartRow">开始行</param>
        /// <param name="rowsCount">每页行数</param>
        /// <returns>未打包的商品集合</returns>
        public IList<ObjectImagesInfo> GetItemListPackaged(string Customer_Id, string User_Id, string Unit_Id, int strartRow, int rowsCount)
        {
            IList<ObjectImagesInfo> itemInfoList = new List<ObjectImagesInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            ObjectImagesService itemService = new ObjectImagesService();
            itemInfoList = itemService.GetItemListPackaged(loggingSessionInfo, rowsCount, strartRow);
            return itemInfoList;

        }
        /// <summary>
        /// 设置记录图片打包批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="ItemInfoList">商品集合</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetObjectImagesBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id, IList<ObjectImagesInfo> ItemInfoList)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            string strError = string.Empty;
            ObjectImagesService itemService = new ObjectImagesService();
            bReturn = itemService.SetItemBatInfo(loggingSessionInfo,bat_id, ItemInfoList,out strError);
            return bReturn;
        }
        /// <summary>
        /// 更新图片表打包标识方法
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetObjectImagesIfFlagInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            string strError = string.Empty;
            ObjectImagesService itemService = new ObjectImagesService();
            bReturn = itemService.SetItemIfFlagInfo(loggingSessionInfo, bat_id, out strError);
            return bReturn;
        }
      
    
     
    }

}
