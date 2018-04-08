using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Service;
using cPos.Model;

namespace cPos.ExchangeBsService
{
    /// <summary>
    /// 监控日志
    /// </summary>
    public class MonitorLogAuthService:BaseInfouAuthService
    {
        #region 监控日志保存
        /// <summary>
        /// 保存监控日志集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="MonitorLogList">监控日志集合</param>
        /// <returns></returns>
        public bool SetMonitorLogInfo(string Customer_Id, string Unit_Id, string User_Id, MonitorLogInfo monitorLogInfo)
        {
            string strError = string.Empty;
            try
            {
                LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
                MonitorLogService monitorLogService = new MonitorLogService();
                
                monitorLogInfo.if_flag = 0;
                monitorLogInfo.customer_id = Customer_Id;

                IList<MonitorLogInfo> MonitorLogList = new List<MonitorLogInfo>();
                MonitorLogList.Add(monitorLogInfo);
                bool bReturn = monitorLogService.SetMonitorLogListInfo(loggingSessionInfo, MonitorLogList, out strError);
                if (bReturn)
                {
                    return bReturn;
                }
                else
                {
                    throw new Exception(string.Format(strError, strError));
                }

            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                throw (ex);
            }
        }
        #endregion

        #region 监控日志上传
        /// <summary>
        /// 获取未打包上传的监控日志数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns>未打包监控日志数量</returns>
        public int GetMonitorLogNotPackagedCount(string Customer_Id, string User_Id, string Unit_Id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            int iCount = 0;
            MonitorLogService mlService = new MonitorLogService();
            iCount = mlService.GetMonitorLogNotPackagedCount(loggingSessionInfo);
            return iCount;
        }
        /// <summary>
        /// 需要打包的MonitorLog集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="strartRow">开始行</param>
        /// <param name="rowsCount">每页行数</param>
        /// <returns>未打包的监控集合</returns>
        public IList<MonitorLogInfo> GetMonitorLogListPackaged(string Customer_Id, string User_Id, string Unit_Id, int strartRow, int rowsCount)
        {
            IList<MonitorLogInfo> mlInfoList = new List<MonitorLogInfo>();
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            MonitorLogService mlService = new MonitorLogService();
            mlInfoList = mlService.GetMonitorLogListPackaged(loggingSessionInfo, rowsCount, strartRow);
            return mlInfoList;

        }
        /// <summary>
        /// 设置记录MonitorLog打包批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="ItemInfoList">商品集合</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetMonitorLogBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id, IList<MonitorLogInfo> MonitorLogInfoList)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            string strError = string.Empty;
            MonitorLogService mlService = new MonitorLogService();
            bReturn = mlService.SetMonitorLogBatInfo(loggingSessionInfo, bat_id, MonitorLogInfoList, out strError);
            return bReturn;
        }
        /// <summary>
        /// 更新MonitorLog表打包标识方法
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetMonitorLogIfFlagInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            string strError = string.Empty;
            MonitorLogService mlService = new MonitorLogService();
            bReturn = mlService.SetMonitorLogIfFlagInfo(loggingSessionInfo, bat_id, out strError);
            return bReturn;
        }
        #endregion
    }
}
