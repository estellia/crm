using Xgx.SyncData.DbAccess.t_unit;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Item_Delivery_Mapping
{
    public class T_Item_Delivery_MappingFacade
    {
        private readonly T_Item_Delivery_MappingCMD _cmd = new T_Item_Delivery_MappingCMD();
        public void Create(T_Item_Delivery_MappingEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_Item_Delivery_MappingEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_Item_Delivery_MappingEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public void DeleteByItemId(string itemId)
        {
            _cmd.DeleteByItemId(itemId);
        }
    }
}
