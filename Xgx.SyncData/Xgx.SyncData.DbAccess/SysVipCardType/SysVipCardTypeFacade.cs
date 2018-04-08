using Xgx.SyncData.DbAccess.t_unit;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.SysVipCardType
{
    public class SysVipCardTypeFacade
    {
        private readonly SysVipCardTypeCMD _cmd = new SysVipCardTypeCMD();
        private readonly SysVipCardTypeQuery _query = new SysVipCardTypeQuery();
        public void Create(SysVipCardTypeEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(SysVipCardTypeEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(SysVipCardTypeEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }
        /// <summary>
        /// 获取级别最低的会员卡
        /// </summary>
        /// <param name="_CustomerID"></param>
        /// <returns></returns>
        public SysVipCardTypeEntity GetMinVipCardType(string _CustomerID)
        {
            return _query.GetMinVipCardType(_CustomerID);
        }
        /// <summary>
        /// 根据会员标识获取对应的卡类型
        /// </summary>
        /// <param name="_CustomerID"></param>
        /// <returns></returns>
        public SysVipCardTypeEntity GetVipCardTypeByVipID(string _VipID)
        {
            return _query.GetVipCardTypeByVipID(_VipID);
        }
        /// <summary>
        /// 获取级别最低的会员卡
        /// </summary>
        /// <param name="_CustomerID"></param>
        /// <returns></returns>
        public SysVipCardTypeEntity GetCardTypeIDByTypeId(int typeId)
        {
            return _query.GetCardTypeIDByTypeId(typeId);
        }

        /// <summary>
        /// 获取某个级别的会员卡
        /// </summary>
        /// <param name="_CustomerID"></param>
        /// <returns></returns>
        public SysVipCardTypeEntity GetCardTypeIDByVipCardLevel(int _VipCardLevel,string _CustomerId)
        {
            return _query.GetCardTypeIDByVipCardLevel(_VipCardLevel, _CustomerId);
        }
    }
}
