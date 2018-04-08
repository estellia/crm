using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_User
{
    internal class T_UserQuery
    {
        internal dynamic GetUserById(string id)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"select top 1 a.user_code, a.user_name, a.user_password, a.user_telephone, a.create_time, a.modify_time, b.unit_id from t_user a
left join T_User_Role b on a.user_id = b.user_id
 where a.user_id = @UserId";
                return conn.QueryFirstOrDefault(sql, new { UserId = id});
            }
        }

        internal List<string> GetUserRoleCode(string id)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"select b.role_code from T_User_Role a
left join T_Role b on a.role_id = b.role_id
 where a.user_id = @UserId";
                var result = conn.Query<string>(sql, new {UserId = id});
                if (result != null)
                {
                    return result.ToList();
                }
                return null;
            }
        }

        internal string GetUserPwd(string id)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"select user_password from t_user where user_id = @UserId";
                var result = conn.ExecuteScalar<string>(sql, new {UserId = id});
                return result;
            }
        }
    }
}
