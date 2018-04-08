using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Unit_Relation
{
    internal class T_Unit_RelationCMD
    {
        internal void Create(T_Unit_RelationEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.create_time = System.DateTime.Now;
                dbEntity.modify_time = System.DateTime.Now;

                conn.Open();
                conn.Insert(dbEntity);
            }
        }
        internal void Update(T_Unit_RelationEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.modify_time = System.DateTime.Now;
                
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(T_Unit_RelationEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                conn.Open();
                conn.Delete(dbEntity);
            }
        }
    }
}
