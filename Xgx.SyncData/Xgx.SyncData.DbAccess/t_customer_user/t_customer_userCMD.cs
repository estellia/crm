using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;
using System;

namespace Xgx.SyncData.DbAccess.t_customer_user
{
    internal class t_customer_userCMD
    {
        internal void Create(t_customer_userEntity dbEntity)
        {
           
            using (var conn = new SqlConnection(ConnectionString.XgxApInsert))
            {
                dbEntity.sys_modify_stamp = System.DateTime.Now;
                
                conn.Open();
                conn.Insert(dbEntity);
            }
        }
        internal void Update(t_customer_userEntity dbEntity)
        {
           
            using (var conn = new SqlConnection(ConnectionString.XgxApInsert))
            {
                dbEntity.sys_modify_stamp = DateTime.Now;
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(t_customer_userEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxApInsert))
            {
                conn.Open();
                conn.Delete(dbEntity);
            }
        }
    }
}
