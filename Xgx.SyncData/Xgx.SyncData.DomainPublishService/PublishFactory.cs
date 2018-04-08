using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xgx.SyncData.Contract;
using Zmind.EventBus.Contract;

namespace Xgx.SyncData.DomainPublishService
{
    public static class PublishFactory
    {

        public static IPublish GetInstance(EventContract msg)
        {
            switch (msg.EntityType)
            {
                case EntityTypeEnum.Vip:
                    return new VipDomainService();
                case EntityTypeEnum.Unit:
                    return new UnitDomainService();
                case EntityTypeEnum.User:
                    return new UserDomainService();
                case EntityTypeEnum.Order:
                    return new OrderDomainService();
                case EntityTypeEnum.VipCardType:
                    return new SysVipCardTypeDomainService();
                case EntityTypeEnum.OrderComment:
                    return new OrderEvaluationItemDomainService();
                case EntityTypeEnum.OrderPayment:
                    return new OrderPaymentDetailsDomainService();
                default:
                    return null;
            }
        }
    }
}
