using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xgx.SyncData.DbEntity;
namespace Xgx.SyncData.DbAccess.T_Item_Category
{
    public class T_Item_CategoryFacade
    {
        private readonly T_Item_CategoryCMD _cmd = new T_Item_CategoryCMD();
        public void Create(T_Item_CategoryEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_Item_CategoryEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_Item_CategoryEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }
    }
}
