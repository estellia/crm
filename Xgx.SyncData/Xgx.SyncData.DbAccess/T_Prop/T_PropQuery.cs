using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Prop
{
    internal class T_PropQuery
    {
        internal List<string> GetIdByParentId(string id)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                const string sql = @"select prop_id from t_prop where parent_prop_id = @ParentId";
                var result = conn.Query<string>(sql, new { ParentId = id });
                if (result != null)
                {
                    return result.ToList();
                }
                return null;
            }
        }

        internal string GetIdByCode(string code)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                const string sql = @"select prop_id from t_prop where prop_code = @Code";
                var result = conn.ExecuteScalar<string>(sql, new {Code = code});
                return result;
            }
        }
    }
}
