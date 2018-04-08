using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using cPos.Dex.Common;
using cPos.Dex.ContractModel;

namespace cPos.ExchangeService
{
    public class DexLogService : IDexLogService
    {
        #region 获取日志接口
        /// <summary>
        /// 获取日志接口
        /// </summary>
        /// <param name="query_info">查询条件</param>
        /// <returns></returns>
        private IList<Log> GetLogs(LogQueryInfo query_info, long startRow, long rowsCount)
        {
            var service = new Dex.Services.LogDBService();
            return service.GetLogs(query_info, startRow, rowsCount);
        }

        /// <summary>
        /// 获取日志数量接口
        /// </summary>
        /// <param name="query_info">查询条件</param>
        /// <returns></returns>
        private int GetLogsCount(LogQueryInfo query_info)
        {
            var service = new Dex.Services.LogDBService();
            return service.GetLogsCount(query_info);
        }
        #endregion

        #region 获取客户日志接口
        /// <summary>
        /// 获取客户日志接口
        /// </summary>
        /// <param name="customer_id">客户ID(必填项)</param>
        /// <param name="query_info">查询条件(customer_id属性可以不设置)</param>
        /// <returns></returns>
        public IList<Log> GetLogsByCustomer(string customer_id, 
            LogQueryInfo query_info, long startRow, long rowsCount)
        {
            var service = new Dex.Services.LogDBService();
            if (customer_id == null || customer_id.Trim().Length > 32)
                throw new Exception("客户ID为空或超出长度限制");
            query_info.customer_id = customer_id;
            return GetLogs(query_info, startRow, rowsCount);
        }

        /// <summary>
        /// 获取客户日志数量接口
        /// </summary>
        /// <param name="customer_id">客户ID(必填项)</param>
        /// <param name="query_info">查询条件(customer_id属性可以不设置)</param>
        /// <returns></returns>
        public int GetLogsCountByCustomer(string customer_id, LogQueryInfo query_info)
        {
            var service = new Dex.Services.LogDBService();
            if (customer_id == null || customer_id.Trim().Length > 32)
                throw new Exception("客户ID为空或超出长度限制");
            query_info.customer_id = customer_id;
            return GetLogsCount(query_info);
        }
        #endregion
    }
}
