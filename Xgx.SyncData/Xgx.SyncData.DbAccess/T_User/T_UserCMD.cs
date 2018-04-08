using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_User
{
    internal class T_UserCMD
    {
        internal void Create(T_UserEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.create_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dbEntity.modify_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //dbEntity.create_user_id = "ERP";
                conn.Open();
                conn.Insert(dbEntity);
            }
        }
        internal void Update(T_UserEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.modify_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //dbEntity.modify_user_id = "ERP";
                conn.Open();
                conn.Update(dbEntity);
            }
        }
        internal void Delete(T_UserEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                conn.Open();
                conn.Delete(dbEntity);
            }
        }
    }
}
