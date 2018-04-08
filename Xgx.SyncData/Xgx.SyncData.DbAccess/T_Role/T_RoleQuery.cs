using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace Xgx.SyncData.DbAccess.T_Role
{
    internal class T_RoleQuery
    {
        internal string GetIdByCode(string roleCode, string customerId)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"select role_id from t_role where role_code = @RoleCode and customer_id = @CustomerId and status = 1";
                var result = conn.ExecuteScalar<string>(sql, new { RoleCode = roleCode, CustomerId = customerId });
                return result;
            }
        }
    }
}
