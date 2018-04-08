using System;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.DbAccess.SysVipCardType;
using Xgx.SyncData.DbAccess.Vip;
using Xgx.SyncData.DbAccess.VipCard;
using Xgx.SyncData.DbAccess.VipCardVipMapping;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DomainSubscribeService
{
    public class VipDomainService
    {
        public void Deal(VipContract contract)
        {
            var dbEntity = Convert(contract);
            var facade = new VipFacade();
            if (contract.Operation != OptEnum.Delete)
            {
                if (facade.GetById(contract.VipId) == null)
                {
                    contract.Operation = OptEnum.Create;
                }
                else
                {
                    contract.Operation = OptEnum.Update;
                }
            }
            switch (contract.Operation)
            {
                case OptEnum.Create:
                    facade.Create(dbEntity);
                    //创建卡                   
                    SysVipCardTypeFacade _SysVipCardTypeFacade = new SysVipCardTypeFacade();
                    //查询最低等级的会员卡类型
                  //  SysVipCardTypeEntity vipCardTypeInfo = _SysVipCardTypeFacade.GetMinVipCardType(ConfigMgr.CustomerId);
                    //查询某个级别的卡,都用金卡
                    SysVipCardTypeEntity vipCardTypeInfo = _SysVipCardTypeFacade.GetCardTypeIDByVipCardLevel(3,ConfigMgr.CustomerId);
                    if (vipCardTypeInfo != null)
                    {
                        var vipCardInfo = new VipCardEntity();
                        vipCardInfo.VipCardID = Guid.NewGuid().ToString();
                        vipCardInfo.VipCardTypeID = vipCardTypeInfo.VipCardTypeID;
                        vipCardInfo.VipCardName = vipCardTypeInfo.VipCardTypeName;
                        vipCardInfo.VipCardCode = dbEntity.VipCode;
                        vipCardInfo.VipCardStatusId = 1;//正常
                      //  vipCardInfo.MembershipUnit = unitId;
                    //    vipCardInfo.MembershipTime = DateTime.Now;
                        vipCardInfo.CustomerID = ConfigMgr.CustomerId;
                        vipCardInfo.IsDelete = 0;

                        var _VipCardFacade = new VipCardFacade();//创建处理类
                        _VipCardFacade.Create(vipCardInfo);
                        //创建会员与卡之间的关系
                        //绑定会员卡和会员
                        var vipCardVipMappingEntity = new VipCardVipMappingEntity()
                        {
                            MappingID = Guid.NewGuid().ToString().Replace("-", ""),
                            VIPID = dbEntity.VIPID,
                            VipCardID = vipCardInfo.VipCardID,
                            CustomerID =  ConfigMgr.CustomerId,
                            IsDelete = 0
                        };
                        var _VipCardVipMappingFacade = new VipCardVipMappingFacade();//创建处理类
                        _VipCardVipMappingFacade.Create(vipCardVipMappingEntity);

                    }
                  
                    break;
                case OptEnum.Update:
                    facade.Update(dbEntity);
                    break;
                case OptEnum.Delete:
                    facade.Delete(dbEntity);
                    break;
            }
        }

        private VipEntity Convert(VipContract contract)
        {
            var dbEntity = new VipEntity
            {
                VIPID = contract.VipId,
                VipName = contract.VipName,
                VipCode = contract.VipCode,
                CreateTime = contract.CreateTime,
                LastUpdateTime = contract.ModifyTime,
               VipLevel = 1,//contract.VipLevel,
                Phone = contract.Phone,
                IDType = contract.IdType,
                IDNumber = contract.IdNumber,
                Birthday = contract.Birthday != null ? contract.Birthday.Value.ToString("yyyy-MM-dd") : null,
                Gender = contract.Gender == 0 ? 2 : 1,
                Email = contract.Email,
                ClientID = ConfigMgr.CustomerId,
                WeiXinUserId = contract.OpenID,//新增会员id
                IsDelete = 0,
                WeiXin = string.IsNullOrEmpty(contract.OpenID) ? "" : ConfigMgr.WeiXinID //微信标识
                ,Status=2,
                VipRealName = contract.VipName,
                CouponInfo = ConfigMgr.HeadUnitId
            };
            return dbEntity;
        }
    }
}
