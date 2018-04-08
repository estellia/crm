using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Inout
{
    public class T_InoutDetailFacade
    {
        private readonly T_InoutDetailCMD _cmd = new T_InoutDetailCMD();
        private readonly T_InoutDetailQuery _query = new T_InoutDetailQuery();

        public void Create(T_Inout_DetailEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_Inout_DetailEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_Inout_DetailEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public List<T_Inout_DetailEntity> GetOrderDetailListByOrderId(string orderid)
        {
            return _query.GetOrderDetailListByOrderId(orderid);
        }
    }
}
