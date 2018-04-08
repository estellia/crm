using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;
using System;

namespace Xgx.SyncData.DbAccess.t_customer_shop
{
    internal class t_customer_shopCMD
    {
        internal void Create(t_customer_shopEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxApInsert))
            {
                dbEntity.create_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dbEntity.modify_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //dbEntity.create_user_id = "ERP";
                conn.Open();
                conn.Insert(dbEntity);
            }
        }
        internal void Update(t_customer_shopEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxApInsert))
            {
                dbEntity.modify_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //dbEntity.modify_user_id = "ERP";
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(t_customer_shopEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxApInsert))
            {
                conn.Open();
                conn.Delete(dbEntity);
            }
        }
    }
}
