using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.ObjectEvaluation
{
    internal class ObjectEvaluationQuery
    {
        /// <summary>
        /// 获取商户最低级别的会员
        /// </summary>
        /// <param name="_CustomerID"></param>
        /// <returns></returns>
        internal List<ObjectEvaluationEntity> GetEvaluationEntityListByOrderId(string _OrderId)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"SELECT * FROM [ObjectEvaluation] where OrderID = @order_id";
                var result = conn.Query<ObjectEvaluationEntity>(sql, new { order_id = _OrderId });
                if (result != null)
                {
                    return result.ToList();
                }
                return null;
            }
        }
    }

}
