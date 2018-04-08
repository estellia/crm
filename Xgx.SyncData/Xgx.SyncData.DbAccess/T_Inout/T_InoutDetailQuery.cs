using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Inout
{
    internal class T_InoutDetailQuery
    {
        internal List<T_Inout_DetailEntity> GetOrderDetailListByOrderId(string orderid)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"SELECT * FROM [T_Inout_Detail] WHERE [order_id] = @order_id";
                var result = conn.Query<T_Inout_DetailEntity>(sql, new { order_id = orderid });
                if (result != null)
                {
                    return result.ToList();
                }
                return null;
            }
        }
    }
}
