using System;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;
using Dapper;

namespace Xgx.SyncData.DbAccess.VipCardVipMapping
{
    internal class VipCardVipMappingQuery
    {
        internal string GetVipCardCodeByVipId(string vipId)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                const string sql = @"select a.vipcardcode from vipcard a left join vipcardvipmapping b on a.vipcardid = b.vipcardid where b.vipid = @VipId";
                var result = conn.ExecuteScalar<string>(sql, new { VipId = vipId });
                return result;
            }
        }
    }
}
