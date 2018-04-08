using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using cPos.Dex.ContractModel;

namespace cPos.ExchangeService
{
    public interface IDexLogService
    {
        /// <summary>
        /// 获取客户日志接口
        /// </summary>
        /// <param name="customer_id">客户ID(必填项)</param>
        /// <param name="query_info">查询条件(customer_id属性可以不设置)</param>
        IList<Log> GetLogsByCustomer(string customer_id, 
            LogQueryInfo query_info, long startRow, long rowsCount);

        /// <summary>
        /// 获取客户日志数量接口
        /// </summary>
        /// <param name="customer_id">客户ID(必填项)</param>
        /// <param name="query_info">查询条件(customer_id属性可以不设置)</param>
        int GetLogsCountByCustomer(string customer_id, LogQueryInfo query_info);

    }
}
