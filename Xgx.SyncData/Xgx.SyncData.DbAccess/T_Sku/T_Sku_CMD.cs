using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Sku
{
    internal class T_SkuCMD
    {
        internal void Create(T_SkuEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.create_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dbEntity.modify_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //dbEntity.create_user_id = "ERP";
                conn.Open();
                conn.Insert(dbEntity);
            }
        }
        internal void Update(T_SkuEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.modify_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //dbEntity.modify_user_id = "ERP";
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(T_SkuEntity dbEntity)
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
                var sql = @"delete from t_sku where item_id = @ItemId";
                conn.Execute(sql, new {ItemId = itemId});
            }
        }
    }
}
