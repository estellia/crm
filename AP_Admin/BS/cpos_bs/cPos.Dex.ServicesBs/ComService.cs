using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Model;
using cPos.Dex.Components.SqlMappers;
using cPos.Dex.Common;
using cPos.Dex.ContractModel;
using cPos.Dex.Services;

namespace cPos.Dex.ServicesBs
{
    public class ComService
    {
        #region CheckMonitorLogs
        /// <summary>
        /// 检查MonitorLog
        /// </summary>
        public Hashtable CheckMonitorLog(MonitorLogContract order)
        {
            Hashtable htError = new Hashtable();
            if (order == null)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "信息不能为空", true);
                return htError;
            }
            if (order.user_id == null || order.user_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "用户标识不能为空", true);
                return htError;
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }

        /// <summary>
        /// 检查MonitorLog集合
        /// </summary>
        public Hashtable CheckMonitorLogs(IList<MonitorLogContract> orders)
        {
            Hashtable htError = new Hashtable();
            if (orders == null || orders.Count == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "集合不能为空", true);
                return htError;
            }
            foreach (var order in orders)
            {
                htError = CheckMonitorLog(order);
                if (!Convert.ToBoolean(htError["status"])) return htError;
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }
        #endregion

        #region SaveMonitorLog
        /// <summary>
        /// 保存MonitorLog
        /// </summary>
        public void SaveMonitorLog(MonitorLogContract order,
            string customerId, string unitId, string userId)
        {
            if (order.monitor_log_id == null || order.monitor_log_id.Trim().Length == 0)
            {
                order.monitor_log_id = Utils.NewGuid();
            }

            if (order.upload_time == null || order.upload_time.Trim().Length == 0)
            {
                order.upload_time = Utils.GetNow();
            }

            order.customer_id = customerId;
            order.create_user_id = userId;
            order.create_time = Utils.GetNow();

            var orderInfo = ToMonitorLogModel(order);

            // Save
            var logService = new ExchangeBsService.MonitorLogAuthService();
            logService.SetMonitorLogInfo(customerId, unitId, userId, orderInfo);
        }
        #endregion

        #region ToMonitorLogModel
        public cPos.Model.MonitorLogInfo ToMonitorLogModel(MonitorLogContract model)
        {
            var obj = new cPos.Model.MonitorLogInfo();
            obj.monitor_log_id = model.monitor_log_id;
            obj.customer_id = model.customer_id;
            obj.unit_id = model.unit_id;
            obj.pos_id = model.pos_id;
            obj.upload_time = model.upload_time;
            obj.remark = model.remark;
            obj.create_time = model.create_time;
            obj.create_user_id = model.create_user_id;
            return obj;
        }
        #endregion

        #region ToMonitorLogContract
        public MonitorLogContract ToMonitorLogContract(cPos.Model.MonitorLogInfo model)
        {
            var obj = new MonitorLogContract();
            obj.monitor_log_id = model.monitor_log_id;
            obj.customer_id = model.customer_id;
            obj.unit_id = model.unit_id;
            obj.pos_id = model.pos_id;
            obj.upload_time = model.upload_time;
            obj.remark = model.remark;
            obj.create_time = model.create_time;
            obj.create_user_id = model.create_user_id;
            return obj;
        }
        #endregion
    }
}
