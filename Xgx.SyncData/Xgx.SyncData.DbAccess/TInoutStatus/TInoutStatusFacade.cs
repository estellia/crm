using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess
{
    public class TInoutStatusFacade
    {
        private readonly TInoutStatusCMD _cmd = new TInoutStatusCMD();
        public void Create(TInoutStatusEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
    }
}
