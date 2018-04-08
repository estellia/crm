using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Xgx.SyncData.DbEntity;
using Dapper.Contrib.Extensions;


namespace Xgx.SyncData.DbAccess.T_Inout
{
    internal class T_InoutQuery
    {
        internal T_InoutEntity GetOrderByOrderId(string orderid)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql =
                    @"SELECT* FROM [T_Inout] WHERE status <> '-1' AND [order_id] = @order_id";
                return conn.QueryFirst<T_InoutEntity>(sql, new { order_id = orderid });
                ;
            }
        }

        internal dynamic UpdateInoutByField(string updateFields, string orderid, string value)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql =
                    "UPDATE [T_Inout] SET " + updateFields + " = '" + value + "' WHERE [order_id] = '" +
                    orderid + "'";
                return conn.Execute(sql);
            }
        }

        internal dynamic UpdateInoutStauts(string orderid, string value, string desc)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql =
                    "UPDATE [T_Inout] SET [status] = '" + value + "',[Field7] = '" + value + "',[status_desc] = '" +
                    desc + "',[Field10] = '" + desc +
                    "' WHERE [order_id] = '" +
                    orderid + "'";
                return conn.Execute(sql);
            }
        }
    }
}
