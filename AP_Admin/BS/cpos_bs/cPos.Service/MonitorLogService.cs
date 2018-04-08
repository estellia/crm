using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using cPos.Service;
using cPos.Components.SqlMappers;
using System.Collections;

namespace cPos.Service
{
    public class MonitorLogService:BaseService
    {
        #region 监控保存
        public bool SetMonitorLogListInfo(LoggingSessionInfo loggingSessionInfo, IList<MonitorLogInfo> monitorLogListInfo, out string strError)
        {
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).BeginTransaction();
            try
            {
                string sError = string.Empty;
                bool bReturn = true;
                foreach (MonitorLogInfo monitorlogInfo in monitorLogListInfo)
                {
                    bReturn = SetMonitorLogInfo(loggingSessionInfo, monitorlogInfo);
                    if (!bReturn) { break; }
                }
                strError = sError;
                if (bReturn)
                {
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).CommitTransaction();
                    return true;
                }
                else
                {
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).RollBackTransaction();
                    return false;
                }

            }
            catch (Exception ex)
            {
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).RollBackTransaction();
                throw (ex);
            }
        }
        /// <summary>
        /// 处理单个监控记录
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="monitorlogInfo"></param>
        /// <returns></returns>
        private bool SetMonitorLogInfo(LoggingSessionInfo loggingSessionInfo, MonitorLogInfo monitorlogInfo)
        {
            try
            {
                if (monitorlogInfo != null)
                {
                    if (monitorlogInfo.monitor_log_id == null || monitorlogInfo.monitor_log_id.Equals(""))
                    {
                        monitorlogInfo.monitor_log_id = NewGuid();
                    }
                    monitorlogInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                    if (monitorlogInfo.create_user_id == null || monitorlogInfo.create_user_id.Equals(""))
                    {
                        monitorlogInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
                        monitorlogInfo.create_time = GetCurrentDateTime();
                    }

                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("MonitorLog.InsertOrUpdate", monitorlogInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region 上传到管理平台
        /// <summary>
        /// 获取未打包的监控日志数量
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <returns></returns>
        public int GetMonitorLogNotPackagedCount(LoggingSessionInfo loggingSessionInfo)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
            _ht.Add("UnitId",loggingSessionInfo.CurrentUserRole.UnitId.ToString());
            
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("MonitorLog.SelectUnDownloadCount", _ht);
        }

        /// <summary>
        /// 需要打包的MonitorLog信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="maxRowCount">当前页数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public IList<MonitorLogInfo> GetMonitorLogListPackaged(LoggingSessionInfo loggingSessionInfo, int maxRowCount, int startRowIndex)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
            _ht.Add("UnitId", loggingSessionInfo.CurrentUserRole.UnitId.ToString());
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<MonitorLogInfo>("MonitorLog.SelectUnDownload", _ht);
        }

        /// <summary>
        /// 设置打包批次号
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="MonitorLogList">监控集合</param>
        /// <param name="strError">错误信息返回</param>
        /// <returns></returns>
        public bool SetMonitorLogBatInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, IList<MonitorLogInfo> MonitorLogList, out string strError)
        {
            MonitorLogInfo monitorlogInfo = new MonitorLogInfo();
            
            monitorlogInfo.bat_id = bat_id;
            monitorlogInfo.monitorLogInfoList = MonitorLogList;
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("MonitorLog.UpdateUnDownloadBatId", monitorlogInfo);
            strError = "Success";
            return true;
        }
        /// <summary>
        /// 更新MonitorLog表打包标识方法
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次标识</param>
        /// <param name="strError">错误信息返回</param>
        /// <returns></returns>
        public bool SetMonitorLogIfFlagInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, out string strError)
        {
            MonitorLogInfo monitorLogInfo = new MonitorLogInfo();
            monitorLogInfo.bat_id = bat_id;
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("MonitorLog.UpdateUnDownloadIfFlag", monitorLogInfo);
            strError = "Success";
            return true;
        }

        #endregion

        #region 上传到管理平台(通过webservice)
        /// <summary>
        /// 获取未打包的监控日志数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns></returns>
        public int GetMonitorLogNotPackagedCountWeb(string Customer_Id, string Unit_Id)
        {
            //获取连接数据库信息
            LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(Customer_Id);
            //获取参数
            Hashtable _ht = new Hashtable();
            _ht.Add("CustomerId", Customer_Id);
            _ht.Add("UnitId", Unit_Id);
            //连接数据库
            return cSqlMapper.Instance(loggingManager).QueryForObject<int>("MonitorLog.SelectUnDownloadCount", _ht);
        }
        /// <summary>
        /// 打包
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="maxRowCount">最大行数</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <param name="bat_id">批次号</param>
        /// <returns></returns>
        public IList<MonitorLogInfo> GetMonitorLogListPackagedWeb(string Customer_Id, string Unit_Id, int maxRowCount, int startRowIndex,string bat_id)
        {
            //获取连接数据库信息
            LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(Customer_Id);

            //获取要传输的监控信息集合
            Hashtable _ht = new Hashtable();
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            _ht.Add("CustomerId", Customer_Id);
            _ht.Add("UnitId", Unit_Id);
            IList<MonitorLogInfo> MonitorlogInfoList = new List<MonitorLogInfo>();
            MonitorlogInfoList = cSqlMapper.Instance(loggingManager).QueryForList<MonitorLogInfo>("MonitorLog.SelectUnDownload", _ht);
            //修改获取的监控信息批次号
            MonitorLogInfo monitorlogInfo = new MonitorLogInfo();
            monitorlogInfo.bat_id = bat_id;
            monitorlogInfo.monitorLogInfoList = MonitorlogInfoList;
            cSqlMapper.Instance(loggingManager).Update("MonitorLog.UpdateUnDownloadBatId", monitorlogInfo);
            return MonitorlogInfoList;
        }

        /// <summary>
        /// 更新MonitorLog表打包标识方法
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="bat_id">批次标识</param>
        /// <param name="strError">错误信息返回</param>
        /// <returns></returns>
        public bool SetMonitorLogIfFlagInfoWeb(string Customer_Id, string bat_id, out string strError)
        {
            //获取连接数据库信息
            LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(Customer_Id);
            MonitorLogInfo monitorLogInfo = new MonitorLogInfo();
            monitorLogInfo.bat_id = bat_id;
            cSqlMapper.Instance(loggingManager).Update("MonitorLog.UpdateUnDownloadIfFlag", monitorLogInfo);
            strError = "Success";
            return true;
        }

        //// <summary>
        /// 转换IList<T>为List<T>
        /// </summary>
        /// <typeparam name="T">指定的集合中泛型的类型</typeparam>
        /// <param name="gbList">需要转换的IList</param>
        /// <returns></returns>
        public static List<T> ConvertIListToList<T>(IList gbList) where T : class
        {
            if (gbList != null && gbList.Count > 1)
            {
                List<T> list = new List<T>();
                for (int i = 0; i < gbList.Count; i++)
                {
                    T temp = gbList[i] as T;
                    if (temp != null)
                        list.Add(temp);
                }
                return list;
            }
            return null;
        }
        #endregion
    }
}
