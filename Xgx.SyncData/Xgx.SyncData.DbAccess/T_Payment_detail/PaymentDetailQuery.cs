using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess
{
    internal class PaymentDetailQuery
    {
        internal dynamic GetPaymentById(string id)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"SELECT * FROM T_Payment_detail WHERE Payment_Id = @Paymentid";
                return conn.QueryFirstOrDefault(sql, new { Paymentid = id });
            }
        }

    }
}
