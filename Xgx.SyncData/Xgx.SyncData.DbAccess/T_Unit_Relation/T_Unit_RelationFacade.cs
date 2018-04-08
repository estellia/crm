using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Unit_Relation
{
    public class T_Unit_RelationFacade
    {
        private readonly T_Unit_RelationCMD _cmd = new T_Unit_RelationCMD();
        private readonly T_Unit_RelationQuery _query = new T_Unit_RelationQuery();
        public void Create(T_Unit_RelationEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_Unit_RelationEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_Unit_RelationEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public T_Unit_RelationEntity GetEntityByDstId(string dstUnitId)
        {
            return _query.GetEntityByDstId(dstUnitId);
        }
    }
}
