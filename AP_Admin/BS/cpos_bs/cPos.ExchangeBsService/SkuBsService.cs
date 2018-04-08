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
    /// sku 服务
    /// </summary>
    public class SkuBsService : BaseInfouAuthService
    {
        /// <summary>
        /// 获取未打包的Sku数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns>未打包Sku数量</returns>
        public int GetSkuNotPackagedCount(string Customer_Id, string User_Id, string Unit_Id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            int iCount = 0;
            SkuService skuService = new SkuService();
            iCount = skuService.GetSkuNotPackagedCount(loggingSessionInfo);
            return iCount;
        }
        /// <summary>
        /// 需要打包的Sku集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="strartRow">开始行</param>
        /// <param name="rowsCount">每页行数</param>
        /// <returns>未打包的Sku集合</returns>
        public IList<SkuInfo> GetSkuListPackaged(string Customer_Id, string User_Id, string Unit_Id, int strartRow, int rowsCount)
        {
            IList<SkuInfo> skuInfoList = new List<SkuInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            SkuService skuService = new SkuService();
            skuInfoList = skuService.GetSkuListPackaged(loggingSessionInfo, rowsCount, strartRow);
            return skuInfoList;

        }
        /// <summary>
        /// 设置记录Sku打包批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="SkuInfoList">商品集合</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetSkuBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id, IList<SkuInfo> SkuInfoList)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            SkuService skuService = new SkuService();
            bReturn = skuService.SetSkuBatInfo(loggingSessionInfo, bat_id, SkuInfoList);
            return bReturn;
        }
        /// <summary>
        /// 更新SKu表打包标识方法
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetSkuIfFlagInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            SkuService skuService = new SkuService();
            bReturn = skuService.SetSkuIfFlagInfo(loggingSessionInfo, bat_id);
            return bReturn;
        }
        /// <summary>
        /// 下载sku属性
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns></returns>
        public IList<SkuPropInfo> GetSkuPropListPackage(string Customer_Id, string User_Id, string Unit_Id)
        {
            IList<SkuPropInfo> skuPropInfoList = new List<SkuPropInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            SkuPropServer skuPropServer = new SkuPropServer();
            skuPropInfoList = skuPropServer.GetSkuPropList(loggingSessionInfo);
            return skuPropInfoList;
        }


    }
}
