using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess
{
    internal class PaymentDetailCMD
    {
        internal void Create(T_Payment_detailEntity dbEntity)
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
        internal void Update(T_Payment_detailEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.LastUpdateTime = System.DateTime.Now;
                //dbEntity.LastUpdateBy = "ERP";
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(T_Payment_detailEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                conn.Open();
                conn.Delete(dbEntity);
            }
        }
    }
}
