using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Type
{
    internal class T_TypeQuery
    {
        internal string GetIdByCode(string typeCode, string customerId)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"select type_id from t_type where type_code = @TypeCode and customer_id = @CustomerId and status = 1";
                var result = conn.ExecuteScalar<string>(sql, new { TypeCode = typeCode, CustomerId = customerId });
                return result;
            }
        }
    }
}
