using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_ItemSkuProp
{
    internal class T_ItemSkuPropCMD
    {
        internal void Create(T_ItemSkuPropEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.create_time = System.DateTime.Now;
                dbEntity.modify_time = System.DateTime.Now;
                conn.Open();
                conn.Insert(dbEntity);
            }
        }
        internal void Update(T_ItemSkuPropEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.modify_time = System.DateTime.Now;
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(T_ItemSkuPropEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                conn.Open();
                conn.Delete(dbEntity);
            }
        }

        internal void DeleteByItemId(string itemId)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                conn.Open();
                const string sql = @"delete from T_ItemSkuProp where item_id = @ItemId";
                conn.Execute(sql, new {ItemId = itemId});
            }
        }
    }
}
