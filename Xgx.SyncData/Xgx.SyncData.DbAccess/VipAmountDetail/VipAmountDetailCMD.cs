using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.VipAmountDetail
{
    internal class VipAmountDetailCMD
    {
        internal void Create(VipAmountDetailEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.CreateTime = System.DateTime.Now;
                dbEntity.LastUpdateTime = System.DateTime.Now;
                //dbEntity.CreateBy = "ERP";
                conn.Open();
                conn.Insert(dbEntity);
            }
        }
        internal void Update(VipAmountDetailEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.LastUpdateTime = System.DateTime.Now;
                //dbEntity.LastUpdateBy = "ERP";
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(VipAmountDetailEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                conn.Open();
                conn.Delete(dbEntity);
            }
        }
    }
}
