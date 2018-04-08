using System.Collections.Generic;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Prop
{
    public class T_PropFacade
    {
        private readonly T_PropCMD _cmd = new T_PropCMD();
        private readonly T_PropQuery _query = new T_PropQuery();
        public void Create(T_PropEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_PropEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_PropEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public List<string> GetIdByParentId(string id)
        {
            return _query.GetIdByParentId(id);
        }

        public string GetIdByCode(string code)
        {
            return _query.GetIdByCode(code);
        }
    }
}
