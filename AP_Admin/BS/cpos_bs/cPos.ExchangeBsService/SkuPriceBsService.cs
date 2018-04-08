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
    /// sku 价格
    /// </summary>
    public class SkuPriceBsService : BaseInfouAuthService
    {
        #region SKU零售价格
        /// <summary>
        /// 获取未打包的SKU零售价格数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns>未打包SKU价格数量</returns>
        public int GetSkuPriceNotPackagedCount(string Customer_Id, string User_Id, string Unit_Id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            int iCount = 0;
            SkuPriceService skuPriceService = new SkuPriceService();
            iCount = skuPriceService.GetSkuPriceNotPackagedCount(loggingSessionInfo, "");//75412168A16C4D2296B92CA0E596A188
            return iCount;
        }
        /// <summary>
        /// 需要打包的SKU零售价格集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="strartRow">开始行</param>
        /// <param name="rowsCount">每页行数</param>
        /// <returns>未打包的SKU零售价格集合</returns>
        public IList<SkuPriceInfo> GetSkuPriceListPackaged(string Customer_Id, string User_Id, string Unit_Id, int strartRow, int rowsCount)
        {
            IList<SkuPriceInfo> skuPriceInfoList = new List<SkuPriceInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            SkuPriceService skuPriceService = new SkuPriceService();
            skuPriceInfoList = skuPriceService.GetSkuPriceListPackaged(loggingSessionInfo, "", rowsCount, strartRow);//75412168A16C4D2296B92CA0E596A188
            return skuPriceInfoList;

        }
        /// <summary>
        /// 设置记录SKU零售价格打包批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="SkuPriceInfoList">商品集合</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetSkuPriceBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id, IList<SkuPriceInfo> SkuPriceInfoList)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            string strError = string.Empty;
            SkuPriceService skuPriceService = new SkuPriceService();
            bReturn = skuPriceService.SetSkuPriceBatInfo(loggingSessionInfo, bat_id, "75412168A16C4D2296B92CA0E596A188", SkuPriceInfoList, out strError);
            return bReturn;
        }
        /// <summary>
        /// 更新SKU零售价格打包标识方法
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetSkuPriceIfFlagInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            string strError = string.Empty;
            SkuPriceService skuPriceService = new SkuPriceService();
            bReturn = skuPriceService.SetSkuPriceIfFlagInfo(loggingSessionInfo, bat_id, out strError);
            return bReturn;
        }
        #endregion
    }
}
