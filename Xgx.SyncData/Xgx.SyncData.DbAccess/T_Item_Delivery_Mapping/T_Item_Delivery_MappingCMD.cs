using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Item_Delivery_Mapping
{
    internal class T_Item_Delivery_MappingCMD
    {
        internal void Create(T_Item_Delivery_MappingEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.Create_Time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dbEntity.Modify_Time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                conn.Open();
                conn.Insert(dbEntity);
            }
        }
        internal void Update(T_Item_Delivery_MappingEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {

                dbEntity.Modify_Time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(T_Item_Delivery_MappingEntity dbEntity)
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
                const string sql = @"delete from T_Item_Delivery_Mapping where item_id = @ItemId";
                conn.Execute(sql, new {ItemId = itemId});
            }
        }
    }
}
