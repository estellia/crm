using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Service;
using cPos.Model.User;
using cPos.Model;
using cPos.Model.Pos;

namespace cPos.ExchangeBsService
{
    /// <summary>
    /// 组织类
    /// </summary>
    public class UnitBsService : BaseInfouAuthService
    {
        /// <summary>
        /// 获取未打包的Unit数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns>未打包Unit数量</returns>
        public int GetUnitNotPackagedCount(string Customer_Id, string User_Id, string Unit_Id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            int iCount = 0;
            UnitService unitService = new UnitService();
            iCount = unitService.GetUnitNotPackagedCount(loggingSessionInfo);
            return iCount;
        }
        /// <summary>
        /// 需要打包的Unit集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="strartRow">开始行</param>
        /// <param name="rowsCount">每页行数</param>
        /// <returns>未打包的Unit集合</returns>
        public IList<UnitInfo> GetUnitListPackaged(string Customer_Id, string User_Id, string Unit_Id, int strartRow, int rowsCount)
        {
            IList<UnitInfo> unitInfoList = new List<UnitInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            UnitService unitService = new UnitService();
            unitInfoList = unitService.GetUnitListPackaged(loggingSessionInfo, rowsCount, strartRow);
            return unitInfoList;

        }
        /// <summary>
        /// 设置记录Unit打包批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="UnitInfoList">商品集合</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetUnitBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id, IList<UnitInfo> UnitInfoList)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            UnitService unitService = new UnitService();
            bReturn = unitService.SetUnitBatInfo(loggingSessionInfo, bat_id, UnitInfoList);
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
        public bool SetUnitIfFlagInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            UnitService unitService = new UnitService();
            bReturn = unitService.SetUnitIfFlagInfo(loggingSessionInfo, bat_id);
            return bReturn;
        }

        #region 组织属性
        /// <summary>
        /// 获取组织属性集合信息
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="unitId">需要获取的门店标识</param>
        /// <returns></returns>
        public IList<UnitPropertyInfo> GetUnitPropInfoListPackaged(string Customer_Id, string User_Id, string Unit_Id, string unitId)
        {
            IList<UnitPropertyInfo> unitPropInfoList = new List<UnitPropertyInfo>();
            IList<ItemInfo> itemInfoList = new List<ItemInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            PropertyUnitService unitPropService = new PropertyUnitService();
            unitPropInfoList = unitPropService.GetUnitPropertyListByUnit(loggingSessionInfo, unitId);
            return unitPropInfoList;
        }
        #endregion

        #region 仓库与组织关系
        /// <summary>
        /// 根据组织标识获取仓库
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="unitID">需要获取的组织标识</param>
        /// <returns></returns>
        public IList<WarehouseInfo> GetWarehouseByUnitIDPackaged(string Customer_Id, string User_Id, string Unit_Id, string unitID)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            IList<WarehouseInfo> warehouseInfoList = new List<WarehouseInfo>();
            PosService posServer = new PosService();
            warehouseInfoList = posServer.GetWarehouseByUnitID(loggingSessionInfo, unitID);
            return warehouseInfoList;
        }
        #endregion
    }
}
