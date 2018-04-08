using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.t_unit
{
    public class t_unitFacade
    {
        private readonly t_unitCMD _cmd = new t_unitCMD();
        private readonly t_unitQuery _query = new t_unitQuery();
        public void Create(t_unitEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(t_unitEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(t_unitEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public dynamic GetUnitById(string id)
        {
            return _query.GetUnitById(id);
        }
    }
}
