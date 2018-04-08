using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Admin.Model;
using System.Collections;
using cPos.Admin.Component.SqlMappers;
using cPos.Admin.Component;

namespace cPos.Admin.Service
{
    /// <summary>
    /// Monitor服务
    /// </summary>
    public class MonitorService : BaseService
    {
        #region Monitor Log 保存
        /// <summary>
        /// Monitor Log 保存
        /// </summary>
        /// <param name="logs">MonitorLogInfo model</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <returns>Hashtable: 
        ///  status(成功：true, 失败：false)
        ///  error(错误描述)
        /// </returns>
        public Hashtable SaveMonitorLogList(bool IsTrans, IList<MonitorLogInfo> logs)
        {
            Hashtable ht = new Hashtable();
            ht["status"] = false;
            try
            {
                if (IsTrans) MSSqlMapper.Instance().BeginTransaction();
                foreach (var log in logs)
                {
                    if (!CheckExistMonitorLog(log))
                    {
                        MSSqlMapper.Instance().Insert("Monitor.InsertMonitorLog", log);
                    }
                }

                if (IsTrans) MSSqlMapper.Instance().CommitTransaction();
                ht["status"] = true;
            }
            catch (Exception ex)
            {
                if (IsTrans) MSSqlMapper.Instance().RollBackTransaction();
                throw (ex);
            }
            return ht;
        }

        /// <summary>
        /// 检查MonitorLog是否已存在
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool CheckExistMonitorLog(MonitorLogInfo log)
        {
            int count = MSSqlMapper.Instance().QueryForObject<int>("Monitor.CheckExistMonitorLog", log);
            return count > 0 ? true : false;
        }
        #endregion

        #region Monitor Log 查询
        /// <summary>
        /// Monitor Log 查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_no">订单号</param>
        /// <param name="unit_code">门店</param>
        /// <param name="item_name">商品</param>
        /// <param name="order_date_begin">开始日期</param>
        /// <param name="order_date_end">结束日期</param>
        /// <param name="maxRowCount">每页数量</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns></returns>
        public MonitorLogInfo SearchMonitorLogList(cPos.Model.LoggingSessionInfo loggingSessionInfo
                                            , string customer_code
                                            , string unit_code
                                            , string date_begin
                                            , string date_end
                                            , int maxRowCount
                                            , int startRowIndex
                                            )
        {
            Hashtable ht = new Hashtable();
            ht["customer_code"] = customer_code;
            ht["unit_code"] = unit_code;
            ht["date_begin"] = date_begin;
            ht["date_end"] = date_end;
            ht["StartRow"] = startRowIndex;
            ht["EndRow"] = startRowIndex + maxRowCount;

            MonitorLogInfo obj = new MonitorLogInfo();
            IList<MonitorLogInfo> list = new List<MonitorLogInfo>();
            list = MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<MonitorLogInfo>(
                "Monitor.SearchMonitorLogList", ht);
            if (list != null && list.Count > 0)
                obj.icount = list[0].icount;
            obj.List = list;
            return obj;
        }
        #endregion
    }
}
