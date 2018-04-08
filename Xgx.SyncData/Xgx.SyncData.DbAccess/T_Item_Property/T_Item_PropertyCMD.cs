using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;
using System;
using System.Collections.Generic;

namespace Xgx.SyncData.DbAccess.T_Item_Property
{
    internal class T_Item_PropertyCMD
    {
        internal void Create(T_Item_PropertyEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.create_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dbEntity.modify_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                conn.Open();
                conn.Insert(dbEntity);
            }
        }
        internal void Update(T_Item_PropertyEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.modify_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(T_Item_PropertyEntity dbEntity)
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
                const string sql =
                    @"delete from t_item_property where item_id = @ItemId and prop_id in (select prop_id from t_prop where prop_code = 'Qty')";
                conn.Execute(sql, new {ItemId = itemId});
            }
        }

        internal void DeleteBySkuId(string skuId)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                conn.Open();
                const string sql = @"delete from T_Item_Property where item_id = (select item_id from t_sku where sku_id = @SkuId) and (prop_id = (select prop_id from t_prop where prop_code = 'Qty') or prop_id = (select prop_id from t_prop where prop_code = 'SalesCount'))";
                conn.Execute(sql, new { SkuId = skuId });
            }
        }
        internal void CreateQty(string skuId)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                const string sql = @"select sum(isnull(sku_price,0)) from T_Sku_Price where sku_id in (select sku_id from t_sku where item_id = (select item_id from t_sku where sku_id = @SkuId)) and item_price_type_id = '77850286E3F24CD2AC84F80BC625859E'and status = 1";
                var qty = conn.QueryFirstOrDefault<int>(sql, new { SkuId = skuId });
                const string sql1 = @"select item_id from t_sku where sku_id = @SkuId";
                var itemId = conn.QueryFirstOrDefault<string>(sql1, new { SkuId = skuId });
                var dbEntity = new T_Item_PropertyEntity
                {
                    item_property_id = Guid.NewGuid().ToString("N"),
                    item_id = itemId,
                    prop_id = "34FF4445D39F49AD8174954D18BC1346",
                    prop_value = qty.ToString(),
                    status = "1",
                    create_time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                    modify_time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
                };
                Create(dbEntity);
            }
        }
        internal void CreateSalesCount(string skuId)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                const string sql = @"select sum(isnull(sku_price,0)) from T_Sku_Price where sku_id in (select sku_id from t_sku where item_id = (select item_id from t_sku where sku_id = @SkuId)) and item_price_type_id = '77850286E3F24CD2AC84F80BC625859f'and status = 1";
                var qty = conn.QueryFirstOrDefault<int>(sql, new { SkuId = skuId });
                const string sql1 = @"select item_id from t_sku where sku_id = @SkuId";
                var itemId = conn.QueryFirstOrDefault<string>(sql1, new { SkuId = skuId });
                var dbEntity = new T_Item_PropertyEntity
                {
                    item_property_id = Guid.NewGuid().ToString("N"),
                    item_id = itemId,
                    prop_id = "34FF4445D39F49AD8174954D18BC1347",
                    prop_value = qty.ToString(),
                    status = "1",
                    create_time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                    modify_time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
                };
                Create(dbEntity);
            }
        }
    }
}
