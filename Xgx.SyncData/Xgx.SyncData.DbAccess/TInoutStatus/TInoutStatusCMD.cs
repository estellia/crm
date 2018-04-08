using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper.Contrib.Extensions;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess
{
    internal class TInoutStatusCMD
    {
        internal void Create(TInoutStatusEntity dbEntity)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxInsert))
            {
                dbEntity.CreateTime = System.DateTime.Now;
                //dbEntity.CreateBy = "ERP";

                conn.Open();
                conn.Insert(dbEntity);
            }
        }
    }
}
