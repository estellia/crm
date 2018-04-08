using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace Xgx.SyncData.DbAccess.T_User_Role
{
    internal class T_User_RoleQuery
    {
        internal List<string> GetIdByUserId(string userId)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"select user_role_id from t_user_role where user_id = @UserId";
                var result = conn.Query<string>(sql, new { UserId = userId });
                if (result != null)
                {
                    return result.ToList();
                }
                return null;
            }
        }
    }
}
