using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Sku_Price
{
    internal class T_Sku_PriceCMD
    {
        internal void Create(T_Sku_PriceEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.create_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dbEntity.create_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //dbEntity.create_user_id = "ERP";
                conn.Open();
                conn.Insert(dbEntity);
            }
        }
        internal void Update(T_Sku_PriceEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.create_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //dbEntity.modify_user_id = "ERP";
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(T_Sku_PriceEntity dbEntity)
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
                const string sql = @"delete from T_Sku_Price where sku_id in (select sku_id from t_sku where item_id = @ItemId)";
                conn.Execute(sql, new {ItemId = itemId});
            }
        }

        internal void DeleteBySkuIdAndPriceTypeId(string skuId, string priceTypeId)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                conn.Open();
                const string sql = @"delete from T_Sku_Price where sku_id = @SkuId and item_price_type_id = @PriceTypeId";
                conn.Execute(sql, new {SkuId = skuId, PriceTypeId = priceTypeId});
            }
        }
    }
}
