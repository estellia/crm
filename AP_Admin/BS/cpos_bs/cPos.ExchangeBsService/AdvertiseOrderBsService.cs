using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Service;
using cPos.Model;
using cPos.Model.Advertise;

namespace cPos.ExchangeBsService
{
    public class AdvertiseOrderBsService : BaseInfouAuthService
    {
        #region 广告播放订单信息
        /// <summary>
        /// 获取未打包的广告播放订单数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns>未打包广告播放订单数量</returns>
        public int GetAdvertiseOrderNotPackagedCount(string Customer_Id, string User_Id, string Unit_Id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            int iCount = 0;
            AdvertiseOrderService advertiseOrdereService = new AdvertiseOrderService();
            iCount = advertiseOrdereService.GetAdvertiseOrderNotPackagedCount(loggingSessionInfo,Unit_Id);
            return iCount;
        }
        /// <summary>
        /// 需要打包的广告播放订单集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="strartRow">开始行</param>
        /// <param name="rowsCount">每页行数</param>
        /// <returns>未打包的广告播放订单集合</returns>
        public IList<AdvertiseOrderInfo> GetAdvertiseOrderListPackaged(string Customer_Id, string User_Id, string Unit_Id, int strartRow, int rowsCount)
        {
            IList<AdvertiseOrderInfo> advertiseOrderInfoList = new List<AdvertiseOrderInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            AdvertiseOrderService advertiseOrdereService = new AdvertiseOrderService();
            advertiseOrderInfoList = advertiseOrdereService.GetAdvertiseOrderListPackaged(loggingSessionInfo, rowsCount, strartRow, Unit_Id);
            return advertiseOrderInfoList;

        }
        /// <summary>
        /// 设置记录AdvertiseOrder打包批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="ItemInfoList">广告播放订单集合</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetAdvertiseOrderBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id, IList<AdvertiseOrderUnitInfo> AdvertiseOrderUnitInfoList)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            string strError = string.Empty;
            AdvertiseOrderService advertiseOrdereService = new AdvertiseOrderService();
            bReturn = advertiseOrdereService.SetAdvertiseOrderBatInfo(loggingSessionInfo, bat_id, AdvertiseOrderUnitInfoList, out strError);
            return bReturn;
        }
        /// <summary>
        /// 更新AdvertiseOrder表打包标识方法
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetAdvertiseOrderIfFlagInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            string strError = string.Empty;
            AdvertiseOrderService advertiseOrdereService = new AdvertiseOrderService();
            bReturn = advertiseOrdereService.SetAdvertiseOrderIfFlagInfo(loggingSessionInfo, bat_id, out strError);
            return bReturn;
        }

        /// <summary>
        /// 根据订单标识下载广告信息集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">门店标识</param>
        /// <param name="order_id">订单标识</param>
        /// <returns></returns>
        public IList<AdvertiseInfo> GetAdvertiseListPackaged(string Customer_Id, string User_Id, string Unit_Id,string order_id)
        {
            IList<AdvertiseInfo> advertiseInfoList = new List<AdvertiseInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            AdvertiseService advertiseService = new AdvertiseService();
            advertiseInfoList = advertiseService.GetAdvertiseInfoListPackaged(loggingSessionInfo,order_id);
            return advertiseInfoList;
        }

        #endregion

    }
}
