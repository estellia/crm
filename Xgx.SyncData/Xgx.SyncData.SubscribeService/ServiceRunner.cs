using System;
using EasyNetQ;
using log4net;
using Topshelf;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.Contract.Contract;
using Xgx.SyncData.DomainSubscribeService;

namespace Xgx.SyncData.SubscribeService
{
    public class ServiceRunner : ServiceControl
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(ServiceRunner));
        private readonly IBus _bus = MqBusMgr.GetInstance();
        private readonly VipDomainService _vipDomainService = new VipDomainService();
        private readonly UnitDomainService _unitDomainService = new UnitDomainService();
        private readonly UserDomainService _userDomainService = new UserDomainService();
        private readonly ItemCategoryDomainService _itemCategoryDomainService = new ItemCategoryDomainService();
        private readonly SkuDomainService _skuDomainService = new SkuDomainService();
        private readonly ItemDomainService _itemDomainService = new ItemDomainService();
        private readonly ItemDetailAmountDomainService _itemDetailAmountDomainService = new ItemDetailAmountDomainService();
        private readonly AmountIntergralDomainService _amountIntegralDomainService = new AmountIntergralDomainService();
        private readonly OrderChangeDomainService _orderChangeDomainService = new OrderChangeDomainService();
        public bool Start(HostControl hostControl)
        {
            _bus.Subscribe<IXgxToZmind>("XgxProduct", HandleIXgxToZmind);
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _bus.Dispose();
            return true;
        }
        private void HandleIXgxToZmind(IXgxToZmind msg)
        {
            try
            {
                            
                var unitContract = msg as UnitContract;
                var userContract = msg as UserContract;
                var vipContract = msg as VipContract;
                var itemCategoryContract = msg as ItemCategoryContract;
                var skuContract = msg as SkuContract;
                var itemContract = msg as ItemContract;
                var itemDetailAmountContract = msg as ItemDetailAmountContract;
                var amountIntegralContract = msg as AmountIntergralContract;
                var orderChangeContract = msg as OrderChangeContract;
                if (vipContract != null)
                {
                    _log.Debug(new {
                        tableName= "_vipDomainService",
                        msg=msg
                    });
                    _vipDomainService.Deal(vipContract);
                }
                else if (userContract != null)
                {
                    _log.Debug(new
                    {
                        tableName = "_userDomainService",
                        msg = msg
                    });
                    _userDomainService.Deal(userContract);
                }
                else if (unitContract != null)
                {
                    _log.Debug(new
                    {
                        tableName = "_unitDomainService",
                        msg = msg
                    });
                    _unitDomainService.Deal(unitContract);
                }
                else if (itemCategoryContract != null)
                {
                    _log.Debug(new
                    {
                        tableName = "_itemCategoryDomainService",
                        msg = msg
                    });
                    _itemCategoryDomainService.Deal(itemCategoryContract);
                }
                else if (skuContract != null)
                {
                    _log.Debug(new
                    {
                        tableName = "_skuDomainService",
                        msg = msg
                    });
                    _skuDomainService.Deal(skuContract);
                }
                else if (itemContract != null)
                {
                    _log.Debug(new
                    {
                        tableName = "_itemDomainService",
                        msg = msg
                    });
                    _itemDomainService.Deal(itemContract);
                }
                else if (itemDetailAmountContract != null)
                {
                    _log.Debug(new
                    {
                        tableName = "_itemDetailAmountDomainService",
                        msg = msg
                    });
                    _itemDetailAmountDomainService.Deal(itemDetailAmountContract);
                }
                else if (amountIntegralContract != null)
                {
                    _log.Debug(new
                    {
                        tableName = "_amountIntegralDomainService",
                        msg = msg
                    });
                    _amountIntegralDomainService.Deal(amountIntegralContract);
                }
                else if (orderChangeContract != null)
                {
                    _log.Debug(new
                    {
                        tableName = "_orderChangeDomainService",
                        msg = msg
                    });
                    _orderChangeDomainService.Deal(orderChangeContract);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }
    }
}
