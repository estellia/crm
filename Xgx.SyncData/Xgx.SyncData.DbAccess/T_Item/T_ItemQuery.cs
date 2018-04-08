using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess
{
    internal class T_ItemQuery
    {
        internal T_ItemEntity GetItemBySkuId(string skuid)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                const string sql = @"SELECT * FROM T_Item WHERE item_id = (SELECT item_id FROM T_Sku WHERE sku_id = @Skuid)";
                return conn.QueryFirst<T_ItemEntity>(sql, new { Skuid = skuid });
            }
        }      
    }
}
