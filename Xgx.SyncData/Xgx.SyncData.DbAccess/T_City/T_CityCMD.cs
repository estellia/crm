using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_City
{
    internal class T_CityCMD
    {
        internal void Create(T_CityEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                
                conn.Open();
                conn.Insert(dbEntity);
            }
        }
        internal void Update(T_CityEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(T_CityEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {

                conn.Open();
                conn.Delete(dbEntity);
            }
        }
    }
}
