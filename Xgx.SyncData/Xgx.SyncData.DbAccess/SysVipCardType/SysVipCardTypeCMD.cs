using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;
using System;

namespace Xgx.SyncData.DbAccess.SysVipCardType
{
    internal class SysVipCardTypeCMD
    {
        internal void Create(SysVipCardTypeEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.CreateTime = System.DateTime.Now;
                //dbEntity.CreateBy = "ERP";
                conn.Insert(dbEntity);
            }
        }
        internal void Update(SysVipCardTypeEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.LastUpdateTime = System.DateTime.Now;
                //dbEntity.LastUpdateBy = "ERP";
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(SysVipCardTypeEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                conn.Open();
                conn.Delete(dbEntity);
            }
        }
    }
}
