using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.DbAccess.SysVipCardType;
using Xgx.SyncData.DbAccess.Vip;
using Xgx.SyncData.DbEntity;
using Zmind.EventBus.Contract;
using OptEnum = Xgx.SyncData.Contract.OptEnum;

namespace Xgx.SyncData.DomainPublishService
{
    public class VipDomainService : IPublish
    {
        public void Deal(EventContract msg)
        {
            var bus = MqBusMgr.GetInstance();
            OptEnum operation;
            Enum.TryParse(msg.Operation.ToString(), out operation);
            var vipContract = new VipContract
            {
                Operation = operation,
                VipId = msg.Id
            };
            if (msg.Operation != Zmind.EventBus.Contract.OptEnum.Delete)
            {
                var vipFacade = new VipFacade();
                var vipEntity = vipFacade.GetById(msg.Id);
                if (vipEntity == null)
                {
                    return;
                }
                vipContract.VipName = vipEntity.VipName;
                vipContract.VipCode = vipEntity.VipCode;
                vipContract.CreateTime = vipEntity.CreateTime;
                vipContract.ModifyTime = vipEntity.LastUpdateTime;
             
                vipContract.Phone = vipEntity.Phone;
                vipContract.IdType = vipEntity.IDType;
                vipContract.IdNumber = vipEntity.IDNumber;
                if (string.IsNullOrEmpty(vipEntity.Birthday))
                {
                    vipContract.Birthday = null;
                }
                else
                {
                    vipContract.Birthday = DateTime.Parse(vipEntity.Birthday);
                }
                vipContract.Gender = vipEntity.Gender != null ? vipEntity.Gender.Value : 1;
                vipContract.Email = vipEntity.Email;
                vipContract.OpenID = vipEntity.WeiXinUserId;
                vipContract.WeiXinID= vipEntity.WeiXin;
             //  vipContract.VipLevel = vipEntity.VipLevel != null ? vipEntity.VipLevel.Value : 1;
                //获取会员对应的卡类别
                //查询最低等级的会员卡类型
                SysVipCardTypeFacade _SysVipCardTypeFacade = new SysVipCardTypeFacade();
                SysVipCardTypeEntity vipCardTypeInfo = _SysVipCardTypeFacade.GetVipCardTypeByVipID(msg.Id);
                if (vipCardTypeInfo != null)
                {
                    vipContract.VipCardTypeID = (int)vipCardTypeInfo.VipCardTypeID;
                }
                vipContract.OldVipID = msg.OtherCon;
            }

           
            bus.Publish<IZmindToXgx>(vipContract);
        }
    }
}
