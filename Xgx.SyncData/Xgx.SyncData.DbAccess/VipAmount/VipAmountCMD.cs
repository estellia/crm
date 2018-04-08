using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.VipAmount
{
    internal class VipAmountCMD
    {
        internal void Create(VipAmountEntity dbEntity)
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
        internal void Update(VipAmountEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.LastUpdateTime = System.DateTime.Now;
                //dbEntity.LastUpdateBy = "ERP";

                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(VipAmountEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                conn.Open();
                conn.Delete(dbEntity);
            }
        }
        internal void UpdateVipAmount(string vipId, decimal amount)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                conn.Open();
                const string sql = @"update vipamount set totalamount = totalamount + @Amount where vipid = @VipId";
                conn.Execute(sql, new { Amount = amount, VipId = vipId });
            }
        }
    }
}
