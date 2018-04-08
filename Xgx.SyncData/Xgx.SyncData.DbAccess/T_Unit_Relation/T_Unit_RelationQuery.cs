using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Unit_Relation
{
    internal class T_Unit_RelationQuery
    {
        internal T_Unit_RelationEntity GetEntityByDstId(string dstUnitId)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"select * from t_unit_relation where dst_unit_id = @DstUnitId";
                var result = conn.QueryFirstOrDefault<T_Unit_RelationEntity>(sql, new { DstUnitId = dstUnitId });
                return result;
            }
        }
    }
}
