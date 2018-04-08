using System;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;
using Dapper;

namespace Xgx.SyncData.DbAccess.VipIntegral
{
    internal class VipIntegralQuery
    {
        internal int GetVipIntegralCountByVipId(string vipId)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                const string sql = @"select count(1) from vipintegral where vipid = @VipId";
                var result = conn.ExecuteScalar<int>(sql, new { VipId = vipId });
                return result;
            }
        }
    }
}
