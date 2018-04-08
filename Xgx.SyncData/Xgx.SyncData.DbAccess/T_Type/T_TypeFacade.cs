using Xgx.SyncData.DbAccess.T_City;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Type
{
    public class T_TypeFacade
    {
        private readonly T_TypeCMD _cmd = new T_TypeCMD();
        private readonly T_TypeQuery _query = new T_TypeQuery();
        public void Create(T_TypeEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_TypeEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_TypeEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public string GetIdByCode(string typeCode, string customerId)
        {
            return _query.GetIdByCode(typeCode, customerId);
        }
    }
}
