﻿using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Item
{
    internal class T_ItemCMD
    {
        internal void Create(T_ItemEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.create_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dbEntity.modify_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                conn.Open();
                conn.Insert(dbEntity);
            }
        }
        internal void Update(T_ItemEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.modify_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(T_ItemEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                conn.Open();
                conn.Delete(dbEntity);
            }
        }
    }
}
