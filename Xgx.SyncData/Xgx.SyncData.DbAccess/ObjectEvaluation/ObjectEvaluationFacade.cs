using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.ObjectEvaluation
{
    public class ObjectEvaluationFacade
    {
        private readonly ObjectEvaluationQuery _query = new ObjectEvaluationQuery();

        /// <summary>
        /// 获取商户最低级别的会员
        /// </summary>
        /// <param name="_CustomerID"></param>
        /// <returns></returns>
        public List<ObjectEvaluationEntity> GetEvaluationEntityListByOrderId(string _OrderId)
        {
            return _query.GetEvaluationEntityListByOrderId(_OrderId);
        }
    }
}
