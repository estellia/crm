using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xgx.SyncData.DbEntity;
namespace Xgx.SyncData.DbAccess.T_Sku_Property
{
    public class T_Sku_PropertyFacade
    {
        private readonly T_Sku_PropertyCMD _cmd = new T_Sku_PropertyCMD();
        public void Create(T_Sku_PropertyEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_Sku_PropertyEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_Sku_PropertyEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }
    }
}
